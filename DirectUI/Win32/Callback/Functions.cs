using System;
using System.Collections.Generic;
using System.Text;

namespace DirectUI.Win32.Callback
{
    internal delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);
}
