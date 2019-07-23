using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DirectUI.Win32.Struct
{

    [StructLayout(LayoutKind.Sequential)]
    internal struct OFNOTIFY
    {
        internal NMHDR hdr;
        internal IntPtr OpenFileName;
        internal IntPtr fileNameShareViolation;
    }
}
