using DirectUI.Common;
using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Timers;

namespace DirectUI.Controls
{
    public class DUIButton : DUIControl
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
        private DUIImage mouseDownImage = null;
        private DUIImage mouseHoverImage = null;
        private DUIImage mouseNormalImage = null;
        private bool isMouseDown = false;
        private bool isMouseHover = false;
        #endregion
        #region 属性

        public DUIImage MouseDownImage
        {
            get
            {
                if (isMouseDown) { return mouseDownImage; }
                return null;
            }
            set { mouseDownImage = value; }
        }

        public DUIImage MouseHoverImage
        {
            get
            {
                if (!isMouseDown && this.RectangleToScreen(this.Bounds).Contains(MousePosition)) { return mouseHoverImage; }
                return null;
            }
            set { mouseHoverImage = value; }
        }

        public DUIImage MouseNormalImage
        {
            get
            {
                if (!isMouseDown && !isMouseHover) { return mouseNormalImage; }
                return null;
            }
            set { mouseNormalImage = value; }
        }
        public DUIButton()
        {
            this.Size = new Size(75, 23);
            this.Radius = 3;
            this.BackColor = Color.FromArgb(241, 241, 241);
            this.BorderColor = Color.FromArgb(112, 112, 112);
            this.BorderWidth = 1;
        }
        #endregion
        public override void OnMouseClick(DUIMouseEventArgs e)
        {
            base.OnMouseClick(e);
            OnClick(EventArgs.Empty);
        }
        public override void OnMouseDown(DUIMouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.isMouseDown = true;
            this.Invalidate();
        }
        public override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.isMouseHover = true;
            this.Invalidate();
        }
        public override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.isMouseHover = false;
            this.Invalidate();
        }
        public override void OnMouseUp(DUIMouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.isMouseDown = false;
            this.Invalidate();
        }
        public override void OnPaintBackground(DUIPaintEventArgs e)
        {
            e.Graphics.SmoothingMode = DUISmoothingMode.AntiAlias;
            RectangleF borderRect = new RectangleF(0, 0, this.Width - 1, this.Height - 1);
            using (DUISolidBrush borderBrush = new DUISolidBrush(this.Border.BorderColor))
            using (DUIPen borderPen = new DUIPen(this.Border.BorderColor, 1))
            {
                e.Graphics.FillRoundedRectangle(borderBrush, borderRect, this.Radius);
            }
            RectangleF backRect = new RectangleF(this.Border.BorderWidth, this.Border.BorderWidth, this.ClientSize.Width - 1, (this.ClientSize.Height - 1) / 2);
            RectangleF backShadowRect = new RectangleF(this.Border.BorderWidth, this.Border.BorderWidth + ((this.ClientSize.Height - 1) - (this.ClientSize.Height - 1) / 2), this.ClientSize.Width - 1, ((this.ClientSize.Height - 1) - (this.ClientSize.Height - 1) / 2));
            if (this.mouseDownImage == null)
            {
                if (isMouseDown)
                {
                    Color backColor = Color.FromArgb(this.BackColor.R, Math.Max(this.BackColor.G - 15, 0), Math.Max(this.BackColor.B - 25, 0));
                    Color shadowColor = Color.FromArgb(Math.Max(backColor.R - 25, 0), Math.Max(backColor.G - 25, 0), Math.Max(backColor.B - 25, 0));
                    using (DUISolidBrush backBrush = new DUISolidBrush(backColor))
                    using (DUISolidBrush backShadowBrush = new DUISolidBrush(shadowColor))
                    using (Pen backPen = new Pen(backColor, 1))
                    using (Pen backShadowPen = new Pen(shadowColor, 1))
                    {
                        e.Graphics.FillRoundedRectangle(backBrush, backRect, this.Radius);
                    }
                }
            }
            else
            {
                if (this.MouseDownImage != null)
                {
                    e.Graphics.DrawImage(this.MouseDownImage, new RectangleF(0, 0, this.ClientSize.Width, this.ClientSize.Height), new RectangleF(0, 0, this.mouseDownImage.Width, this.mouseDownImage.Height), GraphicsUnit.Pixel);
                }
            }
            if (this.mouseHoverImage == null)
            {
                if (!isMouseDown && this.RectangleToScreen(this.Bounds).Contains(MousePosition))
                {
                    Color backColor = Color.FromArgb(this.BackColor.R, Math.Min(this.BackColor.G + 15, 255), Math.Min(this.BackColor.B + 25, 255));
                    Color shadowColor = Color.FromArgb(Math.Max(backColor.R - 25, 0), Math.Max(backColor.G - 25, 0), Math.Max(backColor.B - 25, 0));
                    using (DUISolidBrush backBrush = new DUISolidBrush(backColor))
                    using (DUISolidBrush backShadowBrush = new DUISolidBrush(shadowColor))
                    using (Pen backPen = new Pen(backColor, 1))
                    using (Pen backShadowPen = new Pen(shadowColor, 1))
                    {
                        e.Graphics.FillRoundedRectangle(backBrush, backRect, this.Radius);
                    }
                }
            }
            else
            {
                if (this.MouseHoverImage != null)
                {
                    e.Graphics.DrawImage(this.MouseHoverImage, new RectangleF(0, 0, this.ClientSize.Width, this.ClientSize.Height), new RectangleF(0, 0, this.mouseHoverImage.Width, this.mouseHoverImage.Height), GraphicsUnit.Pixel);
                }
            }
            if (this.mouseNormalImage == null)
            {
                if (!isMouseDown && !isMouseHover)
                {
                    Color shadowColor = Color.FromArgb(Math.Max(this.BackColor.R - 25, 0), Math.Max(this.BackColor.G - 25, 0), Math.Max(this.BackColor.B - 25, 0));
                    using (DUISolidBrush backBrush = new DUISolidBrush(this.BackColor))
                    using (DUISolidBrush backShadowBrush = new DUISolidBrush(shadowColor))
                    using (Pen backPen = new Pen(this.BackColor, 1))
                    using (Pen backShadowPen = new Pen(shadowColor, 1))
                    {
                        e.Graphics.FillRoundedRectangle(backBrush, backRect, this.Radius);
                    }
                }
            }
            else
            {
                if (this.MouseNormalImage != null)
                {
                    e.Graphics.DrawImage(this.MouseNormalImage, new RectangleF(0, 0, this.ClientSize.Width, this.ClientSize.Height), new RectangleF(0, 0, this.mouseNormalImage.Width, this.mouseNormalImage.Height), GraphicsUnit.Pixel);
                }
            }
            #region 背景图
            if (this.BackgroundImage != null)
            {
                e.Graphics.DrawImage(this.BackgroundImage, new RectangleF(this.Border.BorderWidth, this.Border.BorderWidth, this.ClientSize.Width, this.ClientSize.Height));
            }
            #endregion
            #region 绘制文字
            SizeF sizeF = e.Graphics.MeasureString(this.Text, this.Font);
            using (DUISolidBrush textbrush = new DUISolidBrush(this.ForeColor))
            {
                e.Graphics.DrawString(this.Text, this.Font, textbrush, new PointF(this.Width / 2 - sizeF.Width / 2, this.Height / 2 - sizeF.Height / 2));
            }
            #endregion
        }
        public void PerformClick()
        {
            OnClick(null);
            base.OnMouseClick(new DUIMouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, 0, 0, 0));
        }
    }
}
