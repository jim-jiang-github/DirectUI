using System;
using System.Runtime.InteropServices;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SCROLLBARINFO
    {
        internal int cbSize;
        internal RECT rcScrollBar;
        internal int dxyLineButton;
        internal int xyThumbTop;
        internal int xyThumbBottom;
        internal int reserved;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        internal int[] rgstate;
    }
}
