using System;
using System.Runtime.InteropServices;

namespace DirectUI.Win32.Const
{
    internal static class TTM
    {
        internal const int WM_USER = 0x400;
        internal const int TTM_ACTIVATE = (WM_USER + 1);
        internal const int TTM_SETDELAYTIME = (WM_USER + 3);
        internal const int TTM_RELAYEVENT = (WM_USER + 7);
        internal const int TTM_GETTOOLCOUNT = (WM_USER + 13);
        internal const int TTM_WINDOWFROMPOINT = (WM_USER + 16);
        internal const int TTM_TRACKACTIVATE = (WM_USER + 17);
        internal const int TTM_TRACKPOSITION = (WM_USER + 18);
        internal const int TTM_SETTIPBKCOLOR = (WM_USER + 19);
        internal const int TTM_SETTIPTEXTCOLOR = (WM_USER + 20);
        internal const int TTM_GETDELAYTIME = (WM_USER + 21);
        internal const int TTM_GETTIPBKCOLOR = (WM_USER + 22);
        internal const int TTM_GETTIPTEXTCOLOR = (WM_USER + 23);
        internal const int TTM_SETMAXTIPWIDTH = (WM_USER + 24);
        internal const int TTM_GETMAXTIPWIDTH = (WM_USER + 25);
        internal const int TTM_SETMARGIN = (WM_USER + 26);
        internal const int TTM_GETMARGIN = (WM_USER + 27);
        internal const int TTM_POP = (WM_USER + 28);
        internal const int TTM_UPDATE = (WM_USER + 29);
        internal const int TTM_POPUP = (WM_USER + 34);
        internal const int TTM_ADJUSTRECT = (WM_USER + 31);
        // ansi
        internal const int TTM_ADDTOOLA = (WM_USER + 4);
        internal const int TTM_DELTOOLA = (WM_USER + 5);
        internal const int TTM_NEWTOOLRECTA = (WM_USER + 6);
        internal const int TTM_GETTOOLINFOA = (WM_USER + 8);
        internal const int TTM_SETTOOLINFOA = (WM_USER + 9);
        internal const int TTM_HITTESTA = (WM_USER + 10);
        internal const int TTM_GETTEXTA = (WM_USER + 11);
        internal const int TTM_UPDATETIPTEXTA = (WM_USER + 12);
        internal const int TTM_GETCURRENTTOOLA = (WM_USER + 15);
        internal const int TTM_ENUMTOOLSA = (WM_USER + 14);
        internal const int TTM_SETTITLEA = (WM_USER + 32);
        // unicode
        internal const int TTM_ADDTOOLW = (WM_USER + 50);
        internal const int TTM_DELTOOLW = (WM_USER + 51);
        internal const int TTM_NEWTOOLRECTW = (WM_USER + 52);
        internal const int TTM_GETTOOLINFOW = (WM_USER + 53);
        internal const int TTM_SETTOOLINFOW = (WM_USER + 54);
        internal const int TTM_HITTESTW = (WM_USER + 55);
        internal const int TTM_GETTEXTW = (WM_USER + 56);
        internal const int TTM_UPDATETIPTEXTW = (WM_USER + 57);
        internal const int TTM_ENUMTOOLSW = (WM_USER + 58);
        internal const int TTM_GETCURRENTTOOLW = (WM_USER + 59);
        internal const int TTM_SETTITLEW = (WM_USER + 33);

        internal readonly static int TTM_ADDTOOL;
        internal readonly static int TTM_DELTOOL;
        internal readonly static int TTM_NEWTOOLRECT;
        internal readonly static int TTM_GETTOOLINFO;
        internal readonly static int TTM_SETTOOLINFO;
        internal readonly static int TTM_HITTEST;
        internal readonly static int TTM_GETTEXT;
        internal readonly static int TTM_UPDATETIPTEXT;
        internal readonly static int TTM_ENUMTOOLS;
        internal readonly static int TTM_GETCURRENTTOOL;
        internal readonly static int TTM_SETTITLE;

        static TTM()
        {
            bool unicode = Marshal.SystemDefaultCharSize != 1;
            if (unicode)
            {
                TTM_ADDTOOL = TTM_ADDTOOLW;
                TTM_DELTOOL = TTM_DELTOOLW;
                TTM_NEWTOOLRECT = TTM_NEWTOOLRECTW;
                TTM_GETTOOLINFO = TTM_GETTOOLINFOW;
                TTM_SETTOOLINFO = TTM_SETTOOLINFOW;
                TTM_HITTEST = TTM_HITTESTW;
                TTM_GETTEXT = TTM_GETTEXTW;
                TTM_UPDATETIPTEXT = TTM_UPDATETIPTEXTW;
                TTM_GETCURRENTTOOL = TTM_GETCURRENTTOOLW;
                TTM_ENUMTOOLS = TTM_ENUMTOOLSW;
                TTM_GETCURRENTTOOL = TTM_GETCURRENTTOOLW;
                TTM_SETTITLE = TTM_SETTITLEW;
            }
            else
            {
                TTM_ADDTOOL = TTM_ADDTOOLA;
                TTM_DELTOOL = TTM_DELTOOLA;
                TTM_NEWTOOLRECT = TTM_NEWTOOLRECTA;
                TTM_GETTOOLINFO = TTM_GETTOOLINFOA;
                TTM_SETTOOLINFO = TTM_SETTOOLINFOA;
                TTM_HITTEST = TTM_HITTESTA;
                TTM_GETTEXT = TTM_GETTEXTA;
                TTM_UPDATETIPTEXT = TTM_UPDATETIPTEXTA;
                TTM_GETCURRENTTOOL = TTM_GETCURRENTTOOLA;
                TTM_ENUMTOOLS = TTM_ENUMTOOLSA;
                TTM_GETCURRENTTOOL = TTM_GETCURRENTTOOLA;
                TTM_SETTITLE = TTM_SETTITLEA;
            }
        }
    }
}
