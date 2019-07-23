using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    /// <summary> 指定在定义控件的大小和位置时要使用的控件边界
    /// </summary>
    [Flags]
    public enum DUIBoundsSpecified
    {
        // 摘要: 
        //     未定义任何边界。
        None = 0,
        //
        // 摘要: 
        //     该控件的左边缘已定义。
        X = 1,
        //
        // 摘要: 
        //     该控件的上边缘已定义。
        Y = 2,
        //
        // 摘要: 
        //     该控件的 X 和 Y 坐标都已定义。
        Location = 3,
        //
        // 摘要: 
        //     该控件的宽度已定义。
        Width = 4,
        //
        // 摘要: 
        //     该控件的高度已定义。
        Height = 8,
        //
        // 摘要: 
        //     该控件的 System.Windows.Forms.Control.Width 和 System.Windows.Forms.Control.Height
        //     属性值都已定义。
        Size = 12,
        Bounds = 15,
        CenterX = 16,
        CenterY = 32,
        Center = 48,
        Rotate = 64,
        SkewX = 128,
        SkewY = 256,
        Skew = 384,
        ScaleX = 512,
        ScaleY = 1024,
        Scale = 1536,
        //
        // 摘要: 
        //     System.Windows.Forms.Control.Location 和 System.Windows.Forms.Control.Size
        //     属性值都已定义。
        All = 2047,
    }
}
