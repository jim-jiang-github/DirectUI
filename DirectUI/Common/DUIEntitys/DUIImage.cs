using DirectUI.Core;
using SharpDX.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIImage : DUIDependentOnRenderTarget
    {
        private int width = 0;
        private int height = 0;
        private SharpDX.Direct2D1.Bitmap dxBitmap = null;
        private System.Drawing.Bitmap bitmap = null;
        private SharpDX.Direct2D1.Bitmap DxBitmap
        {
            get
            {
                if (dxBitmap == null || this.isNewRenderTarget)
                {
                    if (this.RenderTarget != null)
                    {
                        if (dxBitmap != null)
                        {
                            dxBitmap.Dispose();
                            dxBitmap = null;
                        }
                        dxBitmap = DxConvert.ToBitmap(this.RenderTarget, this.bitmap);
                        isNewRenderTarget = false;
                    }
                }
                return dxBitmap;
            }
        }
        public PixelFormat PixelFormat
        {
            get
            {
                if (this.bitmap == null)
                {
                    return PixelFormat.Undefined;
                }
                return this.bitmap.PixelFormat;
            }
        }
        public bool IsDispose
        {
            get
            {
                if (this.bitmap == null)
                {
                    return true;
                }
                else
                {
                    if (this.bitmap.PixelFormat == PixelFormat.Undefined)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public object Tag { get; set; }
        public int Width { get { return this.width; } }
        public int Height { get { return this.height; } }
        private DUIImage(System.Drawing.Bitmap bitmap)
        {
            if (bitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppPArgb)
            {
                Bitmap newBitmap = new System.Drawing.Bitmap(bitmap.Width, bitmap.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(newBitmap))
                {
                    g.DrawImage(bitmap, new System.Drawing.Rectangle(0, 0, newBitmap.Width, newBitmap.Height), new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.GraphicsUnit.Pixel);
                }
                bitmap.Dispose();
                this.bitmap = newBitmap;
            }
            else
            {
                this.bitmap = bitmap;
            }
            this.width = this.bitmap.Width;
            this.height = this.bitmap.Height;
        }
        public DUIImage(float width, float height)
        {
            Size size = Size.Ceiling(new SizeF(width, height));
            this.bitmap = new Bitmap(size.Width, size.Height);
        }
        public static DUIImage FromFile(string imagePath)
        {
            using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                return FromStream(fs);
            }
        }
        public static DUIImage FromImage(System.Drawing.Bitmap image)
        {
            if (image == null)
            {
                return null;
            }
            return new DUIImage(image);
        }
        public static DUIImage FromImage(System.Drawing.Image image)
        {
            if (image == null)
            {
                return null;
            }
            return new DUIImage((System.Drawing.Bitmap)image);
        }
        public static DUIImage FromStream(Stream stream)
        {
            return new DUIImage((System.Drawing.Bitmap)System.Drawing.Image.FromStream(stream));
        }

        public static implicit operator SharpDX.Direct2D1.Bitmap(DUIImage dUIImage)
        {
            return dUIImage.DxBitmap;
        }
        public static implicit operator System.Drawing.Bitmap(DUIImage dUIImage)
        {
            return dUIImage.bitmap;
        }
        public override void Dispose()
        {
            if (this.bitmap != null)
            {
                this.bitmap.Dispose();
                this.bitmap = null;
            }
            if (this.dxBitmap != null)
            {
                this.dxBitmap.Dispose();
                this.dxBitmap = null;
            }
            base.Dispose();
        }
        public override void DisposeDx()
        {
            if (this.dxBitmap != null)
            {
                this.dxBitmap.Dispose();
                this.dxBitmap = null;
            }
        }
    }
}
