/******************************************************************
* 使本项目源码或本项目生成的DLL前请仔细阅读以下协议内容，如果你同意以下协议才能使用本项目所有的功能，
* * 否则如果你违反了以下协议，有可能陷入法律纠纷和赔偿，作者保留追究法律责任的权利。
* * 1、你可以在开发的软件产品中使用和修改本项目的源码和DLL，但是请保留所有相关的版权信息。
* * 2、不能将本项目源码与作者的其他项目整合作为一个单独的软件售卖给他人使用。
* * 3、不能传播本项目的源码和DLL，包括上传到网上、拷贝给他人等方式。
* * 4、以上协议暂时定制，由于还不完善，作者保留以后修改协议的权利。
* 
*         Copyright (C):       煎饼的归宿
*         CLR版本:             4.0.30319.42000
*         注册组织名:          Microsoft
*         命名空间名称:        DirectUI.Common.DUIEnum
*         文件名:              DUIBoundsPolygonSpecified
*         当前系统时间:        2018/5/2 星期三 下午 2:42:22
*         当前登录用户名:      Administrator
*         创建年份:            2018
*         版权所有：           煎饼的归宿QQ：375324644
******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public enum DUIBoundsPolygonSpecified
    {
        // 摘要: 
        //     未定义任何边界。
        None = 0,
        //
        // 摘要: 
        //     该控件的左边缘已定义。
        CenterX = 1,
        //
        // 摘要: 
        //     该控件的上边缘已定义。
        CenterY = 2,
        //
        // 摘要: 
        //     该控件的 X 和 Y 坐标都已定义。
        Center = 3,
        //
        // 摘要: 
        //     该控件的宽度已定义。
        RotateAngle = 4,
        //
        // 摘要: 
        //     该控件的高度已定义。
        SkewX = 8,
        //
        // 摘要: 
        //     该控件的 System.Windows.Forms.Control.Width 和 System.Windows.Forms.Control.Height
        //     属性值都已定义。
        SkewY = 16,
        Skew = 24,
        ScaleX = 32,
        ScaleY = 64,
        Scale = 96,
        Polygon = 128,
        //
        // 摘要: 
        //     System.Windows.Forms.Control.Location 和 System.Windows.Forms.Control.Size
        //     属性值都已定义。
        All = 255,
    }
}
