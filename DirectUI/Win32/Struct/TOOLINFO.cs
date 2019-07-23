using System;
using System.Runtime.InteropServices;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct TOOLINFO
    {
        internal TOOLINFO(int flags)
        {
            this.cbSize = Marshal.SizeOf(typeof(TOOLINFO));
            this.uFlags = flags;
            this.hwnd = IntPtr.Zero;
            this.uId = IntPtr.Zero;
            this.rect = new RECT(0, 0, 0, 0);
            this.hinst = IntPtr.Zero;
            this.lpszText = IntPtr.Zero;
            this.lParam = IntPtr.Zero;
        }

        internal int cbSize;
        internal int uFlags;
        internal IntPtr hwnd;
        internal IntPtr uId;
        internal RECT rect;
        internal IntPtr hinst;
        internal IntPtr lpszText;
        internal IntPtr lParam;
    }
}
