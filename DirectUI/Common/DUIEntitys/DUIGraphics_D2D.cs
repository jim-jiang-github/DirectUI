using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DirectUI.Common
{
    public class DUIGraphics_D2D : IDUIGraphics
    {
        #region 变量
        private DUIRenderTarget target = null;
        private Stack<bool> isPushLayer = new Stack<bool>();
        private DUIMatrix matrix = new DUIMatrix();
        private SharpDX.Direct2D1.LayerParameters layerParameters = new SharpDX.Direct2D1.LayerParameters() { Opacity = 1F };
        #endregion
        #region 属性
        /// <summary> 获取或设置此 DUIGraphics 的呈现质量
        /// </summary>
        public DUISmoothingMode SmoothingMode
        {
            get
            {
                switch (this.target.RenderTarget.AntialiasMode)
                {
                    case SharpDX.Direct2D1.AntialiasMode.Aliased:
                        return DUISmoothingMode.Default;
                    case SharpDX.Direct2D1.AntialiasMode.PerPrimitive:
                        return DUISmoothingMode.AntiAlias;
                }
                return DUISmoothingMode.Default;
            }
            set
            {
                switch (value)
                {
                    case DUISmoothingMode.Default:
                        this.target.RenderTarget.AntialiasMode = SharpDX.Direct2D1.AntialiasMode.Aliased;
                        break;
                    case DUISmoothingMode.AntiAlias:
                        this.target.RenderTarget.AntialiasMode = SharpDX.Direct2D1.AntialiasMode.PerPrimitive;
                        break;
                }
            }
        }
        /// <summary> 获取或设置与此 DUIGraphics 关联的文本的呈现模式
        /// </summary>
        public DUITextRenderingHint TextRenderingHint
        {
            get
            {
                switch (this.target.RenderTarget.TextAntialiasMode)
                {
                    case SharpDX.Direct2D1.TextAntialiasMode.Aliased:
                        return DUITextRenderingHint.Default;
                    case SharpDX.Direct2D1.TextAntialiasMode.Cleartype:
                        return DUITextRenderingHint.AntiAlias;
                }
                return DUITextRenderingHint.Default;
            }
            set
            {
                switch (value)
                {
                    case DUITextRenderingHint.Default:
                        this.target.RenderTarget.TextAntialiasMode = SharpDX.Direct2D1.TextAntialiasMode.Aliased;
                        break;
                    case DUITextRenderingHint.AntiAlias:
                        this.target.RenderTarget.TextAntialiasMode = SharpDX.Direct2D1.TextAntialiasMode.Cleartype;
                        break;
                }
            }
        }
        /// <summary> 获取或设置此 DUIGraphics 的几何世界变换的副本
        /// </summary>
        public DUIMatrix Transform
        {
            get
            {
                return this.matrix.Clone();
            }
            set
            {
                this.matrix = value;
                this.target.RenderTarget.Transform = this.matrix;
            }
        }
        public float DpiX { get { return this.target.DpiX; } }
        public float DpiY { get { return this.target.DpiY; } }
        public RectangleF ClipBounds
        {
            get { return new RectangleF(0, 0, this.target.RenderTarget.Size.Width, this.target.RenderTarget.Size.Height); }
        }
        #endregion
        #region 构造函数
        private DUIGraphics_D2D(DUIRenderTarget target)
        {
            this.target = target;
        }
        //public DUIGraphics_D2D(IntPtr handle) : this(new DUIDeviceContext(handle))
        public DUIGraphics_D2D(IntPtr handle) : this(new DUIWindowRenderTarget(handle))
        {
        }
        public DUIGraphics_D2D(DUIBitmap bitmap) : this(new DUIWICRenderTarget(bitmap))
        {
        }
        /// <summary> 从指定的 Control 创建新的 DUIGraphics
        /// </summary>
        /// <param name="control">System.Windows.Forms.Control</param>
        /// <returns>新的 DUIGraphics</returns>
        public static DUIGraphics_D2D FromControl(Control control)
        {
            return new DUIGraphics_D2D(control.Handle);
        }
        #endregion
        #region 函数
        public DirectUI.Common.DUIGraphicsState Save()
        {
            return new DUIGraphicsState(this.Transform, this.SmoothingMode, this.TextRenderingHint);
        }
        public void Restore(DirectUI.Common.DUIGraphicsState graphicsState)
        {
            this.Transform = graphicsState;
            this.SmoothingMode = graphicsState;
            this.TextRenderingHint = graphicsState;
        }
        public void BeginDraw(Region r)
        {
            this.matrix = new DUIMatrix();
            this.target.BeginDraw();
            this.TranslateTransform(0.5F, 0.5F);
        }
        /// <summary> 清除整个绘图面并以指定背景色填充
        /// </summary>
        /// <param name="color">System.Drawing.Color 结构，它表示绘图面的背景色</param>
        public void Clear(Color color)
        {
            //this.DxRenderInfos.Clear();
            if (color == Color.Transparent)
            {
                this.target.RenderTarget.Clear(DxConvert.ToColor4(color));
                if (this.target is DUIWindowRenderTarget dUIWindowRenderTarget)
                {
                    Control control = Control.FromHandle(dUIWindowRenderTarget.Handle);
                    if (control != null)
                    {
                        using (Graphics ownerGraphics = Graphics.FromHwnd(dUIWindowRenderTarget.Handle))
                        using (DUIImage image = new DUIImage(control.ClientSize.Width, control.ClientSize.Height))
                        using (Graphics targetGraphics = Graphics.FromImage(image))
                        {
                            IntPtr ownerDC = ownerGraphics.GetHdc();
                            IntPtr targetDC = targetGraphics.GetHdc();
                            DirectUI.Win32.NativeMethods.BitBlt(targetDC, 0, 0, control.ClientSize.Width, control.ClientSize.Height, ownerDC, 0, 0, 13369376);
                            ownerGraphics.ReleaseHdc(ownerDC);
                            targetGraphics.ReleaseHdc(targetDC);
                            image.RenderTarget = this.target;
                            this.target.RenderTarget.DrawBitmap(image, DxConvert.ToRectF(0, 0, control.ClientSize.Width, control.ClientSize.Height), 1, SharpDX.Direct2D1.BitmapInterpolationMode.NearestNeighbor, DxConvert.ToRectF(0, 0, image.Width, image.Height));

                        }
                    }
                }
            }
            else
            {
                this.target.RenderTarget.Clear(DxConvert.ToColor4(color));
            }
        }
        public void EndDraw()
        {
            this.target.EndDraw();
            this.ResetTransform();
        }
        public void ResetTransform()
        {
            this.matrix.Reset();
            this.target.RenderTarget.Transform = this.matrix;
        }
        public void Resize(SizeF size)
        {
            this.target.Resize(new Size((int)size.Width, (int)size.Height));
        }
        #endregion
        #region RoundedRectangle
        public void DrawRoundedRectangle(DUIPen pen, float x, float y, float width, float height, float radius)
        {
            pen.RenderTarget = this.target;
            if (pen.IsDefaultStyleProperties)
            {
                this.target.RenderTarget.DrawRoundedRectangle(DxConvert.ToRoundRectF(x, y, width, height, radius), pen, pen.Width);
            }
            else
            {
                this.target.RenderTarget.DrawRoundedRectangle(DxConvert.ToRoundRectF(x, y, width, height, radius), pen, pen.Width, pen);
            }
        }
        public void FillRoundedRectangle(DUIBrush brush, float x, float y, float width, float height, float radius)
        {
            brush.RenderTarget = this.target;
            this.target.RenderTarget.FillRoundedRectangle(DxConvert.ToRoundRectF(x, y, width, height, radius), brush);
        }
        #endregion
        #region Rectangle
        public void DrawRectangle(DUIPen pen, float x, float y, float width, float height)
        {
            pen.RenderTarget = this.target;
            if (pen.IsDefaultStyleProperties)
            {
                this.target.RenderTarget.DrawRectangle(DxConvert.ToRectF(new RectangleF(x, y, width, height)), pen, pen.Width);
            }
            else
            {
                this.target.RenderTarget.DrawRectangle(DxConvert.ToRectF(new RectangleF(x, y, width, height)), pen, pen.Width, pen);
            }
        }
        public void FillRectangle(DUIBrush brush, float x, float y, float width, float height)
        {
            brush.RenderTarget = this.target;
            this.target.RenderTarget.FillRectangle(DxConvert.ToRectF(new RectangleF(x, y, width, height)), brush);
        }
        #endregion
        #region Ellipse
        public void DrawEllipse(DUIPen pen, float x, float y, float width, float height)
        {
            pen.RenderTarget = this.target;
            if (pen.IsDefaultStyleProperties)
            {
                this.target.RenderTarget.DrawEllipse(DxConvert.ToEllipse(new RectangleF(x, y, width, height)), pen, pen.Width);
            }
            else
            {
                this.target.RenderTarget.DrawEllipse(DxConvert.ToEllipse(new RectangleF(x, y, width, height)), pen, pen.Width, pen);
            }
        }
        public void FillEllipse(DUIBrush brush, float x, float y, float width, float height)
        {
            brush.RenderTarget = this.target;
            this.target.RenderTarget.FillEllipse(DxConvert.ToEllipse(new RectangleF(x, y, width, height)), brush);
        }
        #endregion
        #region Polygon
        public void DrawPolygon(DUIPen pen, PointF[] points)
        {
            bool closed = true;
            if (points == null) { return; }
            if (points.Length < (closed ? 3 : 2)) { return; }
            pen.RenderTarget = this.target;
            if (pen.IsDefaultStyleProperties)
            {
                using (SharpDX.Direct2D1.PathGeometry pathGeometry = new SharpDX.Direct2D1.PathGeometry(this.target.RenderTarget.Factory))
                using (SharpDX.Direct2D1.GeometrySink geometrySink = pathGeometry.Open())
                {
                    geometrySink.BeginFigure(DxConvert.ToVector2(points[0]), SharpDX.Direct2D1.FigureBegin.Filled);
                    for (int i = 1; i < points.Length; i++)
                    {
                        geometrySink.AddLine(DxConvert.ToVector2(points[i]));
                    }
                    geometrySink.EndFigure(closed ? SharpDX.Direct2D1.FigureEnd.Closed : SharpDX.Direct2D1.FigureEnd.Open);
                    geometrySink.Close();
                    this.target.RenderTarget.DrawGeometry(pathGeometry, pen, pen.Width);
                }
            }
            else
            {
                using (SharpDX.Direct2D1.PathGeometry pathGeometry = new SharpDX.Direct2D1.PathGeometry(this.target.RenderTarget.Factory))
                using (SharpDX.Direct2D1.GeometrySink geometrySink = pathGeometry.Open())
                {
                    geometrySink.BeginFigure(DxConvert.ToVector2(points[0]), SharpDX.Direct2D1.FigureBegin.Filled);
                    for (int i = 1; i < points.Length; i++)
                    {
                        geometrySink.AddLine(DxConvert.ToVector2(points[i]));
                    }
                    geometrySink.EndFigure(closed ? SharpDX.Direct2D1.FigureEnd.Closed : SharpDX.Direct2D1.FigureEnd.Open);
                    geometrySink.Close();
                    this.target.RenderTarget.DrawGeometry(pathGeometry, pen, pen.Width, pen);
                }
            }
        }
        public void FillPolygon(DUIBrush brush, PointF[] points)
        {
            bool closed = true;
            if (points == null) { return; }
            if (points.Length < 3) { return; }
            brush.RenderTarget = this.target;
            using (SharpDX.Direct2D1.PathGeometry pathGeometry = new SharpDX.Direct2D1.PathGeometry(this.target.RenderTarget.Factory))
            using (SharpDX.Direct2D1.GeometrySink geometrySink = pathGeometry.Open())
            {
                geometrySink.SetFillMode(SharpDX.Direct2D1.FillMode.Alternate);
                geometrySink.BeginFigure(DxConvert.ToVector2(points[0]), SharpDX.Direct2D1.FigureBegin.Filled);
                for (int i = 1; i < points.Length; i++)
                {
                    geometrySink.AddLine(DxConvert.ToVector2(points[i]));
                }
                geometrySink.EndFigure(closed ? SharpDX.Direct2D1.FigureEnd.Closed : SharpDX.Direct2D1.FigureEnd.Open);
                geometrySink.Close();
                this.target.RenderTarget.FillGeometry(pathGeometry, brush);
            }
        }
        #endregion
        #region Region
        public void FillRegion(DUIBrush brush, DUIRegion region)
        {
            brush.RenderTarget = this.target;
            region.RenderTarget = this.target;
            this.target.RenderTarget.FillGeometry(region, brush);
        }
        #endregion
        #region Line
        public void DrawLine(DUIPen pen, float x1, float y1, float x2, float y2)
        {
            pen.RenderTarget = this.target;
            if (pen.IsDefaultStyleProperties)
            {
                this.target.RenderTarget.DrawLine(new SharpDX.Mathematics.Interop.RawVector2(x1, y1), new SharpDX.Mathematics.Interop.RawVector2(x2, y2), pen, pen.Width);
            }
            else
            {
                this.target.RenderTarget.DrawLine(new SharpDX.Mathematics.Interop.RawVector2(x1, y1), new SharpDX.Mathematics.Interop.RawVector2(x2, y2), pen, pen.Width, pen);
            }
        }
        #endregion
        #region Bezier
        public void DrawBezier(DUIPen pen, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
        {
            pen.RenderTarget = this.target;
            if (pen.IsDefaultStyleProperties)
            {
                using (SharpDX.Direct2D1.PathGeometry pathGeometry = new SharpDX.Direct2D1.PathGeometry(this.target.RenderTarget.Factory))
                using (SharpDX.Direct2D1.GeometrySink geometrySink = pathGeometry.Open())
                {
                    geometrySink.BeginFigure(DxConvert.ToVector2(x1, y1), SharpDX.Direct2D1.FigureBegin.Hollow);
                    geometrySink.AddBezier(new SharpDX.Direct2D1.BezierSegment() { Point1 = DxConvert.ToVector2(x2, y2), Point2 = DxConvert.ToVector2(x3, y3), Point3 = DxConvert.ToVector2(x4, y4) });
                    geometrySink.EndFigure(SharpDX.Direct2D1.FigureEnd.Open);
                    geometrySink.Close();
                    this.target.RenderTarget.DrawGeometry(pathGeometry, pen, pen.Width);
                }
            }
            else
            {
                using (SharpDX.Direct2D1.PathGeometry pathGeometry = new SharpDX.Direct2D1.PathGeometry(this.target.RenderTarget.Factory))
                using (SharpDX.Direct2D1.GeometrySink geometrySink = pathGeometry.Open())
                {
                    geometrySink.BeginFigure(DxConvert.ToVector2(x1, y1), SharpDX.Direct2D1.FigureBegin.Hollow);
                    geometrySink.AddBezier(new SharpDX.Direct2D1.BezierSegment() { Point1 = DxConvert.ToVector2(x2, y2), Point2 = DxConvert.ToVector2(x3, y3), Point3 = DxConvert.ToVector2(x4, y4) });
                    geometrySink.EndFigure(SharpDX.Direct2D1.FigureEnd.Open);
                    geometrySink.Close();
                    this.target.RenderTarget.DrawGeometry(pathGeometry, pen, pen.Width, pen);
                }
            }
        }
        #endregion
        #region String
        public SizeF MeasureString(string text, DUIFont font, float width, float height)
        {
            using (SharpDX.DirectWrite.TextLayout tl = DxConvert.ToTextLayout(font, text))
            {
                float w = tl.Metrics.WidthIncludingTrailingWhitespace;
                float h = tl.Metrics.Height;
                return new SizeF(w, h);
            }
        }
        public void DrawString(string s, DUIFont font, DUIBrush brush, RectangleF layoutRectangle, StringFormat format)
        {
            brush.RenderTarget = this.target;
            try
            {
                this.target.RenderTarget.DrawText(s, font, DxConvert.ToRectF(new RectangleF(layoutRectangle.X, layoutRectangle.Y, float.MaxValue, 0)), brush);
            }
            catch (Exception ex)
            {
                Log.DUILog.GettingLog(ex);
                //这里报错率很高，但不知道为什么
            }
        }
        #endregion
        #region Image
        public void DrawImage(DUIImage image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit, float opacity)
        {
            image.RenderTarget = this.target;
            this.target.RenderTarget.DrawBitmap(image, DxConvert.ToRectF(destRect), opacity, SharpDX.Direct2D1.BitmapInterpolationMode.NearestNeighbor, DxConvert.ToRectF(srcRect));
        }
        public void DrawImage(DUIImage image, PointF[] destTriangle, PointF[] srcTriangle, GraphicsUnit srcUnit, float opacity)
        {
            PointF t1 = destTriangle[0];
            PointF t2 = destTriangle[1];
            PointF t3 = destTriangle[2];
            image.RenderTarget = this.target;
            using (DirectUI.Common.DUIBitmapBrush dbs = new DirectUI.Common.DUIBitmapBrush(image, DUIExtendMode.Clamp, opacity))
            using (SharpDX.Direct2D1.PathGeometry pathGeometry = new SharpDX.Direct2D1.PathGeometry(this.target.RenderTarget.Factory))
            using (SharpDX.Direct2D1.GeometrySink gs1 = pathGeometry.Open())
            {
                dbs.RenderTarget = this.target;
                gs1.SetFillMode(SharpDX.Direct2D1.FillMode.Alternate);
                gs1.BeginFigure(DxConvert.ToVector2(t1), SharpDX.Direct2D1.FigureBegin.Filled);
                gs1.AddLine(DxConvert.ToVector2(t2));
                gs1.AddLine(DxConvert.ToVector2(t3));
                gs1.EndFigure(SharpDX.Direct2D1.FigureEnd.Closed);
                gs1.Close();
                dbs.Transform = MatrixTools.ThreePointsAffine(srcTriangle, destTriangle);
                this.target.RenderTarget.FillGeometry(pathGeometry, dbs);
            }
        }
        public void DrawImage(DUIImage image, PointF[] polygon, GraphicsUnit srcUnit, float opacity)
        {
            image.RenderTarget = this.target;
            using (DirectUI.Common.DUIBitmapBrush dbs = new DirectUI.Common.DUIBitmapBrush(image, DUIExtendMode.Clamp, opacity))
            using (SharpDX.Direct2D1.PathGeometry pathGeometry = new SharpDX.Direct2D1.PathGeometry(this.target.RenderTarget.Factory))
            using (SharpDX.Direct2D1.GeometrySink gs1 = pathGeometry.Open())
            {
                dbs.RenderTarget = this.target;
                gs1.SetFillMode(SharpDX.Direct2D1.FillMode.Alternate);
                gs1.BeginFigure(DxConvert.ToVector2(polygon[0]), SharpDX.Direct2D1.FigureBegin.Filled);
                for (int i = 1; i < polygon.Length; i++)
                {
                    gs1.AddLine(DxConvert.ToVector2(polygon[i]));
                }
                gs1.EndFigure(SharpDX.Direct2D1.FigureEnd.Closed);
                gs1.Close();
                this.target.RenderTarget.FillGeometry(pathGeometry, dbs);
            }
        }
        #endregion
        #region Transform
        /// <summary> 通过使此 DUIGraphics 的变换矩阵左乘指定的平移来更改坐标系统的原点
        /// </summary>
        /// <param name="dx">平移的 x 坐标</param>
        /// <param name="dy">平移的 y 坐标</param>
        public void TranslateTransform(float dx, float dy)
        {
            this.matrix.Translate(dx, dy);
            this.target.RenderTarget.Transform = this.matrix;
        }
        /// <summary> 将指定的缩放操作应用于此 System.Drawing.Graphics 的变换矩阵，方法是将该对象的变换矩阵左乘该缩放矩阵
        /// </summary>
        /// <param name="sx">x 方向的缩放比例</param>
        /// <param name="sy">y 方向的缩放比例</param>
        public void ScaleTransform(float sx, float sy)
        {
            this.matrix.Scale(sx, sy);
            this.target.RenderTarget.Transform = this.matrix;
        }
        /// <summary> 将指定旋转应用于此 DUIGraphics 的变换矩阵
        /// </summary>
        /// <param name="angle">旋转角度（以度为单位）</param>
        public void RotateTransform(float angle)
        {
            this.matrix.Rotate(angle);
            this.target.RenderTarget.Transform = this.matrix;
        }
        /// <summary> 通过预先计算切变变换，将指定的切变向量应用到此 System.Drawing.Drawing2D.Matrix。
        /// </summary>
        /// <param name="sx">x 方向的缩放比例</param>
        /// <param name="sy">y 方向的缩放比例</param>
        public void SkewTransform(float sx, float sy)
        {
            this.matrix.Skew(sx, sy);
            this.target.RenderTarget.Transform = this.matrix;
        }
        #endregion
        #region Layer
        /// <summary> 加入一个图层
        /// </summary>
        /// <param name="width">图层宽度</param>
        /// <param name="height">图层深度</param>
        public void PushLayer(float x, float y, float width, float height)
        {
            this.target.RenderTarget.PushAxisAlignedClip(DxConvert.ToRectF(new RectangleF(x, y, width, height)), SharpDX.Direct2D1.AntialiasMode.PerPrimitive);
            //using (SharpDX.Direct2D1.Layer layer = new SharpDX.Direct2D1.Layer(this.target.RenderTarget))
            //{
            //    layerParameters.ContentBounds = DxConvert.ToRectF(new RectangleF(x, y, width, height));
            //    this.target.RenderTarget.PushLayer(ref layerParameters, layer);
            //}
            isPushLayer.Push(true);
        }
        /// <summary> 取出一个图层
        /// </summary>
        public void PopLayer()
        {
            if (isPushLayer.Pop())
            {
                this.target.RenderTarget.PopAxisAlignedClip();
                //this.target.RenderTarget.PopLayer();
            }
        }
        #endregion
        #region IDisposable
        public void Dispose()
        {
            this.target?.Dispose();
            this.matrix?.Dispose();
        }
        #endregion
        public override bool Equals(object obj)
        {
            DUIGraphics_D2D dUIGraphics_D2D = obj as DUIGraphics_D2D;
            if (dUIGraphics_D2D == null)
            {
                return false;
            }
            else
            {
                return this.target.Equals(dUIGraphics_D2D.target);
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
