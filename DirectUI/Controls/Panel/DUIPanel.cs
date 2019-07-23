using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Controls
{
    public class DUIPanel : DUIControl
    {
        private Pen boundPen = new Pen(Brushes.Black, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot };
    }
}
