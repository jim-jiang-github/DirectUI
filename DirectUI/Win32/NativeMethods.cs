using System;
using System.Runtime.InteropServices;
using DirectUI.Win32.Struct;
using DirectUI.Win32.Callback;
using System.Windows.Forms;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.ComponentModel;
using System.Runtime.Versioning;
using System.Drawing.Drawing2D;

namespace DirectUI.Win32
{
    internal class NativeMethods
    {
        internal static HandleRef NullHandleRef = new HandleRef(null, IntPtr.Zero);
        private NativeMethods()
        {
        }
        #region 属性变换
        //Listview universal constants
        internal const int LVM_FIRST = 0x1000;
        //Listview messages
        internal const int LVM_SETEXTENDEDLISTVIEWSTYLE = LVM_FIRST + 54;
        internal const int LVS_EX_FULLROWSELECT = 0x00000020;
        //Listview extended styles
        internal const int LVS_EX_DOUBLEBUFFER = 0x00010000;

        internal const Int32 ULW_ALPHA = 2;
        internal const int WM_USER = 0x0400;
        internal const int EM_GETOLEINTERFACE = WM_USER + 60;
        #endregion

        #region 枚举
        [Flags]
        internal enum ImageListDrawFlags : int
        {
            ILD_NORMAL = 0x00000000,
            ILD_TRANSPARENT = 0x00000001,
            ILD_BLEND25 = 0x00000002,
            ILD_FOCUS = 0x00000002,
            ILD_BLEND50 = 0x00000004,
            ILD_SELECTED = 0x00000004,
            ILD_BLEND = 0x00000004,
            ILD_MASK = 0x00000010,
            ILD_IMAGE = 0x00000020,
            ILD_ROP = 0x00000040,
            ILD_OVERLAYMASK = 0x00000F00,
            ILD_PRESERVEALPHA = 0x00001000,
            ILD_SCALE = 0x00002000,
            ILD_DPISCALE = 0x00004000,
            ILD_ASYNC = 0x00008000
        }
        #endregion

        #region USER32.DLL
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static internal extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static internal extern bool ChangeClipboardChain(IntPtr HWnd, IntPtr HWndNext);
        //卸下钩子的函数 
        [DllImport("user32.dll ", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool UnhookWindowsHookEx(int idHook);
        //下一个钩挂的函数 
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        internal static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        internal static extern int GetKeyboardState(byte[] pbKeyState);
        [DllImport("user32.dll")]
        internal static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);
        /// <summary> 新建光标
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hBitmap"></param>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <returns></returns>

        [DllImport("user32.dll")]
        internal static extern bool CreateCaret(IntPtr hWnd, IntPtr hBitmap, int nWidth, int nHeight);
        /// <summary> 显示光标
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern bool ShowCaret(IntPtr hWnd);
        /// <summary> 隐藏光标
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern bool HideCaret(IntPtr hWnd);
        /// <summary> 销毁光标
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern bool DestroyCaret(IntPtr hWnd);
        /// <summary> 设置光标位置
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern bool SetCaretPos(int X, int Y);
        [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        internal static extern int SetLayeredWindowAttributes(IntPtr Handle, int crKey, byte bAlpha, int dwFlags);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetWindowText(HandleRef hWnd, string lpString);
        internal static IntPtr SetWindowLong(IntPtr hWnd, short nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 4)
            {
                return SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
            }
            return SetWindowLongPtr64(hWnd, (int)nIndex, dwNewLong);
        }
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable")]
        [DllImport("user32", EntryPoint = "SetWindowLong")]
        internal static extern IntPtr SetWindowLongPtr32(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("user32", EntryPoint = "SetWindowLongPtr")]
        internal static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        /// <summary>
        /// 热键定义
        /// </summary>
        /// <param name="hWnd">要定义热键的窗口的句柄 </param>
        /// <param name="id">定义热键ID（不能与其它ID重复）</param>
        /// <param name="fsModifiers">标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效 (None = 0, Alt = 1, Control = 2, Shift = 4, WindowKeys = 8)</param>
        /// <param name="vk">定义热键的内容 </param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, Keys vk);
        /// <summary>
        /// 取消热键定义
        /// </summary>
        /// <param name="hWnd">要取消热键的窗口的句柄</param>
        /// <param name="id">要取消热键的ID</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(
            IntPtr hwnd, int msg, int wParam, ref HDITEM lParam);
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern bool GetComboBoxInfo(
            IntPtr hwndCombo, ref ComboBoxInfo info);

