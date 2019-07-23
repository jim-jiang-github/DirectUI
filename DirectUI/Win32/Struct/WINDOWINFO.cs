using System;
using System.Runtime.InteropServices;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct WINDOWINFO
    {
        internal UInt32 cbSize;
        internal RECT rcWindow;
        internal RECT rcClient;
        internal UInt32 dwStyle;
        internal UInt32 dwExStyle;
        internal UInt32 dwWindowStatus;
        internal UInt32 cxWindowBorders;
        internal UInt32 cyWindowBorders;
        internal IntPtr atomWindowType;
        internal UInt16 wCreatorVersion;
    }
}
