using System;
using System.Runtime.InteropServices;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct NMTTDISPINFO
    {
        internal NMTTDISPINFO(int flags)
        {
            this.hdr = new NMHDR(0);
            this.lpszText = IntPtr.Zero;
            this.szText = IntPtr.Zero;
            this.hinst = IntPtr.Zero;
            this.uFlags = 0;
            this.lParam = IntPtr.Zero;
        }

        internal NMHDR hdr;
        internal IntPtr lpszText;
        internal IntPtr szText;
        internal IntPtr hinst;
        internal int uFlags;
        internal IntPtr lParam;
    }
}
