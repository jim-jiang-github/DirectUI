using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    /// <summary> 用来描述控件的边框
    /// </summary>
    public class DUIBorder
    {
        private Color borderColor = Color.Black;
        private float borderWidth = 0;
        /// <summary> 边框颜色
        /// </summary>
        public virtual Color BorderColor
        {
            get { return borderColor; }
        }
        /// <summary> 边框宽度
        /// </summary>
        public virtual float BorderWidth
        {
            get { return borderWidth; }
        }
        public DUIBorder()
        {
        }
        public DUIBorder(Color borderColor, float borderWidth)
        {
            this.borderColor = borderColor;
            this.borderWidth = borderWidth;
        }
    }
}
