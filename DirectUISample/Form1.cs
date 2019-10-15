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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FmDUIControlDemo().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new FmDUIEditableControlDemo().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new FmDUIScaleableControlDemo().ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new FmDUIScrollableControlDemo().ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new FmCompositeSample_Cubes().ShowDialog();
        }
    }
}
