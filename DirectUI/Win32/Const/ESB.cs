using System;

namespace DirectUI.Win32.Const
{
    internal class ESB
    {
        private ESB() { }

        internal const int ESB_ENABLE_BOTH = 0x0000;
        internal const int ESB_DISABLE_BOTH = 0x0003;

        internal const int ESB_DISABLE_LEFT = 0x0001;
        internal const int ESB_DISABLE_RIGHT = 0x0002;

        internal const int ESB_DISABLE_UP = 0x0001;
        internal const int ESB_DISABLE_DOWN = 0x0002;

        internal const int ESB_DISABLE_LTUP = ESB_DISABLE_LEFT;
        internal const int ESB_DISABLE_RTDN = ESB_DISABLE_RIGHT;
    }
}
