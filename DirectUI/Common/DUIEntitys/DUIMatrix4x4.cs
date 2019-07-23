using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIMatrix4x4 : IDisposable
    {
        private SharpDX.Direct2D1.DeviceContext renderTarget = null;
        private bool isNewRenderTarget = false;
        private SharpDX.Mathematics.Interop.RawMatrix dxMmatrix4x4 = new SharpDX.Mathematics.Interop.RawMatrix();
        public float M11 { get { return dxMmatrix4x4.M11; } }
        public float M12 { get { return dxMmatrix4x4.M12; } }
        public float M13 { get { return dxMmatrix4x4.M13; } }
        public float M14 { get { return dxMmatrix4x4.M14; } }
        public float M21 { get { return dxMmatrix4x4.M21; } }
        public float M22 { get { return dxMmatrix4x4.M22; } }
        public float M23 { get { return dxMmatrix4x4.M23; } }
        public float M24 { get { return dxMmatrix4x4.M24; } }
        public float M31 { get { return dxMmatrix4x4.M31; } }
        public float M32 { get { return dxMmatrix4x4.M32; } }
        public float M33 { get { return dxMmatrix4x4.M33; } }
        public float M34 { get { return dxMmatrix4x4.M34; } }
        public float M41 { get { return dxMmatrix4x4.M41; } }
        public float M42 { get { return dxMmatrix4x4.M42; } }
        public float M43 { get { return dxMmatrix4x4.M43; } }
        public float M44 { get { return dxMmatrix4x4.M44; } }
        public float this[int x, int y]
        {
            get
            {
                int index = x * 4 + y;
                switch (index)
                {
                    case 0:
                        return dxMmatrix4x4.M11;
                    case 1:
                        return dxMmatrix4x4.M12;
                    case 2:
                        return dxMmatrix4x4.M13;
                    case 3:
                        return dxMmatrix4x4.M14;
                    case 4:
                        return dxMmatrix4x4.M21;
                    case 5:
                        return dxMmatrix4x4.M22;
                    case 6:
                        return dxMmatrix4x4.M23;
                    case 7:
                        return dxMmatrix4x4.M24;
                    case 8:
                        return dxMmatrix4x4.M31;
                    case 9:
                        return dxMmatrix4x4.M32;
                    case 10:
                        return dxMmatrix4x4.M33;
                    case 11:
                        return dxMmatrix4x4.M34;
                    case 12:
                        return dxMmatrix4x4.M41;
                    case 13:
                        return dxMmatrix4x4.M42;
                    case 14:
                        return dxMmatrix4x4.M43;
                    case 15:
                        return dxMmatrix4x4.M44;
                }
                return 0;
            }
            set
            {
                int index = x * 4 + y;
                switch (index)
                {
                    case 0:
                        dxMmatrix4x4.M11 = value;
                        break;
                    case 1:
                        dxMmatrix4x4.M12 = value;
                        break;
                    case 2:
                        dxMmatrix4x4.M13 = value;
                        break;
                    case 3:
                        dxMmatrix4x4.M14 = value;
                        break;
                    case 4:
                        dxMmatrix4x4.M21 = value;
                        break;
                    case 5:
                        dxMmatrix4x4.M22 = value;
                        break;
                    case 6:
                        dxMmatrix4x4.M23 = value;
                        break;
                    case 7:
                        dxMmatrix4x4.M24 = value;
                        break;
                    case 8:
                        dxMmatrix4x4.M31 = value;
                        break;
                    case 9:
                        dxMmatrix4x4.M32 = value;
                        break;
                    case 10:
                        dxMmatrix4x4.M33 = value;
                        break;
                    case 11:
                        dxMmatrix4x4.M34 = value;
                        break;
                    case 12:
                        dxMmatrix4x4.M41 = value;
                        break;
                    case 13:
                        dxMmatrix4x4.M42 = value;
                        break;
                    case 14:
                        dxMmatrix4x4.M43 = value;
                        break;
                    case 15:
                        dxMmatrix4x4.M44 = value;
                        break;
                }
            }
        }
        internal SharpDX.Direct2D1.DeviceContext RenderTarget
        {
            get { return renderTarget; }
            set
            {
                if (renderTarget != value)
                {
                    renderTarget = value;
                    isNewRenderTarget = true;
                }
            }
        }
        private SharpDX.Mathematics.Interop.RawMatrix DxMatrix4x4
        {
            get
            {
                //if (dxMmatrix4x4 == null || isNewRenderTarget)
                //{
                //    if (renderTarget != null)
                //    {
                //        //dxMmatrix4x4 = new SharpDX.Matrix();
                //        isNewRenderTarget = false;
                //    }
                //}
                return dxMmatrix4x4;
            }
        }
        public DUIMatrix4x4()
        {
            dxMmatrix4x4 = new SharpDX.Mathematics.Interop.RawMatrix();
            dxMmatrix4x4.M11 = 1;
            dxMmatrix4x4.M12 = 0;
            dxMmatrix4x4.M13 = 0;
            dxMmatrix4x4.M14 = 0;

            dxMmatrix4x4.M21 = 0;
            dxMmatrix4x4.M22 = 1;
            dxMmatrix4x4.M23 = 0;
            dxMmatrix4x4.M24 = 0;

            dxMmatrix4x4.M31 = 0;
            dxMmatrix4x4.M32 = 0;
            dxMmatrix4x4.M33 = 1;
            dxMmatrix4x4.M34 = 0;

            dxMmatrix4x4.M41 = 0;
            dxMmatrix4x4.M42 = 0;
            dxMmatrix4x4.M43 = 0;
            dxMmatrix4x4.M44 = 1;
        }
        public DUIMatrix4x4(float[] values)
        {
            dxMmatrix4x4 = new SharpDX.Mathematics.Interop.RawMatrix();
            dxMmatrix4x4.M11 = values[0];
            dxMmatrix4x4.M12 = values[1];
            dxMmatrix4x4.M13 = values[2];
            dxMmatrix4x4.M14 = values[3];

            dxMmatrix4x4.M21 = values[4];
            dxMmatrix4x4.M22 = values[5];
            dxMmatrix4x4.M23 = values[6];
            dxMmatrix4x4.M24 = values[7]; 

            dxMmatrix4x4.M31 = values[8]; 
            dxMmatrix4x4.M32 = values[9]; 
            dxMmatrix4x4.M33 = values[10]; 
            dxMmatrix4x4.M34 = values[11]; 

            dxMmatrix4x4.M41 = values[12]; 
            dxMmatrix4x4.M42 = values[13]; 
            dxMmatrix4x4.M43 = values[14]; 
            dxMmatrix4x4.M44 = values[15]; 
        }
        public DUIMatrix4x4(float M11, float M12, float M13, float M14, float M21, float M22, float M23, float M24, float M31, float M32, float M33, float M34, float M41, float M42, float M43, float M44)
        {
            dxMmatrix4x4 = new SharpDX.Mathematics.Interop.RawMatrix();
            dxMmatrix4x4.M11 = M11;
            dxMmatrix4x4.M12 = M12;
            dxMmatrix4x4.M13 = M13;
            dxMmatrix4x4.M14 = M14;

            dxMmatrix4x4.M21 = M21;
            dxMmatrix4x4.M22 = M22;
            dxMmatrix4x4.M23 = M23;
            dxMmatrix4x4.M24 = M24;

            dxMmatrix4x4.M31 = M31;
            dxMmatrix4x4.M32 = M32;
            dxMmatrix4x4.M33 = M33;
            dxMmatrix4x4.M34 = M34;

            dxMmatrix4x4.M41 = M41;
            dxMmatrix4x4.M42 = M42;
            dxMmatrix4x4.M43 = M43;
            dxMmatrix4x4.M44 = M44;
        }
        public static implicit operator SharpDX.Mathematics.Interop.RawMatrix(DUIMatrix4x4 matrix4x4)
        {
            return matrix4x4.DxMatrix4x4;
        }
        public static DUIMatrix4x4 operator +(DUIMatrix4x4 matrix4x4_1, DUIMatrix4x4 matrix4x4_2)
        {
            return new DUIMatrix4x4(
                matrix4x4_1.M11 + matrix4x4_2.M11,
                matrix4x4_1.M12 + matrix4x4_2.M12,
                matrix4x4_1.M13 + matrix4x4_2.M13,
                matrix4x4_1.M14 + matrix4x4_2.M14,
                matrix4x4_1.M21 + matrix4x4_2.M21,
                matrix4x4_1.M22 + matrix4x4_2.M22,
                matrix4x4_1.M23 + matrix4x4_2.M23,
                matrix4x4_1.M24 + matrix4x4_2.M24,
                matrix4x4_1.M31 + matrix4x4_2.M31,
                matrix4x4_1.M32 + matrix4x4_2.M32,
                matrix4x4_1.M33 + matrix4x4_2.M33,
                matrix4x4_1.M34 + matrix4x4_2.M34,
                matrix4x4_1.M41 + matrix4x4_2.M41,
                matrix4x4_1.M42 + matrix4x4_2.M42,
                matrix4x4_1.M43 + matrix4x4_2.M43,
                matrix4x4_1.M44 + matrix4x4_2.M44
                );
        }
        public static DUIMatrix4x4 operator -(DUIMatrix4x4 matrix4x4_1, DUIMatrix4x4 matrix4x4_2)
        {
            return new DUIMatrix4x4(
                matrix4x4_1.M11 - matrix4x4_2.M11,
                matrix4x4_1.M12 - matrix4x4_2.M12,
                matrix4x4_1.M13 - matrix4x4_2.M13,
                matrix4x4_1.M14 - matrix4x4_2.M14,
                matrix4x4_1.M21 - matrix4x4_2.M21,
                matrix4x4_1.M22 - matrix4x4_2.M22,
                matrix4x4_1.M23 - matrix4x4_2.M23,
                matrix4x4_1.M24 - matrix4x4_2.M24,
                matrix4x4_1.M31 - matrix4x4_2.M31,
                matrix4x4_1.M32 - matrix4x4_2.M32,
                matrix4x4_1.M33 - matrix4x4_2.M33,
                matrix4x4_1.M34 - matrix4x4_2.M34,
                matrix4x4_1.M41 - matrix4x4_2.M41,
                matrix4x4_1.M42 - matrix4x4_2.M42,
                matrix4x4_1.M43 - matrix4x4_2.M43,
                matrix4x4_1.M44 - matrix4x4_2.M44
                );
        }
        public static DUIMatrix4x4 operator *(DUIMatrix4x4 matrix4x4_1, DUIMatrix4x4 matrix4x4_2)
        {
            DUIMatrix4x4 matrix4x4 = new DUIMatrix4x4();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    matrix4x4[i, j] = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        matrix4x4[i, j] += matrix4x4_1[i, k] * matrix4x4_2[k, j];
                    }
                }
            }
            return matrix4x4;
        }

        public void Dispose()
        {
        }
    }
}
