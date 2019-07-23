using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUICenterChangingEventArgs
    {
        private PointF oldCenter = PointF.Empty;
        private PointF newCenter = PointF.Empty;
        /// <summary> 旧的中心点
        /// </summary>
        public PointF OldCenter { get { return oldCenter; } }
        /// <summary> 新的中心点
        /// </summary>
        public PointF NewCenter { get { return newCenter; } }
        public DUICenterChangingEventArgs(PointF newCenter, PointF oldCenter)
        {
            this.newCenter = newCenter;
            this.oldCenter = oldCenter;
        }
    }
}
