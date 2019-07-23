using System;

namespace DirectUI.Win32.Const
{
    internal class CombineRgnStyles
    {
        private CombineRgnStyles() { }

        internal const int RGN_AND = 1;
        internal const int RGN_OR = 2;
        internal const int RGN_XOR = 3;
        internal const int RGN_DIFF = 4;
        internal const int RGN_COPY = 5;
        internal const int RGN_MIN = RGN_AND;
        internal const int RGN_MAX = RGN_COPY;
    }
}
