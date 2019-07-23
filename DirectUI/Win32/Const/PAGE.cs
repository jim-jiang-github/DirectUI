using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectUI.Win32.Const
{
    internal static class PAGE
    {
        internal const uint PAGE_NOACCESS = 0x01;
        internal const uint PAGE_READONLY = 0x02;
        internal const uint PAGE_READWRITE = 0x04;
        internal const uint PAGE_WRITECOPY = 0x08;
        internal const uint PAGE_EXECUTE = 0x10;
        internal const uint PAGE_EXECUTE_READ = 0x20;
        internal const uint PAGE_EXECUTE_READWRITE = 0x40;
        internal const uint PAGE_EXECUTE_WRITECOPY = 0x80;
        internal const uint PAGE_GUARD = 0x100;
        internal const uint PAGE_NOCACHE = 0x200;
        internal const uint PAGE_WRITECOMBINE = 0x400;
    }
}