        [DllImport("user32.DLL", EntryPoint = "GetCaretBlinkTime")]
        internal static extern uint GetCaretBlinkTime();

        [DllImport("user32.dll")]
        internal static extern IntPtr LoadCursorFromFile(string fileName);

        [DllImport("user32.dll")]//在桌面找寻子窗体
        internal static extern IntPtr ChildWindowFromPointEx(IntPtr pHwnd, POINT pt, uint uFlgs);
        internal const int CWP_SKIPDISABLED = 0x2;   //忽略不可用窗体
        internal const int CWP_SKIPINVISIBL = 0x1;   //忽略隐藏的窗体
        internal const int CWP_All = 0x0;            //一个都不忽略

        [DllImport("user32.dll")]
        internal static extern bool GetCursorInfo(out PCURSORINFO pci);

        [DllImport("user32.dll")]
        internal static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        internal static extern bool ReleaseCapture();

        /// <summary>
        /// 执行动画
        /// </summary>
        /// <param name="whnd">控件句柄</param>
        /// <param name="dwtime">动画时间</param>
        /// <param name="dwflag">动画组合名称</param>
        /// <returns>bool值，动画是否成功</returns>
        [DllImport("user32.dll")]
        internal static extern bool AnimateWindow(IntPtr whnd, int dwtime, int dwflag);

        [DllImport("user32.dll")]
        internal static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        internal static extern bool LockWindowUpdate(IntPtr hWndLock);

        [DllImport("user32.dll")]
        internal static extern bool MessageBeep(int uType);

        [DllImport("user32.dll")]
        internal static extern IntPtr BeginPaint(
            IntPtr hWnd, ref PAINTSTRUCT ps);

        [DllImport("user32.dll")]
        internal static extern bool EndPaint(
            IntPtr hWnd, ref PAINTSTRUCT ps);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool TrackPopupMenuEx(
            IntPtr hMenu, uint uFlags, int x, int y, IntPtr hWnd, IntPtr tpmParams);

        [DllImport("user32.dll")]
        internal static extern IntPtr TrackPopupMenu(
            IntPtr hMenu, int uFlags, int x, int y, int nReserved, IntPtr hWnd, IntPtr par);

        [DllImport("user32.dll")]
        internal static extern bool RedrawWindow(
            IntPtr hWnd, ref RECT rectUpdate, IntPtr hrgnUpdate, int flags);

