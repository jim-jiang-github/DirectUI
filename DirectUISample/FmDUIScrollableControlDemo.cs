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
    public partial class FmDUIScrollableControlDemo : Form
    {
        public FmDUIScrollableControlDemo()
        {
            InitializeComponent();
            DUIScrollableControl dUIScrollableControl = new DUIScrollableControl() { Dock = DockStyle.Fill, BackColor = Color.Violet };
            this.duiNativeControl1.DUIControls.Add(dUIScrollableControl);
            for (int i = 0; i < 100; i++)
            {
                dUIScrollableControl.DUIControls.Add(new DUIControl()
                {
                    X = i * 50,
                    Y = i * 10,
                    Width = 50,
                    Height = 10,
                    BackColor = Color.Yellow
                });
            }
        }
    }
}
