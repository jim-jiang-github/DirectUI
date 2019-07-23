using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct MINMAXINFO
    {
        internal Point reserved;
        internal Size maxSize;
        internal Point maxPosition;
        internal Size minTrackSize;
        internal Size maxTrackSize;
    }
}
