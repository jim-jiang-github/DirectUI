using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIControlRotateAngleChangingEventArgs
    {
        private float oldRotateAngle = 0;
        private float newRotateAngle = 0;
        /// <summary> 旧的坐标
        /// </summary>
        public float OldRotateAngle { get { return oldRotateAngle; } }
        /// <summary> 新的坐标
        /// </summary>
        public float NewRotateAngle { get { return newRotateAngle; } }
        private DUIControl control = null;
        /// <summary> DUIControl
        /// </summary>
        public DUIControl Control { get { return control; } }
        public DUIControlRotateAngleChangingEventArgs(DUIControl control, float newRotateAngle, float oldRotateAngle)
        {
            this.control = control;
            this.newRotateAngle = newRotateAngle;
            this.oldRotateAngle = oldRotateAngle;
        }
    }
}
