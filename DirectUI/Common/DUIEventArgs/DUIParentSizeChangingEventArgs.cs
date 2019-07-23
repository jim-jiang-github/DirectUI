using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIParentSizeChangingEventArgs
    {
        private SizeF oldSize = SizeF.Empty;
        private SizeF newSize = SizeF.Empty;
        /// <summary> 旧的尺寸
        /// </summary>
        public SizeF OldSize { get { return oldSize; } }
        /// <summary> 新的尺寸
        /// </summary>
        public SizeF NewSize { get { return newSize; } }
        private DUIControl control = null;
        /// <summary> DUIControl
        /// </summary>
        public DUIControl Control { get { return control; } }
        public DUIParentSizeChangingEventArgs(DUIControl control, SizeF newSize, SizeF oldSize)
        {
            this.control = control;
            this.newSize = newSize;
            this.oldSize = oldSize;
        }
    }
}
