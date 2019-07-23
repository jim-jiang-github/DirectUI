using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class Matrix4x4 : IDisposable
    {
        private float[] ms = new float[16] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
        /// <summary> 一个浮点值数组，它表示该 Matrix4x4 的元素
        /// </summary>
        public float[] Elements { get { return ms; } }
        public Matrix4x4()
        {
        }
        public Matrix4x4(
            float m11, float m12, float m13, float m14,
            float m21, float m22, float m23, float m24,
            float m31, float m32, float m33, float m34,
            float m41, float m42, float m43, float m44
            )
        {
        }
        public void Dispose()
        {
        }
    }
}
