using System;
using System.Runtime.InteropServices;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct MOUSEHOOKSTRUCT
    {
        internal POINT Pt;
        internal IntPtr hwnd;
        internal uint wHitTestCode;
        internal IntPtr dwExtraInfo;
    }
}
