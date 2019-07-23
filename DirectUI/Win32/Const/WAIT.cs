using System;

namespace DirectUI.Win32.Const
{
    internal static class WAIT
    {
        internal const uint INFINITE = 0xffffffff;
        internal const uint STATUS_WAIT_0 = 0;
        internal const uint STATUS_ABANDONED_WAIT_0 = 0x80;
        internal const uint WAIT_FAILED = 0xffffffff;
        internal const uint WAIT_TIMEOUT = 258;
        internal const uint WAIT_ABANDONED = STATUS_ABANDONED_WAIT_0 + 0;
        internal const uint WAIT_OBJECT_0 = STATUS_WAIT_0 + 0;
        internal const uint WAIT_ABANDONED_0 = STATUS_ABANDONED_WAIT_0 + 0;
        internal const uint STATUS_USER_APC = 0x000000C0;
        internal const uint WAIT_IO_COMPLETION = STATUS_USER_APC;
    }
}
