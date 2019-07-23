using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIPolygonChangingEventArgs
    {
        private PointF[] oldPolygon = null;
        private PointF[]  newPolygon = null;
        /// <summary> 旧的角度
        /// </summary>
        public PointF[] OldPolygon { get { return oldPolygon; } }
        /// <summary> 新的角度
        /// </summary>
        public PointF[] NewPolygon { get { return newPolygon; } }
        public DUIPolygonChangingEventArgs(PointF[] newPolygon, PointF[] oldPolygon)
        {
            this.newPolygon = newPolygon;
            this.oldPolygon = oldPolygon;
        }
    }
}
