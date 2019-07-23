using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUILocationChangingEventArgs
    {
        private PointF oldLocation = PointF.Empty;
        private PointF newLocation = PointF.Empty;
        /// <summary> 旧的坐标
        /// </summary>
        public PointF OldLocation { get { return oldLocation; } }
        /// <summary> 新的坐标
        /// </summary>
        public PointF NewLocation { get { return newLocation; } }
        public DUILocationChangingEventArgs(PointF newLocation, PointF oldLocation)
        {
            this.newLocation = newLocation;
            this.oldLocation = oldLocation;
        }
    }
}
