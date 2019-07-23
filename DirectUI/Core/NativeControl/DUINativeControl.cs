using DirectUI.Share;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DirectUI.Core
{
    public class DUINativeControl : System.Windows.Forms.Control
    {
        #region 事件
        /// <summary> 在重绘控件时发生。
        /// </summary>
        public event DUIPaintEventHandler DUIPaint;
        /// <summary> 在重绘控件背景时发生。
        /// </summary>
        public event DUIPaintEventHandler PaintBackground;
        /// <summary> 在所有绘图操作完成之后发生,此绘图在子控件之上
        /// </summary>
        public event DUIPaintEventHandler PaintForeground;
        #endregion
        #region 变量
        private DUIContainer dUIContainer = null;
        #endregion
        #region 属性
        public DUIControl FocusedControl
        {
            get { return this.dUIContainer.FocusedControl; }
        }
        public DirectUI.Core.DUIControl.DUIControlCollection DUIControls
        {
            get { return this.dUIContainer.DUIControls; }
        }
        #endregion
        #region 构造函数
        public DUINativeControl()
        {
            this.dUIContainer = new DUIContainer(this);
            //指定控件的样式和行为
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);// 控件透明
            this.SetStyle(ControlStyles.UserPaint, true); //用户自行重绘
            //this.SetStyle(ControlStyles.ResizeRedraw, true); //调整大小时重绘
            //this.SetStyle(ControlStyles.DoubleBuffer, true);// 双缓冲
            this.SetStyle(ControlStyles.Opaque, false);
        }
        #endregion
        #region 重写
        //protected override bool ProcessDialogKey(Keys keyData)
        //{
        //    switch (keyData)
        //    {
        //        case Keys.Up:
        //            return false;
        //        case Keys.Down:
        //            return false;
        //        case Keys.Left:
        //            return false;
        //        case Keys.Right:
        //            return false;
        //    }
        //    return base.ProcessDialogKey(keyData);
        //}
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();
            base.OnMouseDown(e);
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == DirectUI.Win32.Const.WM.WM_PAINT)
            {
                this.dUIContainer.Invalidate();
            }
            if (dUIContainer != null) { dUIContainer.DoWndProc(ref m); }
            DefWndProcInternal(ref m);
        }
        internal virtual void DefWndProcInternal(ref Message m)
        {
            base.WndProc(ref m);
        }
        //protected override void WndProc(ref Message m)
        //{
        //    if (dUIContainer != null) { dUIContainer.DoWndProc(ref m); }
        //    base.WndProc(ref m);
        //}
        public virtual void OnPaint(DirectUI.Common.DUIPaintEventArgs e)
        {
            if (DUIPaint != null)
            {
                DUIPaint(this, e);
            }
        }
        public virtual void OnPaintBackground(DirectUI.Common.DUIPaintEventArgs e)
        {
            if (PaintBackground != null)
            {
                PaintBackground(this, e);
            }
        }
        public virtual void OnPaintForeground(DirectUI.Common.DUIPaintEventArgs e)
        {
            if (PaintForeground != null)
            {
                PaintForeground(this, e);
            }
        }
        #endregion
        #region 函数
        public new void SuspendLayout()
        {
            //base.SuspendLayout();
            this.dUIContainer.SuspendLayout();
        }
        public new void ResumeLayout(bool performLayout)
        {
            //base.ResumeLayout(performLayout);
            this.dUIContainer.ResumeLayout(performLayout);
        }
        public new void ResumeLayout()
        {
            //base.ResumeLayout();
            this.dUIContainer.ResumeLayout();
        }
        public new void Refresh()
        {
            this.dUIContainer.Refresh();
        }
        public void DUIInvalidate()
        {
            this.dUIContainer.Invalidate();
        }
        #endregion
    }
}
