using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public static class DxConvert
    {
        private static SharpDX.DirectWrite.Factory directWriteFactory = new SharpDX.DirectWrite.Factory();
        public static SharpDX.Direct2D1.CapStyle ToCapStyle(System.Drawing.Drawing2D.DashCap dashCap)
        {
            switch (dashCap)
            {
                case System.Drawing.Drawing2D.DashCap.Triangle:
                    return SharpDX.Direct2D1.CapStyle.Triangle;
                case System.Drawing.Drawing2D.DashCap.Flat:
                    return SharpDX.Direct2D1.CapStyle.Flat;
                case System.Drawing.Drawing2D.DashCap.Round:
                    return SharpDX.Direct2D1.CapStyle.Round;
            }
            return SharpDX.Direct2D1.CapStyle.Flat;
        }
        public static SharpDX.Direct2D1.CapStyle ToCapStyle(System.Drawing.Drawing2D.LineCap lineCap)
        {
            switch (lineCap)
            {
                case System.Drawing.Drawing2D.LineCap.ArrowAnchor:
                    return SharpDX.Direct2D1.CapStyle.Triangle;
                case System.Drawing.Drawing2D.LineCap.Square:
                    return SharpDX.Direct2D1.CapStyle.Square;
                case System.Drawing.Drawing2D.LineCap.Flat:
                    return SharpDX.Direct2D1.CapStyle.Flat;
                case System.Drawing.Drawing2D.LineCap.Round:
                    return SharpDX.Direct2D1.CapStyle.Round;
            }
            return SharpDX.Direct2D1.CapStyle.Flat;
        }
        /// <summary> System.Drawing.Color转DUIColor
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static DUIGraphicsState ToDUIGraphicsState(GraphicsState graphicsState)
        {
            return new DUIGraphicsState(graphicsState);
        }
        /// <summary> System.Drawing.Color转DUIColor
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static DUIColor ToColor4(Color color)
        {
            return new DUIColor(color);
        }
        public static SharpDX.Mathematics.Interop.RawVector2 ToVector2(PointF p)
        {
            return ToVector2(p.X, p.Y);
        }
        public static SharpDX.Mathematics.Interop.RawVector2 ToVector2(float x, float y)
        {
            return new SharpDX.Mathematics.Interop.RawVector2(x, y);
        }
        public static SharpDX.Size2F ToSizeF(SizeF sizeF)
        {
            return new SharpDX.Size2F(sizeF.Width, sizeF.Height);
        }
        public static SharpDX.Size2F ToSizeF(float width, float height)
        {
            return ToSizeF(new SizeF(width, height));
        }
        public static DUIRectangleF ToRectF(RectangleF rectf)
        {
            return ToRectF(rectf.X, rectf.Y, rectf.Width, rectf.Height);
        }
        public static DUIRectangleF ToRectF(float x, float y, float width, float height)
        {
            return new DUIRectangleF(new RectangleF(x, y, width, height));
        }
        public static SharpDX.Direct2D1.RectangleGeometry ToRectangleGeometry(SharpDX.Direct2D1.Factory factory, RectangleF rect)
        {
            return new SharpDX.Direct2D1.RectangleGeometry(factory, ToRectF(rect));
        }
        public static SharpDX.Direct2D1.RoundedRectangle ToRoundRectF(RectangleF rectf, float radius)
        {
            return ToRoundRectF(rectf.X, rectf.Y, rectf.Width, rectf.Height, radius);
        }
        public static SharpDX.Direct2D1.RoundedRectangle ToRoundRectF(float x, float y, float width, float height, float radius)
        {
            SharpDX.Direct2D1.RoundedRectangle rr = new SharpDX.Direct2D1.RoundedRectangle();
            rr.Rect = ToRectF(x, y, width, height);
            rr.RadiusX = radius;
            rr.RadiusY = radius;
            return rr;
        }
        public static SharpDX.Direct2D1.Ellipse ToEllipse(RectangleF rectf)
        {
            //return new SharpDX.Direct2D1.Ellipse(new SharpDX.Mathematics.Interop.RawVector2(rectf.X + rectf.Width / 2 + 0.5F / this.scaleX, rectf.Y + rectf.Height / 2 + 0.5F / this.scaleX), rectf.Width / 2, rectf.Height / 2);
            return new SharpDX.Direct2D1.Ellipse(new SharpDX.Mathematics.Interop.RawVector2(rectf.X + rectf.Width / 2, rectf.Y + rectf.Height / 2), rectf.Width / 2, rectf.Height / 2);
        }
        public static SharpDX.Direct2D1.EllipseGeometry ToEllipseGeometry(SharpDX.Direct2D1.Factory factory, RectangleF rect)
        {
            return new SharpDX.Direct2D1.EllipseGeometry(factory, ToEllipse(rect));
        }
        public static SharpDX.Mathematics.Interop.RawMatrix3x2 ToMatrix3x2(System.Drawing.Drawing2D.Matrix matrix)
        {
            return new SharpDX.Mathematics.Interop.RawMatrix3x2(matrix.Elements[0], matrix.Elements[1], matrix.Elements[2], matrix.Elements[3], matrix.Elements[4], matrix.Elements[5]);
        }
        public static System.Drawing.Drawing2D.Matrix ToMatrix(SharpDX.Mathematics.Interop.RawMatrix3x2 matrix)
        {
            return new System.Drawing.Drawing2D.Matrix(matrix.M11, matrix.M12, matrix.M21, matrix.M22, matrix.M31, matrix.M32);
        }
        public static SharpDX.Direct2D1.Brush ToBrush(DUIRenderTarget renderTarget, Brush brush)
        {
            if (brush is SolidBrush sb)
            {
                return new SharpDX.Direct2D1.SolidColorBrush(renderTarget, ToColor4(sb.Color));
            }
            return null;
        }
        public static SharpDX.Direct2D1.Brush ToBrush(DUIRenderTarget renderTarget, Brush brush, SharpDX.Direct2D1.LinearGradientBrushProperties linearGradientBrushProperties)
        {
            if (brush is LinearGradientBrush lgb)
            {
                linearGradientBrushProperties.StartPoint = ToVector2(lgb.Rectangle.Location);
                linearGradientBrushProperties.EndPoint = new SharpDX.Mathematics.Interop.RawVector2(lgb.Rectangle.Right, lgb.Rectangle.Bottom);
                SharpDX.Direct2D1.GradientStop gradientStop1 = new SharpDX.Direct2D1.GradientStop() { Color = ToColor4(lgb.LinearColors[0]), Position = 0 };
                SharpDX.Direct2D1.GradientStop gradientStop2 = new SharpDX.Direct2D1.GradientStop() { Color = ToColor4(lgb.LinearColors[1]), Position = 1 };
                SharpDX.Direct2D1.GradientStop[] gradientStops = new SharpDX.Direct2D1.GradientStop[2] { gradientStop1, gradientStop2 };
                using (SharpDX.Direct2D1.GradientStopCollection gradientStopCollection = new SharpDX.Direct2D1.GradientStopCollection(renderTarget, gradientStops))
                {
                    return new SharpDX.Direct2D1.LinearGradientBrush(renderTarget, linearGradientBrushProperties, gradientStopCollection);
                }
            }
            return null;
        }
        public static SharpDX.Direct2D1.Brush ToBrush(DUIRenderTarget renderTarget, Pen pen)
        {
            return new SharpDX.Direct2D1.SolidColorBrush(renderTarget, ToColor4(pen.Color));
        }
        //public static SharpDX.Direct2D1.StrokeStyle ToStrokeStyle(DUIRenderTarget renderTarget, Pen pen)
        //{
        //    SharpDX.Direct2D1.StrokeStyleProperties strokeStyleProperties = new SharpDX.Direct2D1.StrokeStyleProperties();
        //    strokeStyleProperties.DashCap = (SharpDX.Direct2D1.CapStyle)pen.DashCap;
        //    strokeStyleProperties.DashOffset = pen.DashOffset;
        //    strokeStyleProperties.DashStyle = (SharpDX.Direct2D1.DashStyle)pen.DashStyle;
        //    strokeStyleProperties.EndCap = (SharpDX.Direct2D1.CapStyle)pen.EndCap;
        //    strokeStyleProperties.LineJoin = (SharpDX.Direct2D1.LineJoin)pen.LineJoin;
        //    strokeStyleProperties.MiterLimit = pen.MiterLimit;
        //    strokeStyleProperties.StartCap = (SharpDX.Direct2D1.CapStyle)pen.StartCap;
        //    return new SharpDX.Direct2D1.StrokeStyle(renderTarget.Factory, strokeStyleProperties);
        //}
        public static SharpDX.Direct2D1.StrokeStyle ToStrokeStyle(DUIRenderTarget renderTarget, SharpDX.Direct2D1.StrokeStyleProperties strokeStyleProperties)
        {
            return new SharpDX.Direct2D1.StrokeStyle(renderTarget.RenderTarget.Factory, strokeStyleProperties);
        }
        public static SharpDX.Direct2D1.BitmapBrush ToBitmapBrush(DUIRenderTarget renderTarget, string path)
        {
            return new SharpDX.Direct2D1.BitmapBrush(renderTarget, ToBitmap(renderTarget, path));
        }
        //public static SharpDX.Direct2D1.BitmapBrush ToBitmapBrush(DUIRenderTarget renderTarget, Bitmap bitmap)
        //{
        //    return new SharpDX.Direct2D1.BitmapBrush(renderTarget, ToBitmap(renderTarget, bitmap));
        //}
        public static SharpDX.Direct2D1.BitmapBrush ToBitmapBrush(DUIRenderTarget renderTarget, DUIBitmapBrush bitmapBrush)
        {
            bitmapBrush.Image.RenderTarget = renderTarget;
            SharpDX.Direct2D1.BitmapBrush bb = new SharpDX.Direct2D1.BitmapBrush(renderTarget, bitmapBrush.Image);
            var extendMode = DxConvert.ToExtendMode(bitmapBrush.ExtendMode);
            bb.ExtendModeX = extendMode;
            bb.ExtendModeY = extendMode;
            bb.InterpolationMode = SharpDX.Direct2D1.BitmapInterpolationMode.NearestNeighbor;
            bb.Opacity = bitmapBrush.Opacity;
            return bb;
        }
        public static SharpDX.Direct2D1.BitmapBrush ToBitmapBrush(DUIRenderTarget renderTarget, SharpDX.Direct2D1.Bitmap dxBitmap)
        {
            SharpDX.Direct2D1.BitmapBrush bb = new SharpDX.Direct2D1.BitmapBrush(renderTarget, dxBitmap);
            var extendMode = DxConvert.ToExtendMode(DUIExtendMode.Clamp);
            bb.ExtendModeX = extendMode;
            bb.ExtendModeY = extendMode;
            bb.InterpolationMode = SharpDX.Direct2D1.BitmapInterpolationMode.NearestNeighbor;
            return bb;
        }
        public static SharpDX.Direct2D1.ExtendMode ToExtendMode(DUIExtendMode dUIExtendMode)
        {
            switch (dUIExtendMode)
            {
                case DUIExtendMode.Clamp:
                    return SharpDX.Direct2D1.ExtendMode.Clamp;
                case DUIExtendMode.Mirror:
                    return SharpDX.Direct2D1.ExtendMode.Mirror;
                case DUIExtendMode.Wrap:
                    return SharpDX.Direct2D1.ExtendMode.Wrap;
            }
            return SharpDX.Direct2D1.ExtendMode.Wrap;
        }
        public static SharpDX.DirectWrite.TextFormat ToTextFormat(Font font)
        {
            IntPtr hdc = Win32.NativeMethods.GetDC(IntPtr.Zero);
            int DpiX = Win32.NativeMethods.GetDeviceCaps(hdc, 88);
            Win32.NativeMethods.ReleaseDC(IntPtr.Zero, hdc);
            bool isPoint = font.Unit == GraphicsUnit.Point;
            float fontSize = isPoint ? font.Size * DpiX / 72 : font.Size;
            return new SharpDX.DirectWrite.TextFormat(directWriteFactory, font.FontFamily.Name, font.Bold ? SharpDX.DirectWrite.FontWeight.Bold : SharpDX.DirectWrite.FontWeight.Regular, font.Italic ? SharpDX.DirectWrite.FontStyle.Italic : SharpDX.DirectWrite.FontStyle.Normal, fontSize);
        }
        public static SharpDX.DirectWrite.TextLayout ToTextLayout(Font font, string text)
        {
            IntPtr hdc = Win32.NativeMethods.GetDC(IntPtr.Zero);
            int DpiX = Win32.NativeMethods.GetDeviceCaps(hdc, 88);
            Win32.NativeMethods.ReleaseDC(IntPtr.Zero, hdc);
            bool isPoint = font.Unit == GraphicsUnit.Point;
            float fontSize = isPoint ? font.Size * DpiX / 72 : font.Size;
            using (SharpDX.DirectWrite.TextFormat tf = new SharpDX.DirectWrite.TextFormat(directWriteFactory, font.FontFamily.Name, font.Bold ? SharpDX.DirectWrite.FontWeight.Bold : SharpDX.DirectWrite.FontWeight.Regular, font.Italic ? SharpDX.DirectWrite.FontStyle.Italic : SharpDX.DirectWrite.FontStyle.Normal, fontSize))
                return new SharpDX.DirectWrite.TextLayout(directWriteFactory, text, tf, float.MaxValue, 0);
        }
        public static SharpDX.DirectWrite.TextLayout ToTextLayout(DUIFont font, string text)
        {
            return new SharpDX.DirectWrite.TextLayout(directWriteFactory, text, font, float.MaxValue, 0);
        }
        public static SharpDX.Direct2D1.Bitmap ToBitmap(DUIRenderTarget renderTarget, Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");
            System.Drawing.Imaging.BitmapData bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);
            var dataStream = new SharpDX.DataStream(bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, true, false);
            var properties = new SharpDX.Direct2D1.BitmapProperties
            {
                PixelFormat = new SharpDX.Direct2D1.PixelFormat
                {
                    Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm,
                    AlphaMode = SharpDX.Direct2D1.AlphaMode.Premultiplied
                }
            };
            // ToDo apply scaling here!
            //var scaler = new SharpDX.WIC.BitmapScaler(renderTarget.Factory.NativePointer);
            //scaler.
            //Load the image from the gdi resource
            var result = new SharpDX.Direct2D1.Bitmap(renderTarget, new SharpDX.Size2(bitmap.Width, bitmap.Height), dataStream, bitmapData.Stride, properties);
            bitmap.UnlockBits(bitmapData);
            return result;

            //System.Drawing.Bitmap desBitmap;//预定义要是使用的bitmap
            ////如果原始的图像像素格式不是32位带alpha通道,需要转换为32位带alpha通道的格式,否则无法和Direct2D的格式对应
            //if (bitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppPArgb)
            //{
            //    desBitmap = new System.Drawing.Bitmap(bitmap.Width, bitmap.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            //    using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(desBitmap))
            //    {
            //        g.DrawImage(bitmap, new System.Drawing.Rectangle(0, 0, desBitmap.Width, desBitmap.Height), new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.GraphicsUnit.Pixel);
            //    }
            //}
            //else
            //{
            //    desBitmap = bitmap;
            //}
            ////直接内存copy会非常快 
            ////如果使用循环逐点转换会非常慢
            //System.Drawing.Imaging.BitmapData bmpData = desBitmap.LockBits(new System.Drawing.Rectangle(0, 0, desBitmap.Width, desBitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, desBitmap.PixelFormat);
            //int numBytes = bmpData.Stride * desBitmap.Height;
            //byte[] byteData = new byte[numBytes];
            //IntPtr ptr = bmpData.Scan0;
            //System.Runtime.InteropServices.Marshal.Copy(ptr, byteData, 0, numBytes);
            //desBitmap.UnlockBits(bmpData);
            //SharpDX.Direct2D1.PixelFormat pixelFormat = new SharpDX.Direct2D1.PixelFormat(SharpDX.DXGI.Format.B8G8R8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied);
            //SharpDX.Direct2D1.BitmapProperties bp = new SharpDX.Direct2D1.BitmapProperties(pixelFormat, desBitmap.HorizontalResolution, desBitmap.VerticalResolution);
            //SharpDX.Direct2D1.Bitmap tempBitmap = new SharpDX.Direct2D1.Bitmap(renderTarget, new SharpDX.Size2(desBitmap.Width, desBitmap.Height), bp);
            //tempBitmap.CopyFromMemory(byteData, bmpData.Stride);
            //return tempBitmap;
        }
        public static SharpDX.Direct2D1.Bitmap ToBitmap(DUIRenderTarget renderTarget, string path)
        {
            using (Bitmap bmp = (Bitmap)Bitmap.FromFile(path))
            {
                return ToBitmap(renderTarget, bmp);
            }
        }
    }
}
