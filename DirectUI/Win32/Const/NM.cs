using System;

namespace DirectUI.Win32.Const
{
    /// <summary>
    /// Generic WM_NOTIFY notification codes 
    /// </summary>
    internal static class NM
    {
        internal const int NM_FIRST = 0;
        internal const int NM_OUTOFMEMORY = (NM_FIRST - 1);
        internal const int NM_CLICK = (NM_FIRST - 2);    // uses NMCLICK struct
        internal const int NM_DBLCLK = (NM_FIRST - 3);
        internal const int NM_RETURN = (NM_FIRST - 4);
        internal const int NM_RCLICK = (NM_FIRST - 5);    // uses NMCLICK struct
        internal const int NM_RDBLCLK = (NM_FIRST - 6);
        internal const int NM_SETFOCUS = (NM_FIRST - 7);
        internal const int NM_KILLFOCUS = (NM_FIRST - 8);

#if (_WIN32_IE0300) //>= 0x0300
        internal const int NM_CUSTOMDRAW = (NM_FIRST-12);
        internal const int NM_HOVER = (NM_FIRST-13);
#endif

#if (_WIN32_IE0400) //>= 0x0400
        internal const int NM_NCHITTEST = (NM_FIRST-14);   // uses NMMOUSE struct
        internal const int NM_KEYDOWN  = (NM_FIRST-15);   // uses NMKEY struct
        internal const int NM_RELEASEDCAPTURE = (NM_FIRST-16);
        internal const int NM_SETCURSOR = (NM_FIRST-17);   // uses NMMOUSE struct
        internal const int NM_CHAR = (NM_FIRST-18);   // uses NMCHAR struct
#endif

#if (_WIN32_IE0401) //>= 0x0401
        internal const int NM_TOOLTIPSCREATED = (NM_FIRST-19);   // notify of when the tooltips window is create
#endif

#if (_WIN32_IE0500) //>= 0x0500
        internal const int NM_LDOWN = (NM_FIRST-20);
        internal const int NM_RDOWN = (NM_FIRST-21);
        internal const int NM_THEMECHANGED = (NM_FIRST-22);
#endif
    }
}
