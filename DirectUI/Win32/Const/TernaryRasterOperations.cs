using System;

namespace DirectUI.Win32.Const
{
    internal class TernaryRasterOperations
    {
        private TernaryRasterOperations() { }

        internal const int SRCCOPY = 0x00CC0020; /* dest = source*/
        internal const int SRCPAINT = 0x00EE0086; /* dest = source OR dest*/
        internal const int SRCAND = 0x008800C6; /* dest = source AND dest*/
        internal const int SRCINVERT = 0x00660046; /* dest = source XOR dest*/
        internal const int SRCERASE = 0x00440328; /* dest = source AND (NOT dest )*/
        internal const int NOTSRCCOPY = 0x00330008; /* dest = (NOT source)*/
        internal const int NOTSRCERASE = 0x001100A6; /* dest = (NOT src) AND (NOT dest) */
        internal const int MERGECOPY = 0x00C000CA; /* dest = (source AND pattern)*/
        internal const int MERGEPAINT = 0x00BB0226; /* dest = (NOT source) OR dest*/
        internal const int PATCOPY = 0x00F00021; /* dest = pattern*/
        internal const int PATPAINT = 0x00FB0A09; /* dest = DPSnoo*/
        internal const int PATINVERT = 0x005A0049; /* dest = pattern XOR dest*/
        internal const int DSTINVERT = 0x00550009; /* dest = (NOT dest)*/
        internal const int BLACKNESS = 0x00000042; /* dest = BLACK*/
        internal const int WHITENESS = 0x00FF0062; /* dest = WHITE*/
    }
}
