using DirectUI.Common;
using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Controls
{
    public class DUITabPage : DUIScrollableControl
    {
        public float TitleX { get; set; }
        public float TitleY { get; set; }
        public PointF TitleLocation { get { return new PointF(TitleX, TitleY); } }
        internal DUITabControl TabControl { get; set; }
        public bool Selected { get { return this.TabControl == null ? false : this.TabControl.SelectedPage == this; } }
        public override bool Visible { get => base.Visible && this.Selected; set => base.Visible = value; }
        public DUITabPage()
        {
            this.Dock = System.Windows.Forms.DockStyle.Fill;
        }
        public override void OnPaintBackground(DUIPaintEventArgs e)
        {
            this.TabControl.DrawTabPage(this, e);
        }
    }
}
