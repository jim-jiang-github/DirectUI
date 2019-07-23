using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct PAINTSTRUCT
    {
        internal IntPtr hdc;
        internal bool fErase;
        // rcPaint was a by-value RECT structure
        internal int rcPaint_left;
        internal int rcPaint_top;
        internal int rcPaint_right;
        internal int rcPaint_bottom;
        internal bool fRestore;
        internal bool fIncUpdate;
        internal int reserved1;
        internal int reserved2;
        internal int reserved3;
        internal int reserved4;
        internal int reserved5;
        internal int reserved6;
        internal int reserved7;
        internal int reserved8;
    }
}
