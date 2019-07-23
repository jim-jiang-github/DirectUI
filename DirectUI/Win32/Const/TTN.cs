using System;
using System.Runtime.InteropServices;

namespace DirectUI.Win32.Const
{
    internal static class TTN
    {
        // ownerdraw
        internal const int TTN_FIRST = (-520);

        internal const int TTN_GETDISPINFOA = (TTN_FIRST - 0);
        internal const int TTN_GETDISPINFOW = (TTN_FIRST - 10);
        internal const int TTN_SHOW = (TTN_FIRST - 1);
        internal const int TTN_POP = (TTN_FIRST - 2);
        internal const int TTN_LINKCLICK = (TTN_FIRST - 3);

        internal const int TTN_NEEDTEXTA = TTN_GETDISPINFOA;
        internal const int TTN_NEEDTEXTW = TTN_GETDISPINFOW;

        internal const int TTN_LAST = (-549);

        internal readonly static int TTN_GETDISPINFO;
        internal readonly static int TTN_NEEDTEXT;

        static TTN()
        {
            bool unicode = Marshal.SystemDefaultCharSize != 1;
            if (unicode)
            {
                TTN_GETDISPINFO = TTN_GETDISPINFOW;
                TTN_NEEDTEXT = TTN_NEEDTEXTW;
            }
            else
            {
                TTN_GETDISPINFO = TTN_GETDISPINFOA;
                TTN_NEEDTEXT = TTN_NEEDTEXTA;
            }
        }
    }
}
