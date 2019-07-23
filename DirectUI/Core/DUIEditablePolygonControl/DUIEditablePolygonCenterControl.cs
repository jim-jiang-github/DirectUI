using DirectUI.Common;
/********************************************************************
 * * 使本项目源码前请仔细阅读以下协议内容，如果你同意以下协议才能使用本项目所有的功能,
 * * 否则如果你违反了以下协议，有可能陷入法律纠纷和赔偿，作者保留追究法律责任的权利。
 * * Copyright (C) 
 * * 作者： 煎饼的归宿    QQ：375324644   
 * * 请保留以上版权信息，否则作者将保留追究法律责任。
 * * 最后修改：BF-20150503UIWA
 * * 创建时间：2018/1/8 14:11:15
********************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DirectUI.Core
{
    public class DUIEditablePolygonCenterControl : DUIEditableCenterScaleParentControl
    {
        private PointF mouseMovePoint = PointF.Empty;
        private bool isMouseDownInRotateBounds = false;
        protected override System.Drawing.PointF ThisCenter
        {
            get
            {
                return new PointF(this.X + this.Height / 2, this.Y + this.Height / 2);
            }
            set
            {
                this.X = value.X - this.Height / 2;
                this.Y = value.Y - this.Height / 2;
            }
        }
        public DUIEditablePolygonCenterControl()
        {
            this.CanRotate = true;
            this.mouseMovePoint = new PointF(this.Width - this.ThisRadius, this.Height / 2);
            //this.Center = new PointF(this.ThisRadius, this.ThisRadius);
        }
        public override float CenterX
        {
            get
            {
                return this.ThisRadius;
            }
        }
        public override float CenterY
        {
            get
            {
                return this.ThisRadius;
            }
        }
        public override float Width
        {
            get
            {
                return base.Width * 3;
            }
        }
        //public override float Height
        //{
        //    get
        //    {
        //        return base.Height * 3;
        //    }
        //}
        public override System.Windows.Forms.Cursor GetCursor(PointF mouseLocation)
        {
            if (this.CanRotate)
            {
                if ((this.CanEffectMargin | EffectMargin.LeftBottomRotate) == this.CanEffectMargin && this.LeftBottomRotateBounds.Contains(mouseLocation)) { return DUICursors.Rotate; }
                if ((this.CanEffectMargin | EffectMargin.LeftTopRotate) == this.CanEffectMargin && this.LeftTopRotateBounds.Contains(mouseLocation)) { return DUICursors.Rotate; }
                if ((this.CanEffectMargin | EffectMargin.RightBottomRotate) == this.CanEffectMargin && this.RightBottomRotateBounds.Contains(mouseLocation)) { return DUICursors.Rotate; }
                if ((this.CanEffectMargin | EffectMargin.RightTopRotate) == this.CanEffectMargin && this.RightTopRotateBounds.Contains(mouseLocation)) { return DUICursors.Rotate; }
            }
            return Cursors.Default;
        }
        public override EffectMargin CanEffectMargin
        {
            get
            {
                return EffectMargin.Other | EffectMargin.RightBottomRotate;
            }
        }
        public override RectangleF RightBottomRotateBounds
        {
            get
            {
                return new RectangleF(this.Width - this.ThisRadius * 2, 0, this.ThisRadius * 2, this.ThisRadius * 2);
            }
        }
        public override RectangleF OtherBounds
        {
            get
            {
                return new RectangleF(0, 0, this.ThisRadius * 2, this.ThisRadius * 2);
            }
        }
        public override void BindingControl(DUIEditableControl editableControl)
        {
            base.BindingControl(editableControl);
            if (this.editableControl != null)
            {
                this.Rotate = this.editableControl.Rotate;
            }
        }
        internal protected override bool ContainPoint(PointF p)
        {
            return new RectangleF(0, 0, this.Width, this.Height).Contains(p);
        }
        public override void OnMouseDown(DUIMouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.isMouseDownInRotateBounds = RightBottomRotateBounds.Contains(e.Location);
        }
        public override void OnMouseMove(DUIMouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.editableControl.SetCenterChanging(this.ThisCenter);
            }
            this.mouseMovePoint = e.Location;
            this.Invalidate();
        }
        public override void OnMouseUp(DUIMouseEventArgs e)
        {
            this.editableControl.SetCenterChanged(this.ThisCenter);
            this.isMouseDownInRotateBounds = false;
            this.mouseMovePoint = new PointF(this.Width - this.ThisRadius, this.Height / 2);
        }
        public override void OnPaintForeground(DUIPaintEventArgs e)
        {
            var backupSmoothingMode = e.Graphics.SmoothingMode;
            e.Graphics.SmoothingMode = DUISmoothingMode.AntiAlias;
            float width = this.ThisRadius / 3;
            PointF endPoint = this.isMouseDownInRotateBounds ? new PointF(mouseMovePoint.X, this.Height / 2) : new PointF(this.Width - this.ThisRadius, this.Height / 2);
            using (DUIPen pen = new DUIPen(Color.Black, width))
            {
                e.Graphics.DrawLine(pen, new PointF(this.ThisRadius, this.Height / 2), endPoint);
            }
            e.Graphics.FillEllipse(DUIBrushes.Black, new RectangleF(0, 0, this.ThisRadius * 2, this.ThisRadius * 2));
            e.Graphics.FillEllipse(DUIBrushes.White, new RectangleF(width, width, this.ThisRadius * 2 - width * 2, this.ThisRadius * 2 - width * 2));
            e.Graphics.FillRectangle(DUIBrushes.Black, new RectangleF(endPoint.X - this.ThisRadius, endPoint.Y - this.ThisRadius, this.ThisRadius * 2, this.ThisRadius * 2));
            e.Graphics.FillRectangle(DUIBrushes.White, new RectangleF(width + endPoint.X - this.ThisRadius, width + endPoint.Y - this.ThisRadius, this.ThisRadius * 2 - width * 2, this.ThisRadius * 2 - width * 2));
            //e.Graphics.FillEllipse(DUIBrushes.Black, new RectangleF(0, 0, this.Width - 1, this.Height - 1));
            //e.Graphics.FillEllipse(DUIBrushes.White, new RectangleF(2, 2, this.Width - 2 * 2 - 1, this.Height - 2 * 2 - 1));
            e.Graphics.SmoothingMode = backupSmoothingMode;
            //base.OnPaint(e);
        }
    }
}
