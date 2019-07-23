using System;
using System.Drawing;
using System.Runtime.InteropServices;
using DirectUI.Win32.Const;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct TCHITTESTINFO
    {

        internal TCHITTESTINFO(Point location)
        {
            Point = location;
            Flags = TCHT.TCHT_ONITEM;
        }

        internal Point Point;
        internal int Flags;
    }
}
