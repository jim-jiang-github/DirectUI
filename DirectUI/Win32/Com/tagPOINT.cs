using System;
using System.Runtime.InteropServices;

namespace DirectUI.Win32.Com
{
    [ComVisible(true), StructLayout(LayoutKind.Sequential)]
    internal struct tagPOINT
    {
        [MarshalAs(UnmanagedType.I4)]
        internal int X;
        [MarshalAs(UnmanagedType.I4)]
        internal int Y;
    }
}
