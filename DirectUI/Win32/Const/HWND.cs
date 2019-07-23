using System;

namespace DirectUI.Win32.Const
{
    /// <summary>
    /// Use for setwindowpos.
    /// </summary>
    internal static class HWND
    {
        internal static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        internal static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        internal static readonly IntPtr HWND_TOP = new IntPtr(0);
        internal static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
    }
}
