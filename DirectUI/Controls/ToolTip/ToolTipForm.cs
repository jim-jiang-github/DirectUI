using DirectUI.Common;
using DirectUI.Core;
using DirectUI.Win32;
/********************************************************************
 * * 使本项目源码前请仔细阅读以下协议内容，如果你同意以下协议才能使用本项目所有的功能,
 * * 否则如果你违反了以下协议，有可能陷入法律纠纷和赔偿，作者保留追究法律责任的权利。
 * * Copyright (C) 
 * * 作者： 煎饼的归宿    QQ：375324644   
 * * 请保留以上版权信息，否则作者将保留追究法律责任。
 * * 最后修改：BF-20150503UIWA
 * * 创建时间：2017/12/8 22:11:41
********************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DirectUI
{
    public class ToolTipForm : ToolStripDropDown
    {
        private SynchronizationContext synchronizationContext = null;
        private ImageDc backImageDc;
        ToolStripControlHost mControlHost;
        private ToolTipControl control;
        public ToolTipForm()
        {
            this.DoubleBuffered = true;
            this.synchronizationContext = SynchronizationContext.Current;
            this.DropShadowEnabled = false; //是否绘制边框阴影
            this.BackColor = Color.Transparent;
            this.control = new ToolTipControl() { Width = 200, Height = 200 };
            DUIControl dc = new DUIControl() { Dock = DockStyle.Fill };
            this.control.DUIControls.Add(dc);
            mControlHost = new ToolStripControlHost(this.control);
            mControlHost.Padding = Padding.Empty;
            mControlHost.Margin = Padding.Empty;
            //mControlHost.AutoSize = false;
            this.Padding = Padding.Empty;
            this.Items.Add(mControlHost);
            dc.Paint += (s, e) =>
            {

                e.Graphics.DrawImageDc(this.backImageDc, 0, 0);
                e.Graphics.FillRectangle(DUIBrushes.Blue, new RectangleF(60, 60, 60, 60));
            };
            this.control.Paint += (s, e) =>
            {
                //IntPtr hDC = e.Graphics.GetHdc();
                //NativeMethods.BitBlt(hDC, 0, 0, this.Width, this.Height, backImageDc.Hdc, 0, 0, 0xCC0020);
                //e.Graphics.ReleaseHdc(hDC);
                //using (SolidBrush sb = new SolidBrush(Color.FromArgb(60, Color.Red)))
                //{
                //    e.Graphics.FillRectangle(sb, new Rectangle(0, 0, this.Width, this.Height));
                //}
            };
        }
        //public int X { get { return Control.MousePosition.X + 10; } }
        //public int Y { get { return Control.MousePosition.Y + 10; } }
        //public Point Location { get { return new Point(X, Y); } }
        public void SetToolTip()
        {
            this.Location = Control.MousePosition;
            this.backImageDc = new ImageDc(200, 200);
            this.backImageDc.ScreenCapture(Location);
            this.Show(Location);
            //new Thread(() =>
            //{
            //    Thread.Sleep(500);
            //    synchronizationContext.Send((obj) =>
            //    {
            //        this.Show(Location);
            //    }, null);
            //}) { IsBackground = true }.Start();
            ////this.backImageDc.ScreenCapture(this.Location);
            ////this.control = control;
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
