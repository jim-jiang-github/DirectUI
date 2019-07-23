using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIParentScaleChangingEventArgs
    {
        private PointF oldScale = PointF.Empty;
        private PointF newScale = PointF.Empty;
        /// <summary> 旧的缩放值
        /// </summary>
        public PointF OldScale { get { return oldScale; } }
        /// <summary> 新的缩放值
        /// </summary>
        public PointF NewScale { get { return newScale; } }
        private DUIControl control = null;
        /// <summary> DUIControl
        /// </summary>
        public DUIControl Control { get { return control; } }
        public DUIParentScaleChangingEventArgs(DUIControl control, PointF newScale, PointF oldScale)
        {
            this.control = control;
            this.newScale = newScale;
            this.oldScale = oldScale;
        }
    }
}
