using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace DirectUI
{
    /// <summary> 图形工具
    /// </summary>
    internal class ShapeTools
    {
        /// <summary> 根据普通矩形得到圆角矩形的路径
        /// </summary>  
        /// <param name="rect">原始矩形</param>  
        /// <param name="cornerRadius">半径</param>  
        /// <returns>图形路径</returns>  
        public static GraphicsPath GetRoundRectangle(Rectangle rect, int cornerRadius)
        {
            return GetRoundRectangle(rect, cornerRadius, true, true, true, true);
        }
        /// <summary> 根据普通矩形得到圆角矩形的路径
        /// </summary>  
        /// <param name="rect">原始矩形</param>  
        /// <param name="cornerRadius">半径</param>  
        /// <param name="lt">左上角是否是圆角</param>
        /// <param name="rt">右上角是否是圆角</param>
        /// <param name="rb">右下是否是圆角</param>
        /// <param name="lb">左下角是否是圆角</param>
        /// <returns>图形路径</returns>  
        public static GraphicsPath GetRoundRectangle(Rectangle rect, int cornerRadius, bool lt, bool rt, bool rb, bool lb)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.FillMode = FillMode.Alternate;
            if (cornerRadius == 0)
            {
                roundedRect.AddRectangle(rect);
            }
            else
            {
                int ltCornerRadius = lt ? cornerRadius : 0;
                int rtCornerRadius = rt ? cornerRadius : 0;
                int rbCornerRadius = rb ? cornerRadius : 0;
                int lbCornerRadius = lb ? cornerRadius : 0;
                roundedRect.AddLine(new Point(rect.X + ltCornerRadius, rect.Y), new Point(rect.Right - rtCornerRadius, rect.Y));
                if (rtCornerRadius > 0)
                {
                    roundedRect.AddArc(new Rectangle(rect.Right - rtCornerRadius * 2, rect.Y, rtCornerRadius * 2, rtCornerRadius * 2), 270F, 90F);//RT
                }
                roundedRect.AddLine(new Point(rect.Right, rect.Y + rtCornerRadius), new Point(rect.Right, rect.Bottom - rbCornerRadius));
                if (rbCornerRadius > 0)
                {
                    roundedRect.AddArc(new Rectangle(rect.Right - cornerRadius * 2, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2), 0F, 90F);//RB
                }
                roundedRect.AddLine(new Point(rect.Right - rbCornerRadius, rect.Bottom), new Point(rect.X + lbCornerRadius, rect.Bottom));
                if (lbCornerRadius > 0)
                {
                    roundedRect.AddArc(new Rectangle(rect.X, rect.Bottom - lbCornerRadius * 2, lbCornerRadius * 2, lbCornerRadius * 2), 90F, 90F);//LB
                }
                roundedRect.AddLine(new Point(rect.X, rect.Bottom - lbCornerRadius), new Point(rect.X, rect.Y + ltCornerRadius));
                if (ltCornerRadius > 0)
                {
                    roundedRect.AddArc(new Rectangle(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2), 180F, 90F);//LT
                }
                roundedRect.CloseFigure();
            }
            return roundedRect;
        }
        /// <summary> 根据普通矩形得到圆角矩形的路径
        /// </summary>  
        /// <param name="rectangleF">原始矩形</param>  
        /// <param name="cornerRadius">半径</param>  
        /// <returns>图形路径</returns>  
        public static GraphicsPath GetRoundRectangle(RectangleF rect, int cornerRadius)
        {
            return GetRoundRectangle(rect, cornerRadius, true, true, true, true);
        }
        /// <summary> 根据普通矩形得到圆角矩形的路径
        /// </summary>  
        /// <param name="rectangleF">原始矩形</param>  
        /// <param name="cornerRadius">半径</param>  
        /// <param name="lt">左上角是否是圆角</param>
        /// <param name="rl">右上角是否是圆角</param>
        /// <param name="rb">右下是否是圆角</param>
        /// <param name="lb">左下角是否是圆角</param>
        /// <returns>图形路径</returns>
        public static GraphicsPath GetRoundRectangle(RectangleF rect, int cornerRadius, bool lt, bool rt, bool rb, bool lb)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.FillMode = FillMode.Alternate;
            if (cornerRadius == 0)
            {
                roundedRect.AddRectangle(rect);
            }
            else
            {
                int ltCornerRadius = lt ? cornerRadius : 0;
                int rtCornerRadius = rt ? cornerRadius : 0;
                int rbCornerRadius = rb ? cornerRadius : 0;
                int lbCornerRadius = lb ? cornerRadius : 0;
                roundedRect.AddLine(new PointF(rect.X + ltCornerRadius, rect.Y), new PointF(rect.Right - rtCornerRadius, rect.Y));
                if (rtCornerRadius > 0)
                {
                    roundedRect.AddArc(new RectangleF(rect.Right - rtCornerRadius * 2, rect.Y, rtCornerRadius * 2, rtCornerRadius * 2), 270F, 90F);//RT
                }
                roundedRect.AddLine(new PointF(rect.Right, rect.Y + rtCornerRadius), new PointF(rect.Right, rect.Bottom - rbCornerRadius));
                if (rbCornerRadius > 0)
                {
                    roundedRect.AddArc(new RectangleF(rect.Right - cornerRadius * 2, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2), 0F, 90F);//RB
                }
                roundedRect.AddLine(new PointF(rect.Right - rbCornerRadius, rect.Bottom), new PointF(rect.X + lbCornerRadius, rect.Bottom));
                if (lbCornerRadius > 0)
                {
                    roundedRect.AddArc(new RectangleF(rect.X, rect.Bottom - lbCornerRadius * 2, lbCornerRadius * 2, lbCornerRadius * 2), 90F, 90F);//LB
                }
                roundedRect.AddLine(new PointF(rect.X, rect.Bottom - lbCornerRadius), new PointF(rect.X, rect.Y + ltCornerRadius));
                if (ltCornerRadius > 0)
                {
                    roundedRect.AddArc(new RectangleF(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2), 180F, 90F);//LT
                }
                roundedRect.CloseFigure();
            }
            return roundedRect;
        }
    }
}
