using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectUI.Win32.Const
{
    internal static class HEAP
    {
        internal const int HEAP_COMPATIBILITY_INFORMATION = 0;
        internal const uint HEAP_NO_SERIALIZE = 0x00000001;
        internal const uint HEAP_GROWABLE = 0x00000002;
        internal const uint HEAP_GENERATE_EXCEPTIONS = 0x00000004;
        internal const uint HEAP_ZERO_MEMORY = 0x00000008;
        internal const uint HEAP_REALLOC_IN_PLACE_ONLY = 0x00000010;
        internal const uint HEAP_TAIL_CHECKING_ENABLED = 0x00000020;
        internal const uint HEAP_FREE_CHECKING_ENABLED = 0x00000040;
        internal const uint HEAP_DISABLE_COALESCE_ON_FREE = 0x00000080;
        internal const uint HEAP_CREATE_ALIGN_16 = 0x00010000;
        internal const uint HEAP_CREATE_ENABLE_TRACING = 0x00020000;
        internal const uint HEAP_MAXIMUM_TAG = 0x0FFF;
        internal const uint HEAP_PSEUDO_TAG_FLAG = 0x8000;
        internal const uint HEAP_TAG_SHIFT = 18;
    }
}
