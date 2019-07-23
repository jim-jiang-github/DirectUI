using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    internal class GDIP
    {
        public static void SkewTransform(Graphics g, float skewX, float skewY)
        {
            float shearX = (float)(1 / Math.Tan(Math.PI / 180 * (90 - skewY / Math.PI * 180)));
            float shearY = (float)(1 / Math.Tan(Math.PI / 180 * (90 - skewX / Math.PI * 180)));
            g.Transform.Shear(shearX, shearY);
        }
        public static void DrawImage(Graphics g, Image image, PointF[] polygon, float opacity)
        {
            ColorMatrix clrMatrix = new ColorMatrix(new float[][] { new float[] { 1, 0, 0, 0, 0 }, new float[] { 0, 1, 0, 0, 0 }, new float[] { 0, 0, 1, 0, 0 }, new float[] { 0, 0, 0, (float)opacity, 0 }, new float[] { 0, 0, 0, 0, 1 } });
            ImageAttributes imgAttributes = new ImageAttributes();
            imgAttributes.SetColorMatrix(clrMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);//设置图像的颜色属性
            using (TextureBrush tb = new TextureBrush(image, new Rectangle(0, 0, image.Width, image.Height), imgAttributes))
            {
                tb.WrapMode = WrapMode.Clamp;
                g.FillPolygon(tb, polygon);
            }
        }
    }
}
