using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Core
{
    public class DUIHScrollProperties : DUIScrollProperties
    {
        public DUIHScrollProperties(DUIScrollableControl container)
            : base(container)
        {
        }
        internal override float PageSize
        {
            get
            {
                return this.parent.ClientRectangle.Width;
            }
        }
        internal override float HorizontalDisplayPosition
        {
            get
            {
                return -this.value;
            }
        }
        internal override float VerticalDisplayPosition
        {
            get
            {
                return this.parent.DisplayRectangle.Y;
            }
        }
        public override RectangleF ClientRectangle
        {
            get
            {
                return new RectangleF(0, this.parent.Height - this.parent.Border.BorderWidth * 2 - this.Thickness, this.parent.ClientSize.Width, this.Thickness);
            }
        }
        public override RectangleF BodyRectangle
        {
            get
            {
                float hBodyX = this.Value * this.parent.ClientRectangle.Width / this.parent.DisplayRectangle.Width;
                float hBodyY = this.ClientRectangle.Y;
                float hBodyWidth = this.ClientRectangle.Width * this.parent.ClientRectangle.Width / this.parent.DisplayRectangle.Width;
                float hBodyHeight = this.Thickness;
                return new RectangleF(hBodyX, hBodyY, hBodyWidth, hBodyHeight);
            }
        }
    }
}
