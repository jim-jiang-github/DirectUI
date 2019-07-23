using System;
using System.Runtime.InteropServices;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal class MOUSEHOOKSTRUCTEX
    {
        internal MOUSEHOOKSTRUCT Mouse;
        internal int mouseData;
    }
}
