using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIParentCenterChangingEventArgs
    {
        private PointF oldCenter = PointF.Empty;
        private PointF newCenter = PointF.Empty;
        /// <summary> 旧的中心点
        /// </summary>
        public PointF OldCenter { get { return oldCenter; } }
        /// <summary> 新的中心点
        /// </summary>
        public PointF NewCenter { get { return newCenter; } }
        private DUIControl control = null;
        /// <summary> DUIControl
        /// </summary>
        public DUIControl Control { get { return control; } }
        public DUIParentCenterChangingEventArgs(DUIControl control, PointF newCenter, PointF oldCenter)
        {
            this.control = control;
            this.newCenter = newCenter;
            this.oldCenter = oldCenter;
        }
    }
}
