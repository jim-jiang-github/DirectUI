using DirectUI.Common;
using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirectUISample
{
    public partial class FmDUIControlDemo : Form
    {
        private DUIControl dUIControl = new DUIControl() { Width = 100, Height = 100, BackColor = Color.Blue };
        public FmDUIControlDemo()
        {
            InitializeComponent();
            this.duiNativeControl1.DUIControls.Add(dUIControl);
            dUIControl.Paint += (s, e) =>
            {
                e.Graphics.DrawString("这里可以自己绘制\r\n也可以是背景图", dUIControl.Font, DUIBrushes.White, PointF.Empty);
            };
        }

        private void nudX_ValueChanged(object sender, EventArgs e)
        {
            dUIControl.X = (float)nudX.Value;
        }

        private void nudY_ValueChanged(object sender, EventArgs e)
        {
            dUIControl.Y = (float)nudY.Value;
        }

        private void nudWidth_ValueChanged(object sender, EventArgs e)
        {
            dUIControl.Width = (float)nudWidth.Value;
        }

        private void nudHeight_ValueChanged(object sender, EventArgs e)
        {
            dUIControl.Height = (float)nudHeight.Value;
        }

        private void nudRotate_ValueChanged(object sender, EventArgs e)
        {
            dUIControl.Rotate = (float)nudRotate.Value;
        }

        private void nudSkewX_ValueChanged(object sender, EventArgs e)
        {
            dUIControl.SkewX = (float)nudSkewX.Value;
        }

        private void nudSkewY_ValueChanged(object sender, EventArgs e)
        {
            dUIControl.SkewY = (float)nudSkewY.Value;
        }

        private void nudScaleX_ValueChanged(object sender, EventArgs e)
        {
            dUIControl.ScaleX = (float)nudScaleX.Value;
        }

        private void nudScaleY_ValueChanged(object sender, EventArgs e)
        {
            dUIControl.ScaleY = (float)nudScaleY.Value;
        }

        private void nudBorder_ValueChanged(object sender, EventArgs e)
        {
            dUIControl.BorderWidth = (float)nudBorder.Value;
        }
    }
}
