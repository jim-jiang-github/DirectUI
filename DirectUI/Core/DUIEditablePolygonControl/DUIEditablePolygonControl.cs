using DirectUI.Common;
using DirectUI.Share;
/********************************************************************
 * * 使本项目源码前请仔细阅读以下协议内容，如果你同意以下协议才能使用本项目所有的功能,
 * * 否则如果你违反了以下协议，有可能陷入法律纠纷和赔偿，作者保留追究法律责任的权利。
 * * Copyright (C) 
 * * 作者： 煎饼的归宿    QQ：375324644   
 * * 请保留以上版权信息，否则作者将保留追究法律责任。
 * * 最后修改：BF-20150503UIWA
 * * 创建时间：2018/1/3 10:15:02
********************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DirectUI.Core
{
    public delegate void DUIPolygonChangingEventHandler(object sender, DUIPolygonChangingEventArgs e);
    public delegate void DUIPolygonAnyChangingEventHandler(object sender, DUIPolygonAnyChangingEventArgs e);
    public class DUIEditablePolygonControl : DUIEditableScaleParentControl
    {
        #region 事件
        /// <summary> 在 Polygon 属性值更改时发生。
        /// </summary>
        public event DUIPolygonChangingEventHandler PolygonChanging;
        /// <summary> 在 Polygon 属性值更改时发生。
        /// </summary>
        public event EventHandler PolygonChanged;
        /// <summary> 在 Polygon 任意属性值更改时发生。
        /// </summary>
        public event DUIPolygonAnyChangingEventHandler PolygonAnyChanging;
        /// <summary> 在 Polygon 任意属性值更改时发生。
        /// </summary>
        public event EventHandler PolygonAnyChanged;
        public virtual void OnPolygonChanging(DUIPolygonChangingEventArgs e)
        {
            if (PolygonChanging != null)
            {
                PolygonChanging(this, e);
            }
        }
        public virtual void OnPolygonChanged(EventArgs e)
        {
            if (PolygonChanged != null)
            {
                PolygonChanged(this, e);
            }
        }
        public virtual void OnPolygonAnyChanging(DUIPolygonAnyChangingEventArgs e)
        {
            if (PolygonAnyChanging != null)
            {
                PolygonAnyChanging(this, e);
            }
        }
        public virtual void OnPolygonAnyChanged(EventArgs e)
        {
            if (PolygonAnyChanged != null)
            {
                PolygonAnyChanged(this, e);
            }
        }
        #endregion
        #region 变量
        /// <summary> 鼠标停留点的序号
        /// </summary>
        protected int hoverPointIndex = -1;
        private float? leftPolygonPointMargin = null;
        private float? topPolygonPointMargin = null;
        private float? rightPolygonPointMargin = null;
        private float? bottomPolygonPointMargin = null;
        private float appendPolygonPointY = 0;
        private float appendPolygonPointX = 0;
        private float limitPolygonPointY = 0;
        private float limitPolygonPointX = 0;
        internal protected PointF[] polygon = new PointF[4] { new PointF(0, 0), new PointF(100, 0), new PointF(100, 100), new PointF(0, 100) }; //初始多边形的顶点，默认为矩形
        private PointF[] mouseDownPolygon = new PointF[4] { new PointF(0, 0), new PointF(100, 0), new PointF(100, 100), new PointF(0, 100) }; //初始多边形的顶点，默认为矩形
        #endregion
        #region 属性
        /// <summary> 多边形点的半径
        /// </summary>
        protected virtual float PolygonPointsRadius
        {
            get { return 4F / ScaleableControlScaling; }
        }
        /// <summary> 多边形点数据
        /// </summary>
        public virtual PointF[] Polygon
        {
            get { return polygon; }
            set
            {
                this.SetBounds(
                    0
                    , 0
                    , 0
                    , 0
                    , 0
                    , 0
                    , 0
                    , value.ToArray()
                    , DUIBoundsPolygonSpecified.Polygon
                    );
            }
        }
        /// <summary> 多边形的最大外多边形边框
        /// </summary>
        public PointF[] PolygonBounds
        {
            get
            {
                PointF[] polyg1on = PolygonTools.Points2MaximumPolygon(this.Polygon);
                return PolygonTools.Points2MaximumPolygon(this.Polygon);
            }
        }
        /// <summary> 左边界
        /// </summary>
        public virtual float? LeftPolygonPointMargin
        {
            get { return leftPolygonPointMargin; }
            set { leftPolygonPointMargin = value; }
        }
        /// <summary> 上边界
        /// </summary>
        public virtual float? TopPolygonPointMargin
        {
            get { return topPolygonPointMargin; }
            set { topPolygonPointMargin = value; }
        }
        /// <summary> 右边界
        /// </summary>
        public virtual float? RightPolygonPointMargin
        {
            get { return rightPolygonPointMargin; }
            set { rightPolygonPointMargin = value; }
        }
        /// <summary> 下边界
        /// </summary>
        public virtual float? BottomPolygonPointMargin
        {
            get { return bottomPolygonPointMargin; }
            set { bottomPolygonPointMargin = value; }
        }
        private float LimitPolygonPointX
        {
            get
            {
                if (this.VerticalAdsorbs.Count > 0 && Control.ModifierKeys != Keys.Shift)
                {
                    var verticalAdsorb = this.VerticalAdsorbs.OrderBy(v => Math.Abs(limitPolygonPointX - appendPolygonPointX - v)).FirstOrDefault();
                    if (Math.Abs(limitPolygonPointX - appendPolygonPointX - verticalAdsorb) < this.AdsorbDistance)
                    {
                        return verticalAdsorb;
                    }
                }
                return limitPolygonPointX - appendPolygonPointX;
            }
            set
            {
                limitPolygonPointX = value;
                appendPolygonPointX = 0;
                if (LeftPolygonPointMargin != null && limitPolygonPointX < LeftPolygonPointMargin)
                {
                    appendPolygonPointX = limitPolygonPointX - (float)LeftPolygonPointMargin;
                }
                if (RightPolygonPointMargin != null && limitPolygonPointX > RightPolygonPointMargin)
                {
                    appendPolygonPointX = limitPolygonPointX - (float)RightPolygonPointMargin;
                }
            }
        }
        private float LimitPolygonPointY
        {
            get
            {
                if (this.HorizontalAdsorbs.Count > 0 && Control.ModifierKeys != Keys.Shift)
                {
                    var horizontalAdsorb = this.HorizontalAdsorbs.OrderBy(v => Math.Abs(limitPolygonPointY - appendPolygonPointY - v)).FirstOrDefault();
                    if (Math.Abs(limitPolygonPointY - appendPolygonPointY - horizontalAdsorb) < this.AdsorbDistance)
                    {
                        return horizontalAdsorb;
                    }
                }
                return limitPolygonPointY - appendPolygonPointY;
            }
            set
            {
                limitPolygonPointY = value;
                appendPolygonPointY = 0;
                if (TopPolygonPointMargin != null && limitPolygonPointY < TopPolygonPointMargin)
                {
                    appendPolygonPointY = limitPolygonPointY - (float)TopPolygonPointMargin;
                }
                if (BottomPolygonPointMargin != null && limitPolygonPointY > BottomPolygonPointMargin)
                {
                    appendPolygonPointY = limitPolygonPointY - (float)BottomPolygonPointMargin;
                }
            }
        }
        protected float GetLimitPolygonPointLeftOffset(float offset)
        {
            float lastPolygonPointLeft = this.LimitPolygonPointX;
            this.LimitPolygonPointX = this.limitPolygonPointX + offset;
            float offsetLeft = this.LimitPolygonPointX - lastPolygonPointLeft;
            if (offset != offsetLeft)
            {
                return offsetLeft;
            }
            return offset;
        }
        protected float GetLimitPolygonPointTopOffset(float offset)
        {
            float lastTop = this.LimitPolygonPointY;
            this.LimitPolygonPointY = this.limitPolygonPointY + offset;
            float offsetTop = this.LimitPolygonPointY - lastTop;
            if (offset != offsetTop)
            {
                return offsetTop;
            }
            return offset;
        }
        #endregion
        public DUIEditablePolygonControl()
        {
            this.BackColor = SystemColors.Control;
            this.BorderColor = Color.Black;
        }
        #region 重写
        public override float BorderWidth
        {
            get
            {
                return this.PolygonPointsRadius;
            }
        }
        public override EffectMargin CanEffectMargin
        {
            get
            {
                return EffectMargin.Other | EffectMargin.Point;
            }
        }
        protected override EffectMargin GetEffectMargin(PointF mouseLocation)
        {
            if (this.hoverPointIndex == -1)
            {
                if ((this.CanEffectMargin | EffectMargin.Other) == this.CanEffectMargin && this.ContainPoint(mouseLocation))
                {
                    return EffectMargin.Other;
                }
            }
            else
            {
                return EffectMargin.Point;
            }
            return EffectMargin.Unknow;
        }
        internal override void DoMouseDown(DUIMouseEventArgs e)
        {
            if (!this.Visible || !this.Enabled) { return; }
            DUIControl dUIControl = this.GetChildAtPoint(e.Location);
            if (dUIControl == null)
            {
                #region 重写的部分，只有在多边形区域内的时候才判定为设置焦点
                if (this.ContainPoint(e.Location) || this.GetPolygonPointIndex(e.Location) != -1)
                {
                    this.Focus();
                }
                else
                {
                    if (this.DUIParent != null)
                    {
                        this.DUIParent.Focus();
                    }
                }
                #endregion
                this.AlwaysMouseDown(this, e);
                this.InvokeOnMouseDown(this, e);
            }
            else
            {
                dUIControl.DoMouseDown(new DUIMouseEventArgs(e.Button, e.Clicks, dUIControl.PointFromParent(e.Location), e.Delta));
            }
        }
        protected override void DoCenterFollow(DUIAnyBounds anyBounds, PointF center)
        {
            DUIAnyBoundsPolygon anyBoundsPolygon = (DUIAnyBoundsPolygon)anyBounds;
            RectangleF rect = PolygonTools.GetPolygonRect(anyBoundsPolygon.Polygon);
            anyBounds.x = rect.X;
            anyBounds.y = rect.Y;
            anyBounds.width = rect.Width;
            anyBounds.height = rect.Height;
            float lastX = anyBounds.X;
            float lastY = anyBounds.Y;
            base.DoCenterFollow(anyBounds, center);
            anyBoundsPolygon.Polygon = anyBoundsPolygon.Polygon.Select(pl => new PointF(pl.X + anyBounds.X - lastX, pl.Y + anyBounds.Y - lastY)).ToArray();
        }
        protected internal override void SetCenterChanging(PointF center)
        {
            float lastX = this.x;
            float lastY = this.y;
            PointF p = this.ClientBounds.Location;
            DUIMatrix matrix = new DUIMatrix();
            matrix.Translate(this.ClientBounds.X + this.CenterX, this.ClientBounds.Y + this.CenterY);
            matrix.Rotate(this.Rotate);
            matrix.Skew(this.SkewX, this.SkewY);
            matrix.Scale(this.ScaleX, this.ScaleY);
            matrix.Translate(-(this.ClientBounds.X + this.CenterX), -(this.ClientBounds.Y + this.CenterY));
            p = MatrixTools.PointAfterMatrix(p, matrix);
            matrix.Reset();
            matrix.Translate(center.X, center.Y);
            matrix.Scale(1 / this.ScaleX, 1 / this.ScaleY);
            matrix.Skew(-this.SkewX, -this.SkewY);
            var s = (float)(Math.Tan(-this.SkewX) * Math.Tan(this.SkewY) + 1);
            matrix.Scale(1 / s, 1 / s);
            matrix.Rotate(-this.Rotate);
            matrix.Translate(-center.X, -center.Y);
            p = MatrixTools.PointAfterMatrix(p, matrix);
            float x = p.X - this.BorderWidth;
            float y = p.Y - this.BorderWidth;
            var polygon = this.Polygon.Select(pl => new PointF(pl.X + x - lastX, pl.Y + y - lastY)).ToArray();
            this.SetBoundsChanging(
                center.X - p.X
                , center.Y - p.Y
                , this.Rotate
                , 0
                , 0
                , 0
                , 0
                , polygon
                , DUIBoundsPolygonSpecified.Center
                | DUIBoundsPolygonSpecified.Polygon
                | DUIBoundsPolygonSpecified.RotateAngle);
        }
        protected internal override void SetCenterChanged(PointF center)
        {
            float lastX = this.x;
            float lastY = this.y;
            PointF p = this.ClientBounds.Location;
            DUIMatrix matrix = new DUIMatrix();
            matrix.Translate(this.ClientBounds.X + this.CenterX, this.ClientBounds.Y + this.CenterY);
            matrix.Rotate(this.Rotate);
            matrix.Skew(this.SkewX, this.SkewY);
            matrix.Scale(this.ScaleX, this.ScaleY);
            matrix.Translate(-(this.ClientBounds.X + this.CenterX), -(this.ClientBounds.Y + this.CenterY));
            p = MatrixTools.PointAfterMatrix(p, matrix);
            matrix.Reset();
            matrix.Translate(center.X, center.Y);
            matrix.Scale(1 / this.ScaleX, 1 / this.ScaleY);
            matrix.Skew(-this.SkewX, -this.SkewY);
            var s = (float)(Math.Tan(-this.SkewX) * Math.Tan(this.SkewY) + 1);
            matrix.Scale(1 / s, 1 / s);
            matrix.Rotate(-this.Rotate);
            matrix.Translate(-center.X, -center.Y);
            p = MatrixTools.PointAfterMatrix(p, matrix);
            float x = p.X - this.BorderWidth;
            float y = p.Y - this.BorderWidth;
            var polygon = this.Polygon.Select(pl => new PointF(pl.X + x - lastX, pl.Y + y - lastY)).ToArray();
            this.SetBoundsChanged(
                center.X - p.X
                , center.Y - p.Y
                , this.Rotate
                , 0
                , 0
                , 0
                , 0
                , polygon
                , DUIBoundsPolygonSpecified.Center
                | DUIBoundsPolygonSpecified.Polygon
                | DUIBoundsPolygonSpecified.RotateAngle);
        }
        public override Cursor GetCursor(PointF mouseLocation)
        {
            if (this.hoverPointIndex == -1)
            {
                if (this.ContainPoint(mouseLocation))
                {
                    return Cursors.SizeAll;
                }
            }
            else
            {
                return Cursors.Hand;
            }
            return Cursors.Default;
        }
        public override void DoOffsetPoint(float offsetX, float offsetY, DUIAnyBounds anyBounds)
        {
            offsetX = GetLimitPolygonPointLeftOffset(offsetX);
            offsetY = GetLimitPolygonPointTopOffset(offsetY);
            DUIAnyBoundsPolygon anyBoundsPolygon = (DUIAnyBoundsPolygon)anyBounds;
            RectangleF lastPolygonBounds = PolygonTools.GetPolygonRect(this.Polygon);
            PointF[] polygon = this.Polygon.Select(p => new PointF(p.X, p.Y)).ToArray();
            polygon[this.hoverPointIndex].X += offsetX;
            polygon[this.hoverPointIndex].Y += offsetY;
            RectangleF polygonBounds = PolygonTools.GetPolygonRect(polygon);
            DUIMatrix m = new DUIMatrix();
            m.Rotate(this.Rotate);
            PointF offset = MatrixTools.PointAfterMatrix(new PointF(polygonBounds.X - lastPolygonBounds.X, polygonBounds.Y - lastPolygonBounds.Y), m);
            anyBoundsPolygon.Polygon = polygon.Select(pl => new PointF(pl.X + offset.X - (polygonBounds.X - lastPolygonBounds.X), pl.Y + offset.Y - (polygonBounds.Y - lastPolygonBounds.Y))).ToArray();
        }
        public override void DoOffsetOther(float offsetX, float offsetY, DUIAnyBounds anyBounds)
        {
            DUIAnyBoundsPolygon anyBoundsPolygon = (DUIAnyBoundsPolygon)anyBounds;
            DUIMatrix m = new DUIMatrix();
            m.Rotate(this.Rotate);
            PointF p = MatrixTools.PointAfterMatrix(new PointF(offsetX, offsetY), m);
            anyBoundsPolygon.Polygon = this.Polygon.Select(pl => new PointF(pl.X + p.X, pl.Y + p.Y)).ToArray();
            RectangleF bounds = PolygonTools.GetPolygonRect(anyBoundsPolygon.Polygon);
        }
        public override void OnMouseDown(DUIMouseEventArgs e)
        {
            this.mouseDownPolygon = this.polygon;
            if (hoverPointIndex != -1)
            {
                this.LimitPolygonPointY = this.polygon[this.hoverPointIndex].Y;
                this.LimitPolygonPointX = this.polygon[this.hoverPointIndex].X;
            }
            base.OnMouseDown(e);
        }
        public override void OnMouseMove(DUIMouseEventArgs e)
        {
            PointF location = e.Location;
            if (e.Button == MouseButtons.None)
            {
                int newHoverPointIndex = this.GetPolygonPointIndex(e.Location);
                if (this.hoverPointIndex != newHoverPointIndex)
                {
                    this.hoverPointIndex = newHoverPointIndex;
                    this.Invalidate();
                }
            }
            base.OnMouseMove(e);
        }
        public override void OnMouseUp(DUIMouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (!this.ContainPoint(e.Location))
            {
                this.Cursor = Cursors.Default;
            }
        }
        public override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.hoverPointIndex = -1;
            this.Invalidate();
        }
        protected override void SetAnyBoundsChanging(DUIAnyBounds anyBounds)
        {
            DUIAnyBoundsPolygon anyBoundsPolygon = (DUIAnyBoundsPolygon)anyBounds;
            this.SetBoundsChanging(
                 anyBoundsPolygon.centerX
                , anyBoundsPolygon.centerY
                , anyBoundsPolygon.rotate
                , anyBoundsPolygon.skewX
                , anyBoundsPolygon.skewY
                , anyBoundsPolygon.scaleX
                , anyBoundsPolygon.scaleY
                , anyBoundsPolygon.Polygon
                , DUIBoundsPolygonSpecified.RotateAngle
                | DUIBoundsPolygonSpecified.Center
                | DUIBoundsPolygonSpecified.Skew
                | DUIBoundsPolygonSpecified.Scale
                | DUIBoundsPolygonSpecified.Polygon
                );
        }
        protected override void SetAnyBoundsChanged()
        {
            this.SetBoundsChanged(
                this.centerX
                 , this.centerY
                 , this.rotate
                 , this.skewX
                 , this.skewY
                 , this.scaleX
                 , this.scaleY
                 , this.polygon
                 , DUIBoundsPolygonSpecified.RotateAngle
                 | DUIBoundsPolygonSpecified.Center
                 | DUIBoundsPolygonSpecified.Skew
                 | DUIBoundsPolygonSpecified.Scale
                 | DUIBoundsPolygonSpecified.Polygon
                 );
        }
        protected internal override bool ContainPoint(PointF p)
        {
            p = new PointF(p.X + this.X + this.BorderWidth, p.Y + this.Y + this.BorderWidth);
            return PointTools.IsPointInPolygon(p, this.Polygon) || this.Polygon.Any(py => PointTools.Distance(py, p) < PolygonPointsRadius);
        }
        #endregion
        #region 函数
        /// <summary> 获取多边形点的序号
        /// </summary>
        /// <param name="point">点</param>
        /// <returns>点序号</returns>
        public virtual int GetPolygonPointIndex(PointF point)
        {
            point = new PointF(point.X + this.X + this.BorderWidth, point.Y + this.Y + this.BorderWidth);
            var select = this.Polygon.Select((p, i) => new { Index = i, Point = p }).Where(p => PointTools.Distance(new PointF(p.Point.X, p.Point.Y), point) <= PolygonPointsRadius).OrderBy(p => PointTools.Distance(new PointF(p.Point.X, p.Point.Y), point)).FirstOrDefault();
            if (select == null)
            {
                return -1;
            }
            else
            {
                return select.Index;
            }
        }
        public virtual void SetBounds(float centerX, float centerY, float rotateAngle, float skewX, float skewY, float scaleX, float scaleY, PointF[] polygon, DUIBoundsPolygonSpecified specified, bool invalidate = true)
        {
            var polygonRect = PolygonTools.GetPolygonRect(polygon);
            float x = polygonRect.X;
            float y = polygonRect.Y;
            float width = polygonRect.Width;
            float height = polygonRect.Height;
            if ((specified & DUIBoundsPolygonSpecified.CenterX) == DUIBoundsPolygonSpecified.None) centerX = this.centerX;
            if ((specified & DUIBoundsPolygonSpecified.CenterY) == DUIBoundsPolygonSpecified.None) centerY = this.centerY;
            if ((specified & DUIBoundsPolygonSpecified.RotateAngle) == DUIBoundsPolygonSpecified.None) rotateAngle = this.rotate;
            if ((specified & DUIBoundsPolygonSpecified.SkewX) == DUIBoundsPolygonSpecified.None) skewX = this.skewX;
            if ((specified & DUIBoundsPolygonSpecified.SkewY) == DUIBoundsPolygonSpecified.None) skewY = this.skewY;
            if ((specified & DUIBoundsPolygonSpecified.ScaleX) == DUIBoundsPolygonSpecified.None) scaleX = this.scaleX;
            if ((specified & DUIBoundsPolygonSpecified.ScaleY) == DUIBoundsPolygonSpecified.None) scaleY = this.scaleY;
            if ((specified & DUIBoundsPolygonSpecified.Polygon) == DUIBoundsPolygonSpecified.None) polygon = this.polygon;
            if (this.x != x ||
                this.y != y ||
                this.width != width ||
                this.height != height ||
                this.centerX != centerX ||
                this.centerY != centerY ||
                this.rotate != rotateAngle ||
                this.skewX != skewX ||
                this.skewY != skewY ||
                this.scaleX != scaleX ||
                this.scaleY != scaleY ||
                !PolygonTools.PolygonEquels(this.polygon, polygon))
            {
                UpdateBounds(x, y, width, height, centerX, centerY, rotateAngle, skewX, skewY, scaleX, scaleY, polygon, invalidate);
            }
        }
        protected virtual void UpdateBounds(float x, float y, float width, float height, float centerX, float centerY, float rotateAngle, float skewX, float skewY, float scaleX, float scaleY, PointF[] polygon, bool invalidate = true)
        {
            RectangleF lastBounds = new RectangleF(this.x, this.y, this.width, this.height);
            PointF lastCenter = new PointF(this.centerX, this.centerY);
            PointF lastSkew = new PointF(this.skewX, this.skewY);
            PointF lastScale = new PointF(this.scaleX, this.scaleY);
            float lastRotateAngle = this.rotate;
            PointF[] lastPolygon = this.polygon;
            bool newLocation = this.x != x || this.y != y;
            bool newSize = this.width != width || this.height != height;
            bool newCenter = this.centerX != centerX || this.centerY != centerY;
            bool newSkew = this.skewX != skewX || this.skewY != skewY;
            bool newScale = this.scaleX != scaleX || this.scaleY != scaleY;
            bool newRotateAngle = this.rotate != rotateAngle;
            bool newPolygon = !PolygonTools.PolygonEquels(this.polygon, polygon);
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.centerX = centerX;
            this.centerY = centerY;
            this.skewX = skewX;
            this.skewY = skewY;
            this.scaleX = scaleX;
            this.scaleY = scaleY;
            this.rotate = rotateAngle;
            this.polygon = polygon.ToArray();
            if ((newLocation || newSize || newCenter || newRotateAngle || newSkew || newScale || newPolygon) && invalidate)
            {
                this.Invalidate();
            }
            if (newLocation)
            {
                OnLocationChanging(new DUILocationChangingEventArgs(new PointF(this.x, this.y), lastBounds.Location));
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlLocationChanging(new DUIControlLocationChangingEventArgs(this, new PointF(this.x, this.y), lastBounds.Location));
                }
                LayoutEngine.DoLayoutDock(new DUILayoutEventArgs(this.DUIParent, "OnResizing"));
            }
            if (newSize)
            {
                OnResizing(new DUISizeChangingEventArgs(new SizeF(this.width, this.height), lastBounds.Size));
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlSizeChanging(new DUIControlSizeChangingEventArgs(this, new SizeF(this.width, this.height), lastBounds.Size));
                }
                LayoutEngine.DoLayoutDock(new DUILayoutEventArgs(this.DUIParent, "OnResizing"));
                LayoutEngine.DoLayoutDock(new DUILayoutEventArgs(this, "OnResizing"), lastBounds.Size);
            }
            if (newLocation || newSize)
            {
                OnBoundsChanging(new DUIBoundsChangingEventArgs(new RectangleF(this.x, this.y, this.width, this.height), lastBounds));
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlBoundsChanging(new DUIControlBoundsChangingEventArgs(this, new RectangleF(this.x, this.y, this.width, this.height), lastBounds));
                }
            }
            if (newCenter)
            {
                OnCenterChanging(new DUICenterChangingEventArgs(new PointF(this.centerX, this.centerY), lastCenter));
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlCenterChanging(new DUIControlCenterChangingEventArgs(this, new PointF(this.centerX, this.centerY), lastCenter));
                }
            }
            if (newSkew)
            {
                OnSkewChanging(new DUISkewChangingEventArgs(new PointF(this.skewX, this.skewY), lastSkew));
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlSkewChanging(new DUIControlSkewChangingEventArgs(this, new PointF(this.skewX, this.skewY), lastSkew));
                }
            }
            if (newScale)
            {
                OnScaleChanging(new DUIScaleChangingEventArgs(new PointF(this.scaleX, this.scaleY), lastScale));
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlScaleChanging(new DUIControlScaleChangingEventArgs(this, new PointF(this.scaleX, this.scaleY), lastScale));
                }
            }
            if (newRotateAngle)
            {
                OnRotateAngleChanging(new DUIRotateAngleChangingEventArgs(this.rotate, lastRotateAngle));
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlRotateAngleChanging(new DUIControlRotateAngleChangingEventArgs(this, this.rotate, lastRotateAngle));
                }
            }
            if (newPolygon)
            {
                OnPolygonChanging(new DUIPolygonChangingEventArgs(this.polygon, lastPolygon));
            }
            if ((newLocation || newSize || newCenter || newRotateAngle || newSkew || newPolygon) && invalidate)
            {
                OnPolygonAnyChanging(new DUIPolygonAnyChangingEventArgs(new RectangleF(this.x, this.y, this.width, this.height), lastBounds, new PointF(this.centerX, this.centerY), lastCenter, new PointF(this.skewX, this.skewY), lastSkew, new PointF(this.scaleX, this.scaleY), lastScale, this.rotate, lastRotateAngle, this.polygon, lastPolygon));
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlAnyChanging(new DUIControlAnyChangingEventArgs(this, new RectangleF(this.x, this.y, this.width, this.height), lastBounds, new PointF(this.centerX, this.centerY), lastCenter, new PointF(this.skewX, this.skewY), lastSkew, new PointF(this.scaleX, this.scaleY), lastScale, this.rotate, lastRotateAngle));
                }
            }
            if (newLocation)
            {
                OnLocationChanged(EventArgs.Empty);
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlLocationChanged(new DUIControlEventArgs(this));
                }
            }
            if (newSize)
            {
                OnResize(EventArgs.Empty);
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlSizeChanged(new DUIControlEventArgs(this));
                }
            }
            if (newLocation || newSize)
            {
                OnBoundsChanged(EventArgs.Empty);
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlBoundsChanged(new DUIControlEventArgs(this));
                }
            }
            if (newCenter)
            {
                OnCenterChanged(EventArgs.Empty);
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlCenterChanged(new DUIControlEventArgs(this));
                }
            }
            if (newSkew)
            {
                OnSkewChanged(EventArgs.Empty);
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlSkewChanged(new DUIControlEventArgs(this));
                }
            }
            if (newScale)
            {
                OnScaleChanged(EventArgs.Empty);
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlScaleChanged(new DUIControlEventArgs(this));
                }
            }
            if (newRotateAngle)
            {
                OnRotateAngleChanged(EventArgs.Empty);
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlRotateAngleChanged(new DUIControlEventArgs(this));
                }
            }
            if (newPolygon)
            {
                OnPolygonChanged(EventArgs.Empty);
            }
            if ((newLocation || newSize || newCenter || newSkew || newScale || newRotateAngle || newPolygon) && invalidate)
            {
                OnPolygonAnyChanged(EventArgs.Empty);
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlAnyChanged(new DUIControlEventArgs(this));
                }
            }
        }
        protected internal virtual void SetBoundsChanging(float centerX, float centerY, float rotateAngle, float skewX, float skewY, float scaleX, float scaleY, PointF[] polygon, DUIBoundsPolygonSpecified specified)
        {
            var polygonRect = PolygonTools.GetPolygonRect(polygon);
            float x = polygonRect.X;
            float y = polygonRect.Y;
            float width = polygonRect.Width;
            float height = polygonRect.Height;
            if ((specified & DUIBoundsPolygonSpecified.CenterX) == DUIBoundsPolygonSpecified.None) centerX = this.centerX;
            if ((specified & DUIBoundsPolygonSpecified.CenterY) == DUIBoundsPolygonSpecified.None) centerY = this.centerY;
            if ((specified & DUIBoundsPolygonSpecified.RotateAngle) == DUIBoundsPolygonSpecified.None) rotateAngle = this.rotate;
            if ((specified & DUIBoundsPolygonSpecified.SkewX) == DUIBoundsPolygonSpecified.None) skewX = this.skewX;
            if ((specified & DUIBoundsPolygonSpecified.SkewY) == DUIBoundsPolygonSpecified.None) skewY = this.skewY;
            if ((specified & DUIBoundsPolygonSpecified.ScaleX) == DUIBoundsPolygonSpecified.None) scaleX = this.scaleX;
            if ((specified & DUIBoundsPolygonSpecified.ScaleY) == DUIBoundsPolygonSpecified.None) scaleY = this.scaleY;
            if ((specified & DUIBoundsPolygonSpecified.Polygon) == DUIBoundsPolygonSpecified.None) polygon = this.polygon;
            if (this.x != x ||
                this.y != y ||
                this.width != width ||
                this.height != height ||
                this.centerX != centerX ||
                this.centerY != centerY ||
                this.rotate != rotateAngle ||
                this.skewX != skewX ||
                this.skewY != skewY ||
                this.scaleX != scaleX ||
                this.scaleY != scaleY ||
                !PolygonTools.PolygonEquels(this.polygon, polygon))
            {
                UpdateBoundsChanging(x, y, width, height, centerX, centerY, rotateAngle, skewX, skewY, scaleX, scaleY, polygon);
            }
        }
        protected internal virtual void UpdateBoundsChanging(float x, float y, float width, float height, float centerX, float centerY, float rotateAngle, float skewX, float skewY, float scaleX, float scaleY, PointF[] polygon)
        {
            RectangleF lastBounds = new RectangleF(this.x, this.y, this.width, this.height);
            PointF lastCenter = new PointF(this.centerX, this.centerY);
            PointF lastSkew = new PointF(this.skewX, this.skewY);
            PointF lastScale = new PointF(this.scaleX, this.scaleY);
            float lastRotateAngle = this.rotate;
            PointF[] lastPolygon = this.polygon;
            bool newLocation = this.x != x || this.y != y;
            bool newSize = this.width != width || this.height != height;
            bool newCenter = this.centerX != centerX || this.centerY != centerY;
            bool newSkew = this.skewX != skewX || this.skewY != skewY;
            bool newScale = this.scaleX != scaleX || this.scaleY != scaleY;
            bool newRotateAngle = this.rotate != rotateAngle;
            bool newPolygon = !PolygonTools.PolygonEquels(this.polygon, polygon);
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.centerX = centerX;
            this.centerY = centerY;
            this.rotate = rotateAngle;
            this.skewX = skewX;
            this.skewY = skewY;
            this.scaleX = scaleX;
            this.scaleY = scaleY;
            this.polygon = polygon.ToArray();
            if (newLocation || newSize || newCenter || newSkew || newScale || newRotateAngle || newPolygon)
            {
                this.Invalidate();
            }
            if (newLocation)
            {
                OnLocationChanging(new DUILocationChangingEventArgs(new PointF(this.x, this.y), lastBounds.Location));
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlLocationChanging(new DUIControlLocationChangingEventArgs(this, new PointF(this.x, this.y), lastBounds.Location));
                }
                LayoutEngine.DoLayoutDock(new DUILayoutEventArgs(this.DUIParent, "OnResizing"));
            }
            if (newSize)
            {
                OnResizing(new DUISizeChangingEventArgs(new SizeF(this.width, this.height), lastBounds.Size));
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlSizeChanging(new DUIControlSizeChangingEventArgs(this, new SizeF(this.width, this.height), lastBounds.Size));
                }
                LayoutEngine.DoLayoutDock(new DUILayoutEventArgs(this.DUIParent, "OnResizing"));
                LayoutEngine.DoLayoutDock(new DUILayoutEventArgs(this, "OnResizing"), lastBounds.Size);
            }
            if (newLocation || newSize)
            {
                OnBoundsChanging(new DUIBoundsChangingEventArgs(new RectangleF(this.x, this.y, this.width, this.height), lastBounds));
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlBoundsChanging(new DUIControlBoundsChangingEventArgs(this, new RectangleF(this.x, this.y, this.width, this.height), lastBounds));
                }
            }
            if (newCenter)
            {
                OnCenterChanging(new DUICenterChangingEventArgs(new PointF(this.centerX, this.centerY), lastCenter));
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlCenterChanging(new DUIControlCenterChangingEventArgs(this, new PointF(this.centerX, this.centerY), lastCenter));
                }
            }
            if (newSkew)
            {
                OnSkewChanging(new DUISkewChangingEventArgs(new PointF(this.skewX, this.skewY), lastSkew));
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlSkewChanging(new DUIControlSkewChangingEventArgs(this, new PointF(this.skewX, this.skewY), lastSkew));
                }
            }
            if (newScale)
            {
                OnScaleChanging(new DUIScaleChangingEventArgs(new PointF(this.scaleX, this.scaleY), lastScale));
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlScaleChanging(new DUIControlScaleChangingEventArgs(this, new PointF(this.scaleX, this.scaleY), lastSkew));
                }
            }
            if (newRotateAngle)
            {
                OnRotateAngleChanging(new DUIRotateAngleChangingEventArgs(this.rotate, lastRotateAngle));
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlRotateAngleChanging(new DUIControlRotateAngleChangingEventArgs(this, this.rotate, lastRotateAngle));
                }
            }
            if (newPolygon)
            {
                OnPolygonChanging(new DUIPolygonChangingEventArgs(this.polygon, lastPolygon));
            }
            if (newLocation || newSize || newCenter || newRotateAngle || newSkew || newScale || newPolygon)
            {
                OnPolygonAnyChanging(new DUIPolygonAnyChangingEventArgs(new RectangleF(this.x, this.y, this.width, this.height), lastBounds, new PointF(this.centerX, this.centerY), lastCenter, new PointF(this.skewX, this.skewY), lastSkew, new PointF(this.scaleX, this.scaleY), lastScale, this.rotate, lastRotateAngle, this.polygon, lastPolygon));
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlAnyChanging(new DUIControlAnyChangingEventArgs(this, new RectangleF(this.x, this.y, this.width, this.height), lastBounds, new PointF(this.centerX, this.centerY), lastCenter, new PointF(this.skewX, this.skewY), lastSkew, new PointF(this.scaleX, this.scaleY), lastScale, this.rotate, lastRotateAngle));
                }
            }
        }
        protected internal virtual void SetBoundsChanged(float centerX, float centerY, float rotateAngle, float skewX, float skewY, float scaleX, float scaleY, PointF[] polygon, DUIBoundsPolygonSpecified specified)
        {
            var polygonRect = PolygonTools.GetPolygonRect(polygon);
            float x = polygonRect.X;
            float y = polygonRect.Y;
            float width = polygonRect.Width;
            float height = polygonRect.Height;
            if ((specified & DUIBoundsPolygonSpecified.CenterX) == DUIBoundsPolygonSpecified.None) centerX = this.mouseDownCenter.X;
            if ((specified & DUIBoundsPolygonSpecified.CenterY) == DUIBoundsPolygonSpecified.None) centerY = this.mouseDownCenter.Y;
            if ((specified & DUIBoundsPolygonSpecified.RotateAngle) == DUIBoundsPolygonSpecified.None) rotateAngle = this.mouseDownRotateAngle;
            if ((specified & DUIBoundsPolygonSpecified.SkewX) == DUIBoundsPolygonSpecified.None) skewX = this.mouseDownSkew.X;
            if ((specified & DUIBoundsPolygonSpecified.SkewY) == DUIBoundsPolygonSpecified.None) skewY = this.mouseDownSkew.Y;
            if ((specified & DUIBoundsPolygonSpecified.ScaleX) == DUIBoundsPolygonSpecified.None) scaleX = this.mouseDownScale.X;
            if ((specified & DUIBoundsPolygonSpecified.ScaleY) == DUIBoundsPolygonSpecified.None) scaleY = this.mouseDownScale.Y;
            if ((specified & DUIBoundsPolygonSpecified.Polygon) == DUIBoundsPolygonSpecified.None) polygon = this.mouseDownPolygon;
            if (this.mouseDownBounds.X != x ||
                this.mouseDownBounds.Y != y ||
                this.mouseDownBounds.Width != width ||
                this.mouseDownBounds.Height != height ||
                this.mouseDownCenter.X != centerX ||
                this.mouseDownCenter.Y != centerY ||
                this.mouseDownRotateAngle != rotateAngle ||
                this.mouseDownSkew.X != skewX ||
                this.mouseDownSkew.Y != skewY ||
                this.mouseDownScale.X != scaleX ||
                this.mouseDownScale.Y != scaleY ||
                !PolygonTools.PolygonEquels(this.mouseDownPolygon, polygon))
            {
                UpdateBoundsChanged(x, y, width, height, centerX, centerY, rotateAngle, skewX, skewY, scaleX, scaleY, polygon);
            }
        }
        protected internal virtual void UpdateBoundsChanged(float x, float y, float width, float height, float centerX, float centerY, float rotateAngle, float skewX, float skewY, float scaleX, float scaleY, PointF[] polygon)
        {
            bool newLocation = this.mouseDownBounds.X != x || this.mouseDownBounds.Y != y;
            bool newSize = this.mouseDownBounds.Width != width || this.mouseDownBounds.Height != height;
            bool newCenter = this.mouseDownCenter.X != centerX || this.mouseDownCenter.Y != centerY;
            bool newSkew = this.mouseDownSkew.X != skewX || this.mouseDownSkew.Y != skewY;
            bool newScale = this.mouseDownScale.X != scaleX || this.mouseDownScale.Y != scaleY;
            bool newRotateAngle = this.mouseDownRotateAngle != rotateAngle;
            bool newPolygon = !PolygonTools.PolygonEquels(this.mouseDownPolygon, polygon);
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.centerX = centerX;
            this.centerY = centerY;
            this.rotate = rotateAngle;
            this.skewX = skewX;
            this.skewY = skewY;
            this.scaleX = scaleX;
            this.scaleY = scaleY;
            this.polygon = polygon.ToArray();
            if (newLocation || newSize || newCenter || newSkew || newScale || newRotateAngle || newPolygon)
            {
                this.Invalidate();
            }
            if (newLocation)
            {
                OnLocationChanged(EventArgs.Empty);
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlLocationChanged(new DUIControlEventArgs(this));
                }
            }
            if (newSize)
            {
                OnResize(EventArgs.Empty);
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlSizeChanged(new DUIControlEventArgs(this));
                }
            }
            if (newLocation || newSize)
            {
                OnBoundsChanged(EventArgs.Empty);
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlBoundsChanged(new DUIControlEventArgs(this));
                }
            }
            if (newCenter)
            {
                OnCenterChanged(EventArgs.Empty);
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlCenterChanged(new DUIControlEventArgs(this));
                }
            }
            if (newSkew)
            {
                OnSkewChanged(EventArgs.Empty);
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlSkewChanged(new DUIControlEventArgs(this));
                }
            }
            if (newScale)
            {
                OnScaleChanged(EventArgs.Empty);
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlScaleChanged(new DUIControlEventArgs(this));
                }
            }
            if (newRotateAngle)
            {
                OnRotateAngleChanged(EventArgs.Empty);
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlRotateAngleChanged(new DUIControlEventArgs(this));
                }
            }
            if (newPolygon)
            {
                OnPolygonChanged(EventArgs.Empty);
            }
            if (newLocation || newSize || newCenter || newSkew || newScale || newRotateAngle || newPolygon)
            {
                OnPolygonAnyChanged(EventArgs.Empty);
                if (this.DUIParent != null)
                {
                    this.DUIParent.OnControlAnyChanged(new DUIControlEventArgs(this));
                }
            }
        }
        #endregion
        public override void OnPaintBackground(DUIPaintEventArgs e)
        {
            //e.Graphics.FillRectangle(DUIBrushes.LightGreen, e.ClipRectangle);
            //e.Graphics.FillRectangle(DUIBrushes.White, new RectangleF(this.BorderWidth, this.BorderWidth, this.ClientSize.Width, this.ClientSize.Height));
            e.Graphics.TranslateTransform(-this.X, -this.Y);
            e.Graphics.SmoothingMode = DUISmoothingMode.AntiAlias;
            using (DUISolidBrush backBrush = new DUISolidBrush(this.BackColor))
            {
                e.Graphics.FillPolygon(backBrush, this.PolygonBounds);
            }
            using (DUIPen borderPen = new DUIPen(this.Border.BorderColor, 1 / ScaleableControlScaling))
            {
                e.Graphics.DrawPolygon(borderPen, this.PolygonBounds);
            }
            //using (DUIPen borderPen = new DUIPen(Color.Red, 1 / ScaleableControlScaling))
            //{
            //    e.Graphics.DrawPolygon(borderPen, this.Polygon);
            //}
            e.Graphics.SmoothingMode = DUISmoothingMode.Default;
            e.Graphics.TranslateTransform(this.X, this.Y);
            //if (PaintBackground != null)
            //{
            //    PaintBackground(this, e);
            //}
        }
        public override void OnPaintForeground(DUIPaintEventArgs e)
        {
            base.OnPaintForeground(e);
            e.Graphics.TranslateTransform(-this.X - this.BorderWidth, -this.Y - this.BorderWidth);
            e.Graphics.SmoothingMode = DUISmoothingMode.AntiAlias;
            using (DUISolidBrush sb = new DUISolidBrush(Color.FromArgb(255, Color.Blue)))
            {
                foreach (PointF p in Polygon)
                {
                    e.Graphics.FillEllipse(sb, new RectangleF(p.X, p.Y, this.PolygonPointsRadius * 2, this.PolygonPointsRadius * 2));
                    //e.Graphics.DrawString(p.ToString(), this.Font, DUIBrushes.Black, p);
                }
            }
            if (hoverPointIndex != -1)
            {
                using (DUISolidBrush sb = new DUISolidBrush(Color.FromArgb(255, Color.Red)))
                {
                    if (hoverPointIndex <= this.Polygon.Length - 1)
                    {
                        PointF p = this.Polygon[hoverPointIndex];
                        e.Graphics.FillEllipse(sb, new RectangleF(p.X, p.Y, this.PolygonPointsRadius * 2, this.PolygonPointsRadius * 2));
                    }
                }
            }
            e.Graphics.SmoothingMode = DUISmoothingMode.Default;
            e.Graphics.TranslateTransform(this.X + this.BorderWidth, this.Y + this.BorderWidth);
        }
        protected override DUIAnyBounds GetAnyBounds()
        {
            return new DUIAnyBoundsPolygon()
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
                borderWidth = this.borderWidth,
                ScaleableControl = this.ScaleableControl,
                Polygon = this.Polygon
            };
        }
    }
}
