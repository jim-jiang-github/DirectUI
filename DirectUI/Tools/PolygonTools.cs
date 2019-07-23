/********************************************************************
 * * 使本项目源码前请仔细阅读以下协议内容，如果你同意以下协议才能使用本项目所有的功能,
 * * 否则如果你违反了以下协议，有可能陷入法律纠纷和赔偿，作者保留追究法律责任的权利。
 * * Copyright (C) 
 * * 作者： 煎饼的归宿    QQ：375324644   
 * * 请保留以上版权信息，否则作者将保留追究法律责任。
 * * 最后修改：BF-20150503UIWA
 * * 创建时间：2017/12/28 11:52:04
********************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI
{
    internal class PolygonTools
    {
        /// <summary> 判断多边形是顺时针还是逆时针
        /// </summary>  
        /// <param name="points">所有的点</param>  
        /// <param name="isYAxixToDown">true:Y轴向下为正(屏幕坐标系),false:Y轴向上为正(一般的坐标系)</param>  
        /// <returns></returns>  
        public static ClockDirection CalculateClockDirection(PointF[] points, bool isYAxixToDown = false)
        {
            int i, j, k;
            int count = 0;
            double z;
            int yTrans = isYAxixToDown ? (-1) : (1);
            if (points == null || points.Length < 3)
            {
                return (0);
            }
            int n = points.Length;
            for (i = 0; i < n; i++)
            {
                j = (i + 1) % n;
                k = (i + 2) % n;
                z = (points[j].X - points[i].X) * (points[k].Y * yTrans - points[j].Y * yTrans);
                z -= (points[j].Y * yTrans - points[i].Y * yTrans) * (points[k].X - points[j].X);
                if (z < 0)
                {
                    count--;
                }
                else if (z > 0)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                return (ClockDirection.Counterclockwise);
            }
            else if (count < 0)
            {
                return (ClockDirection.Clockwise);
            }
            else
            {
                return (ClockDirection.None);
            }
        }
        /// <summary> 离散点获取最大的外接多边形
        /// </summary>
        /// <param name="points">离散点</param>
        /// <returns>多边形</returns>
        public static PointF[] Points2MaximumPolygon(PointF[] points)
        {
            points = points.Distinct().ToArray();
            if (points.Length < 3) { return new PointF[0]; }
            points = points.OrderBy(p => p.X).ToArray(); //按X坐标从小到大排序
            List<PointF> polygon = new List<PointF>();
            //如果有多个X相等的点，再按Y从小到大排序，获取的第一个点就是顶点
            PointF firstVertex = points.Where(p => p.X == points[0].X).OrderBy(p => p.Y).First(); //顶点
            PointF vertex = firstVertex;
            PointF lastVertex = new PointF(vertex.X, vertex.Y - 1); //第一次点的比较，取X坐标相等的往上一点的点
            polygon.Add(vertex); //多边形加入顶点
            while (true)
            {
                PointF[] noVertex = points.Where(p => p != vertex && p != lastVertex).ToArray(); //不能是上两个顶点
                //var asdasdqweq = noVertex.Select(p => //曾经出现过bug复现不了。。这个函数是用来测试那个bug的先留着
                //{
                //    var clockDirection = PolygonTools.CalculateClockDirection(new PointF[3] { lastVertex, vertex, p }); //三个点构成的时针方向
                //    var angle = PointTools.Point3Angle(vertex, lastVertex, p); //三点夹角
                //    return clockDirection == ClockDirection.Counterclockwise ? 360 - angle : angle; //三点的顺时针夹角
                //});
                var vertexs = noVertex.Select(p =>
                {
                    var clockDirection = PolygonTools.CalculateClockDirection(new PointF[3] { lastVertex, vertex, p }); //三个点构成的时针方向
                    float angle = 360;
                    if (clockDirection != ClockDirection.None)
                    {
                        angle = PointTools.ThreePointAngle(vertex, lastVertex, p); //三点夹角
                        angle = clockDirection == ClockDirection.Counterclockwise ? 360 - angle : angle; //三点的顺时针夹角
                    }
                    return new { Angle = angle, Distance = PointTools.Distance(vertex, p), Vertex = p };
                }).OrderBy(p => p.Angle).ToArray(); //取得顺时针夹角最小的点
                PointF newVertex = vertexs.Where(p => p.Angle == vertexs[0].Angle).OrderByDescending(p => p.Distance).First().Vertex;
                if (newVertex == firstVertex) //如果回到了顶点就说明完成的多边形的查找
                {
                    break;
                }
                polygon.Add(newVertex); //加入顶点
                lastVertex = vertex;
                vertex = polygon[polygon.Count - 1];
            }
            return polygon.ToArray();
        }
        /// <summary> 获取多边形的外接矩形
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static RectangleF GetPolygonRect(PointF[] polygon)
        {
            var polygonRect = PolygonTools.Points2MaximumPolygon(polygon);
            float x = polygonRect.Length == 0 ? 0 : polygonRect.Min(p => p.X);
            float y = polygonRect.Length == 0 ? 0 : polygonRect.Min(p => p.Y);
            float r = polygonRect.Length == 0 ? 0 : polygonRect.Max(p => p.X);
            float b = polygonRect.Length == 0 ? 0 : polygonRect.Max(p => p.Y);
            return new RectangleF(x, y, r - x, b - y);
        }
        /// <summary> 比较两个多边形是否相等
        /// </summary>
        /// <param name="polygon1"></param>
        /// <param name="polygon2"></param>
        /// <returns></returns>
        public static bool PolygonEquels(PointF[] polygon1, PointF[] polygon2, float precision = 0)
        {
            if (polygon1 == null && polygon2 == null) { return true; }
            if ((polygon1 == null && polygon2 != null) || (polygon1 != null && polygon2 == null)) { return false; }
            if (polygon1.Length != polygon2.Length)
            {
                return false;
            }
            else
            {
                int length = polygon1.Length;
                bool isEqual = true;
                for (int i = 0; i < length; i++)
                {

                    isEqual &= Math.Abs(polygon1[i].X - polygon2[i].X) < precision && Math.Abs(polygon1[i].Y - polygon2[i].Y) < precision;
                }
                return isEqual;
            }
        }
    }
    /// <summary>  
    /// 时钟方向  
    /// </summary>  
    public enum ClockDirection
    {
        /// <summary>  
        /// 无.可能是不可计算的图形，比如多点共线  
        /// </summary>  
        None,

        /// <summary>  
        /// 顺时针方向  
        /// </summary>  
        Clockwise,

        /// <summary>  
        /// 逆时针方向  
        /// </summary>  
        Counterclockwise
    }
}
