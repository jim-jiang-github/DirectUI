/********************************************************************
 * * 使本项目源码前请仔细阅读以下协议内容，如果你同意以下协议才能使用本项目所有的功能,
 * * 否则如果你违反了以下协议，有可能陷入法律纠纷和赔偿，作者保留追究法律责任的权利。
 * * Copyright (C) 
 * * 作者： 煎饼的归宿    QQ：375324644   
 * * 请保留以上版权信息，否则作者将保留追究法律责任。
 * * 最后修改：BF-20150503UIWA
 * * 创建时间：2018/2/24 11:53:04
********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public enum DUIMode
    {
        /// <summary> 以GDI+模式绘制
        /// </summary>
        GDIP,
        /// <summary> 以Direct2D模式绘制
        /// </summary>
        Direct2D,
        /// <summary> 默认绘制方式（Direct2D）
        /// </summary>
        Default
    }
}
