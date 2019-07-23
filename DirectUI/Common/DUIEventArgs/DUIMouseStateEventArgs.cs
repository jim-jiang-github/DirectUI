using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DirectUI.Common
{
    public class DUIMouseStateEventArgs
    {
        private DUIControl affectedControl = null;
        private MouseButtons button;

        public DUIControl AffectedControl
        {
            get { return affectedControl; }
            set { affectedControl = value; }
        }

        public MouseButtons Button
        {
            get { return button; }
            set { button = value; }
        }
        public DUIMouseStateEventArgs(DUIControl affectedControl, MouseButtons button)
        {
            this.affectedControl = affectedControl;
            this.button = button;
        }
    }
}
