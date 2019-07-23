using DirectUI.Common;
/********************************************************************
 * * 使本项目源码前请仔细阅读以下协议内容，如果你同意以下协议才能使用本项目所有的功能,
 * * 否则如果你违反了以下协议，有可能陷入法律纠纷和赔偿，作者保留追究法律责任的权利。
 * * Copyright (C) 
 * * 作者： 煎饼的归宿    QQ：375324644   
 * * 请保留以上版权信息，否则作者将保留追究法律责任。
 * * 最后修改：BF-20150503UIWA
 * * 创建时间：2018/1/5 8:51:01
********************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Core
{
    /// <summary> 父控件是可缩放的控件，这个编辑控件的操作点大小会跟随父控件的缩放而变化
    /// </summary>
    public class DUIEditableScaleParentControl : DUIEditableControl
    {
        private DUIScaleableControl scaleableControl = null;
        /// <summary> 父控件
        /// </summary>
        public DUIScaleableControl ScaleableControl
        {
            get
            {
                if (scaleableControl == null)
                {
                    if (this.DUIParent != null && this.DUIParent is DUIScaleableControl dUIScaleableControlParent)
                    {
                        return dUIScaleableControlParent;
                    }
                }
                return scaleableControl;
            }
            set { scaleableControl = value; }
        }
        protected float ScaleableControlScaling
        {
            get
            {
                //return ScaleableControl == null ? 1 : Math.Min(ScaleableControl.Scaling, 1);
                return ScaleableControl == null ? 1 : ScaleableControl.Scaling;
            }
        }
        public override float BorderWidth
        {
            get
            {
                return base.BorderWidth / ScaleableControlScaling;
            }
            set
            {
                base.BorderWidth = value * ScaleableControlScaling;
            }
        }
        public override float X
        {
            get
            {
                return base.X + (ScaleableControlScaling - 1) * this.BorderWidth;
            }
            set
            {
                base.X = value - ((ScaleableControlScaling - 1) * this.BorderWidth);
            }
        }
        public override float Y
        {
            get
            {
                return base.Y + (ScaleableControlScaling - 1) * this.BorderWidth;
            }
            set
            {
                base.Y = value - ((ScaleableControlScaling - 1) * this.BorderWidth);
            }
        }
        public override float Width
        {
            get
            {
                return base.Width + this.BorderWidth * 2;
            }
            set
            {
                base.Width = value - this.BorderWidth * 2;
            }
        }
        public override float Height
        {
            get
            {
                return base.Height + this.BorderWidth * 2;
            }
            set
            {
                base.Height = value - this.BorderWidth * 2;
            }
        }
        public override float SkewRadius
        {
            get
            {
                return base.SkewRadius / ScaleableControlScaling;
            }
        }
        public override float VertexRadius
        {
            get
            {
                return base.VertexRadius / ScaleableControlScaling;
            }
        }
        public override float RotateRadius
        {
            get
            {
                return base.RotateRadius / ScaleableControlScaling;
            }
        }
        public override float CenterRadius
        {
            get
            {
                return base.CenterRadius / ScaleableControlScaling;
            }
        }
        protected override void DrawBounds(DUIPaintEventArgs e)
        {
            //base.DrawBounds(e);
            float boundsWidth = 1 / ScaleableControlScaling;
            using (DUIPen pen = new DUIPen(this.Focused ? Color.Blue : Color.Black, boundsWidth))
            {
                e.Graphics.DrawRectangle(pen, new RectangleF(this.BorderWidth - boundsWidth / 2, this.BorderWidth - boundsWidth / 2, this.ClientRectangle.Width + boundsWidth, this.ClientRectangle.Height + boundsWidth));
            }
        }
        protected override DUIAnyBounds GetAnyBounds()
        {
            return new DUIAnyBoundsScaleParent()
            {
                x = this.x,
                y = this.y,
                width = this.width,
                height = this.height,
                centerX = this.centerX,
                centerY = this.centerY,
                rotate = this.rotate,
                skewX = this.skewX,
                skewY = this.skewY,
                scaleX = this.scaleX,
                scaleY = this.scaleY,
                borderWidth = this.borderWidth,
                ScaleableControl = this.ScaleableControl
            };
        }
        public class DUIAnyBoundsScaleParent : DUIAnyBounds
        {
            /// <summary> 父控件
            /// </summary>
            public DUIScaleableControl ScaleableControl { get; set; }
            protected float ScaleableControlScaling
            {
                //Math.Min(ScaleableControl.Scaling, 1)
                get { return ScaleableControl == null ? 1 : ScaleableControl.Scaling; }
            }
            public override float BorderWidth
            {
                get
                {
                    return base.BorderWidth / ScaleableControlScaling;
                }
                set
                {
                    base.BorderWidth = value * ScaleableControlScaling;
                }
            }
            public override float X
            {
                get
                {
                    return base.X + base.BorderWidth - this.BorderWidth;
                }
                set
                {
                    base.X = value - (base.BorderWidth - this.BorderWidth);
                }
            }
            public override float Y
            {
                get
                {
                    return base.Y + base.BorderWidth - this.BorderWidth;
                }
                set
                {
                    base.Y = value - (base.BorderWidth - this.BorderWidth);
                }
            }
            public override float Width
            {
                get
                {
                    return base.Width + this.BorderWidth * 2;
                }
                set
                {
                    base.Width = value - this.BorderWidth * 2;
                }
            }
            public override float Height
            {
                get
                {
                    return base.Height + this.BorderWidth * 2;
                }
                set
                {
                    base.Height = value - this.BorderWidth * 2;
                }
            }
        }
    }
}
