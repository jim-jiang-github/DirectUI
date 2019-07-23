using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct KEYBOARDHOOKSTRUCT
    {
        internal int vkCode;   //表示一个在1到254间的虚似键盘码 
        internal int scanCode;   //表示硬件扫描码 
        internal int flags;
        internal int time;
        internal int dwExtraInfo;
    }
}
