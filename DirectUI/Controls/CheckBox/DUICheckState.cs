using DirectUI.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Controls
{
    /// <summary> CheckBox的选择状态
    /// </summary>
    public abstract class DUICheckState
    {
        public virtual float X { get; set; }
        public virtual float Y { get; set; }
        public virtual float Width { get; set; }
        public virtual float Height { get; set; }
        public virtual PointF Location { get { return new PointF(X, Y); } }
        public virtual SizeF Size { get { return new SizeF(Width, Height); } }
        public virtual RectangleF Bounds { get { return new RectangleF(Location, Size); } }

        /// <summary> 选中的状态名
        /// </summary>
        public abstract string CheckStateName { get; }
        public abstract void DrawCheckState(DUIPaintEventArgs e);

    }
}
