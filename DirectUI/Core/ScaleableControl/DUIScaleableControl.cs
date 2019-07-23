using DirectUI.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DirectUI.Core
{
    /// <summary> 可缩放的容器对象
    /// </summary>
    public class DUIScaleableControl : DUIControl
    {
        #region 事件
        public event EventHandler ScalingControl;
        protected virtual void OnScalingControl()
        {
            if (ScalingControl != null)
            {
                ScalingControl(this, EventArgs.Empty);
            }
        }
        #endregion
        #region 私有变量
        protected bool isArrowMouseDragCanvasMouseDown = false;
        /// <summary> 缩放比例
        /// </summary>
        private float scaling = 1.0F;
        /// <summary> 缩放精度
        /// </summary>
        private float scalePrecision = 0.1F;
        /// <summary> 最小缩放比例
        /// </summary>
        private float minScale = 0.1F;
        /// <summary> 最大缩放比例
        /// </summary>
        private float maxScale = 20F;
        /// <summary> 是否允许鼠标拖动画布
        /// </summary>
        private bool arrowMouseDragCanvas = true;
        /// <summary> 是否允许缩放画布
        /// </summary>
        private bool arrowScaleCanvas = true;
        private PointF lastMousePoint = PointF.Empty; //记录鼠标最后一个经过的点
        #endregion
        /// <summary> 缩放比例
        /// </summary>
        public float Scaling
        {
            get { return scaling; }
            set
            {
                scaling = value;
                if (scaling > maxScale)
                {
                    scaling = maxScale;
                }
                if (scaling < minScale)
                {
                    scaling = minScale;
                }
                OnScalingControl();
                this.Invalidate();
            }
        }
        /// <summary> 是否允许鼠标拖动画布
        /// </summary>
        public virtual bool ArrowMouseDragCanvas
        {
            get { return arrowMouseDragCanvas; }
            set { arrowMouseDragCanvas = value; }
        }
        /// <summary> 是否允许缩放画布
        /// </summary>
        public virtual bool ArrowScaleCanvas
        {
            get { return arrowScaleCanvas; }
            set { arrowScaleCanvas = value; }
        }
        public DUIScaleableControl()
        {
            this.Center = this.Location; //初始化 坐标
        }
        #region 重写
        public RectangleF ScaleableBounds
        {
            get
            {
                float x = (-this.Center.X / this.scaling);
                float y = (-this.Center.Y / this.scaling);
                float w = (base.ClientSize.Width / this.scaling);
                float h = (base.ClientSize.Height / this.scaling);
                return new RectangleF(x, y, w, h);
            }
        }
        internal override bool IsOutOfArea(DirectUI.Core.DUIControl child)
        {
            if (this.IsDescendant(child))
            {
                RectangleF rect = ScaleableBounds;
                return !rect.IntersectsWith(RotateTools.RectangleRotate(child.Bounds, child.PointToParent(child.Center), child.Rotate));
            }
            return true;
        }
        protected internal override bool IsPointOutOfArea(PointF p)
        {
            RectangleF rect = ScaleableBounds;
            return rect.Contains(p);
        }
        public override DUIControl GetChildAtPoint(PointF p)
        {
            if (this.DUIControlShare.TopMostDUIControl != null && this.DUIControlShare.TopMostDUIControl.Visible && this.DUIControls.Contains(this.DUIControlShare.TopMostDUIControl) && this.DUIControlShare.TopMostDUIControl.CanMouseSelected && this.DUIControlShare.TopMostDUIControl.ContainPoint(this.DUIControlShare.TopMostDUIControl.PointFromParent(p)))
            {
                return this.DUIControlShare.TopMostDUIControl;
            }
            if (ScaleableBounds.Contains(p))
            {
                return this.DUIControls.OfType<DUIControl>().Where(d => d.Visible && d.CanMouseSelected).ToList().LastOrDefault(d => d.ContainPoint(d.PointFromParent(p)));
            }
            else
            {
                return null;
            }
        }
        public override void OnMouseDown(DUIMouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.isArrowMouseDragCanvasMouseDown = true;
        }
        public override void OnMouseUp(DUIMouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.Cursor = Cursors.Default;
            this.isArrowMouseDragCanvasMouseDown = false;
        }
        public override void OnMouseMove(DUIMouseEventArgs e)
        {
            base.OnMouseMove(e);
            #region 鼠标拖动画布
            if (ArrowMouseDragCanvas && this.isArrowMouseDragCanvasMouseDown)
            {
                PointF p = CanvasToPoint(e.Location);
                this.Cursor = Cursors.SizeAll;
                this.CenterX += p.X - lastMousePoint.X;
                this.CenterY += p.Y - lastMousePoint.Y;
                lastMousePoint = p;
                this.Invalidate(); //位置变化时刷新
            }
            #endregion
        }

        internal override void DoMouseClick(DUIMouseEventArgs e)
        {
            base.DoMouseClick(new DUIMouseEventArgs(e.Button, e.Clicks, PointToCanvas(e.Location), e.Delta));
        }
        internal override void DoMouseDoubleClick(DUIMouseEventArgs e)
        {
            base.DoMouseDoubleClick(new DUIMouseEventArgs(e.Button, e.Clicks, PointToCanvas(e.Location), e.Delta));
        }
        internal override void DoMouseDown(DUIMouseEventArgs e)
        {
            lastMousePoint = e.Location;
            base.DoMouseDown(new DUIMouseEventArgs(e.Button, e.Clicks, PointToCanvas(e.Location), e.Delta));
        }
        internal override void DoMouseMove(DUIMouseEventArgs e)
        {
            base.DoMouseMove(new DUIMouseEventArgs(e.Button, e.Clicks, PointToCanvas(e.Location), e.Delta));
        }
        internal override void DoMouseUp(DUIMouseEventArgs e)
        {
            var mouseDownDUIControls = this.DUIControlShare.MouseDownDUIControls.Where(c => c.Button == e.Button).ToList();
            if (mouseDownDUIControls.Count == 0) { return; }
            e = new DUIMouseEventArgs(e.Button, e.Clicks, PointToCanvas(e.Location), e.Delta);
            if (mouseDownDUIControls[0].AffectedControl == this)
            {
                this.AlwaysMouseUp(this, e);
                this.InvokeOnMouseUp(this, e);
            }
            else
            {
                this.DUIControls.OfType<DUIControl>().Where(d => d.Visible).ToList().ForEach(d =>
                {
                    d.DoMouseUp(new DUIMouseEventArgs(e.Button, e.Clicks, d.PointFromParent(e.Location), e.Delta));
                });
            }
        }
        internal override void DoMouseWheel(DUIMouseEventArgs e)
        {
            if (this.DUIControlShare.FocusedDUIControl == null) { return; }
            if (this.AnyFocused)
            {
                #region 缩放画布
                if (this.ArrowScaleCanvas)
                {
                    #region 当前的距离差除以缩放比还原到未缩放长度 这里将画布比例按照当前缩放比还原到没有缩放的状态
                    float tempX = Center.X - e.Location.X;
                    float tempY = Center.Y - e.Location.Y;
                    #endregion
                    float currentScaling = Scaling; //保存当前的缩放比
                    Scaling += e.Delta > 0 ? this.scalePrecision : -this.scalePrecision;
                    //还原上一次的偏移                           
                    this.CenterX -= tempX * (1 - Scaling);
                    this.CenterY -= tempY * (1 - Scaling);
                    //重新计算缩放并重置画布原点坐标
                    this.CenterX += tempX / currentScaling * Scaling - (this.Center.X - e.Location.X);
                    this.CenterY += tempY / currentScaling * Scaling - (this.Center.Y - e.Location.Y);
                    this.Invalidate();
                }
                #endregion
                this.OnMouseWheel(e);
            }
            else
            {
                this.DUIControls.OfType<DUIControl>().Where(d => d.Visible).ToList().ForEach(d =>
                {
                    d.DoMouseWheel(new DUIMouseEventArgs(e.Button, e.Clicks, d.PointFromParent(e.Location), e.Delta));
                    if (d.AnyFocused && d.CanMouseThrough)
                    {
                        this.OnMouseWheel(e);
                    }
                });
            }
        }
        public override void PerformMouseClick(DUIMouseEventArgs e)
        {
            base.PerformMouseClick(new DUIMouseEventArgs(e.Button, e.Clicks, PointToCanvas(e.Location), e.Delta));
        }
        public override void PerformMouseDoubleClick(DUIMouseEventArgs e)
        {
            base.PerformMouseDoubleClick(new DUIMouseEventArgs(e.Button, e.Clicks, PointToCanvas(e.Location), e.Delta));
        }
        public override void PerformMouseDown(DUIMouseEventArgs e)
        {
            base.PerformMouseDown(new DUIMouseEventArgs(e.Button, e.Clicks, PointToCanvas(e.Location), e.Delta));
        }
        public override void PerformMouseMove(DUIMouseEventArgs e)
        {
            base.PerformMouseMove(new DUIMouseEventArgs(e.Button, e.Clicks, PointToCanvas(e.Location), e.Delta));
        }
        public override void PerformMouseUp(DUIMouseEventArgs e)
        {
            base.PerformMouseUp(new DUIMouseEventArgs(e.Button, e.Clicks, PointToCanvas(e.Location), e.Delta));
        }
        public override void PerformMouseWheel(DUIMouseEventArgs e)
        {
            base.PerformMouseWheel(new DUIMouseEventArgs(e.Button, e.Clicks, PointToCanvas(e.Location), e.Delta));
        }
        internal override void DoPaint(DUIPaintEventArgs e)
        {
            e.Graphics.TranslateTransform(this.X, this.Y); //偏移一下坐标系将控件的坐标定义为坐标系原点
            e.Graphics.PushLayer(this.Width, this.Height); //背景图层
            PointF center = new PointF(this.BorderWidth + this.CenterX, this.BorderWidth + this.CenterY);
            e.Graphics.RotateTransform(this.Rotate, center.X, center.Y);
            e.Graphics.TranslateTransform(this.Center.X, this.Center.Y);
            e.Graphics.ScaleTransform(this.scaling, this.scaling);
            var backupSmoothingMode = e.Graphics.SmoothingMode;
            var backupTextRenderingHint = e.Graphics.TextRenderingHint;
            var backupTransform = e.Graphics.Transform;
            OnPaintBackground(new DUIPaintEventArgs(e.Graphics, new RectangleF(0, 0, this.Width, this.Height))); //绘制背景
            e.Graphics.Transform = backupTransform;
            e.Graphics.TextRenderingHint = backupTextRenderingHint;
            e.Graphics.SmoothingMode = backupSmoothingMode;
            e.Graphics.ScaleTransform(1 / this.scaling, 1 / this.scaling);
            e.Graphics.TranslateTransform(-this.Center.X, -this.Center.Y);
            if (this.BorderWidth != 0)
            {
                e.Graphics.PopLayer();
                e.Graphics.TranslateTransform(this.BorderWidth, this.BorderWidth); //偏移一个边框的坐标系
                e.Graphics.PushLayer(this.ClientSize.Width, this.ClientSize.Height); //背景图层
            }
            e.Graphics.TranslateTransform(this.Center.X, this.Center.Y);
            e.Graphics.ScaleTransform(this.scaling, this.scaling);
            backupSmoothingMode = e.Graphics.SmoothingMode;
            backupTextRenderingHint = e.Graphics.TextRenderingHint;
            backupTransform = e.Graphics.Transform;
            OnPaint(new DUIPaintEventArgs(e.Graphics, new RectangleF(0, 0, this.ClientSize.Width, this.ClientSize.Height))); //先让子类画图
            e.Graphics.Transform = backupTransform;
            e.Graphics.TextRenderingHint = backupTextRenderingHint;
            e.Graphics.SmoothingMode = backupSmoothingMode;
            List<DUIControl> dUIControls = new List<DUIControl>();
            List<DUIControl> dUITopMostControls = new List<DUIControl>();
            foreach (DUIControl d in this.DUIControls)
            {
                if (d.Visible && !this.IsOutOfArea(d))
                {
                    if (d.TopMost)
                    {
                        dUITopMostControls.Add(d);
                    }
                    else
                    {
                        dUIControls.Add(d);
                    }
                }
            }
            dUITopMostControls.ForEach(t => { dUIControls.Add(t); });
            foreach (DUIControl dUIControl in dUIControls)
            {
                dUIControl.DoPaint(new DUIPaintEventArgs(e.Graphics, new RectangleF(0, 0, dUIControl.Width, dUIControl.Height)));
            }
            e.Graphics.TranslateTransform(-this.BorderWidth, -this.BorderWidth);
            e.Graphics.PopLayer();
            backupSmoothingMode = e.Graphics.SmoothingMode;
            backupTextRenderingHint = e.Graphics.TextRenderingHint;
            backupTransform = e.Graphics.Transform;
            OnPaintForeground(new DUIPaintEventArgs(e.Graphics, new RectangleF(0, 0, this.Width, this.Height))); //先让子类画图
            e.Graphics.Transform = backupTransform;
            e.Graphics.TextRenderingHint = backupTextRenderingHint;
            e.Graphics.SmoothingMode = backupSmoothingMode;
            e.Graphics.ScaleTransform(1 / this.scaling, 1 / this.scaling);
            e.Graphics.TranslateTransform(-this.Center.X, -this.Center.Y);
            e.Graphics.RotateTransform(-this.Rotate, this.BorderWidth + this.CenterX, this.BorderWidth + this.CenterY); //设置旋转中心点
            e.Graphics.TranslateTransform(-this.X, -this.Y); //偏移一下坐标系将控件的坐标定义为坐标系原点
        }
        public override void OnPaintBackground(DUIPaintEventArgs e)
        {
            using (DUIPen borderPen = new DUIPen(this.Border.BorderColor, this.BorderWidth))
            using (DUISolidBrush backBrush = new DUISolidBrush(this.BackColor))
            {
                e.Graphics.DrawRectangle(borderPen, new RectangleF(this.BorderWidth / 2F, this.BorderWidth / 2F, this.Width - this.BorderWidth, this.Height - this.BorderWidth));
                e.Graphics.FillRectangle(backBrush, new RectangleF((this.BorderWidth - this.Center.X) / this.scaling, (this.BorderWidth - this.Center.Y) / this.scaling, this.ClientSize.Width / this.scaling, this.ClientSize.Height / this.scaling));
            }
            if (this.BackgroundImage != null)
            {
                e.Graphics.DrawImage(this.BackgroundImage, new RectangleF(this.BorderWidth, this.BorderWidth, this.ClientSize.Width, this.ClientSize.Height));
            }
            //if (PaintBackground != null)
            //{
            //    PaintBackground(this, e);
            //}
        }

        #endregion
        /// <summary> 将控件坐标转换成画布坐标
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private PointF PointToCanvas(PointF point)
        {
            return new PointF((point.X - this.Center.X) / this.scaling, (point.Y - this.Center.Y) / this.scaling);
        }
        /// <summary> 将画布坐标转换成控件坐标
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private PointF CanvasToPoint(PointF point)
        {
            return new PointF(point.X * this.scaling + this.Center.X, point.Y * this.scaling + this.Center.Y);
        }
        public override PointF PointToParent(PointF p)
        {
            p = CanvasToPoint(p);
            return base.PointToParent(p);
        }

        public void ScaleCanvasOffset(float scaled)
        {
            if (this.ArrowScaleCanvas)
            {
                this.ScaleCanvasOffsetInternal(scaled);
            }
        }
        protected internal void ScaleCanvasOffsetInternal(float scaled)
        {
            if (scaling <= this.minScale && scaled <= 0) return; //缩小下限
            if (scaling >= this.maxScale && scaled >= 0) return; //放大上限
            #region 当前的距离差除以缩放比还原到未缩放长度 这里将画布比例按照当前缩放比还原到没有缩放的状态
            float locationX = this.Center.X;
            float locationY = this.Center.Y;
            float tempX = this.Center.X - locationX;
            float tempY = this.Center.Y - locationY;
            #endregion
            float currentScaling = scaling; //保存当前的缩放比
            scaling += scaled > 0 ? this.scalePrecision : -this.scalePrecision;
            //还原上一次的偏移                           
            this.CenterX -= tempX * (1 - scaling);
            this.CenterY -= tempY * (1 - scaling);
            //重新计算缩放并重置画布原点坐标
            this.CenterX += tempX / currentScaling * scaling - (this.Center.X - locationX);
            this.CenterY += tempY / currentScaling * scaling - (this.Center.Y - locationY);
            this.Invalidate();
        }

        public void ScaleCanvas(float scaled)
        {
            if (this.ArrowScaleCanvas)
            {
                this.ScaleCanvasInternal(scaled);
            }
        }
        protected internal void ScaleCanvasInternal(float scaled)
        {
            Scaling = scaled;
            this.Invalidate();
        }
    }
}
