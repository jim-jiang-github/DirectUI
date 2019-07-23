using System;
using System.Collections.Generic;
using System.Text;

namespace DirectUI.Win32.Const
{
    internal static class CS
    {
        internal const int CS_VREDRAW = 0x0001;
        internal const int CS_HREDRAW = 0x0002;
        internal const int CS_DBLCLKS = 0x0008;
        internal const int CS_OWNDC = 0x0020;
        internal const int CS_CLASSDC = 0x0040;
        internal const int CS_PARENTDC = 0x0080;
        internal const int CS_NOCLOSE = 0x0200;
        internal const int CS_SAVEBITS = 0x0800;
        internal const int CS_BYTEALIGNCLIENT = 0x1000;
        internal const int CS_BYTEALIGNWINDOW = 0x2000;
        internal const int CS_GLOBALCLASS = 0x4000;

        internal const int CS_IME = 0x00010000;
        //#if(_WIN32_WINNT >= 0x0501)
        internal const int CS_DROPSHADOW = 0x00020000;
    }
}
