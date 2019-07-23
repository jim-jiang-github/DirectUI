using DirectUI.Common;
using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DirectUI.Controls
{
    public class DUILabel : DUIControl
    {
        private string text = "DUILabel";
        public virtual string Text
        {
            get { return text; }
            set
            {
                text = value;
                SizeF size = DUITextRenderer.MeasureText(this.Text, this.Font);
                //this.Height = size.Height;
                //this.Width = size.Width;
                this.Invalidate();
            }
        }

        public DUILabel()
        {
            this.BorderWidth = 0;
            this.BackColor = Color.Transparent;
        }
        public override void OnPaintBackground(DUIPaintEventArgs e)
        {
            base.OnPaintBackground(e);
            using (DUISolidBrush sb = new DUISolidBrush(this.ForeColor))
            {
                e.Graphics.DrawString(Text, this.Font, sb, new Point(0, 0));
            }
        }
    }
}
