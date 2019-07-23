using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct API_MSG
    {
        internal IntPtr Hwnd;
        internal int Msg;
        internal IntPtr WParam;
        internal IntPtr LParam;
        internal int Time;
        internal POINT Pt;

        internal Message ToMessage()
        {
            Message res = new Message();
            res.HWnd = Hwnd;
            res.Msg = Msg;
            res.WParam = WParam;
            res.LParam = LParam;
            return res;
        }

        internal void FromMessage(ref Message msg)
        {
            Hwnd = msg.HWnd;
            Msg = msg.Msg;
            WParam = msg.WParam;
            LParam = msg.LParam;
        }
    }
}
