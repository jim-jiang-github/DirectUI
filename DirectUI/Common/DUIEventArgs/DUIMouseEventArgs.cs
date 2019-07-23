using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DirectUI.Common
{
    public class DUIMouseEventArgs
    {
        private MouseButtons button;
        private int clicks;
        private int delta;
        private float x;
        private float y;
        /// <summary> 获取曾按下的是哪个鼠标按钮
        /// </summary>
        public MouseButtons Button
        {
            get { return button; }
        }
        /// <summary> 获取按下并释放鼠标按钮的次数
        /// </summary>
        public int Clicks
        {
            get { return clicks; }
        }
        /// <summary> 获取鼠标轮已转动的制动器数的有符号计数乘以 WHEEL_DELTA 常数。 制动器是鼠标轮的一个凹口。
        /// </summary>
        public int Delta
        {
            get { return delta; }
        }
        /// <summary> 获取鼠标在产生鼠标事件时的位置
        /// </summary>
        public PointF Location
        {
            get { return new PointF(x, y); }
        }
        /// <summary> 获取鼠标在产生鼠标事件时的 x 坐标
        /// </summary>
        public float X
        {
            get { return x; }
        }
        /// <summary> 获取鼠标在产生鼠标事件时的 y 坐标
        /// </summary>
        public float Y
        {
            get { return y; }
        }

        /// <summary> 初始化 DUIMouseEventArgs 类的新实例
        /// </summary>
        /// <param name="button">System.Windows.Forms.MouseButtons 值之一，指示按下的鼠标按钮</param>
        /// <param name="clicks">鼠标按钮曾被按下的次数</param>
        /// <param name="x">鼠标单击的 x 坐标（以像素为单位）。</param>
        /// <param name="y">鼠标单击的 y 坐标（以像素为单位）。</param>
        /// <param name="delta">鼠标轮已转动的制动器数的有符号计数</param>
        public DUIMouseEventArgs(MouseButtons button, int clicks, float x, float y, int delta)
        {
            this.button = button;
            this.clicks = clicks;
            this.x = x;
            this.y = y;
            this.delta = delta;
        }
        /// <summary> 初始化 DUIMouseEventArgs 类的新实例
        /// </summary>
        /// <param name="button">System.Windows.Forms.MouseButtons 值之一，指示按下的鼠标按钮</param>
        /// <param name="clicks">鼠标按钮曾被按下的次数</param>
        /// <param name="p">鼠标单击的 p 坐标（以像素为单位）。</param>
        /// <param name="delta">鼠标轮已转动的制动器数的有符号计数</param>
        public DUIMouseEventArgs(MouseButtons button, int clicks, PointF p, int delta)
        {
            this.button = button;
            this.clicks = clicks;
            this.x = p.X;
            this.y = p.Y;
            this.delta = delta;
        }
    }
}
