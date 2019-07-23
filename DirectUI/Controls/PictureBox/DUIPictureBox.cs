using DirectUI.Common;
using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Controls
{
    public class DUIPictureBox : DUIControl
    {
        private DUIImage image = null;

        public virtual DUIImage Image
        {
            get { return image; }
            set
            {
                image = value;
                this.Invalidate();
            }
        }
        public DUIPictureBox()
        {
            //this.BackColor = Color.Transparent;
        }
        public override void OnPaint(DUIPaintEventArgs e)
        {
            //e.Graphics.SmoothingMode = DUISmoothingMode.AntiAlias;
            if (this.Image != null)
            {
                float cw = e.ClipRectangle.Width;
                float ch = e.ClipRectangle.Height;
                float iw = this.Image.Width;
                float ih = this.Image.Height;
                if (this.Image.Width > this.Image.Height)
                {
                    iw = cw;
                    ih = (float)this.Image.Height * iw / (float)this.Image.Width;
                }
                else
                {
                    ih = ch;
                    iw = (float)this.Image.Width * ih / (float)this.Image.Height;
                }
                e.Graphics.DrawImage(this.Image, new RectangleF(0, 0, e.ClipRectangle.Width, e.ClipRectangle.Height), new RectangleF(0, 0, this.Image.Width, this.Image.Height), GraphicsUnit.Pixel);
            }
            base.OnPaint(e);
        }
    }
}
