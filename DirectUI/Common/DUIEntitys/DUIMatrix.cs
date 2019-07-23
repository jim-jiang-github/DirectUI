using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIMatrix : IDisposable
    {
        private static DUIMatrix dUIMatrix = new DUIMatrix();
        public static DUIMatrix Default => dUIMatrix;
        private SharpDX.Mathematics.Interop.RawMatrix3x2 dxMmatrix = new SharpDX.Mathematics.Interop.RawMatrix3x2();
        private System.Drawing.Drawing2D.Matrix matrix = null;
        public float OffsetX { get { return this.matrix.OffsetX; } }
        public float OffsetY { get { return this.matrix.OffsetY; } }
        public float M11 { get { return this.dxMmatrix.M11; } }
        public float M12 { get { return this.dxMmatrix.M12; } }
        public float M21 { get { return this.dxMmatrix.M21; } }
        public float M22 { get { return this.dxMmatrix.M22; } }
        public float M31 { get { return this.dxMmatrix.M31; } }
        public float M32 { get { return this.dxMmatrix.M32; } }
        public DUIMatrix()
            : this(new System.Drawing.Drawing2D.Matrix())
        {
        }
        public DUIMatrix(System.Drawing.Drawing2D.Matrix matrix)
        {
            this.matrix = matrix.Clone();
            RefreshMatrix3x2();
        }
        public DUIMatrix(float m11, float m12, float m21, float m22, float m31, float m32)
        {
            this.matrix = new System.Drawing.Drawing2D.Matrix(m11, m12, m21, m22, m31, m32);
            RefreshMatrix3x2();
        }
        private void RefreshMatrix3x2()
        {
            this.dxMmatrix.M11 = float.IsNaN(this.matrix.Elements[0]) ? 0 : this.matrix.Elements[0];
            this.dxMmatrix.M12 = float.IsNaN(this.matrix.Elements[1]) ? 0 : this.matrix.Elements[1];
            this.dxMmatrix.M21 = float.IsNaN(this.matrix.Elements[2]) ? 0 : this.matrix.Elements[2];
            this.dxMmatrix.M22 = float.IsNaN(this.matrix.Elements[3]) ? 0 : this.matrix.Elements[3];
            this.dxMmatrix.M31 = float.IsNaN(this.matrix.Elements[4]) ? 0 : this.matrix.Elements[4];
            this.dxMmatrix.M32 = float.IsNaN(this.matrix.Elements[5]) ? 0 : this.matrix.Elements[5];
        }
        public static implicit operator SharpDX.Mathematics.Interop.RawMatrix3x2(DUIMatrix dUIMatrix)
        {
            return dUIMatrix.dxMmatrix;
        }
        #region Translate
        /// <summary> 通过预先计算转换向量，将指定的转换向量（offsetX 和 offsetY）逆向的应用到此 DUIMatrix。
        /// </summary>
        /// <param name="offset">offset值，通过它转换此 DUIMatrix</param>
        public void TranslateReverse(PointF offset)
        {
            this.Translate(-offset.X, -offset.Y);
        }
        /// <summary> 通过预先计算转换向量，将指定的转换向量（offsetX 和 offsetY）逆向的应用到此 DUIMatrix。
        /// </summary>
        /// <param name="offset">offset值，通过它转换此 DUIMatrix</param>
        public void TranslateReverse(float offsetX, float offsetY)
        {
            this.Translate(-offsetX, -offsetY);
        }
        public void Translate(PointF offset)
        {
            this.Translate(offset.X, offset.Y);
        }
        /// <summary> 通过预先计算转换向量，将指定的转换向量（offsetX 和 offsetY）应用到此 DUIMatrix。
        /// </summary>
        /// <param name="offsetX">x 值，通过它转换此 DUIMatrix</param>
        /// <param name="offsetY">y 值，通过它转换此 DUIMatrix</param>
        public void Translate(float offsetX, float offsetY)
        {
            this.matrix.Translate(offsetX, offsetY);
            RefreshMatrix3x2();
        }
        #endregion
        #region Rotate
        /// <summary> 预先计算此 DUIMatrix，沿原点并按指定角度顺时针旋转。
        /// </summary>
        /// <param name="angle">旋转的角度（单位：度）。</param>
        public void Rotate(float angle)
        {
            this.RotateAt(angle, 0, 0);
        }
        /// <summary> 预先计算此 DUIMatrix，沿原点并按指定角度顺时针旋转。
        /// </summary>
        /// <param name="angle">旋转的角度（单位：度）。</param>
        /// <param name="centerX">旋转的中心点X</param>
        /// <param name="centerY">旋转的中心点Y</param>
        public void RotateAt(float angle, float centerX, float centerY)
        {
            if (centerX != 0 | centerY != 0)
            {
                this.matrix.Translate(centerX, centerY);
            }
            this.matrix.Rotate(angle);
            if (centerX != 0 | centerY != 0)
            {
                this.matrix.Translate(-centerX, -centerY);
            }
            RefreshMatrix3x2();
        }
        /// <summary> 预先计算此 DUIMatrix，沿原点并按指定角度顺时针旋转。
        /// </summary>
        /// <param name="angle">旋转的角度（单位：度）。</param>
        /// <param name="center">旋转的中心点</param>
        public void RotateAt(float angle, PointF center)
        {
            this.RotateAt(angle, center.X, center.Y);
        }
        #endregion
        #region Skew
        /// <summary> 通过预先计算比例向量，将指定的比例向量应用到此 DUIMatrix。
        /// </summary>
        /// <param name="skew">此 DUIMatrix 切变的值。</param>
        public void Skew(PointF skew)
        {
            this.Skew(skew.X, skew.Y);
        }
        /// <summary> 通过预先计算比例向量，将指定的比例向量应用到此 DUIMatrix。
        /// </summary>
        /// <param name="skewX">此 DUIMatrix 在 X 轴方向切变的值。</param>
        /// <param name="skewY">此 DUIMatrix 在 Y 轴方向切变的值。</param>
        public void Skew(float skewX, float skewY)
        {
            this.SkewAt(skewX, skewY, 0, 0);
        }
        /// <summary> 通过预先计算比例向量，将指定的比例向量应用到此 DUIMatrix。
        /// </summary>
        /// <param name="skewX">此 DUIMatrix 在 X 轴方向切变的值。</param>
        /// <param name="skewY">此 DUIMatrix 在 Y 轴方向切变的值。</param>
        /// <param name="center">此 DUIMatrix 切变的中心点。</param>
        public void Skew(float skewX, float skewY, PointF center)
        {
            this.SkewAt(skewX, skewY, center.X, center.Y);
        }
        /// <summary> 通过预先计算比例向量，将指定的比例向量应用到此 DUIMatrix。
        /// </summary>
        /// <param name="skew">此 DUIMatrix 切变的值。</param>
        /// <param name="centerX">此 DUIMatrix 切变的中心点X 轴方向的值。</param>
        /// <param name="centerY">此 DUIMatrix 切变的中心点Y 轴方向的值。</param>
        public void SkewAt(PointF skew, float centerX, float centerY)
        {
            this.SkewAt(skew.X, skew.Y, centerX, centerY);
        }
        /// <summary> 通过预先计算比例向量，将指定的比例向量应用到此 DUIMatrix。
        /// </summary>
        /// <param name="skew">此 DUIMatrix 切变的值。</param>
        /// <param name="center">此 DUIMatrix 切变的中心点。</param>
        public void SkewAt(PointF skew, PointF center)
        {
            this.SkewAt(skew, center.X, center.Y);
        }
        /// <summary> 通过预先计算比例向量，将指定的比例向量应用到此 DUIMatrix。
        /// </summary>
        /// <param name="skewX">此 DUIMatrix 在 X 轴方向切变的值。</param>
        /// <param name="skewY">此 DUIMatrix 在 Y 轴方向切变的值。</param>
        /// <param name="centerX">此 DUIMatrix 切变的中心点X 轴方向的值。</param>
        /// <param name="centerY">此 DUIMatrix 切变的中心点Y 轴方向的值。</param>
        public void SkewAt(float skewX, float skewY, float centerX, float centerY)
        {
            //float shearX = (float)(1 / Math.Tan(Math.PI / 2 - skewY));
            //float shearY = (float)(1 / Math.Tan(Math.PI / 2 - skewX));
            this.ShearAt(skewX, skewY, centerX, centerY);
        }
        #endregion
        #region ShearAt
        /// <summary> 通过预先计算比例向量，将指定的比例向量应用到此 DUIMatrix。
        /// </summary>
        /// <param name="shear">此 DUIMatrix 切变的值。</param>
        public void Shear(PointF shear)
        {
            this.ShearAt(shear.X, shear.Y, 0, 0);
        }
        /// <summary> 通过预先计算比例向量，将指定的比例向量应用到此 DUIMatrix。
        /// </summary>
        /// <param name="shearX">此 DUIMatrix 在 X 轴方向切变的值。</param>
        /// <param name="shearY">此 DUIMatrix 在 Y 轴方向切变的值。</param>
        public void Shear(float shearX, float shearY)
        {
            this.ShearAt(shearX, shearY, 0, 0);
        }
        /// <summary> 通过预先计算比例向量，将指定的比例向量应用到此 DUIMatrix。
        /// </summary>
        /// <param name="shear">此 DUIMatrix 切变的值。</param>
        /// <param name="center">此 DUIMatrix 切变的中心点。</param>
        public void ShearAt(PointF shear, PointF center)
        {
            this.ShearAt(shear.X, shear.Y, center.X, center.Y);
        }
        /// <summary> 通过预先计算比例向量，将指定的比例向量应用到此 DUIMatrix。
        /// </summary>
        /// <param name="shear">此 DUIMatrix 切变的值。</param>
        /// <param name="centerX">此 DUIMatrix 切变的中心点X 轴方向的值。</param>
        /// <param name="centerY">此 DUIMatrix 切变的中心点Y 轴方向的值。</param>
        public void ShearAt(PointF shear, float centerX, float centerY)
        {
            this.ShearAt(shear.X, shear.Y, centerX, centerY);
        }
        /// <summary> 通过预先计算比例向量，将指定的比例向量应用到此 DUIMatrix。
        /// </summary>
        /// <param name="shearX">此 DUIMatrix 在 X 轴方向切变的值。</param>
        /// <param name="shearY">此 DUIMatrix 在 Y 轴方向切变的值。</param>
        /// <param name="center">此 DUIMatrix 切变的中心点。</param>
        public void ShearAt(float shearX, float shearY, PointF center)
        {
            this.ShearAt(shearX, shearY, center.X, center.Y);
        }
        /// <summary> 通过预先计算比例向量，将指定的比例向量应用到此 DUIMatrix。
        /// </summary>
        /// <param name="shearX">此 DUIMatrix 在 X 轴方向切变的值。</param>
        /// <param name="shearY">此 DUIMatrix 在 Y 轴方向切变的值。</param>
        /// <param name="centerX">此 DUIMatrix 切变的中心点X 轴方向的值。</param>
        /// <param name="centerY">此 DUIMatrix 切变的中心点Y 轴方向的值。</param>
        public void ShearAt(float shearX, float shearY, float centerX, float centerY)
        {
            if (centerX != 0 | centerY != 0)
            {
                this.matrix.Translate(centerX, centerY);
            }
            this.matrix.Shear(shearX, shearY);
            if (centerX != 0 | centerY != 0)
            {
                this.matrix.Translate(-centerX, -centerY);
            }
            RefreshMatrix3x2();
        }
        #endregion
        #region Scale
        /// <summary> 通过预先计算比例向量，将指定的比例向量应用到此 DUIMatrix。
        /// </summary>
        /// <param name="scale">此 DUIMatrix 缩放的值。</param>
        public void Scale(PointF scale)
        {
            this.ScaleAt(scale.X, scale.Y, 0, 0);
        }
        /// <summary> 通过预先计算比例向量，将指定的比例向量应用到此 DUIMatrix。
        /// </summary>
        /// <param name="scaleX">此 DUIMatrix 在 X 轴方向缩放的值。</param>
        /// <param name="scaleY">此 DUIMatrix 在 Y 轴方向缩放的值。</param>
        public void Scale(float scaleX, float scaleY)
        {
            this.ScaleAt(scaleX, scaleY, 0, 0);
        }
        /// <summary> 通过预先计算比例向量，将指定的比例向量应用到此 DUIMatrix。
        /// </summary>
        /// <param name="scale">此 DUIMatrix 缩放的值。</param>
        /// <param name="center">此 DUIMatrix 缩放的中心点。</param>
        public void ScaleAt(PointF scale, PointF center)
        {
            this.ScaleAt(scale.X, scale.Y, center.X, center.Y);
        }
        /// <summary> 通过预先计算比例向量，将指定的比例向量应用到此 DUIMatrix。
        /// </summary>
        /// <param name="scaleX">此 DUIMatrix 在 X 轴方向缩放的值。</param>
        /// <param name="scaleY">此 DUIMatrix 在 Y 轴方向缩放的值。</param>
        /// <param name="center">此 DUIMatrix 缩放的中心点。</param>
        public void ScaleAt(float scaleX, float scaleY, PointF center)
        {
            this.ScaleAt(scaleX, scaleY, center.X, center.Y);
        }
        /// <summary> 通过预先计算比例向量，将指定的比例向量应用到此 DUIMatrix。
        /// </summary>
        /// <param name="scale">此 DUIMatrix 缩放的值。</param>
        /// <param name="centerX">此 DUIMatrix 缩放的中心点X 轴方向的值。</param>
        /// <param name="centerY">此 DUIMatrix 缩放的中心点Y 轴方向的值。</param>
        public void ScaleAt(PointF scale, float centerX, float centerY)
        {
            this.ScaleAt(scale.X, scale.Y, centerX, centerY);
        }
        /// <summary> 通过预先计算比例向量，将指定的比例向量应用到此 DUIMatrix。
        /// </summary>
        /// <param name="scaleX">此 DUIMatrix 在 X 轴方向缩放的值。</param>
        /// <param name="scaleY">此 DUIMatrix 在 Y 轴方向缩放的值。</param>
        /// <param name="centerX">此 DUIMatrix 缩放的中心点X 轴方向的值。</param>
        /// <param name="centerY">此 DUIMatrix 缩放的中心点Y 轴方向的值。</param>
        public void ScaleAt(float scaleX, float scaleY, float centerX, float centerY)
        {
            if (centerX != 0 | centerY != 0)
            {
                this.matrix.Translate(centerX, centerY);
            }
            this.matrix.Scale(scaleX, scaleY);
            if (centerX != 0 | centerY != 0)
            {
                this.matrix.Translate(-centerX, -centerY);
            }
            RefreshMatrix3x2();
        }
        #endregion
        public void Multiply(DUIMatrix dUIMatrix)
        {
            Multiply(dUIMatrix, System.Drawing.Drawing2D.MatrixOrder.Prepend);
        }
        public void Multiply(DUIMatrix dUIMatrix, System.Drawing.Drawing2D.MatrixOrder matrixOrder)
        {
            this.matrix.Multiply(dUIMatrix, matrixOrder);
            RefreshMatrix3x2();
        }
        public void Invert()
        {
            this.matrix.Invert();
            RefreshMatrix3x2();
        }
        public void Reset()
        {
            this.matrix.Reset();
            RefreshMatrix3x2();
        }
        public void Dispose()
        {
            this.matrix.Dispose();
        }
        internal DUIMatrix Clone()
        {
            DUIMatrix dxMmatrix = new DUIMatrix();
            dxMmatrix.matrix = new System.Drawing.Drawing2D.Matrix(
                this.matrix.Elements[0],
                this.matrix.Elements[1],
                this.matrix.Elements[2],
                this.matrix.Elements[3],
                this.matrix.Elements[4],
                this.matrix.Elements[5]);
            dxMmatrix.RefreshMatrix3x2();
            return dxMmatrix;
        }
        public static DUIMatrix operator +(DUIMatrix matrix1, DUIMatrix matrix2)
        {
            return new DUIMatrix(
                matrix1.M11 + matrix2.M11,
                matrix1.M12 + matrix2.M12,
                matrix1.M21 + matrix2.M21,
                matrix1.M22 + matrix2.M22,
                matrix1.M31 + matrix2.M31,
                matrix1.M32 + matrix2.M32
                );
        }
        public static DUIMatrix operator -(DUIMatrix matrix1, DUIMatrix matrix2)
        {
            return new DUIMatrix(
                matrix1.M11 - matrix2.M11,
                matrix1.M12 - matrix2.M12,
                matrix1.M21 - matrix2.M21,
                matrix1.M22 - matrix2.M22,
                matrix1.M31 - matrix2.M31,
                matrix1.M32 - matrix2.M32
                );
        }
        public static DUIMatrix operator *(DUIMatrix matrix1, DUIMatrix matrix2)
        {
            matrix1.Multiply(matrix2);
            return matrix1;
        }
        public static implicit operator System.Drawing.Drawing2D.Matrix(DUIMatrix dUIMatrix)
        {
            return dUIMatrix.matrix;
        }
        public static implicit operator DUIMatrix(System.Drawing.Drawing2D.Matrix matrix)
        {
            return new DUIMatrix(matrix);
        }
    }
}
