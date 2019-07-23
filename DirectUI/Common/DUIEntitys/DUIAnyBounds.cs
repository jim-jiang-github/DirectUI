/********************************************************************
 * * 使本项目源码前请仔细阅读以下协议内容，如果你同意以下协议才能使用本项目所有的功能,
 * * 否则如果你违反了以下协议，有可能陷入法律纠纷和赔偿，作者保留追究法律责任的权利。
 * * Copyright (C) 
 * * 作者： 煎饼的归宿    QQ：375324644   
 * * 请保留以上版权信息，否则作者将保留追究法律责任。
 * * 最后修改：BF-20150503UIWA
 * * 创建时间：2017/11/21 10:43:18
********************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIAnyBounds
    {
        internal float x;
        internal float y;
        internal float width;
        internal float height;
        internal float centerX;
        internal float centerY;
        internal float rotate;
        internal float skewX;
        internal float skewY;
        internal float scaleX;
        internal float scaleY;
        internal float borderWidth;
        public virtual float X
        {
            get { return x; }
            set { x = value; }
        }
        public virtual float Y
        {
            get { return y; }
            set { y = value; }
        }
        public PointF Locattion { get { return new PointF(X, Y); } }
        public virtual float Width
        {
            get { return width; }
            set { width = value; }
        }
        public virtual float Height
        {
            get { return height; }
            set { height = value; }
        }
        public SizeF Size { get { return new SizeF(Width, Height); } }
        public RectangleF Bounds { get { return new RectangleF(Locattion, Size); } }
        public RectangleF ClientBounds
        {
            get { return new RectangleF(this.X + this.BorderWidth, this.Y + this.BorderWidth, this.ClientSize.Width, this.ClientSize.Height); }
        }
        public SizeF ClientSize { get { return new SizeF(this.Width - this.BorderWidth * 2, this.Height - this.BorderWidth * 2); } }
        public virtual float CenterX
        {
            get { return centerX; }
            set { centerX = value; }
        }
        public virtual float CenterY
        {
            get { return centerY; }
            set { centerY = value; }
        }
        public PointF Center { get { return new PointF(CenterX, CenterY); } }
        public virtual float Rotate
        {
            get { return rotate; }
            set { rotate = value; }
        }
        public virtual float SkewX
        {
            get { return skewX; }
            set { skewX = value; }
        }
        public virtual float SkewY
        {
            get { return skewY; }
            set { skewY = value; }
        }
        public PointF Skew { get { return new PointF(SkewX, SkewY); } }
        public virtual float ScaleX
        {
            get { return scaleX; }
            set { scaleX = value; }
        }
        public virtual float ScaleY
        {
            get { return scaleY; }
            set { scaleY = value; }
        }
        public PointF Scale { get { return new PointF(ScaleX, ScaleY); } }
        public virtual float BorderWidth
        {
            get { return borderWidth; }
            set { borderWidth = value; }
        }
    }
}
