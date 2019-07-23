using System;
using System.Runtime.InteropServices;


namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal class PCURSORINFO
    {
        internal int cbSize;
        internal int flag;
        internal IntPtr hCursor;
        internal POINT ptScreenPos;
    }
}
