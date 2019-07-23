using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIParentAnyChangingEventArgs
    {
        private RectangleF oldBounds = RectangleF.Empty;
        private RectangleF newBounds = RectangleF.Empty;
        private PointF oldCenter = PointF.Empty;
        private PointF newCenter = PointF.Empty;
        private PointF oldSkew = PointF.Empty;
        private PointF newSkew = PointF.Empty;
        private PointF oldScale = PointF.Empty;
        private PointF newScale = PointF.Empty;
        private float oldRotateAngle = 0;
        private float newRotateAngle = 0;
        /// <summary> 旧的尺寸
        /// </summary>
        public RectangleF OldBounds { get { return oldBounds; } }
        /// <summary> 新的尺寸
        /// </summary>
        public RectangleF NewBounds { get { return newBounds; } }
        /// <summary> 旧的中心点
        /// </summary>
        public PointF OldCenter { get { return oldCenter; } }
        /// <summary> 新的中心点
        /// </summary>
        public PointF NewCenter { get { return newCenter; } }
        /// <summary> 旧的倾斜
        /// </summary>
        public PointF OldSkew { get { return oldSkew; } }
        /// <summary> 新的倾斜
        /// </summary>
        public PointF NewSkew { get { return newSkew; } }
        /// <summary> 旧的缩放值
        /// </summary>
        public PointF OldScale { get { return oldScale; } }
        /// <summary> 新的缩放值
        /// </summary>
        public PointF NewScale { get { return newScale; } }
        /// <summary> 旧的角度
        /// </summary>
        public float OldRotateAngle { get { return oldRotateAngle; } }
        /// <summary> 新的角度
        /// </summary>
        public float NewRotateAngle { get { return newRotateAngle; } }
        private DUIControl control = null;
        /// <summary> DUIControl
        /// </summary>
        public DUIControl Control { get { return control; } }
        public DUIParentAnyChangingEventArgs(DUIControl control, RectangleF newBounds, RectangleF oldBounds, PointF newCenter, PointF oldCenter, PointF newSkew, PointF oldSkew, PointF newScale, PointF oldScale, float newRotateAngle, float oldRotateAngle)
        {
            this.control = control;
            this.newBounds = newBounds;
            this.oldBounds = oldBounds;
            this.newCenter = newCenter;
            this.oldCenter = oldCenter;
            this.newSkew = newSkew;
            this.oldSkew = oldSkew;
            this.newScale = newScale;
            this.oldScale = oldScale;
            this.newRotateAngle = newRotateAngle;
            this.oldRotateAngle = oldRotateAngle;
        }
    }
}
