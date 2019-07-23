using System;
using System.Runtime.InteropServices;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SIZE
    {
        internal int Width;
        internal int Height;

        internal SIZE(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
