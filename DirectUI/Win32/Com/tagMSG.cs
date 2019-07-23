using System;
using System.Runtime.InteropServices;

namespace DirectUI.Win32.Com
{
    [ComVisible(true), StructLayout(LayoutKind.Sequential)]
    internal struct tagMSG
    {
        internal IntPtr hwnd;
        [MarshalAs(UnmanagedType.I4)]
        internal int message;
        internal IntPtr wParam;
        internal IntPtr lParam;
        [MarshalAs(UnmanagedType.I4)]
        internal int time;
        // pt was a by-value POINT structure
        [MarshalAs(UnmanagedType.I4)]
        internal int pt_x;
        [MarshalAs(UnmanagedType.I4)]
        internal int pt_y;
        //internal tagPOINT pt;
    }
}
