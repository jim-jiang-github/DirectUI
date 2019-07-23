using DirectUI.Share;
using DirectUI.Win32.Const;
using DirectUI.Win32.Struct;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace DirectUI.Core
{
    public class DUISkinFormAlpha : DUISkinForm
    {
        /// <summary> 控件的缩率图
        /// </summary>
        private Bitmap controlThumbnail = null;
        /// <summary> 基窗体缩率图,窗体尺寸变化的时候自动变化尺寸
        /// </summary>
        /// <returns></returns>
        public Bitmap ControlThumbnail
        {
            get
            {
                if (controlThumbnail == null)
                {
                    if (this.Width == 0 || this.Height == 0)
                    {
                        return null;
                    }
                    else
                    {
                        controlThumbnail = new Bitmap(this.Width, this.Height);
                    }
                }
                else
                {
                    if (this.Width == 0 || this.Height == 0)
                    {
                        return null;
                    }
                    else
                    {
                        if (controlThumbnail.Width != this.Width || controlThumbnail.Height != this.Height)
                        {
                            controlThumbnail = new Bitmap(this.Width, this.Height);
                        }
                    }
                }
                return controlThumbnail;
            }
        }
        #region 构造函数
        protected override void Init()
        {
            this.dUIContainer = new DUIContainerAlpha(this);
            this.dUICaptionContainer = new DUICaptionContainerAlpha(this);
            this.sysButtons = new SysButtonCollection(this);
            this.sysButtons.Add(new SysButtonClose() { Width = 42 });
            this.sysButtons.Add(new SysButtonMax());
            this.sysButtons.Add(new SysButtonRestore());
            this.sysButtons.Add(new SysButtonMin());
            this.sysButtons.Add(new SysButtonSkin());
        }
        #endregion
        #region 重写
        /// <summary> 可用半透明的Png作为背景图，但是需要 Win32 API UpdateLayeredWindow 来重绘
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams parameters = base.CreateParams;
                parameters.ClassStyle |= Win32.Const.CS.CS_NOCLOSE;
                parameters.ExStyle |= Win32.Const.WS.WS_EX_LAYERED;
                return parameters;
            }
        }
        #endregion
        internal override void DoMaximize()
        {
            //不知道为什么需要这样，只用一个DoMaximize没有反应
            base.DoMaximize();
            base.DoNormal();
            base.DoMaximize();
        }


        public void SetBits()
        {
            //using (Graphics g = Graphics.FromImage(ControlThumbnail))
            //using (SolidBrush brush = new SolidBrush(this.BackColor))
            //{
            //    g.FillRectangle(brush, new Rectangle(this.BorderWidthInternal, this.CaptionHeightInternal, this.Width - this.BorderWidthInternal * 2, this.Height - this.CaptionHeightInternal - this.BorderWidthInternal));
            //    //g.DrawImage(this.ControlThumbnail, this.BorderWidthInternal, this.CaptionHeightInternal);
            //}
            SetBits(new Rectangle(0, 0, ControlThumbnail.Width, ControlThumbnail.Height));
        }
        public void SetBits(Rectangle rect)
        {
            IntPtr oldBits = IntPtr.Zero;
            IntPtr screenDC = Win32.NativeMethods.GetDC(IntPtr.Zero); //屏幕句柄
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr memDc = Win32.NativeMethods.CreateCompatibleDC(screenDC); //内存句柄
            try
            {
                Win32.Struct.POINT topLoc = new Win32.Struct.POINT(this.Left, this.Top);
                Win32.Struct.SIZE bitMapSize = new Win32.Struct.SIZE(rect.Width, rect.Height);
                Win32.Struct.BLENDFUNCTION blendFunc = new Win32.Struct.BLENDFUNCTION();
                Win32.Struct.POINT srcLoc = new Win32.Struct.POINT(rect.Left, rect.Top);
                hBitmap = ControlThumbnail.GetHbitmap(Color.FromArgb(0)); // 为传入的PNG图片设置一个背景
                oldBits = Win32.NativeMethods.SelectObject(memDc, hBitmap); //将图片写入内存
                blendFunc.BlendOp = AC.AC_SRC_OVER;
                blendFunc.SourceConstantAlpha = 255;
                blendFunc.AlphaFormat = AC.AC_SRC_ALPHA;
                blendFunc.BlendFlags = 0;
                Win32.NativeMethods.UpdateLayeredWindow(Handle, screenDC, ref topLoc, ref bitMapSize, memDc, ref srcLoc, 0, ref blendFunc, Win32.NativeMethods.ULW_ALPHA);
            }
            finally
            {
                Win32.NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
                if (hBitmap != IntPtr.Zero)
                {
                    Win32.NativeMethods.SelectObject(memDc, oldBits);
                    Win32.NativeMethods.DeleteObject(hBitmap);
                }
                Win32.NativeMethods.DeleteDC(memDc);
            }
        }
        public void SetBits(Bitmap bmp)
        {
            IntPtr oldBits = IntPtr.Zero;
            IntPtr screenDC = Win32.NativeMethods.GetDC(IntPtr.Zero); //屏幕句柄
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr memDc = Win32.NativeMethods.CreateCompatibleDC(screenDC); //内存句柄
            try
            {
                Win32.Struct.POINT topLoc = new Win32.Struct.POINT(this.Left, this.Top);
                Win32.Struct.SIZE bitMapSize = new Win32.Struct.SIZE(bmp.Width, bmp.Height);
                Win32.Struct.BLENDFUNCTION blendFunc = new Win32.Struct.BLENDFUNCTION();
                Win32.Struct.POINT srcLoc = new Win32.Struct.POINT(0, 0);
                hBitmap = bmp.GetHbitmap(Color.FromArgb(0)); // 为传入的PNG图片设置一个背景
                oldBits = Win32.NativeMethods.SelectObject(memDc, hBitmap); //将图片写入内存
                blendFunc.BlendOp = AC.AC_SRC_OVER;
                blendFunc.SourceConstantAlpha = 255;
                blendFunc.AlphaFormat = AC.AC_SRC_ALPHA;
                blendFunc.BlendFlags = 0;
                Win32.NativeMethods.UpdateLayeredWindow(Handle, screenDC, ref topLoc, ref bitMapSize, memDc, ref srcLoc, 0, ref blendFunc, Win32.NativeMethods.ULW_ALPHA);
            }
            finally
            {
                Win32.NativeMethods.ReleaseDC(IntPtr.Zero, screenDC);
                if (hBitmap != IntPtr.Zero)
                {
                    Win32.NativeMethods.SelectObject(memDc, oldBits);
                    Win32.NativeMethods.DeleteObject(hBitmap);
                }
                Win32.NativeMethods.DeleteDC(memDc);
            }
        }
        internal class DUIContainerAlpha : DUIContainer
        {
            /// <summary> 所在的DUISkinFormAlpha
            /// </summary>
            private DUISkinFormAlpha Owner { get { return this.owner as DUISkinFormAlpha; } }
            public override Bitmap ControlThumbnail
            {
                get
                {
                    return this.Owner.ControlThumbnail;
                }
            }
            public override Color BackColor
            {
                get
                {
                    return this.owner.BackColor;
                }
            }
            public DUIContainerAlpha(DUISkinFormAlpha owner)
                : base(owner)
            {
            }
            public override void Invalidate(System.Drawing.Region r)
            {
                synchronizationContext.Send((obj) =>
                {
                    if (this.CanLayout && this.owner.IsHandleCreated)
                    {
                        InvalidateControlThumbnail(new Rectangle(0, 0, this.owner.ClientSize.Width, this.owner.ClientSize.Height));
                        this.Owner.SetBits();
                    }
                }, null);
            }
            internal override void InvalidateControlThumbnail(Rectangle rect)
            {
                try
                {
                    using (Graphics g = Graphics.FromImage(ControlThumbnail))
                    {
                        g.TranslateTransform(this.Owner.BorderWidthInternal, this.Owner.CaptionHeightInternal);
                        g.SetClip(new Rectangle(0, 0, this.Owner.ClientSize.Width, this.Owner.ClientSize.Height));
                        this.DoPaintBackground(new PaintEventArgs(g, rect));
                        this.DoPaint(new PaintEventArgs(g, rect));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("GDI+绘图错误：\r\n" + ex.ToString());
                }
                //base.InvalidateControlThumbnail(rect);
            }
        }
    }
}
