using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DirectUI.Common;
using DirectUI.Core;

namespace DirectUISample
{
    public partial class Form1 : Form
    {
        private DUIThreadSafeBitmapBrush backGridBrush = new DUIThreadSafeBitmapBrush(new Size(16, 20), (g, s) =>
        {
            using (DUISolidBrush brushFrameDark = new DUISolidBrush(Color.DarkGray))
            using (DUISolidBrush brushFrameLight = new DUISolidBrush(Color.Gray))
            {
                g.FillRectangle(brushFrameDark, new RectangleF(0, 0, 8, s.Height));
                g.FillRectangle(brushFrameLight, new RectangleF(8, 0, 8, s.Height));
                g.DrawLine(DUIPens.Black, new Point(0, s.Height - 1), new Point(16, s.Height - 1));
            }
        });
        public DUIThreadSafeBitmapBrush BackGridBrush
        {
            get
            {
                backGridBrush.Size = new Size(16, 20);
                return backGridBrush;
            }
        }
        public Form1()
        {
            InitializeComponent();
            DirectUI.Core.DUIScaleableControl dUIScaleableControl = new DirectUI.Core.DUIScaleableControl() { Dock = DockStyle.Fill, BackgroundImage = DirectUI.Common.DUIImage.FromImage(global::DirectUISample.Properties.Resources._2) };
            this.duiNativeControl1.DUIControls.Add(dUIScaleableControl);
            DirectUI.Core.DUIEditableCenterScaleParentControl dUIEditableCenterScaleParentControl = new DirectUI.Core.DUIEditableCenterScaleParentControl();
            DirectUI.Core.DUIEditableScaleParentControl dUIEditableScaleParentControl = new DirectUI.Core.DUIEditableScaleParentControl() { BorderWidth = 15, CanRotate = true };
            dUIEditableCenterScaleParentControl.BindingControl(dUIEditableScaleParentControl);
            dUIScaleableControl.DUIControls.Add(dUIEditableScaleParentControl);
            dUIScaleableControl.DUIControls.Add(dUIEditableCenterScaleParentControl);
            dUIScaleableControl.DUIControls.Add(dUIEditableCenterScaleParentControl);


            DirectUI.Core.DUIScrollableControl dUIScrollableControl = new DirectUI.Core.DUIScrollableControl() { Height = 300, BackColor = Color.Orange, Dock = DockStyle.Bottom };
            dUIScrollableControl.Paint += (s, e) =>
            {
                BackGridBrush.Draw(e.Graphics, e.ClipRectangle);
            };
            this.duiNativeControl1.DUIControls.Add(dUIScrollableControl);
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                Bar bar = new Bar() { Index = i, X = random.Next(50, 200), Width = random.Next(50, 200), BackColor = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)) };
                dUIScrollableControl.DUIControls.Add(bar);
            }
        }
    }
    public class Bar : DirectUI.Core.DUIEditableControl
    {
        public int Index { get; set; }
        public override EffectMargin CanEffectMargin => EffectMargin.Other | EffectMargin.Left | EffectMargin.Right;
        public override float Height => 20;
        public override float Y => Index * Height - 1;
        public Bar()
        {
        }
    }
}
