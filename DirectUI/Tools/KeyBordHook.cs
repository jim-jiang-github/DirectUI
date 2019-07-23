using DirectUI.Win32.Callback;
using DirectUI.Win32.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace DirectUI
{
    ///   <summary> 
    ///   这个类可以让你得到一个在运行中程序的所有键盘或鼠标事件 
    ///   并且引发一个带KeyEventArgs参数的.NET事件以便你很容易使用这些信息 
    ///   </summary>
    internal class KeyBordHook
    {
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;
        private const int WM_SYSKEYDOWN = 0x104;
        private const int WM_SYSKEYUP = 0x105;
        //全局的事件 
        public event KeyEventHandler OnKeyDownEvent;
        public event KeyEventHandler OnKeyUpEvent;
        public event KeyPressEventHandler OnKeyPressEvent;
        static IntPtr hKeyboardHook = IntPtr.Zero;   //键盘钩子句柄 
        HookProc KeyboardHookProcedure;   //声明键盘钩子事件类型. 
        ///   <summary> 
        ///   墨认的构造函数构造当前类的实例并自动的运行起来. 
        ///   </summary> 
        public KeyBordHook()
        {
            Start();
        }
        //析构函数. 
        ~KeyBordHook()
        {
            Stop();
        }
        public void Start()
        {
            //安装键盘钩子   
            if (hKeyboardHook == IntPtr.Zero)
            {
                KeyboardHookProcedure = new HookProc(KeyboardHookProc);
                hKeyboardHook = Win32.NativeMethods.SetWindowsHookEx(Win32.Const.WH.WH_KEYBOARD_LL, KeyboardHookProcedure, (int)Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().ManifestModule), 0);
                if (hKeyboardHook == IntPtr.Zero)
                {
                    Stop();
                    throw new Exception("SetWindowsHookEx is failed.");
                }
            }
        }
        public void Stop()
        {
            bool retKeyboard = true;
            if (hKeyboardHook != IntPtr.Zero)
            {
                retKeyboard = Win32.NativeMethods.UnhookWindowsHookEx(hKeyboardHook);
                hKeyboardHook = IntPtr.Zero;
            }
            //如果卸下钩子失败 
            if (!(retKeyboard)) throw new Exception("UnhookWindowsHookEx failed.");
        }
        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if ((nCode >= 0) && (OnKeyDownEvent != null || OnKeyUpEvent != null || OnKeyPressEvent != null))
            {
                KEYBOARDHOOKSTRUCT MyKEYBOARDHOOKSTRUCT = (KEYBOARDHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KEYBOARDHOOKSTRUCT));
                //引发OnKeyDownEvent 
                if (OnKeyDownEvent != null && (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN))
                {
                    Keys keyData = (Keys)MyKEYBOARDHOOKSTRUCT.vkCode;
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    OnKeyDownEvent(this, e);
                }
                //引发OnKeyPressEvent 
                if (OnKeyPressEvent != null && wParam == WM_KEYDOWN)
                {
                    byte[] keyState = new byte[256];
                    Win32.NativeMethods.GetKeyboardState(keyState);
                    byte[] inBuffer = new byte[2];
                    if (Win32.NativeMethods.ToAscii(MyKEYBOARDHOOKSTRUCT.vkCode,
                      MyKEYBOARDHOOKSTRUCT.scanCode,
                      keyState,
                      inBuffer,
                      MyKEYBOARDHOOKSTRUCT.flags) == 1)
                    {
                        KeyPressEventArgs e = new KeyPressEventArgs((char)inBuffer[0]);
                        OnKeyPressEvent(this, e);
                    }
                }
                //引发OnKeyUpEvent 
                if (OnKeyUpEvent != null && (wParam == WM_KEYUP || wParam == WM_SYSKEYUP))
                {
                    Keys keyData = (Keys)MyKEYBOARDHOOKSTRUCT.vkCode;
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    OnKeyUpEvent(this, e);
                }
            }
            return Win32.NativeMethods.CallNextHookEx((int)hKeyboardHook, nCode, wParam, lParam);
        }
    }
}
