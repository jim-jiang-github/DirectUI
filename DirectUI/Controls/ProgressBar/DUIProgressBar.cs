using DirectUI.Common;
using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;

namespace DirectUI.Controls
{
    public class DUIProgressBar : DUIControl
    {
        #region 事件
        public event EventHandler ValueChanged;
        protected virtual void OnValueChanged()
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, EventArgs.Empty);
            }
        }
        #endregion
        private static Timer timer = null;
        static DUIProgressBar()
        {
            DUIProgressBar.timer = new Timer() { Interval = 20 };
            DUIProgressBar.timer.Start();
        }
        private float minimum = 0;
        private float maximum = 100;
        private float value = 0;
        private DUIBorder border = new DUIBorder(Color.Black, 1);
        private Color progressBarColor = Color.LimeGreen;
        private bool isUnknowValue = false;
        public float Minimum
        {
            get { return minimum; }
            set
            {
                if (value >= Maximum)
                {
                    value = Maximum;
                }
                minimum = value;
                Invalidate();
            }
        }
        public float Maximum
        {
            get { return maximum; }
            set
            {
                if (value <= Minimum)
                {
                    value = Minimum;
                }
                maximum = value;
                Invalidate();
            }
        }
        public float Value
        {
            get { return this.value; }
            set
            {
                if (value <= Minimum)
                {
                    value = Minimum;
                }
                if (value >= Maximum)
                {
                    value = Maximum;
                }
                if (this.value != value)
                {
                    this.value = value;
                    OnValueChanged();
                    Invalidate();
                }
            }
        }
        /// <summary> 是否无法准确的获得Value值，如果无法获得Value值，则用一个无线循环的动画来表示进度
        /// </summary>
        public bool IsUnknowValue
        {
            get { return isUnknowValue; }
            set { isUnknowValue = value; }
        }
        public override DUIBorder Border
        {
            get
            {
                return this.border;
            }
        }
        public Color ProgressBarColor
        {
            get { return progressBarColor; }
            set { progressBarColor = value; }
        }
        private PointF animateSliderLocation = PointF.Empty;
        /// <summary> 当Value无法得知的情况下动画所需要的滑块
        /// </summary>
        private RectangleF AnimateSlider
        {
            get
            {
                return new RectangleF(animateSliderLocation, new SizeF(this.Width / 2, this.Height));
            }
        }
        public DUIProgressBar()
        {
            this.Height = 18;
            this.Width = 100;
            this.BackColor = Color.Transparent;
            int direction = 0; //滑块运动的方向0是右1是左
            DUIProgressBar.timer.Elapsed += (s, e) =>
            {
                if (IsUnknowValue)
                {
                    if (animateSliderLocation.X <= 0 || direction == 0)
                    {
                        direction = 0;
                        animateSliderLocation = new PointF(animateSliderLocation.X + 5, animateSliderLocation.Y);
                    }
                    if (animateSliderLocation.X + AnimateSlider.Width > this.Width || direction == 1)
                    {
                        direction = 1;
                        animateSliderLocation = new PointF(animateSliderLocation.X - 5, animateSliderLocation.Y);
                    }
                    this.Invalidate();
                }
            };
        }
        public override void OnPaintBackground(DUIPaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (this.IsUnknowValue)
            {
                using (DUISolidBrush brush = new DUISolidBrush(this.ProgressBarColor))
                {
                    e.Graphics.FillRectangle(brush, AnimateSlider);
                }
            }
            else
            {
                if (this.Maximum == 0)
                {
                    return;
                }
                using (DUISolidBrush brush = new DUISolidBrush(this.ProgressBarColor))
                {
                    float v = this.Width * this.Value / this.Maximum;
                    e.Graphics.FillRectangle(brush, new RectangleF(0, 0, v, this.Height));
                }
            }
        }
    }
}
