using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIMouseEditChangingEventArgs
    {
        private RectangleF editStartBounds = RectangleF.Empty;
        private RectangleF currentBounds = RectangleF.Empty;
        private PointF editStartCenter = PointF.Empty;
        private PointF currentCenter = PointF.Empty;
        private PointF editStartSkew = PointF.Empty;
        private PointF currentSkew = PointF.Empty;
        private PointF editStartScale = PointF.Empty;
        private PointF currentScale = PointF.Empty;
        private float editStartRotateAngle = 0;
        private float currentRotateAngle = 0;
        private bool done = false;
        /// <summary> 旧的尺寸
        /// </summary>
        public RectangleF EditStartBounds { get { return editStartBounds; } }
        /// <summary> 新的尺寸
        /// </summary>
        public RectangleF CurrentBounds { get { return currentBounds; } }
        /// <summary> 旧的中心点
        /// </summary>
        public PointF EditStartCenter { get { return editStartCenter; } }
        /// <summary> 新的中心点
        /// </summary>
        public PointF CurrentCenter { get { return currentCenter; } }
        /// <summary> 旧的倾斜
        /// </summary>
        public PointF EditStartSkew { get { return editStartSkew; } }
        /// <summary> 新的倾斜
        /// </summary>
        public PointF CurrentSkew { get { return currentSkew; } }
        /// <summary> 旧的缩放值
        /// </summary>
        public PointF EditStartScale { get { return editStartScale; } }
        /// <summary> 新的缩放值
        /// </summary>
        public PointF CurrentScale { get { return currentScale; } }
        /// <summary> 旧的角度
        /// </summary>
        public float EditStartRotateAngle { get { return editStartRotateAngle; } }
        /// <summary> 新的角度
        /// </summary>
        public float CurrentRotateAngle { get { return currentRotateAngle; } }
        /// <summary> 是否完成编辑
        /// </summary>
        public bool Done { get { return done; } }
        public DUIMouseEditChangingEventArgs(
            RectangleF currentBounds
            , RectangleF editStartBounds
            , PointF currentCenter
            , PointF editStartCenter
            , PointF currentSkew
            , PointF editStartSkew
            , PointF currentScale
            , PointF editStartScale
            , float currentRotateAngle
            , float editStartRotateAngle
            , bool done)
        {
            this.currentBounds = currentBounds;
            this.editStartBounds = editStartBounds;
            this.currentCenter = currentCenter;
            this.editStartCenter = editStartCenter;
            this.currentSkew = currentSkew;
            this.editStartSkew = editStartSkew;
            this.currentScale = currentScale;
            this.editStartScale = editStartScale;
            this.currentRotateAngle = currentRotateAngle;
            this.editStartRotateAngle = editStartRotateAngle;
            this.done = done;
        }
    }
}
