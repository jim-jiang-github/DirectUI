using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectUI.Win32.Const
{
    internal static class MEM
    {
        internal const uint MEM_COMMIT = 0x1000;
        internal const uint MEM_RESERVE = 0x2000;
        internal const uint MEM_DECOMMIT = 0x4000;
        internal const uint MEM_RELEASE = 0x8000;
        internal const uint MEM_RESET = 0x80000;
        internal const uint MEM_TOP_DOWN = 0x100000;
        internal const uint MEM_PHYSICAL = 0x400000;
    }
}
