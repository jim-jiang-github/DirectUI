using System;

namespace DirectUI.Win32.Const
{
    internal static class TCHT
    {
        internal const int TCHT_NOWHERE = 1;
        internal const int TCHT_ONITEMICON = 2;
        internal const int TCHT_ONITEMLABEL = 4;
        internal const int TCHT_ONITEM = TCHT_ONITEMICON | TCHT_ONITEMLABEL;
    }
}
