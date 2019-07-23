using System;

namespace DirectUI.Win32.Const
{
    /// <summary>
    /// WM_NCCALCSIZE "window valid rect" return values.
    /// </summary>
    internal class WVR
    {
        internal const int WVR_ALIGNTOP = 0x0010;
        internal const int WVR_ALIGNLEFT = 0x0020;
        internal const int WVR_ALIGNBOTTOM = 0x0040;
        internal const int WVR_ALIGNRIGHT = 0x0080;
        internal const int WVR_HREDRAW = 0x0100;
        internal const int WVR_VREDRAW = 0x0200;
        internal const int WVR_REDRAW = (WVR_HREDRAW | WVR_VREDRAW);
        internal const int WVR_VALIDRECTS = 0x0400;
    }
}
