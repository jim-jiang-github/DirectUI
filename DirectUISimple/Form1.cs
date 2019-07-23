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
    public partial class Form1 : Form
    {
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
        }
    }
}
