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
    ///   这个类可以让你得到一个在运行中程序的所有鼠标事件 
    ///   并且引发一个带MouseEventArgs参数的.NET鼠标事件以便你很容易使用这些信息 
    ///   </summary> 
    internal class MouseHook
    {
        //全局的事件 
        public event MouseEventHandler OnMouseActivity;
        static IntPtr hMouseHook = IntPtr.Zero;   //鼠标钩子句柄 
        HookProc MouseHookProcedure;   //声明鼠标钩子事件类型. 
        ///   <summary> 
        ///   墨认的构造函数构造当前类的实例. 
        ///   </summary> 
        public MouseHook()
        {
            //Start(); 
        }
        //析构函数. 
        ~MouseHook()
        {
            Stop();
        }
        public void Start()
        {
            //安装鼠标钩子 
            if (hMouseHook == IntPtr.Zero)
            {
                //生成一个HookProc的实例. 
                MouseHookProcedure = new HookProc(MouseHookProc);
                hMouseHook = Win32.NativeMethods.SetWindowsHookEx(Win32.Const.WH.WH_MOUSE_LL, MouseHookProcedure, (int)Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().ManifestModule), 0);
                //如果装置失败停止钩子 
                if (hMouseHook == IntPtr.Zero)
                {
                    Stop();
                    throw new Exception("SetWindowsHookEx failed.");
                }
            }
        }
        public void Stop()
        {
            bool retMouse = true;
            if (hMouseHook != IntPtr.Zero)
            {
                retMouse = Win32.NativeMethods.UnhookWindowsHookEx(hMouseHook);
                hMouseHook = IntPtr.Zero;
            }
            //如果卸下钩子失败 
            if (!(retMouse)) throw new Exception("UnhookWindowsHookEx failed.");
        }
        private int MouseHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            //如果正常运行并且用户要监听鼠标的消息 
            if ((nCode >= 0) && (OnMouseActivity != null))
            {
                MouseButtons button = MouseButtons.None;
                int clickCount = 0;
                switch (wParam)
                {
                    case Win32.Const.WM.WM_LBUTTONDOWN:
                        button = MouseButtons.Left;
                        clickCount = 1;
                        break;
                    case Win32.Const.WM.WM_LBUTTONUP:
                        button = MouseButtons.Left;
                        clickCount = 1;
                        break;
                    case Win32.Const.WM.WM_LBUTTONDBLCLK:
                        button = MouseButtons.Left;
                        clickCount = 2;
                        break;
                    case Win32.Const.WM.WM_RBUTTONDOWN:
                        button = MouseButtons.Right;
                        clickCount = 1;
                        break;
                    case Win32.Const.WM.WM_RBUTTONUP:
                        button = MouseButtons.Right;
                        clickCount = 1;
                        break;
                    case Win32.Const.WM.WM_RBUTTONDBLCLK:
                        button = MouseButtons.Right;
                        clickCount = 2;
                        break;
                }
                //从回调函数中得到鼠标的信息 
                MOUSEHOOKSTRUCT MyMOUSEHOOKSTRUCT = (MOUSEHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MOUSEHOOKSTRUCT));
                MouseEventArgs e = new MouseEventArgs(button, clickCount, MyMOUSEHOOKSTRUCT.Pt.X, MyMOUSEHOOKSTRUCT.Pt.Y, 0);
                OnMouseActivity(this, e);
            }
            return Win32.NativeMethods.CallNextHookEx((int)hMouseHook, nCode, wParam, lParam);
        }
    }
}