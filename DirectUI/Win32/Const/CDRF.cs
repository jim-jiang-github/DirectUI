using System;

namespace DirectUI.Win32.Const
{
    /// <summary>
    /// custom draw return flags
    /// values under 0x00010000 are reserved for global custom draw values.
    /// above that are for specific controls
    /// </summary>
    internal static class CDRF
    {
        internal const int CDRF_DODEFAULT = 0x0;
        internal const int CDRF_NEWFONT = 0x2;
        internal const int CDRF_SKIPDEFAULT = 0x4;
        internal const int CDRF_NOTIFYPOSTPAINT = 0x10;
        internal const int CDRF_NOTIFYITEMDRAW = 0x20;

#if (_WIN32_IE0400) //>= 0x0400
        internal const int CDRF_NOTIFYSUBITEMDRAW = 0x20; // flags are the same, we can distinguish by context
#endif
        internal const int CDRF_NOTIFYPOSTERASE = 0x40;
        internal const int CDRF_NOTIFYITEMERASE = 0x80;
    }
}
