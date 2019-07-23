/********************************************************************
 * * 使本项目源码前请仔细阅读以下协议内容，如果你同意以下协议才能使用本项目所有的功能,
 * * 否则如果你违反了以下协议，有可能陷入法律纠纷和赔偿，作者保留追究法律责任的权利。
 * * Copyright (C) 
 * * 作者： 煎饼的归宿    QQ：375324644   
 * * 请保留以上版权信息，否则作者将保留追究法律责任。
 * * 最后修改：BF-20150503UIWA
 * * 创建时间：2017/11/21 10:43:18
********************************************************************/
using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIAnyBoundsPolygon : DUIAnyBounds
    {
        /// <summary> 父控件
        /// </summary>
        public DUIScaleableControl ScaleableControl { get; set; }
        protected float ScaleableControlScaling
        {
            get { return ScaleableControl == null ? 1 : Math.Min(ScaleableControl.Scaling, 1); }
        }
        protected float PolygonPointsRadius
        {
            get { return 4F / ScaleableControlScaling; }
        }
        public override float BorderWidth
        {
            get
            {
                return this.PolygonPointsRadius;
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
        public PointF[] Polygon { get; set; }
    }
}
