/********************************************************************
 * * 使本项目源码前请仔细阅读以下协议内容，如果你同意以下协议才能使用本项目所有的功能,
 * * 否则如果你违反了以下协议，有可能陷入法律纠纷和赔偿，作者保留追究法律责任的权利。
 * * Copyright (C) 
 * * 作者： 煎饼的归宿    QQ：375324644   
 * * 请保留以上版权信息，否则作者将保留追究法律责任。
 * * 最后修改：BF-20150503UIWA
 * * 创建时间：2018/1/26 15:36:34
********************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public interface IDUIGraphics : IDisposable
    {
        #region 属性
        DUISmoothingMode SmoothingMode { get; set; }
        DUITextRenderingHint TextRenderingHint { get; set; }
        DUIMatrix Transform { get; set; }
        float DpiX { get; }
        float DpiY { get; }
        RectangleF ClipBounds { get; }
        #endregion
        #region 函数
        DirectUI.Common.DUIGraphicsState Save();
        void Restore(DirectUI.Common.DUIGraphicsState graphicsState);
        void BeginDraw(Region r);
        void Clear(Color color);
        void EndDraw();
        void ResetTransform();
        void Resize(SizeF size);
        #endregion
        #region RoundedRectangle
        void DrawRoundedRectangle(DUIPen pen, float x, float y, float width, float height, float radius);
        void FillRoundedRectangle(DUIBrush brush, float x, float y, float width, float height, float radius);
        #endregion
        #region Rectangle
        void DrawRectangle(DUIPen pen, float x, float y, float width, float height);
        void FillRectangle(DUIBrush brush, float x, float y, float width, float height);
        #endregion
        #region Ellipse
        void DrawEllipse(DUIPen pen, float x, float y, float width, float height);
        void FillEllipse(DUIBrush brush, float x, float y, float width, float height);
        #endregion
        #region Polygon
        void DrawPolygon(DUIPen pen, PointF[] points);
        void FillPolygon(DUIBrush brush, PointF[] points);
        #endregion
        #region Region
        void FillRegion(DUIBrush brush, DUIRegion region);
        #endregion
        #region Line
        void DrawLine(DUIPen pen, float x1, float y1, float x2, float y2);
        #endregion
        #region Bezier
        void DrawBezier(DUIPen pen, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4);
        #endregion
        #region String
        SizeF MeasureString(string text, DUIFont font, float width, float height);
        void DrawString(string s, DUIFont font, DUIBrush brush, RectangleF layoutRectangle, StringFormat format);
        #endregion
        #region Image
        void DrawImage(DUIImage image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit, float opacity);
        void DrawImage(DUIImage image, PointF[] destTriangle, PointF[] srcTriangle, GraphicsUnit srcUnit, float opacity);
        void DrawImage(DUIImage image, PointF[] polygon, GraphicsUnit srcUnit, float opacity);
        #endregion
        #region Transform
        void TranslateTransform(float dx, float dy);
        void ScaleTransform(float sx, float sy);
        void RotateTransform(float angle);
        void SkewTransform(float sx, float sy);
        #endregion
        #region Layer
        void PushLayer(float x, float y, float width, float height);
        void PopLayer();
        #endregion
    }
}
