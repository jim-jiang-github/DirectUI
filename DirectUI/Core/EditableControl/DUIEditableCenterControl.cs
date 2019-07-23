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
    /// <summary> 编辑控件的中心点编辑控件
    /// </summary>
    public class DUIEditableCenterControl : DUIEditableControl
    {
        #region 变量
        protected DUIEditableControl editableControl = null;
        private float thisRadius = 7;
        #endregion
        #region 属性
        /// <summary> 绑定的DUIEditableControl
        /// </summary>
        public DUIEditableControl EditableControl => this.editableControl;
        /// <summary> 这个控件代表的中心点坐标
        /// </summary>
        protected virtual PointF ThisCenter
        {
            get
            {
                return new PointF(this.X + Width / 2, this.Y + this.Height / 2);
            }
            set
            {
                this.X = value.X - this.Width / 2;
                this.Y = value.Y - this.Height / 2;
            }
        }
        /// <summary> 这个控件代表的中心点的半径
        /// </summary>
        public virtual float ThisRadius
        {
            get { return thisRadius; }
            set { thisRadius = value; }
        }
        #endregion
        public DUIEditableCenterControl()
        {
            this.TopMost = true;
        }
        #region 重写
        public override bool Visible
        {
            get
            {
                if (this.editableControl == null) { return false; }
                if (!this.editableControl.Visible) { return false; }
                return true;
            }
            set
            {
                base.Visible = value;
            }
        }
        public override float BorderWidth => 0;
        public override EffectMargin CanEffectMargin => EffectMargin.Other;
        public override Cursor GetCursor(PointF mouseLocation) => Cursors.Default;
        protected override void DrawBounds(DUIPaintEventArgs e)
        {
            //base.DrawBounds(e);
        }
        protected override void DrawVertex(DUIPaintEventArgs e)
        {
            //base.DrawVertex(e);
        }
        public override void OnMouseDown(DUIMouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (this.editableControl != null)
            {
                this.editableControl.mouseDownBounds = new RectangleF(this.editableControl.x, this.editableControl.y, this.editableControl.width, this.editableControl.height);
                this.editableControl.mouseDownCenter = new PointF(this.editableControl.centerX, this.editableControl.centerY);
                this.editableControl.mouseDownScale = new PointF(this.editableControl.scaleX, this.editableControl.scaleY);
                this.editableControl.mouseDownSkew = new PointF(this.editableControl.skewX, this.editableControl.skewY);
                this.editableControl.mouseDownRotateAngle = this.editableControl.rotate;
            }
        }
        public override void OnMouseMove(DUIMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.editableControl.SetCenterChanging(this.ThisCenter);
            }
            base.OnMouseMove(e);
        }
        public override void OnMouseUp(DUIMouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.editableControl.SetCenterChanged(this.ThisCenter);
        }
        public override Color BackColor => Color.Transparent;
        public override float Width=> ThisRadius * 2;
        public override float Height=> ThisRadius * 2;
        public override bool CanFocus => false;
        internal protected override bool ContainPoint(PointF p)
        {
            return PointTools.Distance(p, new PointF(this.Width / 2, this.Height / 2)) < ThisRadius;
        }
        public override void OnPaint(DUIPaintEventArgs e)
        {
            e.Graphics.SmoothingMode = DUISmoothingMode.AntiAlias;
            e.Graphics.FillEllipse(DUIBrushes.Black, new RectangleF(0, 0, this.ThisRadius * 2, this.ThisRadius * 2));
            e.Graphics.FillEllipse(DUIBrushes.White, new RectangleF(this.ThisRadius / 3, this.ThisRadius / 3, this.ThisRadius * 2 * 2 / 3, this.ThisRadius * 2 * 2 / 3));
        }
        #endregion
        #region 函数
        public virtual void BindingControl(DUIEditableControl editableControl)
        {
            if (this.editableControl != null && editableControl == null)
            {
                this.editableControl.CenterChanging -= control_CenterChanging;
                this.editableControl.LocationChanging -= control_LocationChanging;
                this.editableControl.BindingCenterControl(null);
            }
            this.editableControl = editableControl;
            if (this.editableControl != null)
            {
                this.ThisCenter = this.editableControl.PointToParent(this.editableControl.Center);
                this.editableControl.CenterChanging += control_CenterChanging;
                this.editableControl.LocationChanging += control_LocationChanging;
                this.editableControl.BindingCenterControl(this);
            }
        }

        void control_LocationChanging(object sender, DUILocationChangingEventArgs e)
        {
            this.ThisCenter = this.editableControl.PointToParent(new PointF(this.editableControl.centerX, this.editableControl.centerY));
        }

        void control_CenterChanging(object sender, DUICenterChangingEventArgs e)
        {
            this.ThisCenter = this.editableControl.PointToParent(new PointF(this.editableControl.centerX, this.editableControl.centerY));
        }
        #endregion
    }
}
