using DirectUI.Common;
using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DirectUI.Controls.ScrollBar
{
    public class DUIVScrollBar : DUIScrollBar
    {
        private bool VScroll { get; set; }
        public override float Width
        {
            get
            {
                return this.Thickness;
            }
        }
        public DUIVScrollBar()
        {
        }
        public override RectangleF BodyRectangle
        {
            get
            {
                float vBodyX = this.ClientRectangle.X;
                float vBodyY = (float)this.Value * (float)this.ClientRectangle.Height / (float)this.Maximum;
                float vBodyWidth = this.Thickness;
                float vBodyHeight = (float)this.ClientRectangle.Height * (float)this.LargeChange / (float)this.Maximum;
                return new RectangleF(vBodyX, vBodyY, vBodyWidth, vBodyHeight);
            }
        }
        public override void OnMouseDown(DUIMouseEventArgs e)
        {
            if (this.BodyRectangle.Contains(e.Location))
            {
                this.mouseEffectScrollPoint = new PointF(e.Location.X, e.Location.Y - BodyRectangle.Y);
                this.isMouseDownInScroll = true;
            }
            base.OnMouseDown(e);
        }
        public override void OnMouseMove(DUIMouseEventArgs e)
        {
            if (isMouseDownInScroll)
            {
                if (e.Button == MouseButtons.Left)
                {
                    float oldValue = this.Value;
                    this.Value = (e.Location.Y - this.mouseEffectScrollPoint.Y) * this.Maximum / this.ClientRectangle.Height;
                    DUIScrollEventArgs se = new DUIScrollEventArgs(ScrollEventType.SmallDecrement, oldValue, this.Value, ScrollOrientation.VerticalScroll);
                    OnScroll(se);
                }
                //this.Invalidate();
            }
            base.OnMouseMove(e);
        }
        //public override void OnLayout(Common.DUILayoutEventArgs e)
        //{
        //    base.OnLayout(e);
        //    if (this.bindingScrollableControls.Count == 0) { return; }
        //    AdjustFormScrollbars();
        //}
        public float ScrollOffsetY(float scrollValue)
        {
            if (this.BindingScrollableControl == null) { return 0; }
            float lastdisplayRectY = this.BindingScrollableControl.displayRect.Y;
            DUIScrollEventArgs se = new DUIScrollEventArgs(ScrollEventType.SmallDecrement, lastdisplayRectY, -(this.BindingScrollableControl.displayRect.Y + scrollValue), ScrollOrientation.HorizontalScroll);
            OnScroll(se);
            return this.BindingScrollableControl.displayRect.Y - lastdisplayRectY;
        }
        protected override void OnScroll(DUIScrollEventArgs se)
        {
            if (this.BindingScrollableControl == null) { return; }
            this.BindingScrollableControl.ScrollVIntoView(se.NewValue);
            SyncScrollbars(true);
            base.OnScroll(se);
        }
        public override void AdjustFormScrollbars()
        {
            if (this.BindingScrollableControl == null) { return; }
            this.BindingScrollableControl.ApplyScrollbarChanges(this.BindingScrollableControl.DisplayRectangle);
            SetVisibleScrollbars(this.BindingScrollableControl.VScroll);
            SyncScrollbars(true);
        }
        private void SyncScrollbars(bool autoScroll)
        {
            if (this.BindingScrollableControl == null) { return; }
            RectangleF displayRect = this.BindingScrollableControl.displayRect;
            if (autoScroll)
            {
                if (!this.BindingScrollableControl.IsHandleCreated)
                {
                    return;
                }
                if (VScroll)
                {
                    this.Maximum = displayRect.Height - 1;
                    this.LargeChange = this.BindingScrollableControl.ClientRectangle.Height; //base.ClientSize是没有扣除滚动条区域的大小
                    this.SmallChange = 5;
                    if (-displayRect.Y >= this.Minimum && -displayRect.Y < this.Maximum)
                    {
                        this.Value = -displayRect.Y;
                    }
                }
            }
            else
            {
                if (this.Display)
                {
                    this.Value = -displayRect.Y;
                }
                else
                {
                    ResetScrollProperties();
                }
            }
        }
        private void ResetScrollProperties()
        {
            this.Display = false;
            this.Value = 0;
        }
        private bool SetVisibleScrollbars(bool vert)
        {
            if (this.BindingScrollableControl == null) { return false; }
            bool needLayout = false;
            if (!vert && VScroll || vert && !VScroll)
            {
                needLayout = true;
            }
            if (needLayout)
            {
                float x = this.BindingScrollableControl.displayRect.X;
                float y = this.BindingScrollableControl.displayRect.Y;
                if (!vert)
                {
                    x = 0;
                }
                this.BindingScrollableControl.SetDisplayRectangleLocation(x, y);
                VScroll = vert;
                if (vert)
                {
                    this.Display = true;
                }
                else
                {
                    this.Display = false;
                    ResetScrollProperties();
                }
            }
            return needLayout;
        }

        protected override void BindingScrollValueChanged(DUIScrollEventArgs e)
        {
            if (e.ScrollOrientation == System.Windows.Forms.ScrollOrientation.VerticalScroll)
            {
                this.Value = e.NewValue;
            }
        }
    }
}
