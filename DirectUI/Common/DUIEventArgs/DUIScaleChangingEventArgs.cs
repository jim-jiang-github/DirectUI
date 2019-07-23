using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIScaleChangingEventArgs
    {
        private PointF oldScale = PointF.Empty;
        private PointF newScale = PointF.Empty;
        /// <summary> 旧的缩放值
        /// </summary>
        public PointF OldScale { get { return oldScale; } }
        /// <summary> 新的缩放值
        /// </summary>
        public PointF NewScale { get { return newScale; } }
        public DUIScaleChangingEventArgs(PointF newScale, PointF oldScale)
        {
            this.newScale = newScale;
            this.oldScale = oldScale;
        }
    }
}
