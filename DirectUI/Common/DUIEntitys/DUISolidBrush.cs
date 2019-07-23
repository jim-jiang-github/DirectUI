using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUISolidBrush : DUIBrush
    {
        // 摘要: 
        //     初始化指定颜色的新 System.Drawing.SolidBrush 对象。
        //
        // 参数: 
        //   color:
        //     一个 System.Drawing.Color 结构，它表示此画笔的颜色。
        public DUISolidBrush(Color color)
        {
            this.brush = new SolidBrush(color);
        }
        public Color Color
        {
            get { return (this.brush as SolidBrush).Color; }
            set
            {
                (this.brush as SolidBrush).Color = value;
                //if (this.dxBrush != null)
                //{
                //    var c = DxConvert.ToColor4(value);
                //    SharpDX.Direct2D1.SolidColorBrush b = this.dxBrush as SharpDX.Direct2D1.SolidColorBrush;
                //    b.Color = c;
                //    //if (this.dxBrush != null && b.Color != c)
                //    //{
                //    //    b.Color = c;
                //    //}
                //}
            }
        }
    }
}
