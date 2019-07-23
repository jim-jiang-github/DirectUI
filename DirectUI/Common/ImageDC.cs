using DirectUI.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

namespace DirectUI.Common
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public class ImageDc : IDisposable
    {
        #region 变量
        private int _height = 0;
        private int _width = 0;
        private IntPtr _pHdc = IntPtr.Zero;
        private IntPtr _pBmp = IntPtr.Zero;
        private IntPtr _pBmpOld = IntPtr.Zero;
        #endregion
        #region 属性
        public IntPtr Hdc
        {
            get { return _pHdc; }
        }

        public IntPtr HBmp
        {
            get { return _pBmp; }
        }
        public int Width { get { return _width; } }
        public int Height { get { return _height; } }
        #endregion
        public ImageDc(int width, int height, IntPtr hBmp)
        {
            IntPtr pHdc = IntPtr.Zero;
            pHdc = NativeMethods.CreateDCA("DISPLAY", "", "", 0);
            _pHdc = NativeMethods.CreateCompatibleDC(pHdc);
            if (hBmp != IntPtr.Zero)
            {
                _pBmp = hBmp;
            }
            else
            {
                _pBmp = NativeMethods.CreateCompatibleBitmap(pHdc, width, height);
            }
            _pBmpOld = NativeMethods.SelectObject(_pHdc, _pBmp);
            if (_pBmpOld == IntPtr.Zero)
            {
                Dispose();
            }
            else
            {
                _width = width;
                _height = height;
            }
            NativeMethods.DeleteDC(pHdc);
            pHdc = IntPtr.Zero;
        }
        public ImageDc(int width, int height)
            : this(width, height, IntPtr.Zero)
        {
        }
        public void Dispose()
        {
            if (_pBmpOld != IntPtr.Zero)
            {
                NativeMethods.SelectObject(_pHdc, _pBmpOld);
                _pBmpOld = IntPtr.Zero;
            }
            if (_pBmp != IntPtr.Zero)
            {
                NativeMethods.DeleteObject(_pBmp);
                _pBmp = IntPtr.Zero;
            }
            if (_pHdc != IntPtr.Zero)
            {
                NativeMethods.DeleteDC(_pHdc);
                _pHdc = IntPtr.Zero;
            }
        }
        public void ScreenCapture(Point point)
        {
            ScreenCapture(point.X, point.Y);
        }
        public void ScreenCapture(int x, int y)
        {
            IntPtr pD = NativeMethods.GetDesktopWindow();
            IntPtr pH = NativeMethods.GetDC(pD);
            NativeMethods.BitBlt(this.Hdc, 0, 0, this.Width, this.Height, pH, x, y, 0xCC0020);
            NativeMethods.ReleaseDC(pD, pH);
        }
        public void ScreenCapture()
        {
            IntPtr pD = NativeMethods.GetDesktopWindow();
            IntPtr pH = NativeMethods.GetDC(pD);
            NativeMethods.BitBlt(this.Hdc, 0, 0, this.Width, this.Height, pH, 0, 0, 0xCC0020);
            NativeMethods.ReleaseDC(pD, pH);
        }
        public void ControlCapture(Control control)
        {
            Graphics ownerGraphics = Graphics.FromHwnd(control.Handle);
            IntPtr ownerDC = ownerGraphics.GetHdc();
            DirectUI.Win32.NativeMethods.BitBlt(ownerDC, 0, 0, control.ClientSize.Width, control.ClientSize.Height, ownerDC, 0, 0, 0xCC0020);
            ownerGraphics.ReleaseHdc(ownerDC);
        }
        public void ControlCapture(Control control, int x, int y)
        {
            Graphics ownerGraphics = Graphics.FromHwnd(control.Handle);
            IntPtr ownerDC = ownerGraphics.GetHdc();
            DirectUI.Win32.NativeMethods.BitBlt(ownerDC, 0, 0, control.ClientSize.Width, control.ClientSize.Height, ownerDC, x, y, 0xCC0020);
            ownerGraphics.ReleaseHdc(ownerDC);
        }
    }
}
