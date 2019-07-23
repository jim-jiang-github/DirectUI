using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectUI.Win32.Const
{
    internal enum CDN : uint
    {
        CDN_FIRST = 0xFFFFFDA7,
        CDN_INITDONE = (CDN_FIRST - 0x0000),
        CDN_SELCHANGE = (CDN_FIRST - 0x0001),
        CDN_FOLDERCHANGE = (CDN_FIRST - 0x0002),
        CDN_SHAREVIOLATION = (CDN_FIRST - 0x0003),
        CDN_HELP = (CDN_FIRST - 0x0004),
        CDN_FILEOK = (CDN_FIRST - 0x0005),
        CDN_TYPECHANGE = (CDN_FIRST - 0x0006),
        CDN_INCLUDEITEM = (CDN_FIRST - 0x0007)
    }
}
