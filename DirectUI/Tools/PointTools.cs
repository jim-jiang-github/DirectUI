using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI
{
    internal class PointTools
    {
        /// <summary> 计算两点的距离
        /// </summary>
        /// <param name="p1">点1</param>
        /// <param name="p2">点1</param>
        /// <returns>两点的距离</returns>
        public static float Distance(PointF p1, PointF p2)
        {
            return (float)(Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y)));
        }
        /// <summary> 点是否在多边形内部
        /// </summary>
        /// <param name="p">点</param>
        /// <param name="polygonPoints">多边形的点</param>
        /// <returns>是否在多边形内部</returns>
        public static bool IsPointInPolygon(PointF p, PointF[] polygonPoints)
        {
            if (polygonPoints.Length < 3) { return false; }
            //System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            //gp.AddPolygon(polygonPoints);
            //return gp.IsVisible(p);
            int counter = 0;
            int i;
            double xinters;
            PointF p1, p2;
            int pointCount = polygonPoints.Length;
            p1 = polygonPoints[0];
            for (i = 1; i <= pointCount; i++)
            {
                p2 = polygonPoints[i % pointCount];
                if (p.Y > Math.Min(p1.Y, p2.Y)//校验点的Y大于线段端点的最小Y  
                    && p.Y <= Math.Max(p1.Y, p2.Y))//校验点的Y小于线段端点的最大Y  
                {
                    if (p.X <= Math.Max(p1.X, p2.X))//校验点的X小于等线段端点的最大X(使用校验点的左射线判断).  
                    {
                        if (p1.Y != p2.Y)//线段不平行于X轴  
                        {
                            xinters = (p.Y - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y) + p1.X;
                            if (p1.X == p2.X || p.X <= xinters)
                            {
                                counter++;
                            }
                        }
                    }

                }
                p1 = p2;
            }
            if (counter % 2 == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary> 两点所在的直线相对坐标轴的夹角
        /// </summary>
        /// <param name="p1">点1</param>
        /// <param name="p2">点2</param>
        /// <returns></returns>
        public static float TwoPointAngle(PointF p1, PointF p2)
        {
            double angle = 0;
            if (p2.X <= p1.X)
            {
                angle = (Math.Atan((p1.Y - p2.Y) / (p1.X - p2.X)) + Math.PI) * 180 / Math.PI;
            }
            else
            {
                angle = Math.Atan((p1.Y - p2.Y) / (p1.X - p2.X)) * 180 / Math.PI;
            }
            if (angle < 0)
            {
                return (float)(360 + angle);
            }
            else { return (float)angle; }
        }
        /// <summary> 判断点在线的左边还是右边(-1是左边 0是在线上 1是右边)
        /// </summary>
        /// <param name="ps">线的起点</param>
        /// <param name="pe">线的终点</param>
        /// <param name="p">点</param>
        /// <returns>-1是左边 0是在线上 1是右边</returns>
        public static int PointAtLineRightOrLeft(PointF ps, PointF pe, PointF p)
        {
            float ax = pe.X - ps.X;
            float ay = pe.Y - ps.Y;
            float bx = p.X - ps.X;
            float by = p.Y - ps.Y;
            float judge = ax * by - ay * bx;
            if (judge > 0)
            {
                return -1;
            }
            else if (judge < 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        /// <summary> 三点的夹角
        /// </summary>
        /// <param name="pc">中心点</param>
        /// <param name="p1">第一个点</param>
        /// <param name="p2">第二个点</param>
        /// <returns>夹角</returns>
        public static float ThreePointAngle(PointF pc, PointF p1, PointF p2)
        {
            float ma_x = p1.X - pc.X;
            float ma_y = p1.Y - pc.Y;
            float mb_x = p2.X - pc.X;
            float mb_y = p2.Y - pc.Y;
            float v1 = (ma_x * mb_x) + (ma_y * mb_y);
            float ma_val = (float)Math.Sqrt(ma_x * ma_x + ma_y * ma_y);
            float mb_val = (float)Math.Sqrt(mb_x * mb_x + mb_y * mb_y);
            float cosM = v1 / (ma_val * mb_val);
            float angleAMB = (float)(Math.Acos(cosM) * 180 / Math.PI);
            return float.IsNaN(angleAMB) ? 0 : angleAMB;
        }
        /// <summary> 三点的夹角顺时针为正
        /// </summary>
        /// <param name="pc">中心点</param>
        /// <param name="p1">第一个点</param>
        /// <param name="p2">第二个点</param>
        /// <returns>夹角</returns>
        public static float ThreePointAngleWithClockwise(PointF pc, PointF p1, PointF p2)
        {
            float ma_x = p1.X - pc.X;
            float ma_y = p1.Y - pc.Y;
            float mb_x = p2.X - pc.X;
            float mb_y = p2.Y - pc.Y;
            float v1 = (ma_x * mb_x) + (ma_y * mb_y);
            float ma_val = (float)Math.Sqrt(ma_x * ma_x + ma_y * ma_y);
            float mb_val = (float)Math.Sqrt(mb_x * mb_x + mb_y * mb_y);
            float cosM = v1 / (ma_val * mb_val);
            float angleAMB = (float)(Math.Acos(cosM) * 180 / Math.PI);

            return (float.IsNaN(angleAMB) ? 0 : angleAMB) * (PointAtLineRightOrLeft(pc, p1, p2) == 1 ? 1 : -1);
        }
    }
}
