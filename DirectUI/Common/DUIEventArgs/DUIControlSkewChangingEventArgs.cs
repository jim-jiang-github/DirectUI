using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIControlSkewChangingEventArgs
    {
        private PointF oldSkew = PointF.Empty;
        private PointF newSkew = PointF.Empty;
        /// <summary> 旧的倾斜值
        /// </summary>
        public PointF OldSkew { get { return oldSkew; } }
        /// <summary> 新的倾斜值
        /// </summary>
        public PointF NewSkew { get { return newSkew; } }
        private DUIControl control = null;
        /// <summary> DUIControl
        /// </summary>
        public DUIControl Control { get { return control; } }
        public DUIControlSkewChangingEventArgs(DUIControl control, PointF newSkew, PointF oldSkew)
        {
            this.control = control;
            this.newSkew = newSkew;
            this.oldSkew = oldSkew;
        }
    }
}
