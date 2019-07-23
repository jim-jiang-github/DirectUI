using System;
using System.Runtime.InteropServices;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct NCCALCSIZE_PARAMS
    {
        internal RECT rcNewWindow;     //Proposed New Window Coordinates
        internal RECT rcOldWindow;     //Original Window Coordinates (before resize/move)
        internal RECT rcClient;     //Original Client Area (before resize/move)
        internal IntPtr lppos;
    }
}
