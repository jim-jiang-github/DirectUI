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
    public partial class FmDUIEditableControlDemo : Form
    {
        private DUIEditableCenterControl dUIEditableCenterControl = new DUIEditableCenterControl();
        private Random random = new Random();
        public FmDUIEditableControlDemo()
        {
            InitializeComponent();
            DUIEditableControlWithCenter dUIEditableControl1 = new DUIEditableControlWithCenter(dUIEditableCenterControl)
            {
                X = random.Next(0, this.ClientSize.Width - 100),
                Y = random.Next(0, this.ClientSize.Height - 100),
                Width = 100,
                Height = 100,
                CanRotate = true,
                CanSkew = true,
                CenterX = 35,
                CenterY = 35,
                IsCenterFollow = true,
                BorderWidth = 15,
                BackgroundImage = DUIImage.FromFile(@"Resources\1.jpg")
            };
            DUIEditableControlWithCenter dUIEditableControl2 = new DUIEditableControlWithCenter(dUIEditableCenterControl)
            {
                X = random.Next(0, this.ClientSize.Width - 100),
                Y = random.Next(0, this.ClientSize.Height - 100),
                Width = 100,
                Height = 100,
                CanRotate = true,
                CanSkew = true,
                CenterX = 35,
                CenterY = 35,
                IsCenterFollow = true,
                BorderWidth = 15,
                BackgroundImage = DUIImage.FromFile(@"Resources\2.jpg")
            };
            this.duiNativeControl1.DUIControls.Add(dUIEditableCenterControl);
            this.duiNativeControl1.DUIControls.Add(dUIEditableControl1);
            this.duiNativeControl1.DUIControls.Add(dUIEditableControl2);
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Title = "选择图片哦";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.duiNativeControl1.DUIControls.Add(new DUIEditableControlWithCenter(dUIEditableCenterControl)
                {
                    X = random.Next(0, this.ClientSize.Width - 100),
                    Y = random.Next(0, this.ClientSize.Height - 100),
                    Width = 100,
                    Height = 100,
                    CanRotate = true,
                    CanSkew = true,
                    CenterX = 35,
                    CenterY = 35,
                    IsCenterFollow = true,
                    BorderWidth = 15,
                    BackgroundImage = DUIImage.FromFile(ofd.FileName)
                });
            }
        }
    }
    public class DUIEditableControlWithCenter : DUIEditableControl
    {
        private DUIEditableCenterControl dUIEditableCenterControl;
        public DUIEditableControlWithCenter(DUIEditableCenterControl dUIEditableCenterControl)
        {
            this.dUIEditableCenterControl = dUIEditableCenterControl;
        }

        public override void OnLostFocus(EventArgs e)
        {
            this.dUIEditableCenterControl.BindingControl(null);
            base.OnLostFocus(e);
        }
        public override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.dUIEditableCenterControl.BindingControl(this);
        }
    }
}
