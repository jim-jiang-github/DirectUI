using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Core
{
    public class DUIVScrollProperties : DUIScrollProperties
    {
        public DUIVScrollProperties(DUIScrollableControl container)
            : base(container)
        {
        }

        internal override float PageSize
        {
            get
            {
                return this.parent.ClientRectangle.Height;
            }
        }

        internal override float HorizontalDisplayPosition
        {
            get
            {
                return this.parent.DisplayRectangle.X;
            }
        }

        internal override float VerticalDisplayPosition
        {
            get
            {
                return -this.value;
            }
        }
        public override RectangleF ClientRectangle
        {
            get
            {
                return new RectangleF(this.parent.Width - this.parent.Border.BorderWidth * 2 - this.Thickness, 0, this.Thickness, this.parent.Height - this.parent.Border.BorderWidth * 2);
            }
        }

        public override RectangleF BodyRectangle
        {
            get
            {
                float vBodyX = this.ClientRectangle.X;
                float vBodyY = this.Value * this.parent.ClientRectangle.Height / this.parent.DisplayRectangle.Height;
                float vBodyWidth = this.Thickness;
                float vBodyHeight = this.ClientRectangle.Height * this.parent.ClientRectangle.Height / this.parent.DisplayRectangle.Height;
                return new RectangleF(vBodyX, vBodyY, vBodyWidth, vBodyHeight);
            }
        }
    }
}
