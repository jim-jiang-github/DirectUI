using System;
using System.Runtime.InteropServices;

namespace DirectUI.Win32.Com
{
    [ComVisible(true), StructLayout(LayoutKind.Sequential)]
    internal struct tagOLECMD
    {
        [MarshalAs(UnmanagedType.U4)]
        internal uint cmdID;
        [MarshalAs(UnmanagedType.U4)]
        internal uint cmdf;
    }
}
