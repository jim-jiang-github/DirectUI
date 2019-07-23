using System;
using System.Runtime.InteropServices;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct STYLESTRUCT
    {
        internal int styleOld;
        internal int styleNew;
    }
}
