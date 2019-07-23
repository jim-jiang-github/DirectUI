using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    /// <summary> 提供用于测量和呈现文本的方法。无法继承此类。
    /// </summary>
    public sealed class DUITextRenderer
    {
        /// <summary> 在使用指定字体绘制时，提供指定文本的尺寸（以像素为单位）。
        /// </summary>
        /// <param name="text">要测量的文本。</param>
        /// <param name="font">要应用于已测量文本的 DUIFont。</param>
        /// <returns></returns>
        public static SizeF MeasureText(string text, DUIFont font)
        {
            using (SharpDX.DirectWrite.TextLayout tl = DxConvert.ToTextLayout(font, text))
            {
                float w = tl.Metrics.WidthIncludingTrailingWhitespace;
                float h = tl.Metrics.Height;
                return new SizeF(w, h);
            }
        }
    }
}
