using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIRectangleF
    {
        private System.Drawing.RectangleF rectangle = new RectangleF();
        private SharpDX.Mathematics.Interop.RawRectangleF dxRectangleF = new SharpDX.Mathematics.Interop.RawRectangleF();

        internal SharpDX.Mathematics.Interop.RawRectangleF DxRectangleF
        {
            get { return dxRectangleF; }
        }
        public DUIRectangleF(System.Drawing.RectangleF rectangle)
        {
            this.rectangle = rectangle;
            this.dxRectangleF = new SharpDX.Mathematics.Interop.RawRectangleF(this.rectangle.Left, this.rectangle.Top, this.rectangle.Right, this.rectangle.Bottom);
        }
        public static implicit operator SharpDX.Mathematics.Interop.RawRectangleF(DUIRectangleF dUIRectangle)
        {
            return dUIRectangle.DxRectangleF;
        }
        public static implicit operator System.Drawing.RectangleF(DUIRectangleF dUIRectangle)
        {
            return dUIRectangle.rectangle;
        }
    }
}
