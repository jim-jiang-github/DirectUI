using DirectUI.Common;
using DirectUI.Share;
/********************************************************************
* * 使本项目源码前请仔细阅读以下协议内容，如果你同意以下协议才能使用本项目所有的功能,
* * 否则如果你违反了以下协议，有可能陷入法律纠纷和赔偿，作者保留追究法律责任的权利。
* * Copyright (C) 
* * 作者： 煎饼的归宿    QQ：375324644   
* * 请保留以上版权信息，否则作者将保留追究法律责任。
* * 最后修改：BF-20150503UIWA
* * 创建时间：2018/1/2 16:50:31
********************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Core
{
    public class DUIPolygonControl : DUIControl
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
        internal protected PointF[] polygon = new PointF[4] { new PointF(0, 0), new PointF(100, 0), new PointF(100, 100), new PointF(0, 100) }; //初始多边形的顶点，默认为矩形
        #endregion
        #region 属性
        /// <summary> 多边形点的半径
        /// </summary>
        protected virtual float PolygonPointsRadius
        {
            get { return 4F; }
        }
        /// <summary> 多边形点数据
        /// </summary>
        public PointF[] Polygon
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
        private PointF[] PolygonBounds
        {
            get
            {
                return PolygonTools.Points2MaximumPolygon(this.Polygon);
            }
        }
        #endregion
        public DUIPolygonControl()
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
        internal override void DoMouseDown(DUIMouseEventArgs e)
        {
            if (!this.Visible || !this.Enabled) { return; }
            DUIControl dUIControl = this.GetChildAtPoint(e.Location);
            if (dUIControl == null)
            {
                #region 重写的部分，只有在多边形区域内的时候才判定为设置焦点
                if (PolygonContainPoint(e.Location) || this.GetPolygonPointIndex(e.Location) != -1)
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
        #endregion
        #region 函数
        /// <summary> 多边形是否包含点
        /// </summary>
        /// <param name="p">点</param>
        /// <returns>是否包含点</returns>
        protected bool PolygonContainPoint(PointF p)
        {
            p = new PointF(p.X + this.X + this.BorderWidth, p.Y + this.Y + this.BorderWidth);
            return PointTools.IsPointInPolygon(p, this.PolygonBounds);
        }
        /// <summary> 获取多边形点的序号
        /// </summary>
        /// <param name="point">点</param>
        /// <returns>点序号</returns>
        public int GetPolygonPointIndex(PointF point)
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
            using (DUIPen borderPen = new DUIPen(this.BorderColor, 1))
            {
                e.Graphics.DrawPolygon(borderPen, this.PolygonBounds);
            }
            using (DUIPen borderPen = new DUIPen(Color.Red, 1))
            {
                e.Graphics.DrawPolygon(borderPen, this.Polygon);
            }
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
            e.Graphics.SmoothingMode = DUISmoothingMode.Default;
            e.Graphics.TranslateTransform(this.X + this.BorderWidth, this.Y + this.BorderWidth);
        }
    }
}
