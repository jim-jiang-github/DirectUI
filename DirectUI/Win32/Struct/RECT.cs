using System;
using System.Runtime.InteropServices;
using System.Drawing;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct RECT
    {
        internal int Left;
        internal int Top;
        internal int Right;
        internal int Bottom;

        internal RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        internal RECT(Rectangle rect)
        {
            Left = rect.Left;
            Top = rect.Top;
            Right = rect.Right;
            Bottom = rect.Bottom;
        }

        internal Rectangle Rect
        {
            get
            {
                return new Rectangle(
                    Left,
                    Top,
                    Right - Left,
                    Bottom - Top);
            }
        }

        internal Size Size
        {
            get
            {
                return new Size(Right - Left, Bottom - Top);
            }
        }

        internal static RECT FromXYWH(
            int x, int y, int width, int height)
        {
            return new RECT(x,
                            y,
                            x + width,
                            y + height);
        }

        internal static RECT FromRectangle(Rectangle rect)
        {
            return new RECT(rect.Left,
                             rect.Top,
                             rect.Right,
                             rect.Bottom);
        }

        internal uint Width
        {
            get { return (uint)Math.Abs(Right - Left); }
            set { Right = Left + (int)value; }
        }

        internal uint Height
        {
            get { return (uint)Math.Abs(Bottom - Top); }
            set { Bottom = Top + (int)value; }
        }
    }
}
