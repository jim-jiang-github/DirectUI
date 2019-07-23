using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectUI.Core
{
    /// <summary> 作用边缘
    /// </summary>
    public enum EffectMargin
    {
        /// <summary> 左上
        /// </summary>
        LeftTop = 1,
        /// <summary> 上
        /// </summary>
        Top = 2,
        /// <summary> 右上
        /// </summary>
        RightTop = 4,
        /// <summary> 右
        /// </summary>
        Right = 8,
        /// <summary> 右下
        /// </summary>
        RightBottom = 16,
        /// <summary> 下
        /// </summary>
        Bottom = 32,
        /// <summary> 左下
        /// </summary>
        LeftBottom = 64,
        /// <summary> 左
        /// </summary>
        Left = 128,
        /// <summary> 左上角旋转点
        /// </summary>
        LeftTopRotate = 256,
        /// <summary> 右上角旋转点
        /// </summary>
        RightTopRotate = 512,
        /// <summary> 右下角旋转点
        /// </summary>
        RightBottomRotate = 1024,
        /// <summary> 左下角旋转点
        /// </summary>
        LeftBottomRotate = 2048,
        /// <summary> 上方的倾斜区域1
        /// </summary>
        TopSkew1 = 4096,
        /// <summary> 上方的倾斜区域2
        /// </summary>
        TopSkew2 = 8192,
        /// <summary> 左方的倾斜区域1
        /// </summary>
        LeftSkew1 = 16384,
        /// <summary> 左方的倾斜区域2
        /// </summary>
        LeftSkew2 = 32768,
        /// <summary> 下方的倾斜区域1
        /// </summary>
        BottomSkew1 = 65536,
        /// <summary> 下方的倾斜区域2
        /// </summary>
        BottomSkew2 = 131072,
        /// <summary> 右方的倾斜区域1
        /// </summary>
        RightSkew1 = 262144,
        /// <summary> 右方的倾斜区域2
        /// </summary>
        RightSkew2 = 524288,
        /// <summary> 其他
        /// </summary>
        Other = 1048576,
        /// <summary> 点
        /// </summary>
        Point = 2097152,
        /// <summary> 未知
        /// </summary>
        Unknow = 4194304
    }
}
