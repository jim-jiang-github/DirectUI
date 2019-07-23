using DirectUI.Common;
using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Controls
{
    public class DUIToolBarItem : DUIControl
    {
        #region 事件
        public event EventHandler Click;
        protected virtual void OnClick(EventArgs e)
        {
            if (Click != null)
            {
                Click(this, e);
            }
        }
        #endregion
        #region 变量
        #endregion
        #region 属性
        /// <summary> 显示的图片
        /// </summary>
        public DUIImage Image { get; set; }
        #endregion
        public DUIToolBarItem()
        {
            this.BackColor = Color.Transparent;
            this.BorderWidth = 0;
        }
        #region 重写
        public override void OnMouseEnter(EventArgs e)
        {
            //this.BackColor = Color.FromArgb(61, 61, 61);
            this.Invalidate();
            base.OnMouseEnter(e);
        }
        public override void OnMouseLeave(EventArgs e)
        {
            this.BackColor = Color.Transparent;
            this.Invalidate();
            base.OnMouseLeave(e);
        }
        public override void OnPaintBackground(DUIPaintEventArgs e)
        {
            base.OnPaintBackground(e);
            using (DUISolidBrush brush = new DUISolidBrush(BackColor))
            {
                e.Graphics.FillRectangle(brush, e.ClipRectangle);
            }
            if (this.Image != null)
            {
                e.Graphics.DrawImage(this.Image, e.ClipRectangle, new Rectangle(0, 0, this.Image.Width, this.Image.Height), GraphicsUnit.Pixel);
            }
        }
        public override void OnMouseClick(DUIMouseEventArgs e)
        {
            base.OnMouseClick(e);
            OnClick(EventArgs.Empty);
        }
        #endregion
        #region 函数
        public void PerformClick()
        {
            OnClick(EventArgs.Empty);
            base.OnMouseClick(new DUIMouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
        }
        #endregion
    }
}
