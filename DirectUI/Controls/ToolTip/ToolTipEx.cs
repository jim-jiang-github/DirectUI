using DirectUI.Common;
using DirectUI.Win32;
using DirectUI.Win32.Struct;
/********************************************************************
 * * 使本项目源码前请仔细阅读以下协议内容，如果你同意以下协议才能使用本项目所有的功能,
 * * 否则如果你违反了以下协议，有可能陷入法律纠纷和赔偿，作者保留追究法律责任的权利。
 * * Copyright (C) 
 * * 作者： 煎饼的归宿    QQ：375324644   
 * * 请保留以上版权信息，否则作者将保留追究法律责任。
 * * 最后修改：BF-20150503UIWA
 * * 创建时间：2017/12/9 9:10:55
********************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DirectUI
{
    public class ToolTipEx : System.Windows.Forms.ToolTip
    {
        private ImageDc backImageDc;
        private float opacity = 1;
        private int width = 100;
        private int height = 100;
        public float Opacity
        {
            get { return opacity; }
            set { opacity = value; }
        }
        public int X { get { return Location.X; } }
        public int Y { get { return Location.Y; } }
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        public Point Location
        {
            get
            {
                IntPtr handle = Handle;
                if (handle == IntPtr.Zero)
                {
                    return Point.Empty;
                }
                RECT rect = new RECT();
                NativeMethods.GetWindowRect(handle, ref rect);
                return new Point(rect.Left, rect.Top);
            }
        }
        public Size Size { get { return new Size(this.Width, this.Height); } }
        public Rectangle Bounds { get { return new Rectangle(Location, Size); } }
        /// <summary> 本来Handle在ToolTip上是没有的，用这个方法可以获取
        /// </summary>
        public IntPtr Handle
        {
            get
            {
                if (!DesignMode)
                {
                    Type t = typeof(ToolTip);
                    PropertyInfo pi = t.GetProperty("Handle", BindingFlags.NonPublic | BindingFlags.Instance);
                    IntPtr handle = (IntPtr)pi.GetValue(this, null);
                    return handle;
                }

                return IntPtr.Zero;
            }
        }
        public ToolTipEx()
        {
            this.OwnerDraw = true;
            this.Opacity = 0.5F;
            this.Draw += (s, e) =>
            {
                Rectangle bounds = e.Bounds;
                int alpha = (int)(Opacity * 255);
                if (Handle != IntPtr.Zero && Opacity < 1F)
                {
                    IntPtr hDC = e.Graphics.GetHdc();
                    NativeMethods.BitBlt(hDC, 0, 0, bounds.Width, bounds.Height, backImageDc.Hdc, 0, 0, 0xCC0020);
                    e.Graphics.ReleaseHdc(hDC);
                }
                //e.Graphics.FillRectangle(Brushes.Red, new Rectangle(10, 10, 50, 50));
                using (SolidBrush sb = new SolidBrush(Color.FromArgb(alpha, Color.Red)))
                {
                    e.Graphics.FillRectangle(sb, new Rectangle(0, 0, 100, 100));
                }
            };
            this.Popup += (s, e) =>
            {
                e.ToolTipSize = this.Size;
                if (Opacity < 1F)
                {
                    ToolTipCapture(); //如果使用背景透明，获取背景图。
                }
            };
        }
        /// <summary> 获取ToolTip区域的截图
        /// </summary>
        private void ToolTipCapture()
        {
            backImageDc = new ImageDc(this.Size.Width, this.Size.Height);
            IntPtr pD = NativeMethods.GetDesktopWindow();
            IntPtr pH = NativeMethods.GetDC(pD);
            NativeMethods.BitBlt(backImageDc.Hdc, 0, 0, this.Width, this.Height, pH, this.X, this.Y, 0xCC0020);
            NativeMethods.ReleaseDC(pD, pH);
        }
        #region Dispose
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (backImageDc != null)
                {
                    backImageDc.Dispose();
                    backImageDc = null;
                }
            }
        }
        #endregion
    }
}
