using System;

namespace DirectUI.Win32.Const
{
    internal static class WS_EX
    {
        internal const int WS_EX_DLGMODALFRAME = 0x00000001;
        internal const int WS_EX_NOPARENTNOTIFY = 0x00000004;
        internal const int WS_EX_TOPMOST = 0x00000008;
        internal const int WS_EX_ACCEPTFILES = 0x00000010;
        internal const int WS_EX_TRANSPARENT = 0x00000020;
        internal const int WS_EX_MDICHILD = 0x00000040;
        internal const int WS_EX_TOOLWINDOW = 0x00000080;
        internal const int WS_EX_WINDOWEDGE = 0x00000100;
        internal const int WS_EX_CLIENTEDGE = 0x00000200;
        internal const int WS_EX_CONTEXTHELP = 0x00000400;
        internal const int WS_EX_RIGHT = 0x00001000;
        internal const int WS_EX_LEFT = 0x00000000;
        internal const int WS_EX_RTLREADING = 0x00002000;
        internal const int WS_EX_LTRREADING = 0x00000000;
        internal const int WS_EX_LEFTSCROLLBAR = 0x00004000;
        internal const int WS_EX_RIGHTSCROLLBAR = 0x00000000;
        internal const int WS_EX_CONTROLPARENT = 0x00010000;
        internal const int WS_EX_STATICEDGE = 0x00020000;
        internal const int WS_EX_APPWINDOW = 0x00040000;
        internal const int WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);
        internal const int WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);
        internal const int WS_EX_LAYERED = 0x00080000;
        internal const int WS_EX_NOINHERITLAYOUT = 0x00100000; // Disable inheritence of mirroring by children
        internal const int WS_EX_LAYOUTRTL = 0x00400000; // Right to left mirroring
        internal const int WS_EX_COMPOSITED = 0x02000000;
        internal const int WS_EX_NOACTIVATE = 0x08000000;
    }
}
