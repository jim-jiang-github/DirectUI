using System;
using System.Runtime.InteropServices;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct CWPRETSTRUCT
    {
        internal IntPtr lResult;
        internal IntPtr lParam;
        internal IntPtr wParam;
        internal int message;
        internal IntPtr hwnd;
    }
}
