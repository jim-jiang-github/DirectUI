using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI
{
    internal class RotateTools
    {
        /// <summary> 一个点绕中心点旋转一定角度到达的新坐标
        /// </summary>
        /// <param name="p">点</param>
        /// <param name="center">中心点</param>
        /// <param name="angle">角度</param>
        /// <returns>旋转后的点</returns>
        public static PointF PointRotate(PointF p, PointF center, float angle)
        {
            System.Drawing.Drawing2D.Matrix m = new System.Drawing.Drawing2D.Matrix();
            m.RotateAt(angle, new PointF(center.X - p.X, center.Y - p.Y));
            return new PointF(p.X + m.OffsetX, p.Y + m.OffsetY);
        }
        /// <summary> 一个矩形绕中心点旋转一定角度到达的新矩形（外圈的矩形）
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="center">中心点</param>
        /// <param name="angle">角度</param>
        /// <returns></returns>
        public static RectangleF RectangleRotate(RectangleF rect, PointF center, float angle)
        {
            PointF lt = new PointF(rect.Left, rect.Top);
            PointF rt = new PointF(rect.Right, rect.Top);
            PointF lb = new PointF(rect.Left, rect.Bottom);
            PointF rb = new PointF(rect.Right, rect.Bottom);
            PointF _lt = PointRotate(lt, center, angle);
            PointF _rt = PointRotate(rt, center, angle);
            PointF _lb = PointRotate(lb, center, angle);
            PointF _rb = PointRotate(rb, center, angle);
            float x = Min(_lt.X, _rt.X, _lb.X, _rb.X);
            float y = Min(_lt.Y, _rt.Y, _lb.Y, _rb.Y);
            float w = Max(_lt.X, _rt.X, _lb.X, _rb.X) - x;
            float h = Max(_lt.Y, _rt.Y, _lb.Y, _rb.Y) - y;
            return new RectangleF(x, y, w, h);
        }
        /// <summary> 一个矩形绕中心点旋转一定角度到达的新矩形（外圈的矩形）
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="center">中心点</param>
        /// <param name="angle">角度</param>
        /// <returns></returns>
        public static Rectangle RectangleRotate(Rectangle rect, PointF center, float angle)
        {
            PointF lt = new PointF(rect.Left, rect.Top);
            PointF rt = new PointF(rect.Right, rect.Top);
            PointF lb = new PointF(rect.Left, rect.Bottom);
            PointF rb = new PointF(rect.Right, rect.Bottom);
            PointF _lt = PointRotate(lt, center, angle);
            PointF _rt = PointRotate(rt, center, angle);
            PointF _lb = PointRotate(lb, center, angle);
            PointF _rb = PointRotate(rb, center, angle);
            float x = Min(_lt.X, _rt.X, _lb.X, _rb.X);
            float y = Min(_lt.Y, _rt.Y, _lb.Y, _rb.Y);
            float w = Max(_lt.X, _rt.X, _lb.X, _rb.X) - x;
            float h = Max(_lt.Y, _rt.Y, _lb.Y, _rb.Y) - y;
            return new Rectangle((int)Math.Round(x), (int)Math.Round(y), (int)Math.Ceiling(w), (int)Math.Ceiling(h));
        }
        private static float Min(params float[] values)
        {
            return values.Aggregate((v1, v2) => Math.Min(v1, v2));
        }
        private static float Max(params float[] values)
        {
            return values.Aggregate((v1, v2) => Math.Max(v1, v2));
        }
    }
}
