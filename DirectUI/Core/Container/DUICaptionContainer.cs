using DirectUI.Common;
using DirectUI.Share;
using DirectUI.Win32.Const;
using DirectUI.Win32.Struct;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DirectUI.Core
{
    /// <summary> 标题栏里面DUIControl的容器
    /// </summary>
    internal class DUICaptionContainer : DUIContainer
    {
        #region 变量
        /// <summary> 所在的DUISkinForm
        /// </summary>
        private bool stateMouseDoubleClick = false;
        private bool stateMouseClick = false;
        private bool stateMouseDown = false;
        private MouseButtons stateMouseDownButton = MouseButtons.None;
        #endregion
        #region 属性
        private DUISkinForm Owner { get { return this.owner as DUISkinForm; } }
        #endregion
        #region 重写
        public override float Width
        {
            get
            {
                return this.owner.Width;
            }
        }
        public override float Height
        {
            get
            {
                return this.owner.Height;
            }
        }
        public override PointF PointToClient(PointF p)
        {
            p = new PointF(p.X + this.Owner.BorderWidthInternal, p.Y + this.Owner.CaptionHeightInternal);
            if (owner.IsDisposed)
            {
                return PointF.Empty;
            }
            return owner.PointToClient(Point.Ceiling(p));
        }
        public override PointF PointToScreen(PointF p)
        {
            p = new PointF(p.X - this.Owner.BorderWidthInternal, p.Y - this.Owner.CaptionHeightInternal);
            if (owner.IsDisposed)
            {
                return PointF.Empty;
            }
            return owner.PointToScreen(Point.Ceiling(p));
        }
        public override RectangleF RectangleToClient(RectangleF r)
        {
            r = new RectangleF(r.X - this.Owner.BorderWidthInternal, r.Y - this.Owner.CaptionHeightInternal, r.Width, r.Height);
            if (owner.IsDisposed)
            {
                return RectangleF.Empty;
            }
            return owner.RectangleToClient(Rectangle.Ceiling(r));
        }
        public override RectangleF RectangleToScreen(RectangleF r)
        {
            r = new RectangleF(r.X - this.Owner.BorderWidthInternal, r.Y - this.Owner.CaptionHeightInternal, r.Width, r.Height);
            if (owner.IsDisposed)
            {
                return RectangleF.Empty;
            }
            return owner.RectangleToScreen(Rectangle.Ceiling(r));
        }
        public override PointF PointToBaseParent(PointF p)
        {
            return p;
        }
        public override void Invalidate()
        {
            this.invalidateStack.Push(new Action(() =>
            {
                DateTime dt = DateTime.Now;
                this.dUIGraphics.BeginDraw();
                this.dUIGraphics.Clear(this.BackColor);
                this.DoPaint(new DUIPaintEventArgs(this.dUIGraphics, new RectangleF(0, 0, this.Width, this.Height)));
                this.dUIGraphics.EndDraw();
                this.dUIGraphics.ResetTransform();
                GC.Collect();
                System.Diagnostics.Debug.WriteLine((DateTime.Now - dt).TotalMilliseconds);
            }));
        }
        protected override void WndProc(ref Message m)
        {
            if (this.Owner.FormBorderStyle == System.Windows.Forms.FormBorderStyle.None)
            {
                this.Owner.DefWndProcInternal(ref m);
                return;
            }
            switch (m.Msg)
            {
                #region WM_NCCALCSIZE (修改客户端区域)
                case WM.WM_NCCALCSIZE:
                    if (m.WParam != IntPtr.Zero)
                    {
                        NCCALCSIZE_PARAMS rcsize = (NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam, typeof(NCCALCSIZE_PARAMS));
                        rcsize.rcNewWindow.Left += (this.Owner.BorderWidthInternal - 8);
                        rcsize.rcNewWindow.Top += this.Owner.CaptionHeightInternal - SystemInformation.CaptionHeight - 6;
                        rcsize.rcNewWindow.Right -= (this.Owner.BorderWidthInternal - 8);
                        rcsize.rcNewWindow.Bottom -= (this.Owner.BorderWidthInternal - 8);
                        Marshal.StructureToPtr(rcsize, m.LParam, false);
                    }
                    m.Result = new IntPtr(1);
                    break;
                #endregion
                #region WM_NCLBUTTONDOWN (标题栏区域鼠标左键按下)
                case WM.WM_NCLBUTTONDOWN:
                    WmCaptionMouseDown(ref m, MouseButtons.Left, 1);
                    this.stateMouseClick = true;
                    this.stateMouseDown = true;
                    this.stateMouseDownButton = MouseButtons.Left;
                    return;
                #endregion
                #region WM_NCRBUTTONDOWN (标题栏区域鼠标右键按下)
                case WM.WM_NCRBUTTONDOWN:
                    WmCaptionMouseDown(ref m, MouseButtons.Right, 1);
                    this.stateMouseClick = true;
                    this.stateMouseDown = true;
                    this.stateMouseDownButton = MouseButtons.Left;
                    return;
                #endregion
                #region WM_NCMOUSEMOVE (标题栏区域鼠标移动)
                case WM.WM_NCMOUSEMOVE:
                    if (stateMouseDown && MouseButtons == System.Windows.Forms.MouseButtons.None)
                    {
                        WmCaptionMouseUp(ref m, this.stateMouseDownButton, 1);
                        this.stateMouseDown = false;
                    }
                    else
                    {
                        WmCaptionMouseMove(ref m, MouseButtons, 0);
                    }
                    break;
                #endregion
                #region WM_NCLBUTTONUP (标题栏区域鼠标左键弹起)
                case WM.WM_NCLBUTTONUP:
                    WmCaptionMouseUp(ref m, MouseButtons.Left, 1);
                    return;
                #endregion
                #region WM_NCRBUTTONUP (标题栏区域鼠标右键弹起)
                case WM.WM_NCRBUTTONUP:
                    WmCaptionMouseUp(ref m, MouseButtons.Right, 1);
                    return;
                #endregion
                #region WM_NCLBUTTONDBLCLK (标题栏区域鼠标左键双击)
                case WM.WM_NCLBUTTONDBLCLK:
                    WmCaptionMouseDown(ref m, MouseButtons.Left, 2);
                    this.stateMouseDoubleClick = true;
                    return;
                #endregion
                #region WM_NCRBUTTONDBLCLK (标题栏区域鼠标右键双击)
                case WM.WM_NCRBUTTONDBLCLK:
                    WmCaptionMouseDown(ref m, MouseButtons.Right, 2);
                    this.stateMouseDoubleClick = true;
                    return;
                #endregion
                #region WM_NCMOUSELEAVE (鼠标离开)
                case WM.WM_NCMOUSELEAVE:
                    if (!this.stateMouseDown)
                    {
                        if (this.DUIControlShare.MouseMoveDUIControl != null)
                        {
                            this.DUIControlShare.MouseMoveDUIControl.DoMouseLeave(EventArgs.Empty);
                            this.DUIControlShare.MouseMoveDUIControl = null;
                        }
                        this.stateMouseClick = false;
                        this.stateMouseDoubleClick = false;
                    }
                    break;
                #endregion
                #region WM_NCMOUSEHOVER (鼠标停留)
                case WM.WM_NCMOUSEHOVER:
                    this.DoMouseHover(EventArgs.Empty);
                    return;
                #endregion
                #region WM_NCPAINT (标题栏绘制)
                //case WM.WM_SETCURSOR:
                case WM.WM_NCUAHDRAWCAPTION:
                    this.Invalidate();
                    return;
                case WM.WM_NCACTIVATE:
                    this.Owner.DefWndProcInternal(ref m);
                    this.Invalidate();
                    return;
                case WM.WM_NCPAINT:
                    this.Invalidate();
                    return;
                #endregion
                #region WM_SYSCOMMAND (系统命令，最大化最小化关闭之类的)
                case WM.WM_SYSCOMMAND:
                    if (m.WParam.ToInt32() == SC.SC_RESTORE)
                    {
                        this.Owner.DoNormal();
                        return;
                    }
                    if (m.WParam.ToInt32() == SC.SC_MINIMIZE)
                    {
                        this.Owner.DoMinimize();
                        return;
                    }
                    if (m.WParam.ToInt32() == SC.SC_MAXIMIZE)
                    {
                        this.Owner.DoMaximize();
                        return;
                    }
                    if (m.WParam.ToInt32() == SC.SC_MOUSEMENU)
                    {
                        return;
                    }
                    break;
                #endregion
            }
            //base.WndProc(ref m);
            this.Owner.DefWndProcInternal(ref m);
        }
        private void WmCaptionMouseDown(ref Message m, MouseButtons button, int clicks)
        {
            int wp = m.WParam.ToInt32();
            long lp = m.LParam.ToInt64();
            int posX = Win32.Helper.SignedLOWORD(lp);
            int posY = Win32.Helper.SignedHIWORD(lp);
            Point mouseLocation = new Point(posX - this.owner.Location.X, posY - this.owner.Location.Y);
            this.DoMouseDown(new DUIMouseEventArgs(button, clicks, mouseLocation.X, mouseLocation.Y, 0));
            if (this.GetChildAtPoint(mouseLocation) == null)
            {
                if (clicks == 2)
                {
                    if (this.Owner.WindowState == FormWindowState.Normal)
                    {
                        this.Owner.DoMaximize();
                    }
                    else if (this.Owner.WindowState == FormWindowState.Maximized)
                    {
                        this.Owner.DoNormal();
                    }
                }
                else
                {
                    this.Owner.DefWndProcInternal(ref m);
                }
            }
        }
        private void WmCaptionMouseMove(ref Message m, MouseButtons button, int clicks)
        {
            int wp = m.WParam.ToInt32();
            long lp = m.LParam.ToInt64();
            int posX = Win32.Helper.SignedLOWORD(lp);
            int posY = Win32.Helper.SignedHIWORD(lp);
            Point mouseLocation = new Point(posX - this.owner.Location.X, posY - this.owner.Location.Y);
            this.DoMouseMove(new DUIMouseEventArgs(button, clicks, mouseLocation.X, mouseLocation.Y, 0));
            if (this.GetChildAtPoint(mouseLocation) == null)
            {
                this.Owner.DefWndProcInternal(ref m);
            }
        }
        private void WmCaptionMouseUp(ref Message m, MouseButtons button, int clicks)
        {
            int wp = m.WParam.ToInt32();
            long lp = m.LParam.ToInt64();
            int posX = Win32.Helper.SignedLOWORD(lp);
            int posY = Win32.Helper.SignedHIWORD(lp);
            Point mouseLocation = new Point(posX - this.owner.Location.X, posY - this.owner.Location.Y);
            this.DoMouseUp(new DUIMouseEventArgs(button, clicks, mouseLocation.X, mouseLocation.Y, 0));
            if (stateMouseClick)
            {
                this.DoMouseClick(new DUIMouseEventArgs(button, 1, mouseLocation.X, mouseLocation.Y, 0));
                this.DoMouseLeave();
            }
            if (stateMouseDoubleClick)
            {
                this.DoMouseDoubleClick(new DUIMouseEventArgs(button, 2, mouseLocation.X, mouseLocation.Y, 0));
                this.DoMouseLeave();
            }
            this.stateMouseClick = false;
            this.stateMouseDoubleClick = false;
            if (this.GetChildAtPoint(mouseLocation) == null)
            {
                this.Owner.DefWndProcInternal(ref m);
            }
        }
        protected override void RegeditMouseEvent()
        {
            //base.RegeditMouseEvent();
        }
        protected override void RegeditKeyboardEvent()
        {
            //base.RegeditKeyboardEvent();
        }
        protected override void RegeditPaintEvent()
        {
            //base.RegeditPaintEvent();
        }
        #endregion
        public DUICaptionContainer(DUISkinForm owner)
            : base(owner)
        {
            this.Owner.Load += (s, e) =>
            {
                //记录坐标和尺寸
                this.Owner.lastSysCommandLocation = this.owner.Location;
                this.Owner.lastSysCommandSize = this.owner.Size;
            };
            this.Owner.ResizeEnd += (s, e) =>
            {
                if (this.Owner.WindowState == FormWindowState.Normal)
                {
                    //记录坐标和尺寸
                    this.Owner.lastSysCommandLocation = this.owner.Location;
                    this.Owner.lastSysCommandSize = this.owner.Size;
                }
            };
        }
        #region 函数
        /// <summary> 重绘边框
        /// </summary>
        public void BorderInvalidate(DUIGraphics dUIGraphics)
        {
            BorderInvalidate(new RectangleF(0, 0, this.Width, this.Height - this.Owner.CaptionHeightInternal));
        }
        public virtual void BorderInvalidate(RectangleF rect)
        {
            //if (this.Height <= this.Owner.CaptionHeightInternal || this.Owner.BorderWidthInternal == 0)
            //{
            //    return;
            //}
            //Rectangle borderBounds = new Rectangle(0, 0, this.Width, this.Height - this.Owner.CaptionHeightInternal); //边框区域
            //Region borderRegion = new Region(borderBounds); //标题栏区域
            //borderRegion.Intersect(borderBounds);
            //Rectangle excludeBounds = new Rectangle(this.Owner.BorderWidthInternal, 0, this.Width - this.Owner.BorderWidthInternal * 2, this.Height - this.Owner.CaptionHeightInternal - this.Owner.BorderWidthInternal);
            //borderRegion.Exclude(excludeBounds);
            //using (Graphics g = Graphics.FromImage(ControlThumbnail))
            //{
            //    g.TranslateTransform(0, this.Owner.CaptionHeightInternal);
            //    g.SetClip(borderRegion, System.Drawing.Drawing2D.CombineMode.Replace);
            //    OnBorderPaint(new PaintEventArgs(g, borderBounds));
            //}
        }
        protected virtual void OnBorderPaint(DUIPaintEventArgs e)
        {
            using (SolidBrush borderColorBrush = new SolidBrush(this.Owner.BorderColor == Color.Transparent ? this.Owner.BackColor : this.Owner.BorderColor))
            {
                e.Graphics.FillRectangle(borderColorBrush, e.ClipRectangle);
            }
        }
        /// <summary> 重绘标题栏
        /// </summary>
        public void CaptionInvalidate(DUIGraphics dUIGraphics)
        {
            RectangleF captionBounds = new RectangleF(0, 0, this.Width, this.Owner.CaptionHeightInternal); //标题栏区域
            this.Owner.OnCaptionPaint(new DUIPaintEventArgs(dUIGraphics, captionBounds));
            this.DoPaint(new DUIPaintEventArgs(dUIGraphics, captionBounds));
            //Region captionRegion = new Region(captionBounds); //标题栏区域
            //captionRegion.Intersect(region);
            //using (Graphics g = Graphics.FromImage(ControlThumbnail))
            //{
            //    g.SetClip(captionRegion, System.Drawing.Drawing2D.CombineMode.Replace);
            //    RectangleF rectF = captionRegion.GetBounds(g);
            //    this.Owner.OnCaptionPaint(new PaintEventArgs(g, captionBounds));
            //    this.DoPaint(new PaintEventArgs(g, new Rectangle((int)rectF.X, (int)rectF.Y, (int)rectF.Width, (int)rectF.Height)));
            //}
        }
        #endregion
    }
}
