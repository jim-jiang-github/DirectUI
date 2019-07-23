using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct RGNDATAHEADER
    {
        internal uint dwSize;
        internal uint iType;
        internal uint nCount;
        internal uint nRgnSize;
        internal RECT rcBound;
    };
}