        [DllImport("user32.dll")]
        internal static extern bool RedrawWindow(
            IntPtr hWnd, IntPtr rectUpdate, IntPtr hrgnUpdate, int flags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern bool AdjustWindowRectEx(
            ref RECT lpRect, int dwStyle, bool bMenu, int dwExStyle);

        [DllImport("user32.dll")]
        internal static extern int GetSystemMetrics(int nIndex);

        [DllImport("user32.dll")]
        internal static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

        [DllImport("user32.dll")]
        internal static extern void DisableProcessWindowsGhosting();

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        internal static extern IntPtr FindWindowEx(
            IntPtr hwndParent,
            IntPtr hwndChildAfter,
            string lpszClass,
            string lpszWindow);
        /// <summary> 设置目标窗体大小，位置
        /// </summary>
        /// <param name="hWnd">目标句柄</param>
        /// <param name="x">目标窗体新位置X轴坐标</param>
        /// <param name="y">目标窗体新位置Y轴坐标</param>
        /// <param name="nWidth">目标窗体新宽度</param>
        /// <param name="nHeight">目标窗体新高度</param>
        /// <param name="BRePaint">是否刷新窗体</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);

        [DllImport("User32.dll", EntryPoint = "FlashWindow")]
        internal static extern bool FlashWindow(
            IntPtr hWnd, bool bInvert);

        [DllImport("user32.dll")]
        internal static extern IntPtr DefWindowProc(
            IntPtr hWnd, int uMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true,
            CharSet = CharSet.Unicode, BestFitMapping = false)]
        internal static extern IntPtr CreateWindowEx(
            int exstyle,
            string lpClassName,
            string lpWindowName,
            int dwStyle,
            int x,
            int y,
            int nWidth,
            int nHeight,
            IntPtr hwndParent,
            IntPtr Menu,
            IntPtr hInstance,
            IntPtr lpParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern IntPtr LoadIcon(
            IntPtr hInstance, int lpIconName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsZoomed(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndAfter,
            int x,
            int y,
            int cx,
            int cy,
            uint flags);

        [DllImport("gdi32.dll")]
        internal static extern int CreateRoundRectRgn(
            int x1, int y1, int x2, int y2, int x3, int y3);

        [DllImport("user32.dll")]
        internal static extern int SetWindowRgn(
            IntPtr hWnd, IntPtr hRgn, bool bRedraw);

        [DllImport("user32.dll")]
        internal static extern int SetWindowRgn(
            IntPtr hwnd, int hRgn, Boolean bRedraw);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern bool InvalidateRect(
            IntPtr hWnd, ref RECT rect, bool erase);

        [DllImport("user32.dll")]
        internal static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern bool GetClientRect(
            IntPtr hWnd, ref RECT r);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowRect(
            IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll")]
        internal extern static int OffsetRect(
            ref RECT lpRect, int x, int y);

        [DllImport("user32", EntryPoint = "GetWindowLong")]
        internal static extern int GetWindowLong(
            IntPtr hwnd, int nIndex);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetWindowLongPtr(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll")]
        internal static extern int SetWindowLong(
            IntPtr hwnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        internal static extern int GetWindowLong(HandleRef hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
        internal static extern IntPtr SetWindowLong(HandleRef hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        internal static extern IntPtr SetWindowLongPtr(
            IntPtr hwnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref POINT lpPoint);

        [DllImport("user32.dll")]
        internal static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetDC(IntPtr handle);

        [DllImport("USER32.dll")]
        internal static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, int flags);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetWindowDC(IntPtr handle);
        [DllImport("user32.dll")]
        internal static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);
        [DllImport("user32.dll")]
        internal extern static int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll")]
        internal static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, UInt32 nFlags);
        [DllImport("user32.dll")]
        internal static extern int ReleaseDC(IntPtr handle, IntPtr hdc);

        [DllImport("user32.dll", SetLastError = false)]
        internal static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        internal static extern bool TrackMouseEvent(
            ref TRACKMOUSEEVENT lpEventTrack);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PtInRect(ref RECT lprc, POINT pt);

        [DllImport("user32.dll")]
        internal static extern bool EqualRect(
            [In] ref RECT lprc1, [In] ref RECT lprc2);

        [DllImport("user32.dll", ExactSpelling = true)]
        internal static extern IntPtr SetTimer(
            IntPtr hWnd,
            int nIDEvent,
            uint uElapse,
            IntPtr lpTimerFunc);

        [DllImport("user32.dll", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool KillTimer(
            IntPtr hWnd, uint uIDEvent);
        /// <summary> 获得输入焦点
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>

        [DllImport("user32.dll")]
        internal static extern int SetFocus(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern int GetActiveWindow();

        [DllImport("user32.dll")]
        internal static extern IntPtr SetActiveWindow(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PostMessage(
            IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        /// <summary> 进程通讯结构体
        /// </summary>
        internal struct COPYDATASTRUCT
        {
            internal IntPtr dwData; //可以是任意值
            internal int cbData;    //指定lpData内存区域的字节数
            [MarshalAs(UnmanagedType.LPStr)]
            internal string lpData; //发送给目录窗口所在进程的数据
        }
        [DllImport("user32.dll")]
        internal extern static int SendMessage(
           IntPtr hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam);

        [DllImport("user32.dll")]
        internal extern static int SendMessage(
            IntPtr hWnd, int msg, int wParam, int lParam);

        //[DllImport("User32.dll",
        //    CharSet = CharSet.Auto,
        //    PreserveSig = false)]
        //internal static extern IRichEditOle SendMessage(
        //    IntPtr hWnd, int message, int wParam);

        [DllImport("user32.dll")]
        internal extern static int SendMessage(
            IntPtr hWnd, int msg, int wParam, ref TOOLINFO lParam);

        [DllImport("user32.dll")]
        internal extern static int SendMessage(
            IntPtr hWnd, int msg, int wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        internal extern static int SendMessage(
            IntPtr hWnd, int msg, int wParam, ref RECT lParam);

        [DllImport("user32.dll")]
        internal extern static int SendMessage(
            IntPtr hWnd,
            int msg,
            IntPtr wParam,
            [MarshalAs(UnmanagedType.LPTStr)]string lParam);

        [DllImport("user32.dll")]
        internal extern static int SendMessage(
            IntPtr hWnd, int msg, IntPtr wParam, ref NMHDR lParam);

        [DllImport("user32.dll")]
        internal extern static int SendMessage(
            IntPtr hWnd, int msg, IntPtr wParam, int lParam);

        [DllImport("user32.dll")]
        internal static extern int SendMessage(
            IntPtr hWnd, int msg, int wParam, ref SCROLLBARINFO lParam);
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern short GetKeyState(int nVirtKey);

        [DllImport("user32.dll")]
        internal static extern bool ValidateRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll")]
        internal static extern int GetScrollBarInfo(
            IntPtr hWnd, uint idObject, ref SCROLLBARINFO psbi);

        [DllImport("user32.dll")]
        internal static extern bool GetScrollInfo(
            IntPtr hwnd, int fnBar, ref SCROLLINFO scrollInfo);

        [DllImport("user32.dll")]
        internal static extern bool EnableScrollBar(
            IntPtr hWnd, int wSBflags, int wArrows);

        internal static IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 8)
            {
                return GetClassLongPtr64(hWnd, nIndex);
            }
            else
            {
                return GetClassLongPtr32(hWnd, nIndex);
            }
        }

        [DllImport("user32.dll", EntryPoint = "GetClassLong")]
        private static extern IntPtr GetClassLongPtr32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
        private static extern IntPtr GetClassLongPtr64(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon,
           int cxWidth, int cyHeight, int istepIfAniCur, IntPtr hbrFlickerFreeDraw,
           int diFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool UpdateLayeredWindowIndirect(IntPtr hwnd, ref UPDATELAYEREDWINDOWINFO ulwi);

        [DllImport("USER32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, int hMod, int dwThreadId);
        [DllImport("USER32.dll", CharSet = CharSet.Auto)]
        internal static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);

        [DllImport("USER32.dll", CharSet = CharSet.Auto)]
        internal static extern int CallNextHookEx(
            IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("USER32.dll", CharSet = CharSet.Auto)]
        internal static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetClientRect(HandleRef hwnd, ref RECT rect);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern IntPtr GetForegroundWindow(); //获得本窗体的句柄
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);//设置此窗体为活动窗体

        #endregion

        #region GDI32.DLL
        [DllImport("gdi32.dll")]
        internal static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr CreateFontW(
            int nHeight,
            int nWidth,
            int nEscapement,
            int nOrientation,
            int fnWeight,
            uint fdwItalic,
            uint fdwUnderline,
            uint fdwStrikeOut,
            uint fdwCharSet,
            uint fdwOutputPrecision,
            uint fdwClipPrecision,
            uint fdwQuality,
            uint fdwPitchAndFamily,
            string lpszFace);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int DrawTextW(
            IntPtr hdc,
            string lpString,
            int nCount,
            ref RECT lpRect,
            uint uFormat);

        [DllImport("gdi32.dll", SetLastError = true)]
        internal static extern IntPtr CreateDIBSection(
            IntPtr hdc,
            ref BITMAPINFO pbmi,
            uint iUsage,
            out IntPtr ppvBits,
            IntPtr hSection,
            uint dwOffset);
        /// <summary>  
        /// 在指定的设备场景中取得一个像素的RGB值  
        /// </summary>  
        /// <param name="hdc">一个设备场景的句柄</param>  
        /// <param name="nXPos">逻辑坐标中要检查的横坐标</param>  
        /// <param name="nYPos">逻辑坐标中要检查的纵坐标</param>  
        /// <returns>指定点的颜色</returns>  
        [DllImport("gdi32.dll")]
        internal extern static uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        [DllImport("gdi32.dll")]
        internal extern static int ExcludeClipRect(
            IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport("gdi32.dll", EntryPoint = "GdiAlphaBlend")]
        internal static extern bool AlphaBlend(
            IntPtr hdcDest,
            int nXOriginDest,
            int nYOriginDest,
            int nWidthDest,
            int nHeightDest,
            IntPtr hdcSrc,
            int nXOriginSrc,
            int nYOriginSrc,
            int nWidthSrc,
            int nHeightSrc,
            BLENDFUNCTION blendFunction);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool StretchBlt(
            IntPtr hDest,
            int X,
            int Y,
            int nWidth,
            int nHeight,
            IntPtr hdcSrc,
            int sX,
            int sY,
            int nWidthSrc,
            int nHeightSrc,
            int dwRop);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool BitBlt(
            IntPtr hdcDest,
            int nXDest,
            int nYDest,
            int nWidth,
            int nHeight,
            IntPtr hdcSrc,
            int nXSrc,
            int nYSrc,
            int dwRop);
        [DllImport("gdi32.dll")]
        internal static extern bool BitBlt(HandleRef hDC, int x, int y, int nWidth, int nHeight,
                                         HandleRef hSrcDC, int xSrc, int ySrc, int dwRop);

        [DllImport("gdi32.dll")]
        internal static extern bool TransparentBlt(
            IntPtr hdcDest,
            int nXOriginDest,
            int nYOriginDest,
            int nWidthDest,
            int hHeightDest,
            IntPtr hdcSrc,
            int nXOriginSrc,
            int nYOriginSrc,
            int nWidthSrc,
            int nHeightSrc,
            uint crTransparent);

        [DllImport("gdi32.dll")]
        internal static extern bool PlgBlt(
            IntPtr hdcDest,
            ref POINT[] lpPoint,
            IntPtr hdcSrc,
            int nXSrc,
            int nYSrc,
            int nWidth,
            int nHeight,
            IntPtr hbmMask,
            int xMask,
            int yMask);

        [DllImport("gdi32.dll")]
        internal static extern bool PatBlt(
            IntPtr hdc,
            int nXLeft,
            int nYLeft,
            int nWidth,
            int nHeight,
            int dwRop);

        [DllImport("gdi32.dll")]
        internal static extern IntPtr CreateDCA(
            [MarshalAs(UnmanagedType.LPStr)]string lpszDriver,
            [MarshalAs(UnmanagedType.LPStr)]string lpszDevice,
            [MarshalAs(UnmanagedType.LPStr)]string lpszOutput,
            int lpInitData);

        [DllImport("gdi32.dll")]
        internal static extern IntPtr CreateDCW(
            [MarshalAs(UnmanagedType.LPWStr)]string lpszDriver,
            [MarshalAs(UnmanagedType.LPWStr)]string lpszDevice,
            [MarshalAs(UnmanagedType.LPWStr)]string lpszOutput,
            int lpInitData);

        [DllImport("gdi32.dll")]
        internal static extern IntPtr CreateDC(
            string lpszDriver,
            string lpszDevice,
            string lpszOutput,
            int lpInitData);

        [DllImport("gdi32.dll")]
        internal static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        internal static extern IntPtr CreateCompatibleBitmap(
            IntPtr hdc, int nWidth, int nHeight);
        [DllImport("gdi32.dll")]
        internal static extern int SetBkColor(
                IntPtr hdc,
                int crColor
        );

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true, PreserveSig = true)]
        internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll")]
        internal static extern uint SetPixel(IntPtr hdc, int X, int Y, int crColor);

        [DllImport("gdi32.dll",
           CharSet = CharSet.Auto,
           SetLastError = true,
           ExactSpelling = true)]
        internal static extern IntPtr CreateRectRgn(int x1, int y1, int x2, int y2);

        [DllImport("gdi32.dll",
            SetLastError = true,
            ExactSpelling = true)]
        internal static extern int CombineRgn(
            IntPtr hRgn, IntPtr hRgn1, IntPtr hRgn2, int nCombineMode);

        #endregion

        #region gdiplus.dll
        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Unicode)] // 3 = Unicode
        [ResourceExposure(ResourceScope.None)]
        internal static extern int GdipMeasureString(HandleRef graphics, string textString, int length, HandleRef font, ref GPRECTF layoutRect,
                                                     HandleRef stringFormat, [In, Out] ref GPRECTF boundingBox, out int codepointsFitted, out int linesFilled);
        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Unicode)] // 3 = Unicode
        [ResourceExposure(ResourceScope.None)]
        internal static extern int GdipRotateWorldTransform(HandleRef graphics, float angle, MatrixOrder order);

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Unicode)] // 3 = Unicode
        [ResourceExposure(ResourceScope.None)]
        internal static extern int GdipScaleWorldTransform(HandleRef graphics, float sx, float sy, MatrixOrder order);
        #endregion

        #region comctl32.dll
        [DllImport("comctl32.dll", SetLastError = true)]
        internal static extern IntPtr ImageList_GetIcon(
            IntPtr himl, int i, int flags);

        [StructLayout(LayoutKind.Sequential)]
        internal struct HDITEM
        {
            internal int mask;
            internal int cxy;
            internal string pszText;
            internal IntPtr hbm;
            internal int cchTextMax;
            internal int fmt;
            internal IntPtr lParam;
            internal int iImage;
            internal int iOrder;
            internal uint type;
            internal IntPtr pvFilter;
        }

        [DllImport("comctl32.dll",
            CallingConvention = CallingConvention.StdCall)]
        internal static extern bool InitCommonControlsEx(
            ref INITCOMMONCONTROLSEX iccex);
        #endregion

        #region kernel32.dll
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetDllDirectory(string lpPathName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("Kernel32.dll", SetLastError = true)]
        internal static extern Int32 GetLastError();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        internal static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer);


        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr VirtualAlloc(
            IntPtr lpAddress,
            UIntPtr dwSize,
            uint flAllocationType,
            uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool VirtualFree(
            IntPtr lpAddress,
            UIntPtr dwSize,
            uint dwFreeType);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool VirtualProtect(
            IntPtr lpAddress,
            UIntPtr dwSize,
            uint flNewProtect,
            out uint lpflOldProtect);

        [DllImport("Kernel32.dll", SetLastError = false)]
        internal static extern IntPtr HeapAlloc(IntPtr hHeap, uint dwFlags, UIntPtr dwBytes);

        [DllImport("Kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool HeapFree(IntPtr hHeap, uint dwFlags, IntPtr lpMem);

        [DllImport("Kernel32.dll", SetLastError = false)]
        internal static extern UIntPtr HeapSize(IntPtr hHeap, uint dwFlags, IntPtr lpMem);

        [DllImport("Kernel32.dll", SetLastError = true)]
        internal static extern IntPtr HeapCreate(
            uint flOptions,
            [MarshalAs(UnmanagedType.SysUInt)] IntPtr dwInitialSize,
            [MarshalAs(UnmanagedType.SysUInt)] IntPtr dwMaximumSize
            );

        [DllImport("Kernel32.dll", SetLastError = true)]
        internal static extern uint HeapDestroy(IntPtr hHeap);

        [DllImport("Kernel32.Dll", SetLastError = true)]
        internal unsafe static extern uint HeapSetInformation(
            IntPtr HeapHandle,
            int HeapInformationClass,
            void* HeapInformation,
            uint HeapInformationLength
            );

        [DllImport("kernel32.dll", SetLastError = false)]
        internal static extern void GetSystemInfo(ref SYSTEM_INFO lpSystemInfo);

        [DllImport("kernel32.dll", SetLastError = false)]
        internal static extern void GetNativeSystemInfo(ref SYSTEM_INFO lpSystemInfo);
        [DllImport("kernel32.dll", SetLastError = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsProcessorFeaturePresent(uint ProcessorFeature);
        [DllImport("kernel32.dll")]
        internal static extern IntPtr _lopen(string lpPathName, int iReadWrite);
        [DllImport("kernel32.dll")]
        internal static extern bool CloseHandle(IntPtr hObject);
        [DllImport("kernel32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        internal static extern int GetCurrentThreadId();
        //使用WINDOWS API函数代替获取当前实例的函数,防止钩子失效
        [DllImport("kernel32.dll")]
        internal static extern IntPtr GetModuleHandle(string name);

        [DllImport("kernel32.dll")]
        internal extern static int RtlMoveMemory(
            ref NMHDR destination, IntPtr source, int length);

        [DllImport("kernel32.dll")]
        internal extern static int RtlMoveMemory(
            ref NMTTDISPINFO destination, IntPtr source, int length);

        [DllImport("kernel32.dll")]
        internal extern static int RtlMoveMemory(
            IntPtr destination, ref NMTTDISPINFO Source, int length);

        [DllImport("kernel32.dll")]
        internal extern static int RtlMoveMemory(
            ref POINT destination, ref RECT Source, int length);

        [DllImport("kernel32.dll")]
        internal extern static int RtlMoveMemory(
            ref NMTTCUSTOMDRAW destination, IntPtr Source, int length);

        [DllImport("kernel32.dll")]
        internal extern static int RtlMoveMemory(
            ref NMCUSTOMDRAW destination, IntPtr Source, int length);

        [DllImport("kernel32.dll")]
        internal static extern int MulDiv(int nNumber, int nNumerator, int nDenominator);

        [DllImport("kernel32.dll")]
        internal static extern int WinExec(string lpCmdLine, int nCmdShow);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern uint WaitForMultipleObjects(
            uint nCount,
            IntPtr[] lpHandles,
            [MarshalAs(UnmanagedType.Bool)] bool bWaitAll,
            uint dwMilliseconds);
        internal static uint WaitForMultipleObjects(IntPtr[] lpHandles, bool bWaitAll, uint dwMilliseconds)
        {
            return WaitForMultipleObjects((uint)lpHandles.Length, lpHandles, bWaitAll, dwMilliseconds);
        }

        #endregion

        #region uxtheme.dll

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        internal static extern int SetWindowTheme(
            IntPtr hWnd, string pszSubAppName, string pszSubIdList);

        [DllImport("uxtheme.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal extern static bool IsAppThemed();

        #endregion

        #region shell32.dll

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        internal static extern int FindExecutable(string filename, string directory, ref string result);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        internal static extern int ShellExecute(IntPtr hwnd, string lpOperation, string lpFile,
            string lpParameters, string lpDirectory, int nShowCmd);

        #endregion

        #region ole32.dll
        //[DllImport("ole32.dll")]
        //internal static extern int CreateILockBytesOnHGlobal(
        //    IntPtr hGlobal, bool fDeleteOnRelease, out ILockBytes ppLkbyt);

        //[DllImport("ole32.dll")]
        //internal static extern int StgCreateDocfileOnILockBytes(
        //    ILockBytes plkbyt, uint grfMode, uint reserved, out IStorage ppstgOpen);

        //[DllImport("ole32.dll")]
        //internal static extern int OleCreateFromFile([In] ref Guid rclsid,
        //    [MarshalAs(UnmanagedType.LPWStr)] string lpszFileName,
        //    [In] ref Guid riid,
        //    uint renderopt,
        //    ref FORMATETC pFormatEtc,
        //    IOleClientSite pClientSite,
        //    IStorage pStg,
        //    [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

        [DllImport("ole32.dll")]
        internal static extern int OleSetContainedObject(
            [MarshalAs(UnmanagedType.IUnknown)] object pUnk, bool fContained);
        #endregion

        #region Imm32.dll
        [DllImport("Imm32.dll")]
        internal static extern IntPtr ImmGetContext(IntPtr hWnd);
        [DllImport("Imm32.dll")]
        internal static extern IntPtr ImmAssociateContext(IntPtr hWnd, IntPtr hIMC);
        [DllImport("imm32.dll")]
        internal static extern int ImmGetCompositionString(IntPtr hIMC, int dwIndex, StringBuilder lPBuf, int dwBufLen);
        [DllImport("Imm32.dll")]
        internal static extern bool ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);
        #endregion

        #region SetupApi.dll
        [DllImport("SetupApi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr SetupDiGetClassDevsW(
            ref Guid ClassGuid,
            [MarshalAs(UnmanagedType.LPWStr)] string Enumerator,
            IntPtr hwndParent,
            uint Flags);

        [DllImport("SetupApi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

        [DllImport("SetupApi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetupDiEnumDeviceInfo(
            IntPtr DeviceInfoSet,
            uint MemberIndex,
            ref SP_DEVINFO_DATA DeviceInfoData);

        [DllImport("SetupApi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetupDiGetDeviceInstanceIdW(
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            IntPtr DeviceInstanceId,
            uint DeviceInstanceIdSize,
            out uint RequiredSize);
        #endregion

        #region ntdll.dll
        [DllImport("ntdll.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe byte* memcpy(byte* dst, byte* src, int count);
        [DllImport("ntdll.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe byte* memset(byte* dst, int filler, int count);
        #endregion
        #region msvcrt.dll

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        internal static extern unsafe void memcpy(void* dst, void* src, UIntPtr length);

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        internal static extern unsafe void memset(void* dst, int c, UIntPtr length);
        #endregion

        #region ComboBoxButtonState
        internal enum ComboBoxButtonState
        {
            STATE_SYSTEM_NONE = 0,
            STATE_SYSTEM_INVISIBLE = 0x00008000,
            STATE_SYSTEM_PRESSED = 0x00000008
        }
        #endregion

        #region ComboBoxInfo Struct
        [StructLayout(LayoutKind.Sequential)]
        internal struct ComboBoxInfo
        {
            internal int cbSize;
            internal RECT rcItem;
            internal RECT rcButton;
            internal ComboBoxButtonState stateButton;
            internal IntPtr hwndCombo;
            internal IntPtr hwndEdit;
            internal IntPtr hwndList;
        }
        #endregion
    }
}
