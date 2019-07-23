using System;
using System.Runtime.InteropServices;

namespace DirectUI.Win32.Com
{
    [ComVisible(true), StructLayout(LayoutKind.Sequential)]
    internal struct tagRECT
    {
        [MarshalAs(UnmanagedType.I4)]
        internal int Left;
        [MarshalAs(UnmanagedType.I4)]
        internal int Top;
        [MarshalAs(UnmanagedType.I4)]
        internal int Right;
        [MarshalAs(UnmanagedType.I4)]
        internal int Bottom;

        internal tagRECT(int left_, int top_, int right_, int bottom_)
        {
            Left = left_;
            Top = top_;
            Right = right_;
            Bottom = bottom_;
        }
    }
}
