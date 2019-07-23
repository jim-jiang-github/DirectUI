using System;
using System.Runtime.InteropServices;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct NMHDR
    {
        internal NMHDR(int flag)
        {
            this.hwndFrom = IntPtr.Zero;
            this.idFrom = 0;
            this.code = 0;
        }

        internal IntPtr hwndFrom;
        internal int idFrom;
        internal int code;
    }
}
