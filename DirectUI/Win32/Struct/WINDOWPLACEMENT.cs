using System;
using System.Runtime.InteropServices;
using System.Drawing;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct WINDOWPLACEMENT
    {
        internal int length;
        internal int flags;
        internal int showCmd;
        internal Point ptMinPosition;
        internal Point ptMaxPosition;
        internal RECT rcNormalPosition;
        internal static WINDOWPLACEMENT Default
        {
            get
            {
                WINDOWPLACEMENT structure = new WINDOWPLACEMENT();
                structure.length = Marshal.SizeOf(structure);
                return structure;
            }
        }
    }
}
