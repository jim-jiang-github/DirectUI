using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUILinearGradientBrush : DUIBrush
    {
        private SharpDX.Direct2D1.LinearGradientBrushProperties linearGradientBrushProperties = new SharpDX.Direct2D1.LinearGradientBrushProperties();
        protected override SharpDX.Direct2D1.Brush DxBrush
        {
            get
            {
                if (dxBrush == null || isNewRenderTarget)
                {
                    if (this.RenderTarget != null)
                    {
                        if (dxBrush != null)
                        {
                            dxBrush.Dispose();
                            dxBrush = null;
                        }
                        dxBrush = DxConvert.ToBrush(this.RenderTarget, this.brush, linearGradientBrushProperties);
                        isNewRenderTarget = false;
                    }
                }
                return dxBrush;
            }
        }
        /// <summary> 使用指定的点和颜色初始化 DUILinearGradientBrush 类的新实例。
        /// </summary>
        /// <param name="point1">表示线性渐变起始点的 System.Drawing.PointF 结构。</param>
        /// <param name="point2">表示线性渐变终结点的 System.Drawing.PointF 结构。</param>
        /// <param name="color1">表示线性渐变起始色的 System.Drawing.Color 结构。</param>
        /// <param name="color2">表示线性渐变结束色的 System.Drawing.Color 结构。</param>
        public DUILinearGradientBrush(PointF point1, PointF point2, Color color1, Color color2)
        {
            this.brush = new LinearGradientBrush(point1, point2, color1, color2);
        }
        /// <summary> 根据矩形、起始颜色和结束颜色以及方向角度，创建 DUILinearGradientBrush 类的新实例。
        /// </summary>
        /// <param name="rect">一个 System.Drawing.Rectangle 结构，它指定线性渐变的界限</param>
        /// <param name="color1">表示渐变起始色的 System.Drawing.Color 结构</param>
        /// <param name="color2">表示渐变结束色的 System.Drawing.Color 结构</param>
        /// <param name="angle">渐变方向线的角度（从 X 轴以顺时针角度计算）。</param>
        public DUILinearGradientBrush(Rectangle rect, Color color1, Color color2, float angle)
        {
            this.brush = new LinearGradientBrush(rect, color1, color2, angle);
        }
        /// <summary> 根据一个矩形、起始颜色和结束颜色以及方向，创建 DUILinearGradientBrush 类的新实例。
        /// </summary>
        /// <param name="rect">一个 System.Drawing.Rectangle 结构，它指定线性渐变的界限</param>
        /// <param name="color1">表示渐变起始色的 System.Drawing.Color 结构</param>
        /// <param name="color2">表示渐变结束色的 System.Drawing.Color 结构</param>
        /// <param name="linearGradientMode">一个 System.Drawing.Drawing2D.LinearGradientMode 枚举元素，它指定渐变方向。渐变方向决定渐变的起点和终点。例如，LinearGradientMode.ForwardDiagonal指定起始点是矩形的左上角，而结束点是矩形的右下角</param>
        public DUILinearGradientBrush(Rectangle rect, Color color1, Color color2, LinearGradientMode linearGradientMode)
        {
            this.brush = new LinearGradientBrush(rect, color1, color2, linearGradientMode);
        }
        /// <summary> 根据矩形、起始颜色和结束颜色以及方向角度，创建 DUILinearGradientBrush 类的新实例。
        /// </summary>
        /// <param name="rect">一个 System.Drawing.RectangleF 结构，它指定线性渐变的界限</param>
        /// <param name="color1">表示渐变起始色的 System.Drawing.Color 结构</param>
        /// <param name="color2">表示渐变结束色的 System.Drawing.Color 结构</param>
        /// <param name="angle">渐变方向线的角度（从 X 轴以顺时针角度计算）。</param>
        public DUILinearGradientBrush(RectangleF rect, Color color1, Color color2, float angle)
        {
            this.brush = new LinearGradientBrush(rect, color1, color2, angle);
        }
        /// <summary> 根据一个矩形、起始颜色和结束颜色以及方向模式，创建 DUILinearGradientBrush 类的新实例。
        /// </summary>
        /// <param name="rect">一个 System.Drawing.RectangleF 结构，它指定线性渐变的界限</param>
        /// <param name="color1">表示渐变起始色的 System.Drawing.Color 结构</param>
        /// <param name="color2">表示渐变结束色的 System.Drawing.Color 结构</param>
        /// <param name="linearGradientMode">一个 System.Drawing.Drawing2D.LinearGradientMode 枚举元素，它指定渐变方向。渐变方向决定渐变的起点和终点。例如，LinearGradientMode.ForwardDiagonal指定起始点是矩形的左上角，而结束点是矩形的右下角</param>
        public DUILinearGradientBrush(RectangleF rect, Color color1, Color color2, LinearGradientMode linearGradientMode)
        {
            this.brush = new LinearGradientBrush(rect, color1, color2, linearGradientMode);
        }
        /// <summary> 根据矩形、起始颜色和结束颜色以及方向角度，创建 DUILinearGradientBrush 类的新实例。
        /// </summary>
        /// <param name="rect">一个 System.Drawing.Rectangle 结构，它指定线性渐变的界限</param>
        /// <param name="color1">表示渐变起始色的 System.Drawing.Color 结构</param>
        /// <param name="color2">表示渐变结束色的 System.Drawing.Color 结构</param>
        /// <param name="angle">渐变方向线的角度（从 X 轴以顺时针角度计算）。</param>
        /// <param name="isAngleScaleable">设置为 true，指定角度受与此 DUILinearGradientBrush 关联的变换所影响；否则为false。</param>
        public DUILinearGradientBrush(Rectangle rect, Color color1, Color color2, float angle, bool isAngleScaleable)
        {
            this.brush = new LinearGradientBrush(rect, color1, color2, angle, isAngleScaleable);
        }
        /// <summary> 根据矩形、起始颜色和结束颜色以及方向角度，创建 DUILinearGradientBrush 类的新实例。
        /// </summary>
        /// <param name="rect">一个 System.Drawing.RectangleF 结构，它指定线性渐变的界限。</param>
        /// <param name="color1">表示渐变起始色的 System.Drawing.Color 结构。</param>
        /// <param name="color2">表示渐变结束色的 System.Drawing.Color 结构。</param>
        /// <param name="angle">渐变方向线的角度（从 X 轴以顺时针角度计算）。</param>
        /// <param name="isAngleScaleable">设置为 true，指定角度受与此 DUILinearGradientBrush 关联的变换所影响；否则为false</param>
        public DUILinearGradientBrush(RectangleF rect, Color color1, Color color2, float angle, bool isAngleScaleable)
        {
            this.brush = new LinearGradientBrush(rect, color1, color2, angle, isAngleScaleable);
        }


    }
}
