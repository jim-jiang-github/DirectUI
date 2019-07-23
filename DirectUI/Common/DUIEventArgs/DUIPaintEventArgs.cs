using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectUI.Common
{
    public class DUIPaintEventArgs
    {
        private DUIGraphics graphics = null;
        private RectangleF clipRect = RectangleF.Empty;

        // 摘要: 
        //     获取要在其中进行绘画的矩形。
        //
        // 返回结果: 
        //     要在其中进行绘制的 System.Drawing.Rectangle。
        public RectangleF ClipRectangle { get { return clipRect; } }
        //
        // 摘要: 
        //     获取用于进行绘制的图形。
        //
        // 返回结果: 
        //     用于绘画的 D2DGraphics 对象。 D2DGraphics 对象提供将对象绘制到显示设备上的方法。
        public DUIGraphics Graphics { get { return graphics; } }
        // 摘要: 
        //     用指定的图形和剪辑矩形框来初始化 System.Windows.Forms.PaintEventArgs 类的新实例。
        //
        // 参数: 
        //   graphics:
        //     用于绘制该项的 D2DGraphics。
        //
        //   clipRect:
        //     表示绘画所在的矩形的 System.Drawing.Rectangle。
        public DUIPaintEventArgs(DUIGraphics graphics, RectangleF clipRect)
        {
            this.graphics = graphics;
            this.clipRect = clipRect;
        }
    }
}
