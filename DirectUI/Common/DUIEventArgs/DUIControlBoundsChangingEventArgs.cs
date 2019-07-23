using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIControlBoundsChangingEventArgs
    {
        private RectangleF oldBounds = RectangleF.Empty;
        private RectangleF newBounds = RectangleF.Empty;
        /// <summary> 旧的区域
        /// </summary>
        public RectangleF OldBounds { get { return oldBounds; } }
        /// <summary> 新的区域
        /// </summary>
        public RectangleF NewBounds { get { return newBounds; } }
        private DUIControl control = null;
        /// <summary> DUIControl
        /// </summary>
        public DUIControl Control { get { return control; } }
        public DUIControlBoundsChangingEventArgs(DUIControl control, RectangleF newBounds, RectangleF oldBounds)
        {
            this.control = control;
            this.newBounds = newBounds;
            this.oldBounds = oldBounds;
        }
    }
}
