using System;
using System.Collections.Generic;
using System.Text;

namespace DirectUI.Win32.Const
{
    internal sealed class TME
    {
        internal const uint TME_HOVER = 1;
        internal const uint TME_LEAVE = 2;
        internal const uint TME_NONCLIENT = 0x00000010;
        internal const uint TME_QUERY = 0x40000000;
        internal const uint TME_CANCEL = 0x80000000;
    }
}
