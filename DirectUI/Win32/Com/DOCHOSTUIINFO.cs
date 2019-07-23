using System;
using System.Runtime.InteropServices;

namespace DirectUI.Win32.Com
{
    [ComVisible(true), StructLayout(LayoutKind.Sequential)]
    internal struct DOCHOSTUIINFO
    {
        [MarshalAs(UnmanagedType.U4)]
        internal uint cbSize;
        [MarshalAs(UnmanagedType.U4)]
        internal uint dwFlags;
        [MarshalAs(UnmanagedType.U4)]
        internal uint dwDoubleClick;
        [MarshalAs(UnmanagedType.LPWStr)]
        internal string pchHostCss;
        [MarshalAs(UnmanagedType.LPWStr)]
        internal string pchHostNS;
    }
}
