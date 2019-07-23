using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Controls
{
    public class DUINativeTabControl : DUINativeControl
    {
        #region 变量
        private DUITabControl dUITabControl = new DUITabControl() { Dock = System.Windows.Forms.DockStyle.Fill };
        #endregion
        #region 属性
        #endregion
        public DUINativeTabControl()
        {
            this.BackColor = Color.FromArgb(26, 30, 31);
            dUITabControl.BorderColor = Color.White;
            dUITabControl.BorderWidth = 4;
            dUITabControl.Radius = 15;
            DUITabPage t = new DUITabPage();
            t.Text = "1";
            t.DUIControls.Add(new DUIControl() { BackColor = Color.Red });
            DUITabPage t1 = new DUITabPage();
            t1.Text = "2";
            t1.DUIControls.Add(new DUIControl() { BackColor = Color.Gray });
            DUITabPage t2 = new DUITabPage();
            t2.Text = "3";
            t2.DUIControls.Add(new DUIControl() { BackColor = Color.Green });
            dUITabControl.TabPages.Add(t);
            dUITabControl.TabPages.Add(t1);
            dUITabControl.TabPages.Add(t2);
            this.DUIControls.Add(dUITabControl);
        }
    }
}
