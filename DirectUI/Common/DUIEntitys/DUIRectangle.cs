using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIRectangle
    {
        private System.Drawing.Rectangle rectangle = new Rectangle();
        private SharpDX.Mathematics.Interop.RawRectangle dxRectangle = new SharpDX.Mathematics.Interop.RawRectangle();

        internal SharpDX.Mathematics.Interop.RawRectangle DxRectangle
        {
            get { return dxRectangle; }
        }
        public DUIRectangle(System.Drawing.Rectangle rectangle)
        {
            this.rectangle = rectangle;
            this.dxRectangle = new SharpDX.Mathematics.Interop.RawRectangle(this.rectangle.Left, this.rectangle.Top, this.rectangle.Right, this.rectangle.Bottom);
        }
        public static implicit operator SharpDX.Mathematics.Interop.RawRectangle(DUIRectangle dUIRectangle)
        {
            return dUIRectangle.DxRectangle;
        }
        public static implicit operator System.Drawing.Rectangle(DUIRectangle dUIRectangle)
        {
            return dUIRectangle.rectangle;
        }
    }
}
