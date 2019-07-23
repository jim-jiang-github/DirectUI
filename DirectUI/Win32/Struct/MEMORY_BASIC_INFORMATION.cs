using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct MEMORY_BASIC_INFORMATION
    {
        internal void* BaseAddress;
        internal void* AllocationBase;
        internal uint AllocationProtect;
        internal UIntPtr RegionSize;
        internal uint State;
        internal uint Protect;
        internal uint Type;
    };
}
