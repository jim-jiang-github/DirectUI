using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIParentEventArgs
    {
        private DUIControl control = null;
        /// <summary> DUIControl
        /// </summary>
        public DUIControl Control { get { return control; } }
        public DUIParentEventArgs(DUIControl control)
        {
            this.control = control;
        }
    }
}
