using System;

namespace DirectUI.Win32.Const
{
    internal class SBM
    {
        private SBM() { }

        internal const int SBM_SETPOS = 0x00E0;
        internal const int SBM_GETPOS = 0x00E1;
        internal const int SBM_SETRANGE = 0x00E2;
        internal const int SBM_SETRANGEREDRAW = 0x00E6;
        internal const int SBM_GETRANGE = 0x00E3;
        internal const int SBM_ENABLE_ARROWS = 0x00E4;
        internal const int SBM_SETSCROLLINFO = 0x00E9;
        internal const int SBM_GETSCROLLINFO = 0x00EA;
        internal const int SBM_GETSCROLLBARINFO = 0x00EB;
    }
}
