using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct GPRECTF
    {
        internal float X;
        internal float Y;
        internal float Width;
        internal float Height;

        internal GPRECTF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        internal GPRECTF(RectangleF rect)
        {
            X = rect.X;
            Y = rect.Y;
            Width = rect.Width;
            Height = rect.Height;
        }

        internal SizeF SizeF
        {
            get
            {
                return new SizeF(Width, Height);
            }
        }

        internal RectangleF ToRectangleF()
        {
            return new RectangleF(X, Y, Width, Height);
        }
    }
}
