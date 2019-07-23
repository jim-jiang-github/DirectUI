using DirectUI.Common;
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
    /// <summary> 编辑控件的悬浮控件
    /// </summary>
    public class DUIEditableSuspendedControl : DUIControl
    {
        #region 变量
        private bool isControlMouseDown = false;
        protected DUIEditableControl bindingControl = null;
        #endregion
        #region 属性
        #endregion
        public DUIEditableSuspendedControl()
        {
            //this.TopMost = true;
        }
        #region 重写
        public override bool Visible
        {
            get
            {
                if (this.bindingControl == null) { return false; }
                if (!this.bindingControl.Visible) { return false; }
                if (this.isControlMouseDown && DUIControl.MouseButtons != MouseButtons.None) { return false; }
                return true;
            }
            set
            {
                base.Visible = value;
            }
        }
        public override bool CanFocus
        {
            get
            {
                return false;
            }
        }
        #endregion
        #region 函数
        public virtual void BindingControl(DUIEditableControl bindingControl)
        {
            if (this.bindingControl != null && bindingControl == null)
            {
                this.bindingControl.AnyChanging -= Control_AnyChanging;
                this.bindingControl.CenterControl.MouseDown -= Control_MouseDown;
                this.bindingControl.CenterControl.MouseUp -= Control_MouseUp;
                this.bindingControl.MouseDown -= Control_MouseDown;
                this.bindingControl.MouseUp -= Control_MouseUp;
            }
            this.bindingControl = bindingControl;
            if (this.bindingControl != null)
            {
                this.bindingControl.AnyChanging += Control_AnyChanging;
                this.bindingControl.CenterControl.MouseDown += Control_MouseDown;
                this.bindingControl.CenterControl.MouseUp += Control_MouseUp;
                this.bindingControl.MouseDown += Control_MouseDown;
                this.bindingControl.MouseUp += Control_MouseUp;
            }
        }

        private void Control_AnyChanging(object sender, DUIAnyChangingEventArgs e)
        {
            PointF lt = this.bindingControl.PointToParent(new PointF(-this.bindingControl.BorderWidth, -this.bindingControl.BorderWidth));
            PointF rt = this.bindingControl.PointToParent(new PointF(this.bindingControl.Width - this.bindingControl.BorderWidth, -this.bindingControl.BorderWidth));
            PointF rb = this.bindingControl.PointToParent(new PointF(this.bindingControl.Width - this.bindingControl.BorderWidth, this.bindingControl.Height - this.bindingControl.BorderWidth));
            PointF lb = this.bindingControl.PointToParent(new PointF(-this.bindingControl.BorderWidth, this.bindingControl.Height - this.bindingControl.BorderWidth));
            this.Location = new PointF(Math.Max(lt.X, Math.Max(rt.X, Math.Max(rb.X, lb.X))), Math.Min(lt.Y, Math.Min(rt.Y, Math.Min(rb.Y, lb.Y))));
        }

        private void Control_MouseDown(object sender, DUIMouseEventArgs e)
        {
            this.isControlMouseDown = true;
            this.Invalidate();
        }

        private void Control_MouseUp(object sender, DUIMouseEventArgs e)
        {
            this.isControlMouseDown = false;
            this.Invalidate();
        }
        #endregion
    }
}
