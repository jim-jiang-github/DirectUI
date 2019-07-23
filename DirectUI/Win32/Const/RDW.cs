using System;

namespace DirectUI.Win32.Const
{
    internal class RDW
    {
        private RDW() {}

        internal const int RDW_INVALIDATE = 0x0001;
        internal const int RDW_INTERNALPAINT = 0x0002;
        internal const int RDW_ERASE = 0x0004;
        internal const int RDW_VALIDATE = 0x0008;
        internal const int RDW_NOINTERNALPAINT = 0x0010;
        internal const int RDW_NOERASE = 0x0020;
        internal const int RDW_NOCHILDREN = 0x0040;
        internal const int RDW_ALLCHILDREN = 0x0080;
        internal const int RDW_UPDATENOW = 0x0100;
        internal const int RDW_ERASENOW = 0x0200;
        internal const int RDW_FRAME = 0x0400;
        internal const int RDW_NOFRAME = 0x0800;
    }
}
