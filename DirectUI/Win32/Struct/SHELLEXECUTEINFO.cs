using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SHELLEXECUTEINFO
    {
        internal uint cbSize;
        internal uint fMask;
        internal IntPtr hwnd;
        [MarshalAs(UnmanagedType.LPTStr)]
        internal string lpVerb;
        [MarshalAs(UnmanagedType.LPTStr)]
        internal string lpFile;
        [MarshalAs(UnmanagedType.LPTStr)]
        internal string lpParameters;
        [MarshalAs(UnmanagedType.LPTStr)]
        internal string lpDirectory;
        internal int nShow;
        internal IntPtr hInstApp;
        internal IntPtr lpIDList;
        [MarshalAs(UnmanagedType.LPTStr)]
        internal string lpClass;
        internal IntPtr hkeyClass;
        internal uint dwHotKey;
        internal IntPtr hIcon_or_hMonitor;
        internal IntPtr hProcess;
    }
}
