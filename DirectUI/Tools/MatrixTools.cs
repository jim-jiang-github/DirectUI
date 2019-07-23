using DirectUI.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI
{
    internal class MatrixTools
    {
        /// <summary> 一个点经过一个矩阵变换到达的新坐标
        /// </summary>
        /// <param name="p">点</param>
        /// <param name="matrix">矩阵</param>
        /// <returns>矩阵变换后的点</returns>
        public static PointF PointAfterMatrix(PointF p, DUIMatrix matrix)
        {
            double m11 = matrix.M11;
            double m12 = matrix.M12;
            double m21 = matrix.M21;
            double m22 = matrix.M22;
            double m31 = matrix.M31;
            double m32 = matrix.M32;
            return new PointF((float)(p.X * m11 + p.Y * m21 + m31), (float)(p.X * m12 + p.Y * m22 + m32));
        }
        /// <summary> 一个点在经过一个矩阵变换之前的坐标
        /// </summary>
        /// <param name="p">矩阵变换后的点</param>
        /// <param name="matrix">矩阵</param>
        /// <returns>矩阵变换前的点</returns>
        public static PointF PointBeforeMatrix(PointF p, DUIMatrix matrix)
        {
            double m11 = matrix.M11;
            double m12 = matrix.M12;
            double m21 = matrix.M21;
            double m22 = matrix.M22;
            double m31 = matrix.M31;
            double m32 = matrix.M32;
            return new PointF((float)((p.Y * m21 - m32 * m21 - p.X * m22 + m31 * m22) / (m12 * m21 - m11 * m22)), (float)((p.Y * m11 - m32 * m11 - p.X * m12 + m31 * m12) / (m22 * m11 - m21 * m12)));
        }
        /// <summary> 三对点仿射变换
        /// </summary>
        /// <param name="srcTriangle"> 原始三个点</param>
        /// <param name="destTriangle">目标三个点</param>
        /// <returns>仿射变换2x3矩阵</returns>
        public static DUIMatrix ThreePointsAffine(PointF[] srcTriangle, PointF[] destTriangle)
        {
            #region 三对点仿射变换
            PointF s1 = srcTriangle[0];
            PointF s2 = srcTriangle[1];
            PointF s3 = srcTriangle[2];
            PointF t1 = destTriangle[0];
            PointF t2 = destTriangle[1];
            PointF t3 = destTriangle[2];
            double m11 = 1;
            double m12 = 0;
            double m21 = 0;
            double m22 = 1;
            double m31 = 0;
            double m32 = 0;
            m21 =
                (t1.X * s1.X
                - t3.X * s1.X
                - t1.X * s2.X
                + t3.X * s2.X
                - t1.X * s1.X
                + t2.X * s1.X
                + t1.X * s3.X
                - t2.X * s3.X)
                / (s2.Y * s1.X
                - s1.Y * s1.X
                - s2.Y * s3.X
                + s1.Y * s3.X
                - s3.Y * s1.X
                + s1.Y * s1.X
                + s3.Y * s2.X
                - s1.Y * s2.X);
            if (s1.X - s2.X != 0)
            {
                m11 =
                    (m21 * s2.Y
                    - m21 * s1.Y
                    + t1.X
                    - t2.X)
                    / (s1.X
                    - s2.X);
            }
            else if (s1.X - s3.X != 0)
            {
                m11 =
                    (m21 * s3.Y
                    - m21 * s1.Y
                    + t1.X
                    - t3.X)
                    / (s1.X
                    - s3.X);
            }
            else if (s2.X - s3.X != 0)
            {
                m11 =
                    (m21 * s3.Y
                    - m21 * s2.Y
                    + t2.X
                    - t3.X)
                    / (s2.X
                    - s3.X);
            }
            m31 =
                t1.X
                - m11 * s1.X
                - m21 * s1.Y;
            //m31 =
            //    t2.X
            //    - m11 * s2.X
            //    - m21 * s2.Y;
            //m31
            //    = t3.X
            //    - m11 * s3.X
            //    - m21 * s3.Y;
            m22 =
                (t1.Y * s1.X
                - t3.Y * s1.X
                - t1.Y * s2.X
                + t3.Y * s2.X
                - t1.Y * s1.X
                + t2.Y * s1.X
                + t1.Y * s3.X
                - t2.Y * s3.X)
                / (s2.Y * s1.X
                - s1.Y * s1.X
                - s2.Y * s3.X
                + s1.Y * s3.X
                - s3.Y * s1.X
                + s1.Y * s1.X
                + s3.Y * s2.X
                - s1.Y * s2.X);
            if (s1.X - s2.X != 0)
            {
                m12 =
                (m22 * s2.Y
                - m22 * s1.Y
                + t1.Y
                - t2.Y)
                / (s1.X
                - s2.X);
            }
            else if (s1.X - s3.X != 0)
            {
                m12 =
                    (m22 * s3.Y
                    - m22 * s1.Y
                    + t1.Y
                    - t3.Y)
                    / (s1.X
                    - s3.X);
            }
            else if (s2.X - s3.X != 0)
            {
                m12 =
                    (m22 * s3.Y
                    - m22 * s2.Y
                    + t2.Y
                    - t3.Y)
                    / (s2.X
                    - s3.X);
            }
            m32 =
              t1.Y
              - m12 * s1.X
              - m22 * s1.Y;
            //m32 =
            //    t2.Y
            //    - m12 * s2.X
            //    - m22 * s2.Y;
            //m32
            //    = t3.Y
            //    - m12 * s3.X
            //    - m22 * s3.Y;
            #endregion
            DUIMatrix m = new DUIMatrix();
            if (destTriangle != srcTriangle)
            {
                m11 = double.IsNaN(m11) ? 1 : m11;
                m12 = double.IsNaN(m12) ? 0 : m12;
                m21 = double.IsNaN(m21) ? 0 : m21;
                m22 = double.IsNaN(m22) ? 1 : m22;
                m31 = double.IsNaN(m31) ? 0 : m31;
                m32 = double.IsNaN(m32) ? 0 : m32;
                m = new DUIMatrix((float)m11, (float)m12, (float)m21, (float)m22, (float)m31, (float)m32);
            }
            return m;
        }
    }
}