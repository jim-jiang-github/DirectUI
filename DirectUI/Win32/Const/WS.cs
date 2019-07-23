using System;

namespace DirectUI.Win32.Const
{
    internal static class WS
    {
        internal const int WS_EX_WINDOWEDGE = 0x00000100;
        internal const int WS_EX_DLGMODALFRAME = 0x00000001;
        internal const int WS_OVERLAPPED = 0x00000000;
        internal const int WS_POPUP = unchecked((int)0x80000000);
        internal const int WS_CHILD = 0x40000000;
        internal const int WS_MINIMIZE = 0x20000000;
        internal const int WS_VISIBLE = 0x10000000;
        internal const int WS_DISABLED = 0x08000000;
        internal const int WS_CLIPSIBLINGS = 0x04000000;
        internal const int WS_CLIPCHILDREN = 0x02000000;
        internal const int WS_MAXIMIZE = 0x01000000;
        internal const int WS_CAPTION = 0x00C00000;
        internal const int WS_BORDER = 0x00800000;
        internal const int WS_DLGFRAME = 0x00400000;
        internal const int WS_VSCROLL = 0x00200000;
        internal const int WS_HSCROLL = 0x00100000;
        internal const int WS_SYSMENU = 0x00080000;
        internal const int WS_THICKFRAME = 0x00040000;
        internal const int WS_GROUP = 0x00020000;
        internal const int WS_TABSTOP = 0x00010000;
        internal const int WS_MINIMIZEBOX = 0x00020000;
        internal const int WS_MAXIMIZEBOX = 0x00010000;
        internal const int WS_TILED = WS_OVERLAPPED;
        internal const int WS_ICONIC = WS_MINIMIZE;
        internal const int WS_SIZEBOX = WS_THICKFRAME;
        internal const int WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW;
        internal const int WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU |
                                WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX);
        internal const int WS_POPUPWINDOW = (WS_POPUP | WS_BORDER | WS_SYSMENU);
        internal const int WS_CHILDWINDOW = (WS_CHILD);
        internal const int WS_EX_LAYERED = 0x00080000;
    }
}
