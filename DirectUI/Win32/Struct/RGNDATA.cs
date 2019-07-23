using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct RGNDATA
    {
        internal RGNDATAHEADER rdh;

        internal unsafe static RECT* GetRectsPointer(RGNDATA* me)
        {
            return (RECT*)((byte*)me + sizeof(RGNDATAHEADER));
        }
    }
}
