using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIParentChangingEventArgs
    {
        private DUIControl oldParent = null;
        private DUIControl newParent = null;
        /// <summary> 旧的坐标
        /// </summary>
        public DUIControl OldParent { get { return oldParent; } }
        /// <summary> 新的坐标
        /// </summary>
        public DUIControl NewParent { get { return newParent; } }
        public DUIParentChangingEventArgs(DUIControl newParent, DUIControl oldParent)
        {
            this.newParent = newParent;
            this.oldParent = oldParent;
        }
    }
}
