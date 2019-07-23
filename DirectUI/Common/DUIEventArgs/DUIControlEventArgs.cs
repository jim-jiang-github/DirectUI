using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIControlEventArgs
    {
        private DUIControl control = null;
        /// <summary> DUIControl
        /// </summary>
        public DUIControl Control { get { return control; } }
        public DUIControlEventArgs(DUIControl control)
        {
            this.control = control;
        }
    }
}
