/********************************************************************
 * * 使本项目源码前请仔细阅读以下协议内容，如果你同意以下协议才能使用本项目所有的功能,
 * * 否则如果你违反了以下协议，有可能陷入法律纠纷和赔偿，作者保留追究法律责任的权利。
 * * Copyright (C) 
 * * 作者： 煎饼的归宿    QQ：375324644   
 * * 请保留以上版权信息，否则作者将保留追究法律责任。
 * * 最后修改：BF-20150503UIWA
 * * 创建时间：2018/1/5 15:25:51
********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectUI.Core
{
    /// <summary> 父控件是可缩放的控件，这个编辑控件的操作点大小会跟随父控件的缩放而变化
    /// </summary>
    public class DUIEditableCenterScaleParentControl : DUIEditableCenterControl
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
            get { return ScaleableControl == null ? 1 : ScaleableControl.Scaling; }
        }
        public override float ThisRadius
        {
            get
            {
                return base.ThisRadius / ScaleableControlScaling;
            }
            set
            {
                base.ThisRadius = value * ScaleableControlScaling;
            }
        }
        public override float X
        {
            get
            {
                return base.X + (ScaleableControlScaling - 1) * this.ThisRadius;
            }
            set
            {
                base.X = value - ((ScaleableControlScaling - 1) * this.ThisRadius);
            }
        }
        public override float Y
        {
            get
            {
                return base.Y + (ScaleableControlScaling - 1) * this.ThisRadius;
            }
            set
            {
                base.Y = value - ((ScaleableControlScaling - 1) * this.ThisRadius);
            }
        }
    }
}
