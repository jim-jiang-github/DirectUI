using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIRotateAngleChangingEventArgs
    {
        private float oldRotateAngle = 0;
        private float newRotateAngle = 0;
        /// <summary> 旧的角度
        /// </summary>
        public float OldRotateAngle { get { return oldRotateAngle; } }
        /// <summary> 新的角度
        /// </summary>
        public float NewRotateAngle { get { return newRotateAngle; } }
        public DUIRotateAngleChangingEventArgs(float newRotateAngle, float oldRotateAngle)
        {
            this.newRotateAngle = newRotateAngle;
            this.oldRotateAngle = oldRotateAngle;
        }
    }
}
