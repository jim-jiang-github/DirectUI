using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using DirectUI.Proxy;
using DirectUI.Arithmetic;
using System.Threading;

namespace DirectUI.Common
{
    public class DUIGraphics : IDisposable
    {
        IDUIGraphics iDUIGraphics = null;
        private DUIGraphics(IntPtr handle)
        {
            //this.iDUIGraphics = new DUIGraphics_GDIP(handle);
            this.iDUIGraphics = new DUIGraphics_D2D(handle);
            //switch (DUIInfo.Mode)
            //{
            //    case DUIMode.Direct2D:
            //    default:
            //        this.iDUIGraphics = new DUIGraphics_D2D(handle);
            //        break;
            //    case DUIMode.GDIP:
            //        this.iDUIGraphics = new DUIGraphics_GDIP(handle);
            //        break;
            //}
        }
        private DUIGraphics(DUIBitmap dUIBitmap)
        {
            this.iDUIGraphics = new DUIGraphics_D2D(dUIBitmap);
        }
        private DUIGraphics(Graphics graphics)
        {
            //this.iDUIGraphics = new DUIGraphics_GDIP(handle);
            this.iDUIGraphics = new DUIGraphics_GDIP(graphics);
            //switch (DUIInfo.Mode)
            //{
            //    case DUIMode.Direct2D:
            //    default:
            //        this.iDUIGraphics = new DUIGraphics_D2D(handle);
            //        break;
            //    case DUIMode.GDIP:
            //        this.iDUIGraphics = new DUIGraphics_GDIP(handle);
            //        break;
            //}
        }
        public static DUIGraphics FromControl(Control control)
        {
            return new DUIGraphics(control.Handle);
        }
        public static DUIGraphics FromImage(DUIBitmap image)
        {
            return new DUIGraphics(image);
        }
        #region 属性
        /// <summary> 锁定Transform使其失效
        /// </summary>
        public bool LockTransform { get; set; }
        public DUISmoothingMode SmoothingMode
        {
            get
            {
                return this.iDUIGraphics.SmoothingMode;
            }
            set
            {
                this.iDUIGraphics.SmoothingMode = value;
            }
        }

        public DUITextRenderingHint TextRenderingHint
        {
            get
            {
                return this.iDUIGraphics.TextRenderingHint;
            }
            set
            {
                this.iDUIGraphics.TextRenderingHint = value;
            }
        }

