using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIBoundsChangingEventArgs
    {
        private RectangleF oldBounds = RectangleF.Empty;
        private RectangleF newBounds = RectangleF.Empty;
        /// <summary> 旧的尺寸
        /// </summary>
        public RectangleF OldBounds { get { return oldBounds; } }
        /// <summary> 新的尺寸
        /// </summary>
        public RectangleF NewBounds { get { return newBounds; } }
        public DUIBoundsChangingEventArgs(RectangleF newBounds, RectangleF oldBounds)
        {
            this.newBounds = newBounds;
            this.oldBounds = oldBounds;
        }
    }
}
