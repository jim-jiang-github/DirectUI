using System;

namespace DirectUI.Win32.Const
{
    internal class SIF
    {
        internal const int SIF_RANGE = 0x1;
        internal const int SIF_PAGE = 0x2;
        internal const int SIF_POS = 0x4;
        internal const int SIF_DISABLENOSCROLL = 0x8;
        internal const int SIF_TRACKPOS = 0x10;
        internal const int SIF_ALL = SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS;
    }
}