        public DUIMatrix Transform
        {
            get
            {
                return this.iDUIGraphics.Transform;
            }
            set
            {
                if (!this.LockTransform)
                {
                    this.iDUIGraphics.Transform = value;
                }
            }
        }
        public float DpiX
        {
            get
            {
                return this.iDUIGraphics.DpiX;
            }
        }
        public float DpiY
        {
            get
            {
                return this.iDUIGraphics.DpiX;
            }
        }
        public RectangleF ClipBounds
        {
            get { return this.iDUIGraphics.ClipBounds; }
        }
        #endregion
        #region 函数
        public DirectUI.Common.DUIGraphicsState Save()
        {
            return this.iDUIGraphics.Save();
        }
        public void Restore(DirectUI.Common.DUIGraphicsState graphicsState)
        {
            this.iDUIGraphics.Restore(graphicsState);
        }
        public void BeginDraw(Region r = null)
        {
            this.iDUIGraphics.BeginDraw(r);
        }
        public void Clear(Color color)
        {
            this.iDUIGraphics.Clear(color);
        }
        public void EndDraw()
        {
            this.iDUIGraphics.EndDraw();
        }
        public void ResetTransform()
        {
            this.iDUIGraphics.ResetTransform();
        }
        public void Resize(float width, float height)
        {
            this.Resize(new SizeF(width, height));
        }
        public void Resize(SizeF size)
        {
            this.iDUIGraphics.Resize(size);
        }
        #endregion
        #region RoundedRectangle
        public void DrawRoundedRectangle(DUIPen pen, int x, int y, int width, int height, float radius)
        {
            this.DrawRoundedRectangle(pen, (float)x, (float)y, (float)width, (float)height, radius);
        }
        public void DrawRoundedRectangle(DUIPen pen, Rectangle rect, float radius)
        {
            this.DrawRoundedRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height, radius);
        }
        public void DrawRoundedRectangle(DUIPen pen, float x, float y, float width, float height, float radius)
        {
            if (pen.Width == 0 || width == 0 || height == 0) { return; }
            this.iDUIGraphics.DrawRoundedRectangle(pen, x, y, width, height, radius);
        }
        public void DrawRoundedRectangle(DUIPen pen, RectangleF rect, float radius)
        {
            this.DrawRoundedRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height, radius);
        }
        public void FillRoundedRectangle(DUIBrush brush, int x, int y, int width, int height, float radius)
        {
            this.FillRoundedRectangle(brush, (float)x, (float)y, (float)width, (float)height, radius);
        }
        public void FillRoundedRectangle(DUIBrush brush, Rectangle rect, float radius)
        {
            this.FillRoundedRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height, radius);
        }
        public void FillRoundedRectangle(DUIBrush brush, float x, float y, float width, float height, float radius)
        {
            if (width == 0 || height == 0) { return; }
            this.iDUIGraphics.FillRoundedRectangle(brush, x, y, width, height, radius);
        }
        public void FillRoundedRectangle(DUIBrush brush, RectangleF rect, float radius)
        {
            this.FillRoundedRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height, radius);
        }
        #endregion
        #region Rectangle
        public void DrawRectangle(DUIPen pen, int x, int y, int width, int height)
        {
            this.DrawRectangle(pen, (float)x, (float)y, (float)width, (float)height);
        }
        public void DrawRectangle(DUIPen pen, Rectangle rect)
        {
            this.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }
        public void DrawRectangles(DUIPen pen, Rectangle[] rects)
        {
            foreach (Rectangle rect in rects)
            {
                DrawRectangle(pen, rect);
            }
        }
        public void DrawRectangle(DUIPen pen, float x, float y, float width, float height)
        {
            if (pen.Width == 0 || width == 0 || height == 0) { return; }
            this.iDUIGraphics.DrawRectangle(pen, x, y, width, height);
        }
        public void DrawRectangle(DUIPen pen, RectangleF rect)
        {
            this.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }
        public void DrawRectangles(DUIPen pen, RectangleF[] rects)
        {
            foreach (RectangleF rect in rects)
            {
                DrawRectangle(pen, rect);
            }
        }
        public void FillRectangle(DUIBrush brush, int x, int y, int width, int height)
        {
            this.FillRectangle(brush, (float)x, (float)y, (float)width, (float)height);
        }
        public void FillRectangle(DUIBrush brush, Rectangle rect)
        {
            this.FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
        }
        public void FillRectangles(DUIBrush brush, Rectangle[] rects)
        {
            foreach (Rectangle rect in rects)
            {
                FillRectangle(brush, rect);
            }
        }
        public void FillRectangle(DUIBrush brush, float x, float y, float width, float height)
        {
            if (width == 0 || height == 0) { return; }
            this.iDUIGraphics.FillRectangle(brush, x, y, width, height);
        }
        public void FillRectangle(DUIBrush brush, RectangleF rect)
        {
            this.FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
        }
        public void FillRectangles(DUIBrush brush, RectangleF[] rects)
        {
            foreach (RectangleF rect in rects)
            {
                FillRectangle(brush, rect);
            }
        }
        #endregion
        #region Ellipse
        public void DrawEllipse(DUIPen pen, int x, int y, int width, int height)
        {
            this.DrawEllipse(pen, (float)x, (float)y, (float)width, (float)height);
        }
        public void DrawEllipse(DUIPen pen, Rectangle rect)
        {
            this.DrawEllipse(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }
        public void DrawEllipse(DUIPen pen, float x, float y, float width, float height)
        {
            if (pen.Width == 0 || width == 0 || height == 0) { return; }
            this.iDUIGraphics.DrawEllipse(pen, x, y, width, height);
        }
        public void DrawEllipses(DUIPen pen, RectangleF[] rects)
        {
            foreach (RectangleF rect in rects)
            {
                DrawEllipse(pen, rect);
            }
        }
        public void DrawEllipse(DUIPen pen, RectangleF rect)
        {
            this.DrawEllipse(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }
        public void FillEllipse(DUIBrush brush, int x, int y, int width, int height)
        {
            this.FillEllipse(brush, (float)x, (float)y, (float)width, (float)height);
        }
        public void FillEllipses(DUIBrush brush, RectangleF[] rects)
        {
            foreach (RectangleF rect in rects)
            {
                FillEllipse(brush, rect);
            }
        }
        public void FillEllipse(DUIBrush brush, Rectangle rect)
        {
            this.FillEllipse(brush, rect.X, rect.Y, rect.Width, rect.Height);
        }
        public void FillEllipse(DUIBrush brush, float x, float y, float width, float height)
        {
            if (width == 0 || height == 0) { return; }
            this.iDUIGraphics.FillEllipse(brush, x, y, width, height);
        }
        public void FillEllipse(DUIBrush brush, RectangleF rect)
        {
            this.FillEllipse(brush, rect.X, rect.Y, rect.Width, rect.Height);
        }
        #endregion
        #region Polygon
        public void DrawPolygon(DUIPen pen, Point[] points)
        {
            this.DrawPolygon(pen, points.Select(p => (PointF)p).ToArray());
        }
        public void DrawPolygon(DUIPen pen, PointF[] points)
        {
            if (pen.Width == 0) { return; }
            if (points == null) { return; }
            if (points.Length < 3) { return; }
            this.iDUIGraphics.DrawPolygon(pen, points);
        }
        public void FillPolygon(DUIBrush brush, Point[] points)
        {
            this.FillPolygon(brush, points.Select(p => (PointF)p).ToArray());
        }
        public void FillPolygon(DUIBrush brush, PointF[] points)
        {
            if (points == null) { return; }
            if (points.Length < 3) { return; }
            this.iDUIGraphics.FillPolygon(brush, points);
        }
        #endregion
        #region Region
        public void FillRegion(DUIBrush brush, DUIRegion region)
        {
            this.iDUIGraphics.FillRegion(brush, region);
        }
        #endregion
        #region Line
        public void DrawLines(DUIPen pen, PointF[] points)
        {
            if (points.Length < 2)
            {
                return;
            }
            for (int i = 1; i < points.Length; i++)
            {
                PointF pt1 = points[i - 1];
                PointF pt2 = points[i];
                this.DrawLine(pen, pt1, pt2);
            }
        }
        public void DrawLines(DUIPen pen, Point[] points)
        {
            this.DrawLines(pen, points.Select(p => new PointF(p.X, p.Y)).ToArray());
        }
        public void DrawLine(DUIPen pen, int x1, int y1, int x2, int y2)
        {
            this.DrawLine(pen, (float)x1, (float)y1, (float)x2, (float)y2);
        }
        public void DrawLine(DUIPen pen, Point pt1, Point pt2)
        {
            this.DrawLine(pen, pt1.X, pt1.Y, pt2.X, pt2.Y);
        }
        public void DrawLine(DUIPen pen, float x1, float y1, float x2, float y2)
        {
            if (pen.Width == 0) { return; }
            if (x1 == x2 && y1 == y2) { return; }
            this.iDUIGraphics.DrawLine(pen, x1, y1, x2, y2);
        }
        public void DrawLine(DUIPen pen, PointF pt1, PointF pt2)
        {
            this.DrawLine(pen, pt1.X, pt1.Y, pt2.X, pt2.Y);
        }
        #endregion
        #region Bezier
        public void DrawBezier(DUIPen pen, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
        {
            this.DrawBezier(pen, (float)x1, (float)y1, (float)x2, (float)y2, (float)x3, (float)y3, (float)x4, (float)y4);
        }
        public void DrawBezier(DUIPen pen, Point p1, Point p2, Point p3, Point p4)
        {
            this.DrawBezier(pen, p1.X, p1.Y, p2.X, p2.Y, p3.X, p3.Y, p4.X, p4.Y);
        }
        public void DrawBezier(DUIPen pen, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
        {
            if (pen.Width == 0) { return; }
            this.iDUIGraphics.DrawBezier(pen, x1,  y1,  x2,  y2,  x3,  y3,  x4,  y4);
        }
        public void DrawBezier(DUIPen pen, PointF p1, PointF p2, PointF p3, PointF p4)
        {
            this.DrawBezier(pen, p1.X, p1.Y, p2.X, p2.Y, p3.X, p3.Y, p4.X, p4.Y);
        }
        #endregion
        #region String
        public SizeF MeasureString(string text, DUIFont font)
        {
            return this.MeasureString(text, font, float.MaxValue, 0);
        }
        public SizeF MeasureString(string text, DUIFont font, SizeF size)
        {
            return this.MeasureString(text, font, size.Width, size.Height);
        }
        public SizeF MeasureString(string text, DUIFont font, float width, float height)
        {
            if (string.IsNullOrWhiteSpace(text)) { return SizeF.Empty; }
            return this.iDUIGraphics.MeasureString(text, font, width, height);
        }
        public void DrawString(string s, DUIFont font, DUIBrush brush, PointF p)
        {
            DrawString(s, font, brush, p.X, p.Y);
        }
        public void DrawString(string s, DUIFont font, DUIBrush brush, float x, float y)
        {
            DrawString(s, font, brush, new RectangleF(new PointF(x, y), MeasureString(s, font)), StringFormat.GenericDefault);
        }
        public void DrawString(string s, DUIFont font, DUIBrush brush, RectangleF layoutRectangle, StringFormat format)
        {
            if (string.IsNullOrWhiteSpace(s)) { return; }
            this.iDUIGraphics.DrawString(s, font, brush, layoutRectangle, format);
        }
        #endregion
        #region Image
        public void DrawImage(DUIImage image, int x, int y)
        {
            this.DrawImage(image, (float)x, (float)y);
        }
        public void DrawImage(DUIImage image, int x, int y, float opacity)
        {
            this.DrawImage(image, (float)x, (float)y, opacity);
        }
        public void DrawImage(DUIImage image, float x, float y)
        {
            this.DrawImage(image, x, y, 1);
        }
        public void DrawImage(DUIImage image, float x, float y, float opacity)
        {
            this.DrawImage(image, new RectangleF(x, y, image.Width, image.Height), new RectangleF(0, 0, image.Width, image.Height), GraphicsUnit.Pixel, opacity);
        }
        public void DrawImage(DUIImage image, PointF point)
        {
            this.DrawImage(image, point.X, point.Y);
        }
        public void DrawImage(DUIImage image, PointF point, float opacity)
        {
            this.DrawImage(image, point.X, point.Y, opacity);
        }
        public void DrawImage(DUIImage image, int x, int y, int width, int height)
        {
            this.DrawImage(image, (float)x, (float)y, (float)width, (float)height);
        }
        public void DrawImage(DUIImage image, int x, int y, int width, int height, float opacity)
        {
            this.DrawImage(image, (float)x, (float)y, (float)width, (float)height, opacity);
        }
        public void DrawImage(DUIImage image, float x, float y, float width, float height)
        {
            this.DrawImage(image, new RectangleF(x, y, width, height), new RectangleF(0, 0, image.Width, image.Height), GraphicsUnit.Pixel, 1);
        }
        public void DrawImage(DUIImage image, float x, float y, float width, float height, float opacity)
        {
            this.DrawImage(image, new RectangleF(x, y, width, height), new RectangleF(0, 0, image.Width, image.Height), GraphicsUnit.Pixel, opacity);
        }
        public void DrawImage(DUIImage image, Rectangle rect)
        {
            this.DrawImage(image, rect.X, rect.Y, rect.Width, rect.Height);
        }
        public void DrawImage(DUIImage image, Rectangle rect, float opacity)
        {
            this.DrawImage(image, rect.X, rect.Y, rect.Width, rect.Height, opacity);
        }
        public void DrawImage(DUIImage image, RectangleF rect)
        {
            this.DrawImage(image, rect.X, rect.Y, rect.Width, rect.Height);
        }
        public void DrawImage(DUIImage image, RectangleF rect, float opacity)
        {
            this.DrawImage(image, rect.X, rect.Y, rect.Width, rect.Height, opacity);
        }
        public void DrawImage(DUIImage image, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit)
        {
            this.DrawImage(image, (RectangleF)destRect, (RectangleF)srcRect, srcUnit);
        }
        public void DrawImage(DUIImage image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit)
        {
            this.DrawImage(image, destRect, srcRect, srcUnit, 1);
        }
        public void DrawImage(DUIImage image, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit, float opacity)
        {
            this.DrawImage(image, (RectangleF)destRect, (RectangleF)srcRect, srcUnit, opacity);
        }
        public void DrawImage(DUIImage image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit, float opacity)
        {
            if (image == null || destRect.Width == 0 || destRect.Height == 0 || srcRect.Width == 0 || srcRect.Height == 0) { return; }
            this.iDUIGraphics.DrawImage(image, destRect, srcRect, srcUnit, opacity);
        }
        public void DrawImage(DUIImage image, Point[] destTriangle, Point[] srcTriangle)
        {
            this.DrawImage(image, destTriangle, srcTriangle, GraphicsUnit.Pixel, 1);
        }
        public void DrawImage(DUIImage image, Point[] destTriangle, Point[] srcTriangle, GraphicsUnit srcUnit, float opacity)
        {
            this.DrawImage(image, destTriangle.Select(p => (PointF)p).ToArray(), srcTriangle.Select(p => (PointF)p).ToArray(), srcUnit, opacity);
        }
        public void DrawImage(DUIImage image, PointF[] destTriangle, PointF[] srcTriangle)
        {
            this.DrawImage(image, destTriangle, srcTriangle, GraphicsUnit.Pixel, 1);
        }
        public void DrawImage(DUIImage image, PointF[] destTriangle, PointF[] srcTriangle, GraphicsUnit srcUnit, float opacity)
        {
            if (image == null || destTriangle.Length != 3 || srcTriangle.Length != 3) { return; }
            this.iDUIGraphics.DrawImage(image, destTriangle, srcTriangle, srcUnit, opacity);
        }
        public void DrawImage(DUIImage image, PointF[] polygon)
        {
            this.DrawImage(image, polygon, GraphicsUnit.Pixel, 1);
        }
        public void DrawImage(DUIImage image, Point[] polygon)
        {
            this.DrawImage(image, polygon, GraphicsUnit.Pixel, 1);
        }
        public void DrawImage(DUIImage image, PointF[] polygon, float opacity)
        {
            this.DrawImage(image, polygon, GraphicsUnit.Pixel, opacity);
        }
        public void DrawImage(DUIImage image, Point[] polygon, float opacity)
        {
            this.DrawImage(image, polygon, GraphicsUnit.Pixel, opacity);
        }
        public void DrawImage(DUIImage image, Point[] polygon, GraphicsUnit srcUnit, float opacity)
        {
            this.DrawImage(image, polygon.Select(p => (PointF)p).ToArray(), srcUnit, opacity);
        }
        public void DrawImage(DUIImage image, PointF[] polygon, GraphicsUnit srcUnit, float opacity)
        {
            if (image == null || polygon.Length < 3) { return; }
            this.iDUIGraphics.DrawImage(image, polygon, srcUnit, opacity);
        }
        //  public void DrawBitmapMesh(DUIImage image, int meshWidth, int meshHeight, PointF[] points)
        //{
        //    if (image == null) { return; }
        //    image.RenderTarget = this.target.RenderTarget;
        //    float w = (float)image.Width / meshWidth;
        //    float h = (float)image.Height / meshHeight;
        //    PointF[,] pts = new PointF[meshWidth + 1, meshHeight + 1];
        //    for (int y = 0; y <= meshHeight; y++)
        //    {
        //        for (int x = 0; x <= meshWidth; x++)
        //        {
        //            pts[x, y] = points[(meshWidth + 1) * y + x];
        //        }
        //    }
        //    for (int y = 0; y < meshHeight; y++)
        //    {
        //        for (int x = 0; x < meshWidth; x++)
        //        {
        //            float offset = 0.5F;
        //            PointF ptLT = pts[x, y];
        //            PointF ptRT = pts[x + 1, y];
        //            PointF ptLB = pts[x, y + 1];
        //            PointF ptRB = pts[x + 1, y + 1];
        //            ptRT = new PointF(ptRT.X + offset, ptRT.Y);
        //            ptLB = new PointF(ptLB.X, ptLB.Y + offset);
        //            ptRB = new PointF(ptRB.X + offset, ptRB.Y + offset);
        //            DUIMatrix4x4 matrix4x4_1 = new DUIMatrix4x4(
        //                1F / w, 0, 0, 0,
        //                0, 1F / h, 0, 0,
        //                0, 0, 1, 0,
        //                0, 0, 0, 1);
        //            DUIMatrix4x4 matrix4x4_2 = new DUIMatrix4x4(
        //                ptRT.X - ptLT.X, ptRT.Y - ptLT.Y, 0, 0,
        //                ptLB.X - ptLT.X, ptLB.Y - ptLT.Y, 0, 0,
        //                0, 0, 1, 0,
        //                ptLT.X, ptLT.Y, 0, 1);
        //            float den = matrix4x4_2.M11 * matrix4x4_2.M22 - matrix4x4_2.M12 * matrix4x4_2.M21;
        //            float a = (matrix4x4_2.M22 * ptRB.X - matrix4x4_2.M21 * ptRB.Y +
        //                matrix4x4_2.M21 * matrix4x4_2.M42 - matrix4x4_2.M22 * matrix4x4_2.M41) / den;
        //            float b = (matrix4x4_2.M11 * ptRB.Y - matrix4x4_2.M12 * ptRB.X +
        //                matrix4x4_2.M12 * matrix4x4_2.M41 - matrix4x4_2.M11 * matrix4x4_2.M42) / den;
        //            DUIMatrix4x4 matrix4x4_3 = new DUIMatrix4x4(
        //                a / (a + b - 1), 0, 0, a / (a + b - 1) - 1,
        //                0, b / (a + b - 1), 0, b / (a + b - 1) - 1,
        //                0, 0, 1, 0,
        //                0, 0, 0, 1);
        //            DUIMatrix4x4 matrix4x4 = matrix4x4_1 * matrix4x4_3 * matrix4x4_2;
        //            //((SharpDX.Direct2D1.DeviceContext)this.target.RenderTarget).DrawBitmap(image, DxConvert.ToRectF(new RectangleF(0, 0, w, h)), 1, SharpDX.Direct2D1.InterpolationMode.NearestNeighbor, DxConvert.ToRectF(new RectangleF(x * w, y * h, w, h)), matrix4x4);
        //        }
        //    }
        #endregion
        #region Transform
        public void TranslateTransform(PointF offset)
        {
            this.iDUIGraphics.TranslateTransform(offset.X, offset.Y);
        }
        public void TranslateTransform(float offsetX, float offsetY)
        {
            this.iDUIGraphics.TranslateTransform(offsetX, offsetY);
        }
        public void ScaleTransform(PointF scale)
        {
            this.iDUIGraphics.ScaleTransform(scale.X, scale.Y);
        }
        public void ScaleTransform(float scaleX, float scaleY)
        {
            this.iDUIGraphics.ScaleTransform(scaleX, scaleY);
        }
        public void ScaleTransform(PointF scale, float centerX, float centerY)
        {
            this.ScaleTransform(scale.X, scale.Y, centerX, centerY);
        }
        public void ScaleTransform(float scaleX, float scaleY, PointF center)
        {
            this.ScaleTransform(scaleX, scaleY, center.X, center.Y);
        }
        public void ScaleTransform(PointF scale, PointF center)
        {
            this.ScaleTransform(scale.X, scale.Y, center.X, center.Y);
        }
        public void ScaleTransform(float scaleX, float scaleY, float centerX, float centerY)
        {
            this.TranslateTransform(centerX, centerY);
            this.ScaleTransform(scaleX, scaleY);
            this.TranslateTransform(-centerX, -centerY);
        }
        public void RotateTransform(float rotate)
        {
            this.iDUIGraphics.RotateTransform(rotate);
        }
        public void RotateTransform(float rotate, PointF center)
        {
            this.RotateTransform(rotate, center.X, center.Y);
        }
        public void RotateTransform(float rotate, float centerX, float centerY)
        {
            this.TranslateTransform(centerX, centerY);
            this.RotateTransform(rotate);
            this.TranslateTransform(-centerX, -centerY);
        }
        public void SkewTransform(PointF skew)
        {
            this.iDUIGraphics.SkewTransform(skew.X, skew.Y);
        }
        public void SkewTransform(float skewX, float skewY)
        {
            this.iDUIGraphics.SkewTransform(skewX, skewY);
        }
        public void SkewTransform(PointF skew, float centerX, float centerY)
        {
            this.SkewTransform(skew.X, skew.Y, centerX, centerY);
        }
        public void SkewTransform(float skewX, float skewY, PointF center)
        {
            this.SkewTransform(skewX, skewY, center.X, center.Y);
        }
        public void SkewTransform(PointF skew, PointF center)
        {
            this.SkewTransform(skew.X, skew.Y, center.X, center.Y);
        }
        public void SkewTransform(float skewX, float skewY, float centerX, float centerY)
        {
            this.TranslateTransform(centerX, centerY);
            this.SkewTransform(skewX, skewY);
            this.TranslateTransform(-centerX, -centerY);
        }
        #endregion
        #region Layer
        public void PushLayer(RectangleF bounds)
        {
            PushLayer(bounds.X, bounds.Y, bounds.Width, bounds.Height);
        }
        public void PushLayer(SizeF size)
        {
            PushLayer(0, 0, size.Width, size.Height);
        }
        public void PushLayer(float width, float height)
        {
            PushLayer(0, 0, width, height);
        }
        public void PushLayer(float x, float y, float width, float height)
        {
            iDUIGraphics.PushLayer(x, y, width, height);
        }
        public void PopLayer()
        {
            iDUIGraphics.PopLayer();
        }
        #endregion
        #region IDisposable
        public void Dispose()
        {
            iDUIGraphics.Dispose();
        }
        #endregion
        public static implicit operator DUIGraphics(Graphics graphics)
        {
            return new DUIGraphics(graphics);
        }
        public override bool Equals(object obj)
        {
            DUIGraphics dUIGraphics = obj as DUIGraphics;
            if (dUIGraphics == null)
            {
                return false;
            }
            else
            {
                return this.iDUIGraphics.Equals(dUIGraphics.iDUIGraphics);
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
