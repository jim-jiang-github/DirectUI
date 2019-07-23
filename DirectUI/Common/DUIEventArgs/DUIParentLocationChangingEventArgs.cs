using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIParentLocationChangingEventArgs
    {
        private PointF oldLocation = PointF.Empty;
        private PointF newLocation = PointF.Empty;
        /// <summary> 旧的坐标
        /// </summary>
        public PointF OldLocation { get { return oldLocation; } }
        /// <summary> 新的坐标
        /// </summary>
        public PointF NewLocation { get { return newLocation; } }
        private DUIControl control = null;
        /// <summary> DUIControl
        /// </summary>
        public DUIControl Control { get { return control; } }
        public DUIParentLocationChangingEventArgs(DUIControl control, PointF newLocation, PointF oldLocation)
        {
            this.control = control;
            this.newLocation = newLocation;
            this.oldLocation = oldLocation;
        }
    }
}
