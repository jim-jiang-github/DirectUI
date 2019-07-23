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
    public class DUIHScrollBar : DUIScrollBar
    {
        private bool HScroll { get; set; }
        public override float Height
        {
            get
            {
                return this.Thickness;
            }
        }

        public DUIHScrollBar()
        {
        }
        public override RectangleF BodyRectangle
        {
            get
            {
                float hBodyX = this.Value * this.ClientRectangle.Width / this.Maximum;
                float hBodyY = this.ClientRectangle.Y;
                float hBodyWidth = this.ClientRectangle.Width * this.LargeChange / this.Maximum;
                float hBodyHeight = this.Thickness;
                return new RectangleF(hBodyX, hBodyY, hBodyWidth, hBodyHeight);
            }
        }
        public override void OnMouseDown(DUIMouseEventArgs e)
        {
            if (this.BodyRectangle.Contains(e.Location))
            {
                this.mouseEffectScrollPoint = new PointF(e.Location.X - BodyRectangle.X, e.Location.Y);
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
                    this.Value = (e.Location.X - this.mouseEffectScrollPoint.X) * this.Maximum / this.ClientRectangle.Width;
                    DUIScrollEventArgs se = new DUIScrollEventArgs(ScrollEventType.SmallDecrement, oldValue, this.Value, ScrollOrientation.HorizontalScroll);
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
        public float ScrollOffsetX(float scrollValue)
        {
            if (this.BindingScrollableControl == null) { return 0; }
            float lastdisplayRectX = this.BindingScrollableControl.displayRect.X;
            DUIScrollEventArgs se = new DUIScrollEventArgs(ScrollEventType.SmallDecrement, lastdisplayRectX, -(this.BindingScrollableControl.displayRect.X + scrollValue), ScrollOrientation.HorizontalScroll);
            OnScroll(se);
            return this.BindingScrollableControl.displayRect.X - lastdisplayRectX;
        }
        protected override void OnScroll(DUIScrollEventArgs se)
        {
            if (this.BindingScrollableControl == null) { return; }
            this.BindingScrollableControl.ScrollHIntoView(se.NewValue);
            SyncScrollbars(true);
            base.OnScroll(se);
        }
        public override void AdjustFormScrollbars()
        {
            if (this.BindingScrollableControl == null) { return; }
            this.BindingScrollableControl.ApplyScrollbarChanges(this.BindingScrollableControl.DisplayRectangle);
            SetVisibleScrollbars(this.BindingScrollableControl.HScroll);
            SyncScrollbars(true);
            //ApplyScrollbarChanges(this.BindingScrollableControl.DisplayRectangle);
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
                if (HScroll)
                {
                    this.Maximum = displayRect.Width - 1;
                    this.LargeChange = this.BindingScrollableControl.ClientRectangle.Width;
                    this.SmallChange = 5;
                    if (-displayRect.X >= this.Minimum && -displayRect.X < this.Maximum)
                    {
                        this.Value = -displayRect.X;
                    }
                }
            }
            else
            {
                if (this.Display)
                {
                    this.Value = -displayRect.X;
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
        private bool SetVisibleScrollbars(bool horiz)
        {
            if (this.BindingScrollableControl == null) { return false; }
            bool needLayout = false;
            if (!horiz && HScroll || horiz && !HScroll)
            {
                needLayout = true;
            }
            if (needLayout)
            {
                float x = this.BindingScrollableControl.displayRect.X;
                float y = this.BindingScrollableControl.displayRect.Y;
                if (!horiz)
                {
                    x = 0;
                }
                this.BindingScrollableControl.SetDisplayRectangleLocation(x, y);
                HScroll = horiz;
                if (horiz)
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
            if (e.ScrollOrientation == System.Windows.Forms.ScrollOrientation.HorizontalScroll)
            {
                this.Value = e.NewValue;
            }
        }
    }
}
