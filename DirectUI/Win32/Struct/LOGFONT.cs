using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class LOGFONT
    {
        internal int lfHeight = 0;
        internal int lfWidth = 0;
        internal int lfEscapement = 0;
        internal int lfOrientation = 0;
        internal int lfWeight = 0;
        internal byte lfItalic = 0;
        internal byte lfUnderline = 0;
        internal byte lfStrikeOut = 0;
        internal byte lfCharSet = 0;
        internal byte lfOutPrecision = 0;
        internal byte lfClipPrecision = 0;
        internal byte lfQuality = 0;
        internal byte lfPitchAndFamily = 0;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        internal string lfFaceName = string.Empty;
    }
}
