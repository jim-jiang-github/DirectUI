using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    internal class Tools
    {
        /// <summary> 根据普通矩形得到圆角矩形的路径
        /// </summary>  
        /// <param name="rectangle">原始矩形</param>  
        /// <param name="r">半径</param>  
        /// <returns>图形路径</returns>  
        public static GraphicsPath GetRoundRectangle(Rectangle rectangle, int r)
        {
            int l = 2 * r;
            // 把圆角矩形分成八段直线、弧的组合，依次加到路径中  
            GraphicsPath gp = new GraphicsPath(); if (r == 0)
            {
                gp.AddRectangle(rectangle);
            }
            else
            {
                gp.AddLine(new Point(rectangle.X + r, rectangle.Y), new Point(rectangle.Right - r, rectangle.Y));
                gp.AddArc(new Rectangle(rectangle.Right - l, rectangle.Y, l, l), 270F, 90F);
                gp.AddLine(new Point(rectangle.Right, rectangle.Y + r), new Point(rectangle.Right, rectangle.Bottom - r));
                gp.AddArc(new Rectangle(rectangle.Right - l, rectangle.Bottom - l, l, l), 0F, 90F);
                gp.AddLine(new Point(rectangle.Right - r, rectangle.Bottom), new Point(rectangle.X + r, rectangle.Bottom));
                gp.AddArc(new Rectangle(rectangle.X, rectangle.Bottom - l, l, l), 90F, 90F);
                gp.AddLine(new Point(rectangle.X, rectangle.Bottom - r), new Point(rectangle.X, rectangle.Y + r));
                gp.AddArc(new Rectangle(rectangle.X, rectangle.Y, l, l), 180F, 90F);
            }
            return gp;
        }
        /// <summary> 根据普通矩形得到圆角矩形的路径
        /// </summary>  
        /// <param name="rectangleF">原始矩形</param>  
        /// <param name="r">半径</param>  
        /// <returns>图形路径</returns>  
        public static GraphicsPath GetRoundRectangleF(RectangleF rectangleF, float r)
        {
            float l = 2 * r;
            // 把圆角矩形分成八段直线、弧的组合，依次加到路径中  
            GraphicsPath gp = new GraphicsPath();
            if (r == 0)
            {
                gp.AddRectangle(rectangleF);
            }
            else
            {
                gp.AddLine(new PointF(rectangleF.X + r, rectangleF.Y), new PointF(rectangleF.Right - r, rectangleF.Y));
                gp.AddArc(new RectangleF(rectangleF.Right - l, rectangleF.Y, l, l), 270F, 90F);
                gp.AddLine(new PointF(rectangleF.Right, rectangleF.Y + r), new PointF(rectangleF.Right, rectangleF.Bottom - r));
                gp.AddArc(new RectangleF(rectangleF.Right - l, rectangleF.Bottom - l, l, l), 0F, 90F);
                gp.AddLine(new PointF(rectangleF.Right - r, rectangleF.Bottom), new PointF(rectangleF.X + r, rectangleF.Bottom));
                gp.AddArc(new RectangleF(rectangleF.X, rectangleF.Bottom - l, l, l), 90F, 90F);
                gp.AddLine(new PointF(rectangleF.X, rectangleF.Bottom - r), new PointF(rectangleF.X, rectangleF.Y + r));
                gp.AddArc(new RectangleF(rectangleF.X, rectangleF.Y, l, l), 180F, 90F);
            }
            return gp;
        }
    }
}
