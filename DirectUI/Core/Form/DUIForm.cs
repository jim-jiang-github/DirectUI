using DirectUI.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DirectUI.Core
{
    public partial class DUIForm : System.Windows.Forms.Form
    {
        #region 事件
        /// <summary> 在重绘控件时发生。
        /// </summary>
        public event DUIPaintEventHandler Paint;
        /// <summary> 在重绘控件背景时发生。
        /// </summary>
        public event DUIPaintEventHandler PaintBackground;
        /// <summary> 在所有绘图操作完成之后发生,此绘图在子控件之上
        /// </summary>
        public event DUIPaintEventHandler PaintForeground;
        #endregion
        #region 变量
        internal DUIContainer dUIContainer = null;
        #endregion
        #region 属性
        /// <summary> 获得焦点的控件
        /// </summary>
        public DUIControl FocusedControl
        {
            get { return this.dUIContainer.FocusedControl; }
        }
        public virtual DirectUI.Core.DUIControl.DUIControlCollection DUIControls
        {
            get { return this.dUIContainer.DUIControls; }
        }
        #endregion
        #region 构造函数
        public DUIForm()
        {
            Init();
        }
        protected virtual void Init()
        {
            //指定控件的样式和行为
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);// 控件透明
            this.SetStyle(ControlStyles.UserPaint, true); //用户自行重绘
            this.dUIContainer = new DUIContainer(this);
        }
        #endregion
        #region 重写
        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Tab:
                    return false;
                case Keys.Up:
                    return false;
                case Keys.Down:
                    return false;
                case Keys.Left:
                    return false;
                case Keys.Right:
                    return false;
            }
            return base.ProcessDialogKey(keyData);
        }
        protected override void WndProc(ref Message m)
        {
            if (dUIContainer != null) { dUIContainer.DoWndProc(ref m); }
            DefWndProcInternal(ref m);
        }
        internal virtual void DefWndProcInternal(ref Message m)
        {
            base.WndProc(ref m);
        }
        public virtual void OnPaint(DirectUI.Common.DUIPaintEventArgs e)
        {
            if (Paint != null)
            {
                Paint(this, e);
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
            base.SuspendLayout();
            this.dUIContainer.SuspendLayout();
        }
        public new void ResumeLayout(bool performLayout)
        {
            base.ResumeLayout(performLayout);
            this.dUIContainer.ResumeLayout(performLayout);
        }
        public new void ResumeLayout()
        {
            base.ResumeLayout();
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
