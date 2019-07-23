using DirectUI.Common;
using DirectUI.Share;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DirectUI.Core
{
    #region 委托
    public delegate void DUIMouseEditChangingHandler(object sender, DUIMouseEditChangingEventArgs e);
    #endregion
    public class DUIEditableControl : DUIControl
    {
        #region 事件
        public event DUIMouseEditChangingHandler MouseEditChanging;
        public virtual void OnMouseEditChanging(DUIMouseEditChangingEventArgs e)
        {
            if (MouseEditChanging != null)
            {
                MouseEditChanging(this, e);
            }
        }
        #endregion
        #region 变量
        private DUIEditableCenterControl centerControl = null;
        private AlwaysModifierKeys alwaysModifierKeys = AlwaysModifierKeys.None;
        private bool canRotate = false;
        private bool canSkew = false;
        protected bool firstMouseDown = false; //这里进行判断鼠标按下的坐标，因为有的时候 鼠标按下的动作出发了大小的变化 所以要在鼠标按下后第一次移动的时候获取按下的点
        protected DUIAnyBounds firstMouseDownAnyBounds = null;
        private bool firstShiftDown = false;
        private PointF[] polygonMargin = null;
        private float? leftMargin = null;
        private float? topMargin = null;
        private float? rightMargin = null;
        private float? bottomMargin = null;
        private float adsorbDistance = 10;
        private List<int> verticalAdsorbs = new List<int>();
        private List<int> horizontalAdsorbs = new List<int>();
        private float? minWidth = null;
        private float? maxWidth = null;
        private float? minHeight = null;
        private float? maxHeight = null;
        protected float appendStepX = 0;
        protected float appendStepY = 0;
        private float changeStepX = 1;
        private float changeStepY = 1;
        private float appendTop = 0;
        private float appendLeft = 0;
        private float appendBottom = 0;
        private float appendRight = 0;
        private float appendWidth = 0;
        private float appendHeight = 0;
        private float limitTop = 0;
        private float limitLeft = 0;
        private float limitBottom = 0;
        private float limitRight = 0;
        private float limitWidth = 0;
        private float limitHeight = 0;
        private float vertexRadius = 8;
        private float skewRadius = 8;
        private float rotateRadius = 15;
        /// <summary> 中心点的半径
        /// </summary>
        private float centerRadius = 14;
        /// <summary> 鼠标可作用的位置
        /// </summary>
        protected EffectMargin canEffectMargin =
            EffectMargin.Bottom
            | EffectMargin.Left
            | EffectMargin.LeftBottom
            | EffectMargin.LeftBottomRotate
            | EffectMargin.LeftTop
            | EffectMargin.LeftTopRotate
            | EffectMargin.Other
            | EffectMargin.Right
            | EffectMargin.RightBottom
            | EffectMargin.RightBottomRotate
            | EffectMargin.RightTop
            | EffectMargin.RightTopRotate
            | EffectMargin.TopSkew1
            | EffectMargin.TopSkew2
            | EffectMargin.LeftSkew1
            | EffectMargin.LeftSkew2
            | EffectMargin.BottomSkew1
            | EffectMargin.BottomSkew2
            | EffectMargin.RightSkew1
            | EffectMargin.RightSkew2
            | EffectMargin.Top;  //缩放时鼠标作用的位置
        /// <summary> 当前鼠标作用的区域
        /// </summary>
        protected EffectMargin mouseDownMargin = EffectMargin.Unknow;
        protected PointF mouseDownPosition = PointF.Empty;
        protected PointF mouseDownCenterPercent = PointF.Empty;
        /// <summary> 鼠标最后作用的点
        /// </summary>
        protected PointF lastMouseDownPosition = PointF.Empty; //记录鼠标最后一个经过的点
        protected float mouseDownDirection = 0;
        protected bool isShiftUniformScale = true;
        protected bool isAltCenterScale = true;
        //protected bool isShiftDown = false;
        //protected bool isAltDown = false;
        protected float lastUniformScale = 1;
        private RectangleF _mouseDownBounds = RectangleF.Empty;

        protected internal RectangleF mouseDownBounds
        {
            get { return _mouseDownBounds; }
            set { _mouseDownBounds = value; }
        }
        protected internal PointF mouseDownCenter = PointF.Empty;
        protected internal float mouseDownRotateAngle = 0;
        protected internal PointF mouseDownSkew = PointF.Empty;
        protected internal PointF mouseDownScale = new PointF(1, 1);
        //private RectangleF mouseDownClientBounds = RectangleF.Empty; //鼠标按下时的客户端区域
        //private PointF mouseDownCenterAtParentLocation = PointF.Empty; //鼠标按下时中心点相对父容器的位置
        //private PointF mouseDownAtParentLocation = PointF.Empty; //鼠标按下时相对父容器的位置
        private bool isCenterFollow = false;
        #endregion
        #region 属性
        /// <summary> 绑定的DUIEditableCenterControl
        /// </summary>
        public DUIEditableCenterControl CenterControl => this.centerControl;
        /// <summary> 总是被按下的ModifierKeys键
        /// </summary>
        public virtual AlwaysModifierKeys AlwaysModifierKeys { get => this.alwaysModifierKeys; set => this.alwaysModifierKeys = value; }
        public bool IsCenterFollow
        {
            get { return isCenterFollow; }
            set { isCenterFollow = value; }
        }
        /// <summary> 顶点半径
        /// </summary>
        public virtual float VertexRadius
        {
            get { return vertexRadius; }
            set { vertexRadius = value; }
        }
        /// <summary> 切斜半径
        /// </summary>
        public virtual float SkewRadius
        {
            get { return skewRadius; }
            set { skewRadius = value; }
        }
        /// <summary> 旋转点半径
        /// </summary>
        public virtual float RotateRadius
        {
            get { return rotateRadius; }
            set { rotateRadius = value; }
        }
        /// <summary> 中心点的半径
        /// </summary>
        public virtual float CenterRadius
        {
            get { return centerRadius; }
            set { centerRadius = value; }
        }
        #region 边界限制属性
        /// <summary> 多边形区域边界,设置这个值可以吧矩形限定在一个多边形的区域范围，目前这个功能还没有实现20171201
        /// </summary>
        public virtual PointF[] PolygonMargin
        {
            get { return polygonMargin; }
            set { polygonMargin = value; }
        }
        /// <summary> 左边界
        /// </summary>
        public virtual float? LeftMargin
        {
            get { return leftMargin; }
            set { leftMargin = value; }
        }
        /// <summary> 上边界
        /// </summary>
        public virtual float? TopMargin
        {
            get { return topMargin; }
            set { topMargin = value; }
        }
        /// <summary> 右边界
        /// </summary>
        public virtual float? RightMargin
        {
            get { return rightMargin; }
            set { rightMargin = value; }
        }
        /// <summary> 下边界
        /// </summary>
        public virtual float? BottomMargin
        {
            get { return bottomMargin; }
            set { bottomMargin = value; }
        }
        /// <summary> 吸附的距离
        /// </summary>
        public virtual float AdsorbDistance
        {
            get
            {
                if (DUIControl.MouseButtons == System.Windows.Forms.MouseButtons.Left)
                {
                    return adsorbDistance;
                }
                return 0;
            }
            set { adsorbDistance = value; }
        }
        /// <summary> 垂直吸附的值
        /// </summary>
        public List<int> VerticalAdsorbs
        {
            get { return verticalAdsorbs; }
            set { verticalAdsorbs = value; }
        }
        /// <summary> 水平吸附的值
        /// </summary>
        public List<int> HorizontalAdsorbs
        {
            get { return horizontalAdsorbs; }
            set { horizontalAdsorbs = value; }
        }
        /// <summary> 最小的宽度
        /// </summary>
        public virtual float? MinWidth
        {
            get { return minWidth; }
            set { minWidth = value; }
        }
        /// <summary> 最大的宽度
        /// </summary>
        public virtual float? MaxWidth
        {
            get { return maxWidth; }
            set { maxWidth = value; }
        }
        /// <summary> 最小的高度
        /// </summary>
        public virtual float? MinHeight
        {
            get { return minHeight; }
            set { minHeight = value; }
        }
        /// <summary> 最大的高度
        /// </summary>
        public virtual float? MaxHeight
        {
            get { return maxHeight; }
            set { maxHeight = value; }
        }
        /// <summary> 改变的步长
        /// </summary>
        public float ChangeStep
        {
            get
            {
                if (this.changeStepX == this.changeStepY)
                {
                    return changeStepX;
                }
                return -1;
            }
            set
            {
                changeStepX = value;
                changeStepY = value;
            }
        }
        public virtual float ChangeStepX { get => changeStepX; set => changeStepX = value; }
        public virtual float ChangeStepY { get => changeStepY; set => changeStepY = value; }
        /// <summary> 限制控件的Top 如果超过限制的Top就会吧超出的部分追加到appendTop变量之中
        /// </summary>
        private float LimitTop
        {
            get
            {
                if (this.HorizontalAdsorbs.Count > 0 && Control.ModifierKeys != Keys.Shift)
                {
                    var horizontalAdsorb = this.HorizontalAdsorbs.OrderBy(v => Math.Abs(limitTop - appendTop - v)).FirstOrDefault();
                    if (Math.Abs(limitTop - appendTop - horizontalAdsorb) < this.AdsorbDistance)
                    {
                        return horizontalAdsorb;
                    }
                }
                return limitTop - appendTop;
            }
            set
            {
                limitTop = value;
                appendTop = 0;
                if (TopMargin != null && limitTop < TopMargin)
                {
                    appendTop = limitTop - (float)TopMargin;
                }
                if (BottomMargin != null && limitTop > BottomMargin)
                {
                    appendTop = limitTop - (float)BottomMargin;
                }
            }
        }
        /// <summary> 限制控件的Left 如果超过限制的Left就会吧超出的部分追加到appendLeft变量之中
        /// </summary>
        private float LimitLeft
        {
            get
            {
                if (this.VerticalAdsorbs.Count > 0 && Control.ModifierKeys != Keys.Shift)
                {
                    var verticalAdsorb = this.VerticalAdsorbs.OrderBy(v => Math.Abs(limitLeft - appendLeft - v)).FirstOrDefault();
                    if (Math.Abs(limitLeft - appendLeft - verticalAdsorb) < this.AdsorbDistance)
                    {
                        return verticalAdsorb;
                    }
                }
                return limitLeft - appendLeft;
            }
            set
            {
                limitLeft = value;
                appendLeft = 0;
                if (LeftMargin != null && limitLeft < LeftMargin)
                {
                    appendLeft = limitLeft - (float)LeftMargin;
                }
                if (RightMargin != null && limitLeft > RightMargin)
                {
                    appendLeft = limitLeft - (float)RightMargin;
                }
            }
        }
        /// <summary> 限制控件的Bottom 如果超过限制的Bottom就会吧超出的部分追加到appendBottom变量之中
        /// </summary>
        public float LimitBottom
        {
            get
            {
                if (this.HorizontalAdsorbs.Count > 0 && Control.ModifierKeys != Keys.Shift)
                {
                    var horizontalAdsorb = this.HorizontalAdsorbs.OrderBy(v => Math.Abs(limitBottom - appendBottom - v)).FirstOrDefault();
                    if (Math.Abs(limitBottom - appendBottom - horizontalAdsorb) < this.AdsorbDistance)
                    {
                        return horizontalAdsorb;
                    }
                }
                return limitBottom - appendBottom;
            }
            set
            {
                limitBottom = value;
                appendBottom = 0;
                if (BottomMargin != null && limitBottom > BottomMargin)
                {
                    appendBottom = limitBottom - (float)BottomMargin;
                }
                if (TopMargin != null && limitBottom < TopMargin)
                {
                    appendBottom = limitBottom - (float)TopMargin;
                }
            }
        }
        /// <summary> 限制控件的Right 如果超过限制的Right就会吧超出的部分追加到appendRight变量之中
        /// </summary>
        public float LimitRight
        {
            get
            {
                if (this.VerticalAdsorbs.Count > 0 && Control.ModifierKeys != Keys.Shift)
                {
                    var verticalAdsorb = this.VerticalAdsorbs.OrderBy(v => Math.Abs(limitRight - appendRight - v)).FirstOrDefault();
                    if (Math.Abs(limitRight - appendRight - verticalAdsorb) < this.AdsorbDistance)
                    {
                        return verticalAdsorb;
                    }
                }
                return limitRight - appendRight;
            }
            set
            {
                limitRight = value;
                appendRight = 0;
                if (RightMargin != null && limitRight > RightMargin)
                {
                    appendRight = limitRight - (float)RightMargin;
                }
                if (LeftMargin != null && limitRight < LeftMargin)
                {
                    appendRight = limitRight - (float)LeftMargin;
                }
            }
        }
        /// <summary> 限制控件的Width 如果超过限制的Width就会吧超出的部分追加到appendWidth变量之中
        /// </summary>
        public float LimitWidth
        {
            get { return limitWidth - appendWidth; }
            set
            {
                limitWidth = value;
                appendWidth = 0;
                if (MinWidth != null && limitWidth <= MinWidth)
                {
                    appendWidth = limitWidth - (float)MinWidth;
                }
                if (MaxWidth != null && limitWidth >= MaxWidth)
                {
                    appendWidth = limitWidth - (float)MaxWidth;
                }
            }
        }
        /// <summary> 限制控件的Height 如果超过限制的Height就会吧超出的部分追加到appendHeight变量之中
        /// </summary>
        public float LimitHeight
        {
            get
            {
                return limitHeight - appendHeight;
            }
            set
            {
                limitHeight = value;
                appendHeight = 0;
                if (MinHeight != null && limitHeight <= MinHeight)
                {
                    appendHeight = limitHeight - (float)MinHeight;
                }
                if (MaxHeight != null && limitHeight >= MaxHeight)
                {
                    appendHeight = limitHeight - (float)MaxHeight;
                }
            }
        }
        /// <summary> 使Top偏移offset单位，并返回实际的偏移量（因为如果有限制偏移的范围有可能不会偏移那么多）
        /// </summary>
        /// <param name="offset">偏移offset单位</param>
        /// <returns>实际的偏移量</returns>
        protected float GetLimitTopOffset(float offset)
        {
            float lastTop = this.LimitTop;
            this.LimitTop = this.limitTop + offset;
            float offsetTop = this.LimitTop - lastTop;
            if (offset != offsetTop)
            {
                return offsetTop;
            }
            return offset;
        }
        /// <summary> 使Left偏移offset单位，并返回实际的偏移量（因为如果有限制偏移的范围有可能不会偏移那么多）
        /// </summary>
        /// <param name="offset">偏移offset单位</param>
        /// <returns>实际的偏移量</returns>
        protected float GetLimitLeftOffset(float offset)
        {
            float lastLeft = this.LimitLeft;
            this.LimitLeft = this.limitLeft + offset;
            float offsetLeft = this.LimitLeft - lastLeft;
            if (offset != offsetLeft)
            {
                return offsetLeft;
            }
            return offset;
        }
        /// <summary> 使Bottom偏移offset单位，并返回实际的偏移量（因为如果有限制偏移的范围有可能不会偏移那么多）
        /// </summary>
        /// <param name="offset">偏移offset单位</param>
        /// <returns>实际的偏移量</returns>
        protected float GetLimitBottomOffset(float offset)
        {
            float lastBottom = this.LimitBottom;
            this.LimitBottom = this.limitBottom + offset;
            float offsetBottom = this.LimitBottom - lastBottom;
            if (offset != offsetBottom)
            {
                return offsetBottom;
            }
            return offset;
        }
        /// <summary> 使Right偏移offset单位，并返回实际的偏移量（因为如果有限制偏移的范围有可能不会偏移那么多）
        /// </summary>
        /// <param name="offset">偏移offset单位</param>
        /// <returns>实际的偏移量</returns>
        protected float GetLimitRightOffset(float offset)
        {
            float lastRight = this.LimitRight;
            this.LimitRight = this.limitRight + offset;
            float offsetRight = this.LimitRight - lastRight;
            if (offset != offsetRight)
            {
                return offsetRight;
            }
            return offset;
        }
        /// <summary> 使Width偏移offset单位，并返回实际的偏移量（因为如果有限制偏移的范围有可能不会偏移那么多）
        /// </summary>
        /// <param name="offset">偏移offset单位</param>
        /// <returns>实际的偏移量</returns>
        protected float GetLimitWidthOffset(float offset)
        {
            float lastWidth = this.LimitWidth;
            this.LimitWidth = this.limitWidth + offset;
            float offsetWidth = this.LimitWidth - lastWidth;
            if (offset != offsetWidth)
            {
                return offsetWidth;
            }
            return offset;
        }
        /// <summary> 使Height偏移offset单位，并返回实际的偏移量（因为如果有限制偏移的范围有可能不会偏移那么多）
        /// </summary>
        /// <param name="offset">偏移offset单位</param>
        /// <returns>实际的偏移量</returns>
        protected float GetLimitHeightOffset(float offset)
        {
            float lastHeight = this.LimitHeight;
            this.LimitHeight = this.limitHeight + offset;
            float offsetHeight = this.LimitHeight - lastHeight;
            if (offset != offsetHeight)
            {
                return offsetHeight;
            }
            return offset;
        }
        /// <summary> 使Top偏移offset单位，并返回实际的偏移量，这个偏移带有长宽锁定的比例（因为如果有限制偏移的范围有可能不会偏移那么多）
        /// </summary>
        /// <param name="offset">偏移offset单位</param>
        /// <returns>实际的偏移量</returns>
        protected float GetLimitTopOffsetWithScale(float offset)
        {
            float lastTop = this.LimitTop;
            this.LimitTop = this.limitTop + offset / lastUniformScale;
            float offsetTop = this.LimitTop - lastTop;
            if (Math.Round(offset / lastUniformScale, 3) != Math.Round(offsetTop, 3))
            {
                return (offsetTop * lastUniformScale);
            }
            return offset;
        }
        /// <summary> 使Left偏移offset单位，并返回实际的偏移量，这个偏移带有长宽锁定的比例（因为如果有限制偏移的范围有可能不会偏移那么多）
        /// </summary>
        /// <param name="offset">偏移offset单位</param>
        /// <returns>实际的偏移量</returns>
        protected float GetLimitLeftOffsetWithScale(float offset)
        {
            float lastLeft = this.LimitLeft;
            this.LimitLeft = this.limitLeft + offset * lastUniformScale;
            float offsetLeft = this.LimitLeft - lastLeft;
            if (Math.Round(offset * lastUniformScale, 3) != Math.Round(offsetLeft, 3))
            {
                return (offsetLeft / lastUniformScale);
            }
            return offset;
        }
        /// <summary> 使Bottom偏移offset单位，并返回实际的偏移量，这个偏移带有长宽锁定的比例（因为如果有限制偏移的范围有可能不会偏移那么多）
        /// </summary>
        /// <param name="offset">偏移offset单位</param>
        /// <returns>实际的偏移量</returns>
        protected float GetLimitBottomOffsetWithScale(float offset)
        {
            float lastBottom = this.LimitBottom;
            this.LimitBottom = this.limitBottom + offset / lastUniformScale;
            float offsetBottom = this.LimitBottom - lastBottom;
            if (Math.Round(offset / lastUniformScale, 3) != Math.Round(offsetBottom, 3))
            {
                return (offsetBottom * lastUniformScale);
            }
            return offset;
        }
        /// <summary> 使Right偏移offset单位，并返回实际的偏移量，这个偏移带有长宽锁定的比例（因为如果有限制偏移的范围有可能不会偏移那么多）
        /// </summary>
        /// <param name="offset">偏移offset单位</param>
        /// <returns>实际的偏移量</returns>
        protected float GetLimitRightOffsetWithScale(float offset)
        {
            float lastRight = this.LimitRight;
            this.LimitRight = this.limitRight + offset * lastUniformScale;
            float offsetRight = this.LimitRight - lastRight;
            if (Math.Round(offset * lastUniformScale, 3) != Math.Round(offsetRight, 3))
            {
                return (offsetRight / lastUniformScale);
            }
            return offset;
        }
        /// <summary> 使Width偏移offset单位，并返回实际的偏移量，这个偏移带有长宽锁定的比例（因为如果有限制偏移的范围有可能不会偏移那么多）
        /// </summary>
        /// <param name="offset">偏移offset单位</param>
        /// <returns>实际的偏移量</returns>
        protected float GetLimitWidthOffsetWithScale(float offset)
        {
            float lastWidth = this.LimitWidth;
            this.LimitWidth = this.limitWidth + offset * lastUniformScale;
            float offsetWidth = this.LimitWidth - lastWidth;
            if (Math.Round(offset * lastUniformScale, 3) != Math.Round(offsetWidth, 3))
            {
                return (offsetWidth / lastUniformScale);
            }
            return offset;
        }
        /// <summary> 使Height偏移offset单位，并返回实际的偏移量，这个偏移带有长宽锁定的比例（因为如果有限制偏移的范围有可能不会偏移那么多）
        /// </summary>
        /// <param name="offset">偏移offset单位</param>
        /// <returns>实际的偏移量</returns>
        protected float GetLimitHeightOffsetWithScale(float offset)
        {
            float lastHeight = this.LimitHeight;
            this.LimitHeight = this.limitHeight + offset / lastUniformScale;
            float offsetHeight = this.LimitHeight - lastHeight;
            if (Math.Round(offset / lastUniformScale, 3) != Math.Round(offsetHeight, 3))
            {
                return (offsetHeight * lastUniformScale);
            }
            return offset;
        }
        #endregion
        /// <summary> 鼠标可作用的位置
        /// </summary>
        public virtual EffectMargin CanEffectMargin
        {
            get { return canEffectMargin; }
            set { canEffectMargin = value; }
        }
        /// <summary> 当前鼠标作用的区域
        /// </summary>
        public EffectMargin MouseDownMargin
        {
            get { return mouseDownMargin; }
        }
        /// <summary> 按下Shift是否可以等比缩放
        /// </summary>
        public virtual bool IsShiftUniformScale
        {
            get { return isShiftUniformScale; }
            set { isShiftUniformScale = value; }
        }
        /// <summary> 按下Alt是否可以以中心点缩放
        /// </summary>
        public virtual bool IsAltCenterScale
        {
            get { return isAltCenterScale; }
            set { isAltCenterScale = value; }
        }
        /// <summary> 是否可以旋转
        /// </summary>
        public bool CanRotate
        {
            get { return this.canRotate; }
            set
            {
                if (canRotate != value)
                {
                    //if (value)
                    //{
                    //    this.BorderWidth = RotateRadius;
                    //}
                    //else
                    //{
                    //    this.BorderWidth = VertexRadius;
                    //}
                    canRotate = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary> 是否可以倾斜
        /// </summary>
        public bool CanSkew
        {
            get { return this.canSkew; }
            set
            {
                if (canSkew != value)
                {
                    //if (value)
                    //{
                    //    this.BorderWidth = SkewRadius;
                    //}
                    //else
                    //{
                    //    this.BorderWidth = VertexRadius;
                    //}
                    canSkew = value;
                    this.Invalidate();
                }
            }
        }
        #endregion
        #region 顶点区域
        /// <summary> 左上方的区域
        /// </summary>
        public virtual RectangleF LeftTopBounds
        {
            get { return new RectangleF(new PointF(this.ClientRectangle.X - VertexRadius, this.ClientRectangle.Y - VertexRadius), new SizeF(VertexRadius, VertexRadius)); }
        }

        /// <summary> 正上方的区域
        /// </summary>
        public virtual RectangleF TopBounds
        {
            get { return new RectangleF(new PointF(this.ClientRectangle.X + this.ClientRectangle.Width / 2 - VertexRadius / 2, this.ClientRectangle.Y - VertexRadius), new SizeF(VertexRadius, VertexRadius)); }
        }
        /// <summary> 右上方的区域
        /// </summary>
        public virtual RectangleF RightTopBounds
        {
            get { return new RectangleF(new PointF(this.ClientRectangle.X + this.ClientRectangle.Width, this.ClientRectangle.Y - VertexRadius), new SizeF(VertexRadius, VertexRadius)); }
        }
        /// <summary> 正右方的区域
        /// </summary>
        public virtual RectangleF RightBounds
        {
            get { return new RectangleF(new PointF(this.ClientRectangle.X + this.ClientRectangle.Width, this.ClientRectangle.Y + this.ClientRectangle.Height / 2 - VertexRadius / 2), new SizeF(VertexRadius, VertexRadius)); }
        }
        /// <summary> 右下方的区域
        /// </summary>
        public virtual RectangleF RightBottomBounds
        {
            get { return new RectangleF(new PointF(this.ClientRectangle.X + this.ClientRectangle.Width, this.ClientRectangle.Y + this.ClientRectangle.Height), new SizeF(VertexRadius, VertexRadius)); }
        }
        /// <summary> 正下方的区域
        /// </summary>
        public virtual RectangleF BottomBounds
        {
            get { return new RectangleF(new PointF(this.ClientRectangle.X + this.ClientRectangle.Width / 2 - VertexRadius / 2, this.ClientRectangle.Y + this.ClientRectangle.Height), new SizeF(VertexRadius, VertexRadius)); }
        }
        /// <summary> 左下方的区域
        /// </summary>
        public virtual RectangleF LeftBottomBounds
        {
            get { return new RectangleF(new PointF(this.ClientRectangle.X - VertexRadius, this.ClientRectangle.Y + this.ClientRectangle.Height), new SizeF(VertexRadius, VertexRadius)); }
        }
        /// <summary> 正左方的区域
        /// </summary>
        public virtual RectangleF LeftBounds
        {
            get { return new RectangleF(new PointF(this.ClientRectangle.X - VertexRadius, this.ClientRectangle.Y + this.ClientRectangle.Height / 2 - VertexRadius / 2), new SizeF(VertexRadius, VertexRadius)); }
        }
        #endregion
        #region 旋转区域
        /// <summary> 左上方的旋转区域
        /// </summary>
        public virtual RectangleF LeftTopRotateBounds
        {
            get { return new RectangleF(new PointF(this.ClientRectangle.X - RotateRadius, this.ClientRectangle.Y - RotateRadius), new SizeF(RotateRadius, RotateRadius)); }
        }
        /// <summary> 右上方的旋转区域
        /// </summary>
        public virtual RectangleF RightTopRotateBounds
        {
            get { return new RectangleF(new PointF(this.ClientRectangle.X + this.ClientRectangle.Width, this.ClientRectangle.Y - RotateRadius), new SizeF(RotateRadius, RotateRadius)); }
        }
        /// <summary> 右下方的旋转区域
        /// </summary>
        public virtual RectangleF RightBottomRotateBounds
        {
            get { return new RectangleF(new PointF(this.ClientRectangle.X + this.ClientRectangle.Width, this.ClientRectangle.Y + this.ClientRectangle.Height), new SizeF(RotateRadius, RotateRadius)); }
        }
        /// <summary> 左下方的旋转区域
        /// </summary>
        public virtual RectangleF LeftBottomRotateBounds
        {
            get { return new RectangleF(new PointF(this.ClientRectangle.X - RotateRadius, this.ClientRectangle.Y + this.ClientRectangle.Height), new SizeF(RotateRadius, RotateRadius)); }
        }
        #endregion
        #region 倾斜区域
        /// <summary> 上方的倾斜区域1
        /// </summary>
        public virtual RectangleF TopSkewBounds1
        {
            get { return new RectangleF(new PointF(this.ClientRectangle.X, this.ClientRectangle.Y - SkewRadius), new SizeF((this.ClientRectangle.Width - VertexRadius) / 2, SkewRadius)); }
        }
        /// <summary> 上方的倾斜区域2
        /// </summary>
        public virtual RectangleF TopSkewBounds2
        {
            get { return new RectangleF(new PointF(this.ClientRectangle.X + (this.ClientRectangle.Width - VertexRadius) / 2 + VertexRadius, this.ClientRectangle.Y - SkewRadius), new SizeF((this.ClientRectangle.Width - VertexRadius) / 2, SkewRadius)); }
        }
        /// <summary> 左方的倾斜区域1
        /// </summary>
        public virtual RectangleF LeftSkewBounds1
        {
            get { return new RectangleF(new PointF(this.ClientRectangle.X - SkewRadius, this.ClientRectangle.Y), new SizeF(SkewRadius, (this.ClientRectangle.Height - VertexRadius) / 2)); }
        }
        /// <summary> 左方的倾斜区域2
        /// </summary>
        public virtual RectangleF LeftSkewBounds2
        {
            get { return new RectangleF(new PointF(this.ClientRectangle.X - SkewRadius, this.ClientRectangle.Y + (this.ClientRectangle.Height - VertexRadius) / 2 + VertexRadius), new SizeF(SkewRadius, (this.ClientRectangle.Height - VertexRadius) / 2)); }
        }
        /// <summary> 下方的倾斜区域1
        /// </summary>
        public virtual RectangleF BottomSkewBounds1
        {
            get { return new RectangleF(new PointF(this.ClientRectangle.X, this.ClientRectangle.Y + this.ClientRectangle.Height), new SizeF((this.ClientRectangle.Width - VertexRadius) / 2, SkewRadius)); }
        }
        /// <summary> 下方的倾斜区域2
        /// </summary>
        public virtual RectangleF BottomSkewBounds2
        {
            get { return new RectangleF(new PointF(this.ClientRectangle.X + (this.ClientRectangle.Width - VertexRadius) / 2 + VertexRadius, this.ClientRectangle.Y + this.ClientRectangle.Height), new SizeF((this.ClientRectangle.Width - VertexRadius) / 2, SkewRadius)); }
        }
        /// <summary> 右方的倾斜区域1
        /// </summary>
        public virtual RectangleF RightSkewBounds1
        {
            get { return new RectangleF(new PointF(this.ClientRectangle.X + this.ClientRectangle.Width, this.ClientRectangle.Y), new SizeF(SkewRadius, (this.ClientRectangle.Height - VertexRadius) / 2)); }
        }
        /// <summary> 右方的倾斜区域2
        /// </summary>
        public virtual RectangleF RightSkewBounds2
        {
            get { return new RectangleF(new PointF(this.ClientRectangle.X + this.ClientRectangle.Width, this.ClientRectangle.Y + (this.ClientRectangle.Height - VertexRadius) / 2 + VertexRadius), new SizeF(SkewRadius, (this.ClientRectangle.Height - VertexRadius) / 2)); }
        }
        #endregion
        #region 其他区域
        public virtual RectangleF OtherBounds
        {
            get { return this.ClientRectangle; }
        }
        #endregion
        /// <summary> 是否等比缩放
        /// </summary>
        private bool IsUniformScale => this.IsShiftUniformScale && ((Control.ModifierKeys == Keys.Shift && this.AlwaysModifierKeys == AlwaysModifierKeys.None) || this.AlwaysModifierKeys == AlwaysModifierKeys.Shift);
        private bool IsCenterScale => this.IsAltCenterScale && ((Control.ModifierKeys == Keys.Alt && this.AlwaysModifierKeys == AlwaysModifierKeys.None) || this.AlwaysModifierKeys == AlwaysModifierKeys.Alt);
        public DUIEditableControl()
        {
            this.BackColor = Color.Transparent;
            this.BorderColor = Color.Transparent;
            //this.BorderWidth = 8;
        }
        #region 重写
        public override float Radius
        {
            get
            {
                return 0;
            }
        }
        public override void OnMouseDown(DUIMouseEventArgs e)
        {
            firstMouseDown = true;
            firstMouseDownAnyBounds = null;
            this.LimitTop = this.Top;
            this.LimitLeft = this.Left;
            this.LimitBottom = this.Bottom;
            this.LimitRight = this.Right;
            this.LimitWidth = this.Width;
            this.LimitHeight = this.Height;
            this.mouseDownMargin = GetEffectMargin(e.Location);
            this.Cursor = this.GetCursor(e.Location); //判断鼠标在编辑框的位置获取鼠标的样式 
            this.mouseDownBounds = new RectangleF(this.x, this.y, this.width, this.height);
            this.mouseDownSkew = new PointF(this.skewX, this.skewY);
            this.mouseDownScale = new PointF(this.scaleX, this.scaleY);
            this.mouseDownCenter = new PointF(this.centerX, this.centerY);
            this.mouseDownRotateAngle = this.rotate;
            base.OnMouseDown(e);
            //this.Center = e.Location;
        }
        public override void OnMouseMove(DUIMouseEventArgs e)
        {
            PointF location = e.Location;
            if (e.Button == MouseButtons.None)
            {
                this.Cursor = this.GetCursor(location); //判断鼠标在编辑框的位置获取鼠标的样式  
                base.OnMouseMove(e);
                return;
            }
            location = this.PointToParent(e.Location);
            location = new PointF(location.X - this.ClientBounds.X, location.Y - this.ClientBounds.Y);
            if (this.firstMouseDown) //这里进行判断鼠标按下的坐标，因为有的时候 鼠标按下的动作出发了大小的变化 所以要在鼠标按下后第一次移动的时候获取按下的点
            {
                this.firstMouseDown = false;
                if (this.IsUniformScale)
                {
                    this.lastUniformScale = this.ClientSize.Width / this.ClientSize.Height;
                }
                this.mouseDownDirection = PointTools.TwoPointAngle(this.Center, location);
                this.lastMouseDownPosition = location;
                this.mouseDownPosition = this.PointToParent(e.Location);
                this.mouseDownCenterPercent = new PointF(this.HFlipping ? (1 - this.CenterX / this.ClientSize.Width) : this.CenterX / this.ClientSize.Width, this.VFlipping ? (1 - this.CenterY / this.ClientSize.Height) : this.CenterY / this.ClientSize.Height);

                this.firstMouseDownAnyBounds = GetAnyBounds();
            }
            //if (this.IsUniformScale)
            //{
            //    if (this.firstShiftDown)
            //    {
            //        this.firstShiftDown = false;
            //        this.lastUniformScale = (float)this.Width / (float)this.Height;
            //    }
            //}
            else { this.firstShiftDown = true; }
            if (location == this.lastMouseDownPosition) { return; }
            appendStepX += (location.X - this.lastMouseDownPosition.X);
            appendStepY += (location.Y - this.lastMouseDownPosition.Y);
            if (float.IsNaN(appendStepX)) { appendStepX = 0; }//有时候会出现NaN的值，不知道产生原因，先这么处理吧
            if (float.IsNaN(appendStepY)) { appendStepY = 0; }//有时候会出现NaN的值，不知道产生原因，先这么处理吧
            PointF offset = new PointF((appendStepX - (appendStepX % ChangeStepX)), (appendStepY - (appendStepY % ChangeStepY)));
            offset = MatrixTools.PointAfterMatrix(offset, this.MatrixReverse);
            appendStepX -= (appendStepX - (appendStepX % ChangeStepX));
            appendStepY -= (appendStepY - (appendStepY % ChangeStepY));
            float lastX = this.X;
            float lastY = this.Y;
            float lastWidth = this.ClientSize.Width;
            float lastHeight = this.ClientSize.Height;
            float lastAngle = this.Rotate;
            PointF lastSkew = this.Skew;
            DUIAnyBounds anyBounds = this.GetAnyBounds();
            switch (this.mouseDownMargin)
            {
                #region Point
                case EffectMargin.Point:
                    DoOffsetPoint(offset.X, offset.Y, anyBounds);
                    break;
                #endregion
                #region Other
                case EffectMargin.Other:
                    DoOffsetOther(offset.X, offset.Y, anyBounds);
                    break;
                #endregion
                #region Top
                case EffectMargin.Top:
                    DoOffsetTop(offset.Y, anyBounds);
                    break;
                #endregion
                #region Left
                case EffectMargin.Left:
                    DoOffsetLeft(offset.X, anyBounds);
                    break;
                #endregion
                #region Bottom
                case EffectMargin.Bottom:
                    DoOffsetBottom(offset.Y, anyBounds);
                    break;
                #endregion
                #region Right
                case EffectMargin.Right:
                    DoOffsetRight(offset.X, anyBounds);
                    break;
                #endregion
                #region LeftTop
                case EffectMargin.LeftTop:
                    DoOffsetLeftTop(offset.X, offset.Y, e.Location, anyBounds);
                    break;
                #endregion
                #region RightTop
                case EffectMargin.RightTop:
                    DoOffsetRightTop(offset.X, offset.Y, e.Location, anyBounds);
                    break;
                #endregion
                #region RightBottom
                case EffectMargin.RightBottom:
                    DoOffsetRightBottom(offset.X, offset.Y, e.Location, anyBounds);
                    break;
                #endregion
                #region LeftBottom
                case EffectMargin.LeftBottom:
                    DoOffsetLeftBottom(offset.X, offset.Y, e.Location, anyBounds);
                    break;
                #endregion
                #region TopSkew
                case EffectMargin.TopSkew1:
                case EffectMargin.TopSkew2:
                    DoOffsetTopSkew(offset.X, offset.Y, lastSkew, anyBounds);
                    break;
                #endregion
                #region LeftSkew
                case EffectMargin.LeftSkew1:
                case EffectMargin.LeftSkew2:
                    DoOffsetLeftSkew(offset.X, offset.Y, lastSkew, anyBounds);
                    break;
                #endregion
                #region BottomSkew
                case EffectMargin.BottomSkew1:
                case EffectMargin.BottomSkew2:
                    DoOffsetBottomSkew(offset.X, offset.Y, lastSkew, anyBounds);
                    break;
                #endregion
                #region RightSkew
                case EffectMargin.RightSkew1:
                case EffectMargin.RightSkew2:
                    DoOffsetRightSkew(offset.X, offset.Y, lastSkew, anyBounds);
                    break;
                #endregion
                #region Rotate
                case EffectMargin.LeftBottomRotate:
                case EffectMargin.LeftTopRotate:
                case EffectMargin.RightBottomRotate:
                case EffectMargin.RightTopRotate:
                    float mouseDownDirection = PointTools.TwoPointAngle(this.Center, location);
                    if (Math.Abs(mouseDownDirection - this.mouseDownDirection) > 300)
                    {
                        if (Math.Abs(360 - mouseDownDirection) > Math.Abs(360 - this.mouseDownDirection))
                        {
                            this.mouseDownDirection = 360 - this.mouseDownDirection;
                        }
                        else
                        {
                            this.mouseDownDirection = 360 + this.mouseDownDirection;
                        }
                    }
                    anyBounds.Rotate += mouseDownDirection - this.mouseDownDirection;
                    //this.SetBounds(0, 0, 0, 0, 0, 0, this.Rotate + mouseDownDirection - this.mouseDownDirection, 0, 0, 0, 0, DUIBoundsSpecified.Rotate);
                    //DUIBoundsSpecified.Rotate;
                    this.mouseDownDirection = mouseDownDirection;
                    break;
                    #endregion
            }
            #region 中心点跟随
            if (this.IsCenterFollow)
            {
                //Debug.WriteLine(mouseDownCenterPercent);
                PointF center = this.AnyBoundsPointToParent(anyBounds, new PointF(anyBounds.ClientBounds.Width * (this.HFlipping ? (1 - this.mouseDownCenterPercent.X) : this.mouseDownCenterPercent.X), anyBounds.ClientBounds.Height * (this.VFlipping ? (1 - this.mouseDownCenterPercent.Y) : this.mouseDownCenterPercent.Y)));
                DoCenterFollow(anyBounds, center);
            }
            #endregion
            #region SetBoundsChanging
            this.SetAnyBoundsChanging(anyBounds);
            #endregion
            #region OnMouseEditChanging
            if (firstMouseDownAnyBounds != null)
            {
                OnMouseEditChanging(new DUIMouseEditChangingEventArgs(
                       this.Bounds
                       , firstMouseDownAnyBounds.Bounds
                       , this.Center
                       , firstMouseDownAnyBounds.Center
                       , this.Skew
                       , firstMouseDownAnyBounds.Skew
                       , this.Scale
                       , firstMouseDownAnyBounds.Scale
                       , this.Rotate
                       , firstMouseDownAnyBounds.Rotate
                       , false));
            }
            #endregion
            this.lastMouseDownPosition = new PointF(location.X - (this.X - lastX), location.Y - (this.Y - lastY));
            lastSkew = this.Skew;
            base.OnMouseMove(e);
        }
        public override void OnMouseUp(DUIMouseEventArgs e)
        {
            this.mouseDownMargin = EffectMargin.Other;
            this.Cursor = this.GetCursor(e.Location); //判断鼠标在编辑框的位置获取鼠标的样式 
            if (firstMouseDown)
            {
                firstMouseDown = false;
            }
            if (firstMouseDownAnyBounds != null)
            {
                OnMouseEditChanging(new DUIMouseEditChangingEventArgs(
                       new RectangleF(this.x, this.y, this.width, this.height)
                       , firstMouseDownAnyBounds.Bounds
                       , new PointF(this.centerX, this.centerY)
                       , firstMouseDownAnyBounds.Center
                       , new PointF(this.skewX, this.skewY)
                       , firstMouseDownAnyBounds.Skew
                       , new PointF(this.scaleX, this.scaleY)
                       , firstMouseDownAnyBounds.Scale
                       , this.rotate
                       , firstMouseDownAnyBounds.Rotate
                       , true));
                firstMouseDownAnyBounds = null;
            }
            this.SetAnyBoundsChanged();
            base.OnMouseUp(e);
        }
        public override void OnMouseLeave(EventArgs e)
        {
            this.Cursor = Cursors.Default;
            base.OnMouseLeave(e);
        }
        public override void OnKeyDown(KeyEventArgs e)
        {
            //if (e.KeyData == Keys.Left)
            //{
            //    this.X--;
            //}
            //if (e.KeyData == Keys.Right)
            //{
            //    this.X++;
            //}
            //if (e.KeyData == Keys.Up)
            //{
            //    this.Y--;
            //}
            //if (e.KeyData == Keys.Down)
            //{
            //    this.Y++;
            //}
            base.OnKeyDown(e);
        }
        public override void OnPaintBackground(DUIPaintEventArgs e)
        {
            base.OnPaintBackground(e);
            //var backupSmoothingMode = e.Graphics.SmoothingMode;
            e.Graphics.SmoothingMode = DUISmoothingMode.AntiAlias;
            DrawBounds(new DUIPaintEventArgs(e.Graphics, new RectangleF(0, 0, this.Width, this.Height)));
            if (this.HFlipping || this.VFlipping)
            {
                e.Graphics.TranslateTransform(this.HFlipping ? this.Width / 2 : 0, this.VFlipping ? this.Height / 2 : 0);
                e.Graphics.ScaleTransform(this.HFlipping ? -1 : 1, this.VFlipping ? -1 : 1);
                e.Graphics.TranslateTransform(this.HFlipping ? -this.Width / 2 : 0, this.VFlipping ? -this.Height / 2 : 0);
            }
            DrawVertex(new DUIPaintEventArgs(e.Graphics, new RectangleF(0, 0, this.Width, this.Height)));
            //e.Graphics.SmoothingMode = backupSmoothingMode;
        }
        #endregion
        #region 函数
        /// <summary> 绑定中心点控件
        /// </summary>
        /// <param name="centerControl"></param>
        internal virtual void BindingCenterControl(DUIEditableCenterControl bindingCenterControl)
        {
            this.centerControl = bindingCenterControl;
        }
        public virtual PointF AnyBoundsPointToParent(DUIAnyBounds anyBounds, PointF p)
        {
            DUIMatrix matrix = new DUIMatrix();
            matrix.Translate(anyBounds.Center);
            matrix *= this.GetAnyBoundsMatrix(anyBounds);
            matrix.TranslateReverse(anyBounds.Center);
            var matrixPoint = MatrixTools.PointAfterMatrix(p, matrix);
            return new PointF(anyBounds.ClientBounds.X + matrixPoint.X, anyBounds.ClientBounds.Y + matrixPoint.Y);
        }
        protected virtual void SetAnyBoundsChanging(DUIAnyBounds anyBounds)
        {
            this.SetBoundsChanging(
                anyBounds.x
                , anyBounds.y
                , anyBounds.width
                , anyBounds.height
                , anyBounds.centerX
                , anyBounds.centerY
                , anyBounds.rotate
                , anyBounds.skewX
                , anyBounds.skewY
                , anyBounds.scaleX
                , anyBounds.scaleY
                , DUIBoundsSpecified.Bounds
                | DUIBoundsSpecified.Rotate
                | DUIBoundsSpecified.Center
                | DUIBoundsSpecified.Skew
                | DUIBoundsSpecified.Scale
                );
        }
        protected virtual void SetAnyBoundsChanged()
        {
            this.SetBoundsChanged(
                this.x
                , this.y
                , this.width
                , this.height
                , this.centerX
                , this.centerY
                , this.rotate
                , this.skewX
                , this.skewY
                , this.scaleX
                , this.scaleY
                , DUIBoundsSpecified.Bounds
                | DUIBoundsSpecified.Rotate
                | DUIBoundsSpecified.Center
                | DUIBoundsSpecified.Skew
                | DUIBoundsSpecified.Scale
                );
        }
        /// <summary>
        /// 中心点跟随控件的变化而等比例的变化
        /// </summary>
        /// <param name="anyBounds"></param>
        protected virtual void DoCenterFollow(DUIAnyBounds anyBounds, PointF center)
        {
            PointF p = anyBounds.ClientBounds.Location;
            DUIMatrix matrix = new DUIMatrix();
            matrix.Translate(anyBounds.ClientBounds.X + anyBounds.CenterX, anyBounds.ClientBounds.Y + anyBounds.CenterY);
            matrix *= this.GetAnyBoundsMatrix(anyBounds);
            matrix.Translate(-(anyBounds.ClientBounds.X + anyBounds.CenterX), -(anyBounds.ClientBounds.Y + anyBounds.CenterY));
            p = MatrixTools.PointAfterMatrix(p, matrix);
            matrix.Reset();
            matrix.Translate(center);
            matrix *= this.GetAnyBoundsMatrixReverse(anyBounds);
            matrix.TranslateReverse(center);
            p = MatrixTools.PointAfterMatrix(p, matrix);
            anyBounds.X = p.X - anyBounds.BorderWidth;
            anyBounds.Y = p.Y - anyBounds.BorderWidth;
            anyBounds.CenterX = center.X - p.X;
            anyBounds.CenterY = center.Y - p.Y;
        }
        protected virtual DUIAnyBounds GetAnyBounds()
        {
            return new DUIAnyBounds()
            {
                x = this.x,
                y = this.y,
                width = this.width,
                height = this.height,
                centerX = this.centerX,
                centerY = this.centerY,
                rotate = this.rotate,
                skewX = this.skewX,
                skewY = this.skewY,
                scaleX = this.scaleX,
                scaleY = this.scaleY,
                borderWidth = this.borderWidth
            };
        }
        protected internal virtual void SetClientBoundsChanging(float x, float y, float width, float height, float centerX, float centerY, float rotate, float skewX, float skewY, float scaleX, float scaleY, DUIBoundsSpecified specified)
        {
            x = x - this.borderWidth;
            y = y - this.borderWidth;
            SetBoundsChanging(x, y, width, height, centerX, centerY, rotate, skewX, skewY, scaleX, scaleY, specified);
        }
        protected internal virtual void SetClientBoundsChanged(float x, float y, float width, float height, float centerX, float centerY, float rotate, float skewX, float skewY, float scaleX, float scaleY, DUIBoundsSpecified specified)
        {
            x = x - this.borderWidth;
            y = y - this.borderWidth;
            SetBoundsChanged(x, y, width, height, centerX, centerY, rotate, skewX, skewY, scaleX, scaleY, specified);
        }
        protected internal virtual void SetBoundsChanging(float x, float y, float width, float height, float centerX, float centerY, float rotate, float skewX, float skewY, float scaleX, float scaleY, DUIBoundsSpecified specified)
        {
            if ((specified & DUIBoundsSpecified.X) == DUIBoundsSpecified.None) x = this.x;
            if ((specified & DUIBoundsSpecified.Y) == DUIBoundsSpecified.None) y = this.y;
            if ((specified & DUIBoundsSpecified.Width) == DUIBoundsSpecified.None) width = this.width;
            if ((specified & DUIBoundsSpecified.Height) == DUIBoundsSpecified.None) height = this.height;
            if ((specified & DUIBoundsSpecified.CenterX) == DUIBoundsSpecified.None) centerX = this.centerX;
            if ((specified & DUIBoundsSpecified.CenterY) == DUIBoundsSpecified.None) centerY = this.centerY;
            if ((specified & DUIBoundsSpecified.Rotate) == DUIBoundsSpecified.None) rotate = this.rotate;
            if ((specified & DUIBoundsSpecified.SkewX) == DUIBoundsSpecified.None) skewX = this.skewX;
            if ((specified & DUIBoundsSpecified.SkewY) == DUIBoundsSpecified.None) skewY = this.skewY;
            if ((specified & DUIBoundsSpecified.ScaleX) == DUIBoundsSpecified.None) scaleX = this.scaleX;
            if ((specified & DUIBoundsSpecified.ScaleY) == DUIBoundsSpecified.None) scaleY = this.scaleY;
            if (this.x != x ||
                this.y != y ||
                this.width != width ||
                this.height != height ||
                this.centerX != centerX ||
                this.centerY != centerY ||
                this.rotate != rotate ||
                this.skewX != skewX ||
                this.skewY != skewY ||
                this.scaleX != scaleX ||
                this.scaleY != scaleY)
            {
                UpdateBoundsChanging(x, y, width, height, centerX, centerY, rotate, skewX, skewY, scaleX, scaleY);
            }
        }
        protected internal virtual void SetBoundsChanged(float x, float y, float width, float height, float centerX, float centerY, float rotate, float skewX, float skewY, float scaleX, float scaleY, DUIBoundsSpecified specified)
        {
            if ((specified & DUIBoundsSpecified.X) == DUIBoundsSpecified.None) x = this.mouseDownBounds.X;
            if ((specified & DUIBoundsSpecified.Y) == DUIBoundsSpecified.None) y = this.mouseDownBounds.Y;
            if ((specified & DUIBoundsSpecified.Width) == DUIBoundsSpecified.None) width = this.mouseDownBounds.Width;
            if ((specified & DUIBoundsSpecified.Height) == DUIBoundsSpecified.None) height = this.mouseDownBounds.Height;
            if ((specified & DUIBoundsSpecified.CenterX) == DUIBoundsSpecified.None) centerX = this.mouseDownCenter.X;
            if ((specified & DUIBoundsSpecified.CenterY) == DUIBoundsSpecified.None) centerY = this.mouseDownCenter.Y;
            if ((specified & DUIBoundsSpecified.Rotate) == DUIBoundsSpecified.None) rotate = this.mouseDownRotateAngle;
            if ((specified & DUIBoundsSpecified.SkewX) == DUIBoundsSpecified.None) skewX = this.mouseDownSkew.X;
            if ((specified & DUIBoundsSpecified.SkewY) == DUIBoundsSpecified.None) skewY = this.mouseDownSkew.Y;
            if ((specified & DUIBoundsSpecified.ScaleX) == DUIBoundsSpecified.None) scaleX = this.mouseDownScale.X;
            if ((specified & DUIBoundsSpecified.ScaleY) == DUIBoundsSpecified.None) scaleY = this.mouseDownScale.Y;
            if (this.mouseDownBounds.X != x ||
                this.mouseDownBounds.Y != y ||
                this.mouseDownBounds.Width != width ||
                this.mouseDownBounds.Height != height ||
                this.mouseDownCenter.X != centerX ||
                this.mouseDownCenter.Y != centerY ||
                this.mouseDownRotateAngle != rotate ||
                this.mouseDownSkew.X != skewX ||
                this.mouseDownSkew.Y != skewY ||
                this.mouseDownScale.X != scaleX ||
                this.mouseDownScale.Y != scaleY)
            {
                UpdateBoundsChanged(x, y, width, height, centerX, centerY, rotate, skewX, skewY, scaleX, scaleY);
            }
        }
        protected internal virtual void UpdateBoundsChanging(float x, float y, float width, float height, float centerX, float centerY, float rotate, float skewX, float skewY, float scaleX, float scaleY)
        {
            RectangleF lastBounds = new RectangleF(this.x, this.y, this.width, this.height);
            PointF lastCenter = new PointF(this.centerX, this.centerY);
            PointF lastSkew = new PointF(this.skewX, this.skewY);
            PointF lastScale = new PointF(this.scaleX, this.scaleY);
            float lastRotateAngle = this.rotate;
            Region lastRegion = new Region(this.Bounds);
            Matrix lastMatrix = new Matrix();
            lastMatrix.RotateAt(this.Rotate, new PointF(this.ClientBounds.X + this.CenterX, this.ClientBounds.Y + this.CenterY));
            lastRegion.Transform(lastMatrix);
            bool newLocation = this.x != x || this.y != y;
            bool newSize = this.width != width || this.height != height;
            bool newCenter = this.centerX != centerX || this.centerY != centerY;
            bool newSkew = this.skewX != skewX || this.skewY != skewY;
            bool newScale = this.scaleX != scaleX || this.scaleY != scaleY;
            bool newRotateAngle = this.rotate != rotate;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.centerX = centerX;
            this.centerY = centerY;
            this.rotate = rotate;
            this.skewX = skewX;
            this.skewY = skewY;
            this.scaleX = scaleX;
            this.scaleY = scaleY;
            Region currentRegion = new Region(this.Bounds);
            Matrix currentMatrix = new Matrix();
            currentMatrix.RotateAt(this.Rotate, new PointF(this.ClientBounds.X + this.CenterX, this.ClientBounds.Y + this.CenterY));
            currentRegion.Transform(currentMatrix);
            if (newLocation || newSize || newCenter || newSkew || newScale || newRotateAngle)
            {
                lastRegion.Union(currentRegion);
                this.Invalidate(lastRegion);
            }
            if (newLocation)
            {
                this.UpdateLocationChanging(lastBounds);
            }
            if (newSize)
            {
                this.UpdateResizing(lastBounds);
            }
            if (newLocation || newSize)
            {
                this.UpdateBoundsChanging(lastBounds);
            }
            if (newCenter)
            {
                this.UpdateCenterChanging(lastCenter);
            }
            if (newSkew)
            {
                this.UpdateSkewChanging(lastSkew);
            }
            if (newScale)
            {
                this.UpdateScaleChanging(lastScale);
            }
            if (newRotateAngle)
            {
                this.UpdateRotateAngleChanging(lastRotateAngle);
            }
            if (newLocation || newSize || newCenter || newRotateAngle || newSkew || newScale)
            {
                this.UpdateAnyChanging(lastBounds, lastCenter, lastSkew, lastScale, lastRotateAngle);
            }
        }
        protected internal virtual void UpdateBoundsChanged(float x, float y, float width, float height, float centerX, float centerY, float rotate, float skewX, float skewY, float scaleX, float scaleY)
        {
            bool newLocation = this.mouseDownBounds.X != x || this.mouseDownBounds.Y != y;
            bool newSize = this.mouseDownBounds.Width != width || this.mouseDownBounds.Height != height;
            bool newCenter = this.mouseDownCenter.X != centerX || this.mouseDownCenter.Y != centerY;
            bool newSkew = this.mouseDownSkew.X != skewX || this.mouseDownSkew.Y != skewY;
            bool newScale = this.mouseDownScale.X != scaleX || this.mouseDownScale.Y != scaleY;
            bool newRotateAngle = this.mouseDownRotateAngle != rotate;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.centerX = centerX;
            this.centerY = centerY;
            this.rotate = rotate;
            this.skewX = skewX;
            this.skewY = skewY;
            this.scaleX = scaleX;
            this.scaleY = scaleY;
            if (newLocation || newSize || newCenter || newSkew || newScale || newRotateAngle)
            {
                this.Invalidate();
            }
            if (newLocation)
            {
                this.UpdateLocationChanged();
            }
            if (newSize)
            {
                this.UpdateResize();
            }
            if (newLocation || newSize)
            {
                this.UpdateBoundsChanged();
            }
            if (newCenter)
            {
                this.UpdateCenterChanged();
            }
            if (newSkew)
            {
                this.UpdateSkewChanged();
            }
            if (newScale)
            {
                this.UpdateScaleChanged();
            }
            if (newRotateAngle)
            {
                this.UpdateRotateAngleChanged();
            }
            if (newLocation || newSize || newCenter || newSkew || newScale || newRotateAngle)
            {
                this.UpdateAnyChanged();
            }
        }
        /// <summary> 用相对父控件的中心坐标来设置中心坐标
        /// </summary>
        /// <param name="center"></param>
        protected internal virtual void SetCenterChanged(PointF center)
        {
            DUIMatrix matrix = new DUIMatrix();
            matrix.Translate(this.ClientBounds.X + this.CenterX, this.ClientBounds.Y + this.CenterY);
            matrix *= this.Matrix;
            matrix.TranslateReverse(this.ClientBounds.X + this.CenterX, this.ClientBounds.Y + this.CenterY);
            PointF p = MatrixTools.PointAfterMatrix(this.ClientBounds.Location, matrix);
            matrix.Reset();
            matrix.Translate(center);
            matrix *= this.MatrixReverse;
            matrix.TranslateReverse(center);
            p = MatrixTools.PointAfterMatrix(p, matrix);
            this.SetBoundsChanged(p.X - this.borderWidth, p.Y - this.borderWidth, 0, 0, center.X - p.X, center.Y - p.Y, 0, 0, 0, 0, 0, DUIBoundsSpecified.Location | DUIBoundsSpecified.Center);
        }
        /// <summary> 用相对父控件的中心坐标来设置中心坐标
        /// </summary>
        /// <param name="center"></param>
        protected internal virtual void SetCenterChanging(PointF center)
        {
            DUIMatrix matrix = new DUIMatrix();
            matrix.Translate(this.ClientBounds.X + this.CenterX, this.ClientBounds.Y + this.CenterY);
            matrix *= this.Matrix;
            matrix.TranslateReverse(this.ClientBounds.X + this.CenterX, this.ClientBounds.Y + this.CenterY);
            PointF p = MatrixTools.PointAfterMatrix(this.ClientBounds.Location, matrix);
            matrix.Reset();
            matrix.Translate(center);
            matrix *= this.MatrixReverse;
            matrix.TranslateReverse(center);
            p = MatrixTools.PointAfterMatrix(p, matrix);
            this.SetBoundsChanging(p.X - this.borderWidth, p.Y - this.borderWidth, 0, 0, center.X - p.X, center.Y - p.Y, 0, 0, 0, 0, 0, DUIBoundsSpecified.Location | DUIBoundsSpecified.Center);
        }
        public virtual void DoOffsetPoint(float offsetX, float offsetY, DUIAnyBounds anyBounds)
        {
        }
        public virtual void DoOffsetOther(float offsetX, float offsetY, DUIAnyBounds anyBounds)
        {
            offsetY = GetLimitTopOffset(offsetY);
            offsetX = GetLimitLeftOffset(offsetX);
            offsetY = GetLimitBottomOffset(offsetY);
            offsetX = GetLimitRightOffset(offsetX);
            PointF p = MatrixTools.PointAfterMatrix(new PointF(offsetX, offsetY), this.Matrix);
            anyBounds.X += p.X;
            anyBounds.Y += p.Y;
        }
        public virtual void DoOffsetTop(float offsetY, DUIAnyBounds anyBounds)
        {
            if (this.IsUniformScale)
            {
                offsetY = GetLimitLeftOffsetWithScale(offsetY);
                offsetY = GetLimitTopOffset(offsetY);
                offsetY = -GetLimitHeightOffset(-offsetY);
                offsetY = -GetLimitWidthOffsetWithScale(-offsetY);
                float overflowY = offsetY - anyBounds.ClientBounds.Height;
                if (overflowY > 0) { offsetY = anyBounds.ClientBounds.Height; }
                PointF p = MatrixTools.PointAfterMatrix(new PointF(offsetY * lastUniformScale, offsetY), this.Matrix);
                anyBounds.X += p.X;
                anyBounds.Y += p.Y;
                anyBounds.Width -= offsetY * lastUniformScale;
                anyBounds.Height -= offsetY;
                if (overflowY > 0)
                {
                    this.mouseDownMargin = EffectMargin.Bottom;
                    this.HFlipping = !this.HFlipping;
                    this.VFlipping = !this.VFlipping;
                    DoOffsetBottom(overflowY, anyBounds);
                }
            }
            else if (this.IsCenterScale)
            {
                offsetY = GetLimitTopOffset(offsetY);
                offsetY = -GetLimitHeightOffset(-offsetY * 2) / 2;
                offsetY = -GetLimitBottomOffset(-offsetY);
                float overflowY = offsetY * 2 - anyBounds.ClientBounds.Height;
                if (overflowY > 0)
                {
                    offsetY = anyBounds.ClientBounds.Height / 2;
                }
                PointF p = MatrixTools.PointAfterMatrix(new PointF(0, offsetY), this.Matrix);
                anyBounds.X += p.X;
                anyBounds.Y += p.Y;
                anyBounds.Height -= offsetY * 2;
                if (overflowY > 0)
                {
                    this.mouseDownMargin = EffectMargin.Bottom;
                    this.VFlipping = !this.VFlipping;
                    DoOffsetBottom(overflowY / 2, anyBounds);
                }
            }
            else
            {
                offsetY = GetLimitTopOffset(offsetY);
                offsetY = -GetLimitHeightOffset(-offsetY);
                float overflowY = offsetY - anyBounds.ClientBounds.Height;
                if (overflowY > 0) { offsetY = anyBounds.ClientBounds.Height; }
                PointF p = MatrixTools.PointAfterMatrix(new PointF(0, offsetY), this.Matrix);
                anyBounds.X += p.X;
                anyBounds.Y += p.Y;
                anyBounds.Height -= offsetY;
                if (overflowY > 0)
                {
                    this.mouseDownMargin = EffectMargin.Bottom;
                    this.VFlipping = !this.VFlipping;
                    DoOffsetBottom(overflowY, anyBounds);
                }
            }
        }
        public virtual void DoOffsetLeft(float offsetX, DUIAnyBounds anyBounds)
        {
            if (this.IsUniformScale)
            {
                offsetX = GetLimitLeftOffset(offsetX);
                offsetX = GetLimitTopOffsetWithScale(offsetX);
                offsetX = -GetLimitWidthOffset(-offsetX);
                offsetX = -GetLimitHeightOffsetWithScale(-offsetX);
                float overflowX = offsetX - anyBounds.ClientBounds.Width;
                if (overflowX > 0) { offsetX = anyBounds.ClientBounds.Width; }
                PointF p = MatrixTools.PointAfterMatrix(new PointF(offsetX, offsetX / lastUniformScale), this.Matrix);
                anyBounds.X += p.X;
                anyBounds.Y += p.Y;
                anyBounds.Width -= offsetX;
                anyBounds.Height -= (offsetX / lastUniformScale);
                if (overflowX > 0)
                {
                    this.mouseDownMargin = EffectMargin.Right;
                    this.HFlipping = !this.HFlipping;
                    this.VFlipping = !this.VFlipping;
                    DoOffsetRight(overflowX, anyBounds);
                }
            }
            else if (this.IsCenterScale)
            {
                offsetX = GetLimitLeftOffset(offsetX);
                offsetX = -GetLimitWidthOffset(-offsetX * 2) / 2;
                offsetX = -GetLimitRightOffset(-offsetX);
                float overflowX = offsetX * 2 - anyBounds.ClientBounds.Width;
                if (overflowX > 0) { offsetX = anyBounds.ClientBounds.Width / 2; }
                PointF p = MatrixTools.PointAfterMatrix(new PointF(offsetX, 0), this.Matrix);
                anyBounds.X += p.X;
                anyBounds.Y += p.Y;
                anyBounds.Width -= offsetX * 2;
                if (overflowX > 0)
                {
                    this.mouseDownMargin = EffectMargin.Right;
                    this.HFlipping = !this.HFlipping;
                    DoOffsetRight(overflowX / 2, anyBounds);
                }
            }
            else
            {
                offsetX = GetLimitLeftOffset(offsetX);
                offsetX = -GetLimitWidthOffset(-offsetX);
                float overflowX = offsetX - anyBounds.ClientBounds.Width;
                if (overflowX > 0) { offsetX = anyBounds.ClientBounds.Width; }
                PointF p = MatrixTools.PointAfterMatrix(new PointF(offsetX, 0), this.Matrix);
                anyBounds.X += p.X;
                anyBounds.Y += p.Y;
                anyBounds.Width -= offsetX;
                if (overflowX > 0)
                {
                    this.mouseDownMargin = EffectMargin.Right;
                    this.HFlipping = !this.HFlipping;
                    DoOffsetRight(overflowX, anyBounds);
                }
            }
        }
        public virtual void DoOffsetBottom(float offsetY, DUIAnyBounds anyBounds)
        {
            if (this.IsUniformScale)
            {
                offsetY = GetLimitHeightOffset(offsetY);
                offsetY = GetLimitWidthOffsetWithScale(offsetY);
                offsetY = GetLimitRightOffsetWithScale(offsetY);
                offsetY = GetLimitBottomOffset(offsetY);
                float overflowY = offsetY + anyBounds.ClientBounds.Height;
                if (overflowY < 0) { offsetY = -anyBounds.ClientBounds.Height; }
                anyBounds.Width += offsetY * lastUniformScale;
                anyBounds.Height += offsetY;
                if (overflowY < 0)
                {
                    this.mouseDownMargin = EffectMargin.Top;
                    this.HFlipping = !this.HFlipping;
                    this.VFlipping = !this.VFlipping;
                    DoOffsetTop(overflowY, anyBounds);
                }
            }
            else if (this.IsCenterScale)
            {
                offsetY = GetLimitHeightOffset(offsetY * 2) / 2;
                offsetY = -GetLimitTopOffset(-offsetY);
                offsetY = GetLimitBottomOffset(offsetY);
                float overflowY = offsetY * 2 + anyBounds.ClientBounds.Height;
                if (overflowY < 0) { offsetY = -anyBounds.ClientBounds.Height / 2; }
                PointF p = MatrixTools.PointAfterMatrix(new PointF(0, offsetY), this.Matrix);
                anyBounds.X -= p.X;
                anyBounds.Y -= p.Y;
                anyBounds.Height += offsetY * 2;
                if (overflowY < 0)
                {
                    this.mouseDownMargin = EffectMargin.Top;
                    this.VFlipping = !this.VFlipping;
                    DoOffsetTop(overflowY / 2, anyBounds);
                }
            }
            else
            {
                offsetY = GetLimitHeightOffset(offsetY);
                offsetY = GetLimitBottomOffset(offsetY);
                float overflowY = offsetY + anyBounds.ClientBounds.Height;
                if (overflowY < 0) { offsetY = -anyBounds.ClientBounds.Height; }
                anyBounds.Height += offsetY;
                if (overflowY < 0)
                {
                    this.mouseDownMargin = EffectMargin.Top;
                    this.VFlipping = !this.VFlipping;
                    DoOffsetTop(overflowY, anyBounds);
                }
            }
        }
        public virtual void DoOffsetRight(float offsetX, DUIAnyBounds anyBounds)
        {
            if (this.IsUniformScale)
            {
                offsetX = GetLimitWidthOffset(offsetX);
                offsetX = GetLimitHeightOffsetWithScale(offsetX);
                offsetX = GetLimitBottomOffsetWithScale(offsetX);
                offsetX = GetLimitRightOffset(offsetX);
                float overflowX = offsetX + anyBounds.ClientBounds.Width;
                if (overflowX < 0) { offsetX = -anyBounds.ClientBounds.Width; }
                anyBounds.Width += offsetX;
                anyBounds.Height += offsetX / lastUniformScale;
                if (overflowX < 0)
                {
                    this.mouseDownMargin = EffectMargin.Left;
                    this.HFlipping = !this.HFlipping;
                    this.VFlipping = !this.VFlipping;
                    DoOffsetLeft(overflowX, anyBounds);
                }
            }
            else if (this.IsCenterScale)
            {
                offsetX = GetLimitWidthOffset(offsetX * 2) / 2;
                offsetX = -GetLimitLeftOffset(-offsetX);
                offsetX = GetLimitRightOffset(offsetX);
                float overflowX = offsetX * 2 + anyBounds.ClientBounds.Width;
                if (overflowX < 0) { offsetX = -anyBounds.ClientBounds.Width / 2; }
                PointF p = MatrixTools.PointAfterMatrix(new PointF(offsetX, 0), this.Matrix);
                anyBounds.X -= p.X;
                anyBounds.Y -= p.Y;
                anyBounds.Width += offsetX * 2;
                if (overflowX < 0)
                {
                    this.mouseDownMargin = EffectMargin.Left;
                    this.HFlipping = !this.HFlipping;
                    DoOffsetLeft(overflowX / 2, anyBounds);
                }
            }
            else
            {
                offsetX = GetLimitWidthOffset(offsetX);
                offsetX = GetLimitRightOffset(offsetX);
                float overflowX = offsetX + anyBounds.ClientBounds.Width;
                if (overflowX < 0) { offsetX = -anyBounds.ClientBounds.Width; }
                anyBounds.Width += offsetX;
                if (overflowX < 0)
                {
                    this.mouseDownMargin = EffectMargin.Left;
                    this.HFlipping = !this.HFlipping;
                    DoOffsetLeft(overflowX, anyBounds);
                }
            }
        }
        public virtual void DoOffsetLeftTop(float offsetX, float offsetY, PointF d, DUIAnyBounds anyBounds)
        {
            if (this.IsUniformScale)
            {
                int direction = PointTools.PointAtLineRightOrLeft(new PointF(0, 0), new PointF(anyBounds.ClientBounds.Width, anyBounds.ClientBounds.Height), d);
                if (direction == 1 || direction == 0)
                {
                    offsetX = GetLimitLeftOffset(offsetX);
                    offsetX = GetLimitTopOffsetWithScale(offsetX);
                    offsetX = -GetLimitWidthOffset(-offsetX);
                    offsetX = -GetLimitHeightOffsetWithScale(-offsetX);
                    float overflowX = offsetX - anyBounds.ClientBounds.Width;
                    if (overflowX > 0) { offsetX = anyBounds.ClientBounds.Width; }
                    float overflowY = offsetX / lastUniformScale - anyBounds.ClientBounds.Height;
                    if (overflowY > 0) { offsetX = anyBounds.ClientBounds.Height * lastUniformScale; }
                    PointF p = MatrixTools.PointAfterMatrix(new PointF(offsetX, offsetX / lastUniformScale), this.Matrix);
                    anyBounds.X += p.X;
                    anyBounds.Y += p.Y;
                    anyBounds.Width -= offsetX;
                    anyBounds.Height -= offsetX / lastUniformScale;
                    if (overflowX > 0)
                    {
                        this.mouseDownMargin = EffectMargin.RightBottom;
                        this.HFlipping = !this.HFlipping;
                        this.VFlipping = !this.VFlipping;
                        DoOffsetRightBottom(overflowX, overflowX / lastUniformScale, d, anyBounds);
                    }
                    if (overflowY > 0)
                    {
                        this.mouseDownMargin = EffectMargin.RightBottom;
                        this.HFlipping = !this.HFlipping;
                        this.VFlipping = !this.VFlipping;
                        DoOffsetRightBottom(overflowY * lastUniformScale, overflowY, d, anyBounds);
                    }
                }
                if (direction == -1)
                {
                    offsetY = GetLimitLeftOffsetWithScale(offsetY);
                    offsetY = GetLimitTopOffset(offsetY);
                    offsetY = -GetLimitHeightOffset(-offsetY);
                    offsetY = -GetLimitWidthOffsetWithScale(-offsetY);
                    float overflowX = offsetY * lastUniformScale - anyBounds.ClientBounds.Width;
                    if (overflowX > 0) { offsetY = anyBounds.ClientBounds.Width / lastUniformScale; }
                    float overflowY = offsetY - anyBounds.ClientBounds.Height;
                    if (overflowY > 0) { offsetY = anyBounds.ClientBounds.Height; }
                    PointF p = MatrixTools.PointAfterMatrix(new PointF(offsetY * lastUniformScale, offsetY), this.Matrix);
                    anyBounds.X += p.X;
                    anyBounds.Y += p.Y;
                    anyBounds.Width -= offsetY * lastUniformScale;
                    anyBounds.Height -= offsetY;
                    if (overflowX > 0)
                    {
                        this.mouseDownMargin = EffectMargin.RightBottom;
                        this.HFlipping = !this.HFlipping;
                        this.VFlipping = !this.VFlipping;
                        DoOffsetRightBottom(overflowX, overflowX / lastUniformScale, d, anyBounds);
                    }
                    if (overflowY > 0)
                    {
                        this.mouseDownMargin = EffectMargin.RightBottom;
                        this.HFlipping = !this.HFlipping;
                        this.VFlipping = !this.VFlipping;
                        DoOffsetRightBottom(overflowY * lastUniformScale, overflowY, d, anyBounds);
                    }
                }
            }
            else if (this.IsCenterScale)
            {
                offsetX = GetLimitLeftOffset(offsetX);
                offsetX = -GetLimitWidthOffset(-offsetX * 2) / 2;
                offsetX = -GetLimitRightOffset(-offsetX);
                offsetY = GetLimitTopOffset(offsetY);
                offsetY = -GetLimitHeightOffset(-offsetY * 2) / 2;
                offsetY = -GetLimitBottomOffset(-offsetY);
                float overflowX = offsetX * 2 - anyBounds.ClientBounds.Width;
                if (overflowX > 0) { offsetX = anyBounds.ClientBounds.Width / 2; }
                float overflowY = offsetY * 2 - anyBounds.ClientBounds.Height;
                if (overflowY > 0) { offsetY = anyBounds.ClientBounds.Height / 2; }
                PointF p = MatrixTools.PointAfterMatrix(new PointF(offsetX, offsetY), this.Matrix);
                anyBounds.X += p.X;
                anyBounds.Y += p.Y;
                anyBounds.Width -= offsetX * 2;
                anyBounds.Height -= offsetY * 2;
                if (overflowX > 0 && overflowY <= 0)
                {
                    this.mouseDownMargin = EffectMargin.RightTop;
                    this.HFlipping = !this.HFlipping;
                    DoOffsetRightTop(overflowX / 2, 0, d, anyBounds);
                }
                if (overflowY > 0 && overflowX <= 0)
                {
                    this.mouseDownMargin = EffectMargin.LeftBottom;
                    this.VFlipping = !this.VFlipping;
                    DoOffsetLeftBottom(0, overflowY / 2, d, anyBounds);
                }
                if (overflowX > 0 && overflowY > 0)
                {
                    this.mouseDownMargin = EffectMargin.RightBottom;
                    this.HFlipping = !this.HFlipping;
                    this.VFlipping = !this.VFlipping;
                    DoOffsetRightBottom(overflowX / 2, overflowY / 2, d, anyBounds);
                }
            }
            else
            {
                offsetX = GetLimitLeftOffset(offsetX);
                offsetX = -GetLimitWidthOffset(-offsetX);
                offsetY = GetLimitTopOffset(offsetY);
                offsetY = -GetLimitHeightOffset(-offsetY);
                float overflowX = offsetX - anyBounds.ClientBounds.Width;
                if (overflowX > 0) { offsetX = anyBounds.ClientBounds.Width; }
                float overflowY = offsetY - anyBounds.ClientBounds.Height;
                if (overflowY > 0) { offsetY = anyBounds.ClientBounds.Height; }
                PointF p = MatrixTools.PointAfterMatrix(new PointF(offsetX, offsetY), this.Matrix);
                anyBounds.X += p.X;
                anyBounds.Y += p.Y;
                anyBounds.Width -= offsetX;
                anyBounds.Height -= offsetY;
                if (overflowX > 0 && overflowY <= 0)
                {
                    this.mouseDownMargin = EffectMargin.RightTop;
                    this.HFlipping = !this.HFlipping;
                    DoOffsetRightTop(overflowX, 0, d, anyBounds);
                }
                if (overflowY > 0 && overflowX <= 0)
                {
                    this.mouseDownMargin = EffectMargin.LeftBottom;
                    this.VFlipping = !this.VFlipping;
                    DoOffsetLeftBottom(0, overflowY, d, anyBounds);
                }
                if (overflowX > 0 && overflowY > 0)
                {
                    this.mouseDownMargin = EffectMargin.RightBottom;
                    this.HFlipping = !this.HFlipping;
                    this.VFlipping = !this.VFlipping;
                    DoOffsetRightBottom(overflowX, overflowY, d, anyBounds);
                }
            }
        }
        public virtual void DoOffsetRightTop(float offsetX, float offsetY, PointF d, DUIAnyBounds anyBounds)
        {
            if (this.IsUniformScale)
            {
                int direction = PointTools.PointAtLineRightOrLeft(new PointF(anyBounds.ClientBounds.Width, 0), new PointF(0, anyBounds.ClientBounds.Height), d);
                if (direction == 1 || direction == 0)
                {
                    offsetY = GetLimitLeftOffsetWithScale(offsetY);
                    offsetY = GetLimitTopOffset(offsetY);
                    offsetY = -GetLimitHeightOffset(-offsetY);
                    offsetY = -GetLimitWidthOffsetWithScale(-offsetY);
                    float overflowX = offsetY * lastUniformScale - anyBounds.ClientBounds.Width;
                    if (overflowX > 0) { offsetY = anyBounds.ClientBounds.Width / lastUniformScale; }
                    float overflowY = offsetY - anyBounds.ClientBounds.Height;
                    if (overflowY > 0) { offsetY = anyBounds.ClientBounds.Height; }
                    PointF p = MatrixTools.PointAfterMatrix(new PointF(0, offsetY), this.Matrix);
                    anyBounds.X += p.X;
                    anyBounds.Y += p.Y;
                    anyBounds.Width -= offsetY * lastUniformScale;
                    anyBounds.Height -= offsetY;
                    if (overflowX > 0)
                    {
                        this.mouseDownMargin = EffectMargin.LeftBottom;
                        this.HFlipping = !this.HFlipping;
                        this.VFlipping = !this.VFlipping;
                        DoOffsetLeftBottom(0, overflowX / lastUniformScale, d, anyBounds);
                    }
                    if (overflowY > 0)
                    {
                        this.mouseDownMargin = EffectMargin.LeftBottom;
                        this.HFlipping = !this.HFlipping;
                        this.VFlipping = !this.VFlipping;
                        DoOffsetLeftBottom(0, overflowY, d, anyBounds);
                    }
                }
                if (direction == -1)
                {
                    offsetX = GetLimitWidthOffset(offsetX);
                    offsetX = GetLimitTopOffsetWithScale(offsetX);
                    offsetX = GetLimitHeightOffsetWithScale(offsetX);
                    offsetX = GetLimitRightOffset(offsetX);
                    float overflowX = offsetX + anyBounds.ClientBounds.Width;
                    if (overflowX < 0) { offsetX = -anyBounds.ClientBounds.Width; }
                    float overflowY = offsetX / lastUniformScale + anyBounds.ClientBounds.Height;
                    if (overflowY < 0) { offsetX = -anyBounds.ClientBounds.Height * lastUniformScale; }
                    PointF p = MatrixTools.PointAfterMatrix(new PointF(0, -offsetX / lastUniformScale), this.Matrix);
                    anyBounds.X += p.X;
                    anyBounds.Y += p.Y;
                    anyBounds.Width += offsetX;
                    anyBounds.Height += offsetX / lastUniformScale;
                    if (overflowX < 0)
                    {
                        this.mouseDownMargin = EffectMargin.LeftBottom;
                        this.HFlipping = !this.HFlipping;
                        this.VFlipping = !this.VFlipping;
                        DoOffsetLeftBottom(0, overflowX / lastUniformScale, d, anyBounds);
                    }
                    if (overflowY < 0)
                    {
                        this.mouseDownMargin = EffectMargin.LeftBottom;
                        this.HFlipping = !this.HFlipping;
                        this.VFlipping = !this.VFlipping;
                        DoOffsetLeftBottom(0, overflowY, d, anyBounds);
                    }
                }
            }
            else if (this.IsCenterScale)
            {
                offsetY = GetLimitTopOffset(offsetY);
                offsetY = -GetLimitHeightOffset(-offsetY * 2) / 2;
                offsetY = -GetLimitBottomOffset(-offsetY);
                offsetX = GetLimitWidthOffset(offsetX * 2) / 2;
                offsetX = -GetLimitLeftOffset(-offsetX);
                offsetX = GetLimitRightOffset(offsetX);
                float overflowX = offsetX * 2 + anyBounds.ClientBounds.Width;
                if (overflowX < 0) { offsetX = -anyBounds.ClientBounds.Width / 2; }
                float overflowY = offsetY * 2 - anyBounds.ClientBounds.Height;
                if (overflowY > 0) { offsetY = anyBounds.ClientBounds.Height / 2; }
                PointF p = MatrixTools.PointAfterMatrix(new PointF(-offsetX, offsetY), this.Matrix);
                anyBounds.X += p.X;
                anyBounds.Y += p.Y;
                anyBounds.Width += offsetX * 2;
                anyBounds.Height -= offsetY * 2;
                if (overflowX < 0 && overflowY <= 0)
                {
                    this.mouseDownMargin = EffectMargin.LeftTop;
                    this.HFlipping = !this.HFlipping;
                    DoOffsetLeftTop(overflowX / 2, 0, d, anyBounds);
                }
                if (overflowY > 0 && overflowX >= 0)
                {
                    this.mouseDownMargin = EffectMargin.RightBottom;
                    this.VFlipping = !this.VFlipping;
                    DoOffsetRightBottom(0, overflowY / 2, d, anyBounds);
                }
                if (overflowX < 0 && overflowY > 0)
                {
                    this.mouseDownMargin = EffectMargin.LeftBottom;
                    this.HFlipping = !this.HFlipping;
                    this.VFlipping = !this.VFlipping;
                    DoOffsetLeftBottom(overflowX / 2, overflowY / 2, d, anyBounds);
                }
            }
            else
            {
                offsetY = GetLimitTopOffset(offsetY);
                offsetY = -GetLimitHeightOffset(-offsetY);
                offsetX = GetLimitWidthOffset(offsetX);
                offsetX = GetLimitRightOffset(offsetX);
                float overflowX = offsetX + anyBounds.ClientBounds.Width;
                if (overflowX < 0) { offsetX = -anyBounds.ClientBounds.Width; }
                float overflowY = offsetY - anyBounds.ClientBounds.Height;
                if (overflowY > 0) { offsetY = anyBounds.ClientBounds.Height; }
                PointF p = MatrixTools.PointAfterMatrix(new PointF(0, offsetY), this.Matrix);
                anyBounds.X += p.X;
                anyBounds.Y += p.Y;
                anyBounds.Width += offsetX;
                anyBounds.Height -= offsetY;
                if (overflowX < 0 && overflowY <= 0)
                {
                    this.mouseDownMargin = EffectMargin.LeftTop;
                    this.HFlipping = !this.HFlipping;
                    DoOffsetLeftTop(overflowX, 0, d, anyBounds);
                }
                if (overflowY > 0 && overflowX >= 0)
                {
                    this.mouseDownMargin = EffectMargin.RightBottom;
                    this.VFlipping = !this.VFlipping;
                    DoOffsetRightBottom(0, overflowY, d, anyBounds);
                }
                if (overflowX < 0 && overflowY > 0)
                {
                    this.mouseDownMargin = EffectMargin.LeftBottom;
                    this.HFlipping = !this.HFlipping;
                    this.VFlipping = !this.VFlipping;
                    DoOffsetLeftBottom(overflowX, overflowY, d, anyBounds);
                }
            }
        }
        public virtual void DoOffsetRightBottom(float offsetX, float offsetY, PointF d, DUIAnyBounds anyBounds)
        {
            if (this.IsUniformScale)
            {
                int direction = PointTools.PointAtLineRightOrLeft(new PointF(0, 0), new PointF(anyBounds.ClientBounds.Width, anyBounds.ClientBounds.Height), d);
                if (direction == 1 || direction == 0)
                {
                    offsetX = GetLimitWidthOffset(offsetX);
                    offsetX = GetLimitRightOffsetWithScale(offsetX);
                    offsetX = GetLimitHeightOffsetWithScale(offsetX);
                    offsetX = GetLimitBottomOffset(offsetX);
                    float overflowX = offsetX + anyBounds.ClientBounds.Width;
                    if (overflowX < 0) { offsetX = -anyBounds.ClientBounds.Width; }
                    float overflowY = offsetX / lastUniformScale + anyBounds.ClientBounds.Height;
                    if (overflowY < 0) { offsetX = -anyBounds.ClientBounds.Height * lastUniformScale; }
                    anyBounds.Width += offsetX;
                    anyBounds.Height += offsetX / lastUniformScale;
                    if (overflowX < 0)
                    {
                        this.mouseDownMargin = EffectMargin.LeftTop;
                        this.HFlipping = !this.HFlipping;
                        this.VFlipping = !this.VFlipping;
                        DoOffsetLeftTop(overflowX, overflowX / lastUniformScale, d, anyBounds);
                    }
                    if (overflowY < 0)
                    {
                        this.mouseDownMargin = EffectMargin.LeftTop;
                        this.HFlipping = !this.HFlipping;
                        this.VFlipping = !this.VFlipping;
                        DoOffsetLeftTop(overflowY * lastUniformScale, overflowY, d, anyBounds);
                    }
                }
                if (direction == -1)
                {
                    offsetY = GetLimitHeightOffset(offsetY);
                    offsetY = GetLimitWidthOffsetWithScale(offsetY);
                    offsetY = GetLimitRightOffsetWithScale(offsetY);
                    offsetY = GetLimitBottomOffset(offsetY);
                    float overflowX = offsetY * lastUniformScale + anyBounds.ClientBounds.Width;
                    if (overflowX < 0) { offsetX = -anyBounds.ClientBounds.Width / lastUniformScale; }
                    float overflowY = offsetY + anyBounds.ClientBounds.Height;
                    if (overflowY < 0) { offsetY = -anyBounds.ClientBounds.Height; }
                    anyBounds.Width += offsetY * lastUniformScale;
                    anyBounds.Height += offsetY;
                    if (overflowX < 0)
                    {
                        this.mouseDownMargin = EffectMargin.LeftTop;
                        this.HFlipping = !this.HFlipping;
                        this.VFlipping = !this.VFlipping;
                        DoOffsetLeftTop(overflowX, overflowX / lastUniformScale, d, anyBounds);
                    }
                    if (overflowY < 0)
                    {
                        this.mouseDownMargin = EffectMargin.LeftTop;
                        this.HFlipping = !this.HFlipping;
                        this.VFlipping = !this.VFlipping;
                        DoOffsetLeftTop(overflowY * lastUniformScale, overflowY, d, anyBounds);
                    }
                }
            }
            else if (this.IsCenterScale)
            {
                offsetY = GetLimitHeightOffset(offsetY * 2) / 2;
                offsetY = -GetLimitTopOffset(-offsetY);
                offsetY = GetLimitBottomOffset(offsetY);
                offsetX = GetLimitWidthOffset(offsetX * 2) / 2;
                offsetX = -GetLimitLeftOffset(-offsetX);
                offsetX = GetLimitRightOffset(offsetX);
                float overflowX = offsetX * 2 + anyBounds.ClientBounds.Width;
                if (overflowX < 0) { offsetX = -anyBounds.ClientBounds.Width / 2; }
                float overflowY = offsetY * 2 + anyBounds.ClientBounds.Height;
                if (overflowY < 0) { offsetY = -anyBounds.ClientBounds.Height / 2; }
                PointF p = MatrixTools.PointAfterMatrix(new PointF(offsetX, offsetY), this.Matrix);
                anyBounds.X -= p.X;
                anyBounds.Y -= p.Y;
                anyBounds.Width += offsetX * 2;
                anyBounds.Height += offsetY * 2;
                if (overflowX < 0 && overflowY >= 0)
                {
                    this.mouseDownMargin = EffectMargin.LeftBottom;
                    this.HFlipping = !this.HFlipping;
                    DoOffsetLeftBottom(overflowX / 2, 0, d, anyBounds);
                }
                if (overflowX >= 0 && overflowY < 0)
                {
                    this.mouseDownMargin = EffectMargin.RightTop;
                    this.VFlipping = !this.VFlipping;
                    DoOffsetRightTop(0, overflowY / 2, d, anyBounds);
                }
                if (overflowX < 0 && overflowY < 0)
                {
                    this.mouseDownMargin = EffectMargin.LeftTop;
                    this.HFlipping = !this.HFlipping;
                    this.VFlipping = !this.VFlipping;
                    DoOffsetLeftTop(overflowX / 2, overflowY / 2, d, anyBounds);
                }
            }
            else
            {
                offsetY = GetLimitHeightOffset(offsetY);
                offsetY = GetLimitBottomOffset(offsetY);
                offsetX = GetLimitWidthOffset(offsetX);
                offsetX = GetLimitRightOffset(offsetX);
                float overflowX = offsetX + anyBounds.ClientBounds.Width;
                if (overflowX < 0) { offsetX = -anyBounds.ClientBounds.Width; }
                float overflowY = offsetY + anyBounds.ClientBounds.Height;
                if (overflowY < 0) { offsetY = -anyBounds.ClientBounds.Height; }
                anyBounds.Width += offsetX;
                anyBounds.Height += offsetY;
                if (overflowX < 0 && overflowY >= 0)
                {
                    this.mouseDownMargin = EffectMargin.LeftBottom;
                    this.HFlipping = !this.HFlipping;
                    DoOffsetLeftBottom(overflowX, 0, d, anyBounds);
                }
                if (overflowX >= 0 && overflowY < 0)
                {
                    this.mouseDownMargin = EffectMargin.RightTop;
                    this.VFlipping = !this.VFlipping;
                    DoOffsetRightTop(0, overflowY, d, anyBounds);
                }
                if (overflowX < 0 && overflowY < 0)
                {
                    this.mouseDownMargin = EffectMargin.LeftTop;
                    this.HFlipping = !this.HFlipping;
                    this.VFlipping = !this.VFlipping;
                    DoOffsetLeftTop(overflowX, overflowY, d, anyBounds);
                }
            }
        }
        public virtual void DoOffsetLeftBottom(float offsetX, float offsetY, PointF d, DUIAnyBounds anyBounds)
        {
            if (this.IsUniformScale)
            {
                int direction = PointTools.PointAtLineRightOrLeft(new PointF(0, anyBounds.ClientBounds.Height), new PointF(anyBounds.ClientBounds.Width, 0), d);
                if (direction == 1 || direction == 0)
                {
                    offsetX = GetLimitLeftOffset(offsetX);
                    offsetX = GetLimitTopOffsetWithScale(offsetX);
                    offsetX = -GetLimitWidthOffset(-offsetX);
                    offsetX = -GetLimitHeightOffsetWithScale(-offsetX);
                    float overflowX = offsetX - anyBounds.ClientBounds.Width;
                    if (overflowX > 0) { offsetX = anyBounds.ClientBounds.Width; }
                    float overflowY = offsetX / lastUniformScale - anyBounds.ClientBounds.Height;
                    if (overflowY > 0) { offsetX = anyBounds.ClientBounds.Height * lastUniformScale; }
                    PointF p = MatrixTools.PointAfterMatrix(new PointF(offsetX, 0), this.Matrix);
                    anyBounds.X += p.X;
                    anyBounds.Y += p.Y;
                    anyBounds.Width -= offsetX;
                    anyBounds.Height -= offsetX / lastUniformScale;
                    if (overflowX > 0)
                    {
                        this.mouseDownMargin = EffectMargin.RightTop;
                        this.HFlipping = !this.HFlipping;
                        this.VFlipping = !this.VFlipping;
                        DoOffsetRightTop(overflowX, 0, d, anyBounds);
                    }
                    if (overflowY > 0)
                    {
                        this.mouseDownMargin = EffectMargin.RightTop;
                        this.HFlipping = !this.HFlipping;
                        this.VFlipping = !this.VFlipping;
                        DoOffsetRightTop(overflowY * lastUniformScale, 0, d, anyBounds);
                    }
                }
                if (direction == -1)
                {
                    offsetY = GetLimitHeightOffset(offsetY);
                    offsetY = GetLimitWidthOffsetWithScale(offsetY);
                    offsetY = GetLimitRightOffsetWithScale(offsetY);
                    offsetY = GetLimitBottomOffset(offsetY);
                    float overflowX = offsetY * lastUniformScale + anyBounds.ClientBounds.Width;
                    if (overflowX < 0) { offsetX = -anyBounds.ClientBounds.Width / lastUniformScale; }
                    float overflowY = offsetY + anyBounds.ClientBounds.Height;
                    if (overflowY < 0) { offsetY = -anyBounds.ClientBounds.Height; }
                    PointF p = MatrixTools.PointAfterMatrix(new PointF(-offsetY * lastUniformScale, 0), this.Matrix);
                    anyBounds.X += p.X;
                    anyBounds.Y += p.Y;
                    anyBounds.Width += offsetY * lastUniformScale;
                    anyBounds.Height += offsetY;
                    if (overflowX < 0)
                    {
                        this.mouseDownMargin = EffectMargin.RightTop;
                        this.HFlipping = !this.HFlipping;
                        this.VFlipping = !this.VFlipping;
                        DoOffsetRightTop(overflowX, 0, d, anyBounds);
                    }
                    if (overflowY < 0)
                    {
                        this.mouseDownMargin = EffectMargin.RightTop;
                        this.HFlipping = !this.HFlipping;
                        this.VFlipping = !this.VFlipping;
                        DoOffsetRightTop(overflowY * lastUniformScale, 0, d, anyBounds);
                    }
                }
            }
            else if (this.IsCenterScale)
            {
                offsetX = GetLimitLeftOffset(offsetX);
                offsetX = -GetLimitWidthOffset(-offsetX * 2) / 2;
                offsetX = -GetLimitRightOffset(-offsetX);
                offsetY = GetLimitHeightOffset(offsetY * 2) / 2;
                offsetY = -GetLimitTopOffset(-offsetY);
                offsetY = GetLimitBottomOffset(offsetY);
                float overflowX = offsetX * 2 - anyBounds.ClientBounds.Width;
                if (overflowX > 0) { offsetX = anyBounds.ClientBounds.Width / 2; }
                float overflowY = offsetY * 2 + anyBounds.ClientBounds.Height;
                if (overflowY < 0) { offsetY = -anyBounds.ClientBounds.Height / 2; }
                PointF p = MatrixTools.PointAfterMatrix(new PointF(offsetX, -offsetY), this.Matrix);
                anyBounds.X += p.X;
                anyBounds.Y += p.Y;
                anyBounds.Width -= offsetX * 2;
                anyBounds.Height += offsetY * 2;
                if (overflowX > 0 && overflowY >= 0)
                {
                    this.HFlipping = !this.HFlipping;
                    this.mouseDownMargin = EffectMargin.RightBottom;
                    DoOffsetRightBottom(overflowX / 2, 0, d, anyBounds);
                }
                if (overflowX <= 0 && overflowY < 0)
                {
                    this.VFlipping = !this.VFlipping;
                    this.mouseDownMargin = EffectMargin.LeftTop;
                    DoOffsetLeftTop(0, overflowY / 2, d, anyBounds);
                }
                if (overflowX > 0 && overflowY < 0)
                {
                    this.HFlipping = !this.HFlipping;
                    this.VFlipping = !this.VFlipping;
                    this.mouseDownMargin = EffectMargin.RightTop;
                    DoOffsetRightTop(overflowX / 2, overflowY / 2, d, anyBounds);
                }
            }
            else
            {
                offsetX = GetLimitLeftOffset(offsetX);
                offsetX = -GetLimitWidthOffset(-offsetX);
                offsetY = GetLimitHeightOffset(offsetY);
                offsetY = GetLimitBottomOffset(offsetY);
                float overflowX = offsetX - anyBounds.ClientBounds.Width;
                if (overflowX > 0) { offsetX = anyBounds.ClientBounds.Width; }
                float overflowY = offsetY + anyBounds.ClientBounds.Height;
                if (overflowY < 0) { offsetY = -anyBounds.ClientBounds.Height; }
                PointF p = MatrixTools.PointAfterMatrix(new PointF(offsetX, 0), this.Matrix);
                anyBounds.X += p.X;
                anyBounds.Y += p.Y;
                anyBounds.Width -= offsetX;
                anyBounds.Height += offsetY;
                if (overflowX > 0 && overflowY >= 0)
                {
                    this.mouseDownMargin = EffectMargin.RightBottom;
                    this.HFlipping = !this.HFlipping;
                    DoOffsetRightBottom(overflowX, 0, d, anyBounds);
                }
                if (overflowX <= 0 && overflowY < 0)
                {
                    this.mouseDownMargin = EffectMargin.LeftTop;
                    this.VFlipping = !this.VFlipping;
                    DoOffsetLeftTop(0, overflowY, d, anyBounds);
                }
                if (overflowX > 0 && overflowY < 0)
                {
                    this.mouseDownMargin = EffectMargin.RightTop;
                    this.HFlipping = !this.HFlipping;
                    this.VFlipping = !this.VFlipping;
                    DoOffsetRightTop(overflowX, overflowY, d, anyBounds);
                }
            }
        }
        protected virtual void DoOffsetLeftSkew(float offsetX, float offsetY, PointF mouseDownSkew, DUIAnyBounds anyBounds)
        {
            if (Math.Abs(this.Center.X - 0) < 0.001) { return; }
            PointF p = MatrixTools.PointAfterMatrix(new PointF(offsetX, offsetY), this.Matrix);
            double lastOffsetY = mouseDownSkew.Y * (this.Center.X);
            float lastScale = (float)(anyBounds.SkewX * -anyBounds.SkewY) + 1;
            float lastscaleX = anyBounds.ScaleX;
            float lastscaleY = anyBounds.ScaleY;
            anyBounds.SkewY = (float)(lastOffsetY - p.Y) / (this.Center.X);
            var scale = (float)(anyBounds.SkewX * -anyBounds.SkewY) + 1;
            anyBounds.ScaleX = 1 / scale;
            anyBounds.ScaleY = 1 / scale;
            anyBounds.ScaleX *= lastscaleX * lastScale;
            anyBounds.ScaleY /= anyBounds.ScaleY / lastscaleY;
        }
        protected virtual void DoOffsetTopSkew(float offsetX, float offsetY, PointF mouseDownSkew, DUIAnyBounds anyBounds)
        {
            if (Math.Abs(this.Center.Y - 0) < 0.001) { return; }
            PointF p = MatrixTools.PointAfterMatrix(new PointF(offsetX, offsetY), this.Matrix);
            double lastOffsetX = mouseDownSkew.X * (this.Center.Y);
            float lastScale = (float)(anyBounds.SkewX * -anyBounds.SkewY) + 1;
            float lastscaleX = anyBounds.ScaleX;
            float lastscaleY = anyBounds.ScaleY;
            anyBounds.SkewX = (float)(lastOffsetX - p.X) / (this.Center.Y);
            var scale = (float)(anyBounds.SkewX * -anyBounds.SkewY) + 1;
            anyBounds.ScaleX = 1 / scale;
            anyBounds.ScaleY = 1 / scale;
            anyBounds.ScaleX /= anyBounds.ScaleX / lastscaleX;
            anyBounds.ScaleY *= lastscaleY * lastScale;
        }
        protected virtual void DoOffsetRightSkew(float offsetX, float offsetY, PointF mouseDownSkew, DUIAnyBounds anyBounds)
        {
            if (Math.Abs(this.Center.X - this.ClientSize.Width) < 0.001) { return; }
            PointF p = MatrixTools.PointAfterMatrix(new PointF(offsetX, offsetY), this.Matrix);
            double lastOffsetY = mouseDownSkew.Y * (this.ClientSize.Width - this.Center.X);
            float lastScale = (float)(anyBounds.SkewX * -anyBounds.SkewY) + 1;
            float lastscaleX = anyBounds.ScaleX;
            float lastscaleY = anyBounds.ScaleY;
            anyBounds.SkewY = (float)(lastOffsetY + p.Y) / (this.ClientSize.Width - this.Center.X);
            var scale = (float)(anyBounds.SkewX * -anyBounds.SkewY) + 1;
            anyBounds.ScaleX = 1 / scale;
            anyBounds.ScaleY = 1 / scale;
            anyBounds.ScaleX *= lastscaleX * lastScale;
            anyBounds.ScaleY /= anyBounds.ScaleY / lastscaleY;
        }
        protected virtual void DoOffsetBottomSkew(float offsetX, float offsetY, PointF mouseDownSkew, DUIAnyBounds anyBounds)
        {
            if (Math.Abs(this.Center.Y - this.ClientSize.Height) < 0.001) { return; }
            PointF p = MatrixTools.PointAfterMatrix(new PointF(offsetX, offsetY), this.Matrix);
            double lastOffsetX = mouseDownSkew.X * (this.ClientSize.Height - this.Center.Y);
            float lastScale = (float)(anyBounds.SkewX * -anyBounds.SkewY) + 1;
            float lastscaleX = anyBounds.ScaleX;
            float lastscaleY = anyBounds.ScaleY;
            anyBounds.SkewX = (float)(lastOffsetX + p.X) / (this.ClientSize.Height - this.Center.Y);
            var scale = (float)(anyBounds.SkewX * -anyBounds.SkewY) + 1;
            anyBounds.ScaleX = 1 / scale;
            anyBounds.ScaleY = 1 / scale;
            anyBounds.ScaleX /= anyBounds.ScaleX / lastscaleX;
            anyBounds.ScaleY *= lastscaleY * lastScale;
        }
        /// <summary> 在8个点的不同鼠标样式
        /// </summary>
        /// <param name="mouseLocation"></param>
        /// <returns></returns>
        public virtual Cursor GetCursor(PointF mouseLocation)
        {
            if ((this.CanEffectMargin | EffectMargin.LeftTop) == this.CanEffectMargin && this.LeftTopBounds.Contains(mouseLocation)) { return Cursors.SizeNWSE; }
            if ((this.CanEffectMargin | EffectMargin.Top) == this.CanEffectMargin && this.TopBounds.Contains(mouseLocation)) { return Cursors.SizeNS; }
            if ((this.CanEffectMargin | EffectMargin.RightTop) == this.CanEffectMargin && this.RightTopBounds.Contains(mouseLocation)) { return Cursors.SizeNESW; }
            if ((this.CanEffectMargin | EffectMargin.Right) == this.CanEffectMargin && this.RightBounds.Contains(mouseLocation)) { return Cursors.SizeWE; }
            if ((this.CanEffectMargin | EffectMargin.RightBottom) == this.CanEffectMargin && this.RightBottomBounds.Contains(mouseLocation)) { return Cursors.SizeNWSE; }
            if ((this.CanEffectMargin | EffectMargin.Bottom) == this.CanEffectMargin && this.BottomBounds.Contains(mouseLocation)) { return Cursors.SizeNS; }
            if ((this.CanEffectMargin | EffectMargin.LeftBottom) == this.CanEffectMargin && this.LeftBottomBounds.Contains(mouseLocation)) { return Cursors.SizeNESW; }
            if ((this.CanEffectMargin | EffectMargin.Left) == this.CanEffectMargin && this.LeftBounds.Contains(mouseLocation)) { return Cursors.SizeWE; }
            if (this.CanRotate)
            {
                if ((this.CanEffectMargin | EffectMargin.LeftBottomRotate) == this.CanEffectMargin && this.LeftBottomRotateBounds.Contains(mouseLocation)) { return DUICursors.Rotate; }
                if ((this.CanEffectMargin | EffectMargin.LeftTopRotate) == this.CanEffectMargin && this.LeftTopRotateBounds.Contains(mouseLocation)) { return DUICursors.Rotate; }
                if ((this.CanEffectMargin | EffectMargin.RightBottomRotate) == this.CanEffectMargin && this.RightBottomRotateBounds.Contains(mouseLocation)) { return DUICursors.Rotate; }
                if ((this.CanEffectMargin | EffectMargin.RightTopRotate) == this.CanEffectMargin && this.RightTopRotateBounds.Contains(mouseLocation)) { return DUICursors.Rotate; }
            }
            if (this.CanSkew)
            {
                if ((this.CanEffectMargin | EffectMargin.TopSkew1) == this.CanEffectMargin && this.TopSkewBounds1.Contains(mouseLocation)) { return DUICursors.Skew; }
                if ((this.CanEffectMargin | EffectMargin.LeftSkew1) == this.CanEffectMargin && this.LeftSkewBounds1.Contains(mouseLocation)) { return DUICursors.Skew; }
                if ((this.CanEffectMargin | EffectMargin.BottomSkew1) == this.CanEffectMargin && this.BottomSkewBounds1.Contains(mouseLocation)) { return DUICursors.Skew; }
                if ((this.CanEffectMargin | EffectMargin.RightSkew1) == this.CanEffectMargin && this.RightSkewBounds1.Contains(mouseLocation)) { return DUICursors.Skew; }
                if ((this.CanEffectMargin | EffectMargin.TopSkew2) == this.CanEffectMargin && this.TopSkewBounds2.Contains(mouseLocation)) { return DUICursors.Skew; }
                if ((this.CanEffectMargin | EffectMargin.LeftSkew2) == this.CanEffectMargin && this.LeftSkewBounds2.Contains(mouseLocation)) { return DUICursors.Skew; }
                if ((this.CanEffectMargin | EffectMargin.BottomSkew2) == this.CanEffectMargin && this.BottomSkewBounds2.Contains(mouseLocation)) { return DUICursors.Skew; }
                if ((this.CanEffectMargin | EffectMargin.RightSkew2) == this.CanEffectMargin && this.RightSkewBounds2.Contains(mouseLocation)) { return DUICursors.Skew; }
            }
            //if ((this.CanEffectMargin | EffectMargin.Center) == this.CanEffectMargin && this.CenterBounds.Contains(mouseLocation)) { return Cursors.Hand; }
            if (this.ClientRectangle.Contains(mouseLocation)) { return Cursors.SizeAll; }
            return Cursors.Default;
        }
        /// <summary> 获取鼠标缩放的区域
        /// </summary>
        /// <param name="mouseLocation"></param>
        /// <returns></returns>
        protected virtual EffectMargin GetEffectMargin(PointF mouseLocation)
        {
            if ((this.CanEffectMargin | EffectMargin.Bottom) == this.CanEffectMargin && BottomBounds.Contains(mouseLocation))
            {
                return EffectMargin.Bottom;
            }
            if ((this.CanEffectMargin | EffectMargin.Left) == this.CanEffectMargin && this.LeftBounds.Contains(mouseLocation))
            {
                return EffectMargin.Left;
            }
            if ((this.CanEffectMargin | EffectMargin.LeftBottom) == this.CanEffectMargin && this.LeftBottomBounds.Contains(mouseLocation))
            {
                return EffectMargin.LeftBottom;
            }
            if ((this.CanEffectMargin | EffectMargin.LeftTop) == this.CanEffectMargin && this.LeftTopBounds.Contains(mouseLocation))
            {
                return EffectMargin.LeftTop;
            }
            if ((this.CanEffectMargin | EffectMargin.Right) == this.CanEffectMargin && this.RightBounds.Contains(mouseLocation))
            {
                return EffectMargin.Right;
            }
            if ((this.CanEffectMargin | EffectMargin.RightBottom) == this.CanEffectMargin && this.RightBottomBounds.Contains(mouseLocation))
            {
                return EffectMargin.RightBottom;
            }
            if ((this.CanEffectMargin | EffectMargin.RightTop) == this.CanEffectMargin && this.RightTopBounds.Contains(mouseLocation))
            {
                return EffectMargin.RightTop;
            }
            if ((this.CanEffectMargin | EffectMargin.Top) == this.CanEffectMargin && this.TopBounds.Contains(mouseLocation))
            {
                return EffectMargin.Top;
            }
            if (this.CanRotate && (this.CanEffectMargin | EffectMargin.LeftBottomRotate) == this.CanEffectMargin && this.LeftBottomRotateBounds.Contains(mouseLocation))
            {
                return EffectMargin.LeftBottomRotate;
            }
            if (this.CanRotate && (this.CanEffectMargin | EffectMargin.LeftTopRotate) == this.CanEffectMargin && this.LeftTopRotateBounds.Contains(mouseLocation))
            {
                return EffectMargin.LeftTopRotate;
            }
            if (this.CanRotate && (this.CanEffectMargin | EffectMargin.RightBottomRotate) == this.CanEffectMargin && this.RightBottomRotateBounds.Contains(mouseLocation))
            {
                return EffectMargin.RightBottomRotate;
            }
            if (this.CanRotate && (this.CanEffectMargin | EffectMargin.RightTopRotate) == this.CanEffectMargin && this.RightTopRotateBounds.Contains(mouseLocation))
            {
                return EffectMargin.RightTopRotate;
            }
            if (this.CanSkew && (this.CanEffectMargin | EffectMargin.TopSkew1) == this.CanEffectMargin && this.TopSkewBounds1.Contains(mouseLocation))
            {
                return EffectMargin.TopSkew1;
            }
            if (this.CanSkew && (this.CanEffectMargin | EffectMargin.TopSkew2) == this.CanEffectMargin && this.TopSkewBounds2.Contains(mouseLocation))
            {
                return EffectMargin.TopSkew2;
            }
            if (this.CanSkew && (this.CanEffectMargin | EffectMargin.LeftSkew1) == this.CanEffectMargin && this.LeftSkewBounds1.Contains(mouseLocation))
            {
                return EffectMargin.LeftSkew1;
            }
            if (this.CanSkew && (this.CanEffectMargin | EffectMargin.LeftSkew2) == this.CanEffectMargin && this.LeftSkewBounds2.Contains(mouseLocation))
            {
                return EffectMargin.LeftSkew2;
            }
            if (this.CanSkew && (this.CanEffectMargin | EffectMargin.BottomSkew1) == this.CanEffectMargin && this.BottomSkewBounds1.Contains(mouseLocation))
            {
                return EffectMargin.BottomSkew1;
            }
            if (this.CanSkew && (this.CanEffectMargin | EffectMargin.BottomSkew2) == this.CanEffectMargin && this.BottomSkewBounds2.Contains(mouseLocation))
            {
                return EffectMargin.BottomSkew2;
            }
            if (this.CanSkew && (this.CanEffectMargin | EffectMargin.RightSkew1) == this.CanEffectMargin && this.RightSkewBounds1.Contains(mouseLocation))
            {
                return EffectMargin.RightSkew1;
            }
            if (this.CanSkew && (this.CanEffectMargin | EffectMargin.RightSkew2) == this.CanEffectMargin && this.RightSkewBounds2.Contains(mouseLocation))
            {
                return EffectMargin.RightSkew2;
            }
            if ((this.CanEffectMargin | EffectMargin.Other) == this.CanEffectMargin && this.OtherBounds.Contains(mouseLocation))
            {
                return EffectMargin.Other;
            }
            return EffectMargin.Unknow;
        }
        /// <summary> 绘制边框
        /// </summary>
        /// <param name="e"></param>
        protected virtual void DrawBounds(DUIPaintEventArgs e)
        {
            float boundsWidth = 1;
            using (DUIPen pen = new DUIPen(this.Focused ? Color.Blue : Color.Black, boundsWidth))
            {
                e.Graphics.DrawRectangle(pen, new RectangleF(this.BorderWidth - boundsWidth / 2, this.BorderWidth - boundsWidth / 2, this.ClientRectangle.Width + boundsWidth, this.ClientRectangle.Height + boundsWidth));
            }
        }
        /// <summary> 绘制顶点
        /// </summary>
        /// <param name="e"></param>
        protected virtual void DrawVertex(DUIPaintEventArgs e)
        {
            using (DUISolidBrush brushBounds = new DUISolidBrush(this.Focused ? Color.White : Color.FromArgb(100, Color.White)))
            using (DUISolidBrush brushContent = new DUISolidBrush(this.Focused ? Color.Black : Color.FromArgb(100, Color.Black)))
            {
                if ((this.CanEffectMargin | EffectMargin.LeftTop) == this.CanEffectMargin)
                {
                    var leftTopBounds = this.LeftTopBounds;
                    DrawVertex(leftTopBounds, brushBounds, brushContent, e.Graphics);
                }
                if ((this.CanEffectMargin | EffectMargin.Top) == this.CanEffectMargin)
                {
                    var topBounds = this.TopBounds;
                    DrawVertex(topBounds, brushBounds, brushContent, e.Graphics);
                }
                if ((this.CanEffectMargin | EffectMargin.RightTop) == this.CanEffectMargin)
                {
                    var rightTopBounds = this.RightTopBounds;
                    DrawVertex(rightTopBounds, brushBounds, brushContent, e.Graphics);
                }
                if ((this.CanEffectMargin | EffectMargin.Right) == this.CanEffectMargin)
                {
                    var rightBounds = this.RightBounds;
                    DrawVertex(rightBounds, brushBounds, brushContent, e.Graphics);
                }
                if ((this.CanEffectMargin | EffectMargin.RightBottom) == this.CanEffectMargin)
                {
                    var rightBottomBounds = this.RightBottomBounds;
                    DrawVertex(rightBottomBounds, brushBounds, brushContent, e.Graphics);
                }
                if ((this.CanEffectMargin | EffectMargin.Bottom) == this.CanEffectMargin)
                {
                    var bottomBounds = this.BottomBounds;
                    DrawVertex(bottomBounds, brushBounds, brushContent, e.Graphics);
                }
                if ((this.CanEffectMargin | EffectMargin.LeftBottom) == this.CanEffectMargin)
                {
                    var leftBottomBounds = this.LeftBottomBounds;
                    DrawVertex(leftBottomBounds, brushBounds, brushContent, e.Graphics);
                }
                if ((this.CanEffectMargin | EffectMargin.Left) == this.CanEffectMargin)
                {
                    var leftBounds = this.LeftBounds;
                    DrawVertex(leftBounds, brushBounds, brushContent, e.Graphics);
                }
            }
        }
        /// <summary> 变形的时候绘制顶点
        /// </summary>
        /// <param name="vertexBounds"></param>
        /// <param name="brushBounds"></param>
        /// <param name="brushContent"></param>
        /// <param name="g"></param>
        protected virtual void DrawVertex(RectangleF vertexBounds, DUIBrush brushBounds, DUIBrush brushContent, DUIGraphics g)
        {
            float width = this.VertexRadius / 5;
            PointF center = new PointF(this.BorderWidth + vertexBounds.X + vertexBounds.Width / 2, this.BorderWidth + vertexBounds.Y + vertexBounds.Height / 2);
            DUIMatrix matrix = new DUIMatrix();
            matrix.Translate(center);
            matrix *= this.MatrixReverse;
            matrix.TranslateReverse(center);
            g.Transform *= matrix;
            vertexBounds.Offset(this.BorderWidth, this.BorderWidth);
            g.FillRectangle(brushBounds, vertexBounds);
            vertexBounds.Offset(width, width);
            vertexBounds.Width -= width * 2;
            vertexBounds.Height -= width * 2;
            g.FillRectangle(brushContent, vertexBounds);
            matrix.Reset();
            matrix.Translate(center);
            matrix *= this.Matrix;
            matrix.TranslateReverse(center);
            g.Transform *= matrix;
        }
        #endregion
        private DUIMatrix GetAnyBoundsMatrix(DUIAnyBounds anyBounds)
        {
            DUIMatrix matrix = new DUIMatrix();
            matrix.Rotate(anyBounds.Rotate);
            matrix.Skew(anyBounds.Skew);
            matrix.Scale(anyBounds.Scale);
            return matrix;
        }
        private DUIMatrix GetAnyBoundsMatrixReverse(DUIAnyBounds anyBounds)
        {
            DUIMatrix matrix = new DUIMatrix();
            matrix.Scale(1 / anyBounds.ScaleX, 1 / anyBounds.ScaleY);
            matrix.Skew(-anyBounds.SkewX, -anyBounds.SkewY);
            float sx = (float)Math.Cos(Math.Atan(anyBounds.SkewY));
            float sy = (float)Math.Cos(Math.Atan(anyBounds.SkewX));
            //matrix.Scale(sx, sy);
            var s = (float)(-anyBounds.SkewX * anyBounds.SkewY) + 1;
            matrix.Scale(1 / s, 1 / s);
            matrix.Rotate(-anyBounds.Rotate);
            return matrix;
        }
    }
}
