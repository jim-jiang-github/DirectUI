using System;
using System.Windows.Forms;
using DirectUI.Win32.Const;
using System.Runtime.InteropServices;

namespace DirectUI.Win32
{
    internal static class Helper
    {
        internal static bool LeftKeyPressed()
        {
            if (SystemInformation.MouseButtonsSwapped)
            {
                return (NativeMethods.GetKeyState(VK.VK_RBUTTON) < 0);
            }
            else
            {
                return (NativeMethods.GetKeyState(VK.VK_LBUTTON) < 0);
            }
        }

        internal static int HIWORD(int n)
        {
            return ((n >> 0x10) & 0xffff);
        }

        internal static int HIWORD(IntPtr n)
        {
            return HIWORD((int)((long)n));
        }

        internal static int LOWORD(int n)
        {
            return (n & 0xffff);
        }

        internal static int LOWORD(IntPtr n)
        {
            return LOWORD((int)((long)n));
        }

        internal static int MAKELONG(int low, int high)
        {
            return ((high << 0x10) | (low & 0xffff));
        }

        internal static IntPtr MAKELPARAM(int low, int high)
        {
            return (IntPtr)((high << 0x10) | (low & 0xffff));
        }

        internal static int SignedHIWORD(int n)
        {
            return (short)((n >> 0x10) & 0xffff);
        }

        internal static int SignedHIWORD(IntPtr n)
        {
            return SignedHIWORD((int)((long)n));
        }
        internal static int SignedHIWORD(long n)
        {
            return SignedHIWORD((int)n);
        }

        internal static int SignedLOWORD(int n)
        {
            return (short)(n & 0xffff);
        }

        internal static int SignedLOWORD(IntPtr n)
        {
            return SignedLOWORD((int)((long)n));
        }

        internal static int SignedLOWORD(long n)
        {
            return SignedLOWORD((int)n);
        }

        internal static void Swap(ref int x, ref int y)
        {
            int tmp = x;
            x = y;
            y = tmp;
        }

        internal static IntPtr ToIntPtr(object structure)
        {
            IntPtr lparam = IntPtr.Zero;
            lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(structure));
            Marshal.StructureToPtr(structure, lparam, false);
            return lparam;
        }

        internal static void SetRedraw(IntPtr hWnd, bool redraw)
        {
            IntPtr ptr = redraw ? Result.TRUE : Result.FALSE;
            NativeMethods.SendMessage(hWnd, WM.WM_SETREDRAW, ptr, 0);
        }
    }
}
