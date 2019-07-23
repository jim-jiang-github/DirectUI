using System;
using System.Collections.Generic;
using System.Text;

namespace DirectUI.Win32.Const
{
    /// <summary>
    /// trackbar messages.
    /// </summary>
    internal static class TBM
    {
        private const int WM_USER = 0x0400;

        internal const int TBM_GETRANGEMIN = (WM_USER + 1);
        internal const int TBM_GETRANGEMAX = (WM_USER + 2);
        internal const int TBM_GETTIC = (WM_USER + 3);
        internal const int TBM_SETTIC = (WM_USER + 4);
        internal const int TBM_SETPOS = (WM_USER + 5);
        internal const int TBM_SETRANGE = (WM_USER + 6);
        internal const int TBM_SETRANGEMIN = (WM_USER + 7);
        internal const int TBM_SETRANGEMAX = (WM_USER + 8);
        internal const int TBM_CLEARTICS = (WM_USER + 9);
        internal const int TBM_SETSEL = (WM_USER + 10);
        internal const int TBM_SETSELSTART = (WM_USER + 11);
        internal const int TBM_SETSELEND = (WM_USER + 12);
        internal const int TBM_GETPTICS = (WM_USER + 14);
        internal const int TBM_GETTICPOS = (WM_USER + 15);
        internal const int TBM_GETNUMTICS = (WM_USER + 16);
        internal const int TBM_GETSELSTART = (WM_USER + 17);
        internal const int TBM_GETSELEND = (WM_USER + 18);
        internal const int TBM_CLEARSEL = (WM_USER + 19);
        internal const int TBM_SETTICFREQ = (WM_USER + 20);
        internal const int TBM_SETPAGESIZE = (WM_USER + 21);
        internal const int TBM_GETPAGESIZE = (WM_USER + 22);
        internal const int TBM_SETLINESIZE = (WM_USER + 23);
        internal const int TBM_GETLINESIZE = (WM_USER + 24);
        internal const int TBM_GETTHUMBRECT = (WM_USER + 25);
        internal const int TBM_GETCHANNELRECT = (WM_USER + 26);
        internal const int TBM_SETTHUMBLENGTH = (WM_USER + 27);
        internal const int TBM_GETTHUMBLENGTH = (WM_USER + 28);
    }
}
