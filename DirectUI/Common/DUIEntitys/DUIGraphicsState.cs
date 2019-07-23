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
*         命名空间名称:        DirectUI.Common.DUIEntitys
*         文件名:              DUIGraphicsState
*         当前系统时间:        2019/3/5 星期二 下午 5:35:48
*         当前登录用户名:      Administrator
*         创建年份:            2019
*         版权所有：           煎饼的归宿QQ：375324644
******************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIGraphicsState
    {
        private GraphicsState graphicsState = null;
        private DUIMatrix dUIMatrix = null;
        private DUISmoothingMode dUISmoothingMode = DUISmoothingMode.Default;
        private DUITextRenderingHint dUITextRenderingHint = DUITextRenderingHint.Default;
        public DUIGraphicsState(DUIMatrix dUIMatrix, DUISmoothingMode dUISmoothingMode, DUITextRenderingHint dUITextRenderingHint)
        {
            this.dUIMatrix = dUIMatrix;
            this.dUISmoothingMode = dUISmoothingMode;
            this.dUITextRenderingHint = dUITextRenderingHint;
        }
        public DUIGraphicsState(GraphicsState graphicsState)
        {
            this.graphicsState = graphicsState;
        }
        public static implicit operator DUIGraphicsState(GraphicsState graphicsState)
        {
            return new DUIGraphicsState(graphicsState);
        }
        public static implicit operator GraphicsState(DUIGraphicsState dUIGraphicsState)
        {
            return dUIGraphicsState.graphicsState;
        }
        public static implicit operator DUISmoothingMode(DUIGraphicsState dUIGraphicsState)
        {
            return dUIGraphicsState.dUISmoothingMode;
        }
        public static implicit operator DUITextRenderingHint(DUIGraphicsState dUIGraphicsState)
        {
            return dUIGraphicsState.dUITextRenderingHint;
        }
        public static implicit operator DUIMatrix(DUIGraphicsState dUIGraphicsState)
        {
            return dUIGraphicsState.dUIMatrix;
        }
    }
}
