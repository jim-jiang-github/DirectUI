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
    public partial class FmDUIScaleableControlDemo : Form
    {
        public FmDUIScaleableControlDemo()
        {
            InitializeComponent();
            DUIThreadSafeBitmapBrush backMeshBrush = new DUIThreadSafeBitmapBrush(new Size(20, 20), (g, s) =>
            {
                using (DUISolidBrush brushMeshDark = new DUISolidBrush(Color.FromArgb(29, 29, 29)))
                using (DUISolidBrush brushMeshLight = new DUISolidBrush(Color.FromArgb(159, 159, 159)))
                {
                    g.FillRectangle(brushMeshDark, new Rectangle(0, 0, 10, 10));
                    g.FillRectangle(brushMeshDark, new Rectangle(10, 10, 10, 10));
                    g.FillRectangle(brushMeshLight, new Rectangle(10, 0, 10, 10));
                    g.FillRectangle(brushMeshLight, new Rectangle(0, 10, 10, 10));
                }
            });
            DUIImage dUIImage = DUIImage.FromFile(@"Resources\1.jpg");
            DUIScaleableControl dUIScaleableControl = new DUIScaleableControl() { Dock = DockStyle.Fill };
            dUIScaleableControl.Paint += (s, e) =>
            {
                backMeshBrush.Draw(e.Graphics, dUIScaleableControl.ScaleableBounds);
                e.Graphics.DrawImage(dUIImage, 0, 0);
            };
            this.duiNativeControl1.DUIControls.Add(dUIScaleableControl);
        }
    }
}
