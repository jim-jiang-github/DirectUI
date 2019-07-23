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
    /// <summary> 编辑控件的悬浮控件。父控件是可缩放的控件，这个编辑控件的操作点大小会跟随父控件的缩放而变化
    /// </summary>
    public class DUIEditableSuspendedScaleParentControl : DUIEditableSuspendedControl
    {
        private DUIScaleableControl scaleableControl = null;
        /// <summary> 父控件
        /// </summary>
        public DUIScaleableControl ScaleableControl
        {
            get
            {
                if (scaleableControl == null)
                {
                    if (this.DUIParent != null && this.DUIParent is DUIScaleableControl dUIScaleableControlParent)
                    {
                        return dUIScaleableControlParent;
                    }
                }
                return scaleableControl;
            }
            set { scaleableControl = value; }
        }
        public float ScaleableControlScaling
        {
            get { return ScaleableControl == null ? 1 : ScaleableControl.Scaling; }
        }

        public override float BorderWidth => 0;
        public override float X => this.bindingControl == null ? 0 : this.bindingControl.MatrixBounds.Right;
        public override float Y => this.bindingControl == null ? 0 : (this.bindingControl.MatrixBounds.Top + (this.bindingControl.MatrixBounds.Height - this.Height) / 2);
        public override float Width
        {
            get
            {
                return base.Width / ScaleableControlScaling;
            }
            set
            {
                base.Width = value * ScaleableControlScaling;
            }
        }
        public override float Height
        {
            get
            {
                return base.Height / ScaleableControlScaling;
            }
            set
            {
                base.Height = value * ScaleableControlScaling;
            }
        }
    }
}
