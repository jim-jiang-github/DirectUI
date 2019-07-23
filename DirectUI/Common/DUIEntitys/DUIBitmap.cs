using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIBitmap : ICloneable, IDisposable
    {
        private SharpDX.WIC.ImagingFactory imagingFactory = new SharpDX.WIC.ImagingFactory();
        private SharpDX.WIC.Bitmap wicBitmap;
        public int Width => this.wicBitmap.Size.Width;
        public int Height => this.wicBitmap.Size.Height;
        public DUIBitmap(int width, int height)
        {
            this.wicBitmap = new SharpDX.WIC.Bitmap(imagingFactory, width, height, SharpDX.WIC.PixelFormat.Format32bppPBGRA, SharpDX.WIC.BitmapCreateCacheOption.CacheOnLoad);
        }
        public void Save(string filePath)
        {
            using (SharpDX.WIC.WICStream wICStream = new SharpDX.WIC.WICStream(imagingFactory, filePath, SharpDX.IO.NativeFileAccess.Write))
            using (SharpDX.WIC.PngBitmapEncoder encoder = new SharpDX.WIC.PngBitmapEncoder(imagingFactory))
            {
                encoder.Initialize(wICStream);
                using (SharpDX.WIC.BitmapFrameEncode bitmapFrameEncode = new SharpDX.WIC.BitmapFrameEncode(encoder))
                {
                    bitmapFrameEncode.Initialize();
                    bitmapFrameEncode.SetSize((int)this.Width, (int)this.Height);
                    var pixelFormatGuid = SharpDX.WIC.PixelFormat.FormatDontCare;
                    bitmapFrameEncode.SetPixelFormat(ref pixelFormatGuid);
                    bitmapFrameEncode.WriteSource(wicBitmap);
                    bitmapFrameEncode.Commit();
                    encoder.Commit();
                }
            }
        }
        public static implicit operator SharpDX.WIC.Bitmap(DUIBitmap dUIBitmap)
        {
            return dUIBitmap.wicBitmap;
        }
        public static implicit operator System.Drawing.Bitmap(DUIBitmap dUIBitmap)
        {
            lock (DUIWICRenderTarget.lockObj)
            {
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(dUIBitmap.Width, dUIBitmap.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, bitmap.PixelFormat);
                dUIBitmap.wicBitmap.CopyPixels(data.Stride, data.Scan0, data.Height * data.Stride);
                bitmap.UnlockBits(data);
                return bitmap;
            }
        }
        #region ICloneable
        public object Clone()
        {
            return new SharpDX.WIC.Bitmap(imagingFactory, this.wicBitmap, SharpDX.WIC.BitmapCreateCacheOption.CacheOnLoad);
        }
        #endregion
        #region IDisposable
        public void Dispose()
        {
            wicBitmap?.Dispose();
            imagingFactory?.Dispose();
        }
        #endregion
    }
}
