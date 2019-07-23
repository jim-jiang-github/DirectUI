using DirectUI.Common;
using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DirectUI.Controls
{
    public class DUITrackBar : DUIControl
    {
        #region 事件
        public event EventHandler Scroll;
        public event EventHandler ValueChanged;
        public event EventHandler ValueChanging;
        protected virtual void OnScroll()
        {
            if (Scroll != null)
            {
                Scroll(this, EventArgs.Empty);
            }
        }
        protected virtual void OnValueChanging()
        {
            if (ValueChanging != null)
            {
                ValueChanging(this, EventArgs.Empty);
            }
        }
        protected virtual void OnValueChanged()
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, EventArgs.Empty);
            }
        }
        #endregion
        #region 变量
        private int maximum = 100;
        private int minimum = 0;
        private int value = 0;
        private SliderBody sliderBody = null;
        private Size sliderSize = new Size(14, 14);
        private bool leftToRight = true;
        #endregion
        #region 属性
        public virtual Size SliderSize
        {
            get { return sliderSize; }
            set
            {
                if (sliderSize != value)
                {
                    sliderSize = value;
                    this.Invalidate();
                }
            }
        }
        public int Maximum
        {
            get { return maximum; }
            set { maximum = value; }
        }

        public int Minimum
        {
            get { return minimum; }
            set { minimum = value; }
        }

        public int Value
        {
            get { return this.value; }
            set
            {
                if (value < Minimum)
                {
                    value = Minimum;
                }
                if (value > Maximum)
                {
                    value = Maximum;
                }
                if (this.value != value)
                {
                    this.value = value;
                    OnValueChanged();
                    this.sliderBody.LocationChanged -= sliderBody_LocationChanged;
                    if (this.LeftToRight)
                    {
                        this.sliderBody.X = ((this.value - this.Minimum) * ((float)(this.ClientSize.Width - SliderSize.Width) / (float)(this.Maximum - this.Minimum)));
                    }
                    else
                    {
                        this.sliderBody.Y = ((this.value - this.Minimum) * ((float)(this.ClientSize.Height - SliderSize.Height) / (float)(this.Maximum - this.Minimum)));
                    }
                    this.sliderBody.LocationChanged += sliderBody_LocationChanged;
                }
            }
        }
        public bool LeftToRight
        {
            get { return this.leftToRight; }
            set
            {
                this.leftToRight = value;
                this.Invalidate();
            }
        }
        public override float Width
        {
            get
            {
                return LeftToRight ? base.Width : 16;
            }
            set
            {
                base.Width = value;
            }
        }
        public override float Height
        {
            get
            {
                return LeftToRight ? 16 : base.Height;
            }
            set
            {
                base.Height = value;
            }
        }
        #endregion
        public DUITrackBar()
        {
            this.sliderBody = new SliderBody(this);
            this.DUIControls.Add(sliderBody);
            this.BackColor = Color.AliceBlue;
            this.sliderBody.LocationChanging += sliderBody_LocationChanging;
            this.sliderBody.LocationChanged += sliderBody_LocationChanged;
        }

        void sliderBody_LocationChanging(object sender, DUILocationChangingEventArgs e)
        {
            OnScroll();
            int value = ConvertTools.ChineseRounding(this.LeftToRight ? (this.sliderBody.X / this.sliderBody.ChangeStepX) : (this.sliderBody.Y / this.sliderBody.ChangeStepY));
            if (this.value != value)
            {
                this.value = value + this.Minimum;
                OnValueChanging();
            }
        }

        void sliderBody_LocationChanged(object sender, EventArgs e)
        {
            OnValueChanged();
        }
        public override void OnMouseDown(DUIMouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (this.LeftToRight)
            {
                float x = e.X - this.sliderBody.Width / 2;
                this.sliderBody.X = x - x % this.sliderBody.ChangeStepX;
            }
            else
            {
                float y = e.Y - this.sliderBody.Height / 2;
                this.sliderBody.Y = y - y % this.sliderBody.ChangeStepY;
            }
            this.sliderBody.PerformMouseDown(new DUIMouseEventArgs(e.Button, e.Clicks, new PointF(this.sliderBody.Width / 2, this.sliderBody.Height / 2), e.Delta));
            this.PerformMouseUp(new DUIMouseEventArgs(e.Button, e.Clicks, e.Location, e.Delta));
        }
        public override void OnMouseWheel(DUIMouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (e.Delta < 0)
            {
                if (this.LeftToRight)
                {
                    if (this.sliderBody.X - this.sliderBody.ChangeStepX < this.sliderBody.LeftMargin)
                    {
                        this.sliderBody.X = (float)this.sliderBody.LeftMargin;
                    }
                    else
                    {
                        this.sliderBody.X -= this.sliderBody.ChangeStepX;
                    }
                }
                else
                {
                    if (this.sliderBody.Bottom + this.sliderBody.ChangeStepY > this.sliderBody.BottomMargin)
                    {
                        this.sliderBody.Y = (float)this.sliderBody.BottomMargin - this.sliderBody.Height;
                    }
                    else
                    {
                        this.sliderBody.Y += this.sliderBody.ChangeStepY;
                    }
                }
            }
            else
            {
                if (this.LeftToRight)
                {
                    if (this.sliderBody.Right + this.sliderBody.ChangeStepX > this.sliderBody.RightMargin)
                    {
                        this.sliderBody.X = (float)this.sliderBody.RightMargin - this.sliderBody.Width;
                    }
                    else
                    {
                        this.sliderBody.X += this.sliderBody.ChangeStepX;
                    }
                }
                else
                {
                    if (this.sliderBody.Y - this.sliderBody.ChangeStepY < this.sliderBody.TopMargin)
                    {
                        this.sliderBody.Y = (float)this.sliderBody.TopMargin;
                    }
                    else
                    {
                        this.sliderBody.Y -= this.sliderBody.ChangeStepY;
                    }
                }
            }
        }
        public override void OnResizing(DUISizeChangingEventArgs e)
        {
            base.OnResizing(e);
            float width = e.NewSize.Width - e.OldSize.Width;
        }
        #region 重写
        public override void OnPaint(DUIPaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = DUISmoothingMode.AntiAlias;
            //if (!this.LeftToRight)
            //{
            //    e.Graphics.RotateTransform(-90);
            //}
            using (DUIPen pen1 = new DUIPen(Color.FromArgb(50, 50, 50)))
            using (DUIPen pen2 = new DUIPen(Color.FromArgb(150, 150, 150)))
            using (DUIPen pen3 = new DUIPen(Color.FromArgb(222, 222, 222)))
            {
                if (this.LeftToRight)
                {
                    e.Graphics.DrawLine(pen1, new PointF(SliderSize.Width / 2, SliderSize.Width / 2 - 1), new PointF(this.ClientSize.Width - SliderSize.Width / 2, SliderSize.Width / 2 - 1));
                    e.Graphics.DrawLine(pen2, new PointF(SliderSize.Width / 2, SliderSize.Width / 2 + 0), new PointF(this.ClientSize.Width - SliderSize.Width / 2, SliderSize.Width / 2 + 0));
                    e.Graphics.DrawLine(pen3, new PointF(SliderSize.Width / 2, SliderSize.Width / 2 + 1), new PointF(this.ClientSize.Width - SliderSize.Width / 2, SliderSize.Width / 2 + 1));
                }
                else
                {
                    e.Graphics.DrawLine(pen1, new PointF(SliderSize.Height / 2 - 1, SliderSize.Height / 2), new PointF(SliderSize.Height / 2 - 1, this.ClientSize.Height - SliderSize.Height / 2));
                    e.Graphics.DrawLine(pen2, new PointF(SliderSize.Height / 2 + 0, SliderSize.Height / 2), new PointF(SliderSize.Height / 2 + 0, this.ClientSize.Height - SliderSize.Height / 2));
                    e.Graphics.DrawLine(pen3, new PointF(SliderSize.Height / 2 + 1, SliderSize.Height / 2), new PointF(SliderSize.Height / 2 + 1, this.ClientSize.Height - SliderSize.Height / 2));
                }
            }
            using (DUIPen pen = new DUIPen(this.ForeColor))
            {
                int tickCount = this.Maximum - this.Minimum + 1;
                if (this.LeftToRight)
                {
                    for (int i = 0; i <= tickCount; i++)
                    {
                        e.Graphics.DrawLine(pen, new PointF((float)SliderSize.Width / 2F + i * ((float)(this.ClientSize.Width - SliderSize.Width) / (float)(tickCount - 1)), SliderSize.Width + 2), new PointF((float)SliderSize.Width / 2 + i * ((float)(this.ClientSize.Width - SliderSize.Width) / (float)(tickCount - 1)), SliderSize.Width + 2 + 5));
                    }
                }
                else
                {
                    for (int i = 0; i <= tickCount; i++)
                    {
                        e.Graphics.DrawLine(pen, new PointF(SliderSize.Height + 2, (float)SliderSize.Height / 2F + i * ((float)(this.ClientSize.Height - SliderSize.Height) / (float)(tickCount - 1))), new PointF(SliderSize.Height + 2 + 5, (float)SliderSize.Height / 2 + i * ((float)(this.ClientSize.Height - SliderSize.Height) / (float)(tickCount - 1))));
                    }
                }
            }
        }
        public virtual void DrawSliderBody(DUIPaintEventArgs e)
        {
            e.Graphics.SmoothingMode = DUISmoothingMode.AntiAlias;
            if (DUIControl.MouseButtons != System.Windows.Forms.MouseButtons.None && this.sliderBody.isMouseDown)
            {
                e.Graphics.FillEllipse(DUIBrushes.PowderBlue, new RectangleF(0, 0, this.sliderBody.Width, this.sliderBody.Height));
            }
            else
            {
                e.Graphics.FillEllipse(DUIBrushes.White, new RectangleF(0, 0, this.sliderBody.Width, this.sliderBody.Height));
            }
            e.Graphics.DrawEllipse(DUIPens.Black, new RectangleF(0, 0, this.sliderBody.Width, this.sliderBody.Height));
        }
        #endregion
        public class SliderBody : DUIEditableControl
        {
            private DUITrackBar owner = null;
            internal bool isMouseDown = false;
            public override float ChangeStepX { get => ((this.owner.Width - this.Width) / (this.owner.Maximum - this.owner.Minimum)); }
            public override float ChangeStepY { get => ((this.owner.Height - this.Height) / (this.owner.Maximum - this.owner.Minimum)); }
            public override bool CanFocus { get => false; }
            public override Color BackColor
            {
                get
                {
                    return Color.Transparent;
                }
            }
            public override float X { get => this.owner == null ? base.X : (this.owner.LeftToRight ? base.X : 0); set => base.X = value; }
            public override float Y { get => this.owner == null ? base.X : (this.owner.LeftToRight ? 0 : base.Y); set => base.Y = value; }
            public override float? LeftMargin { get => this.owner.LeftToRight ? 0 : base.LeftMargin; set => base.LeftMargin = value; }
            public override float? RightMargin { get => this.owner.LeftToRight ? this.owner.Width : base.RightMargin; set => base.RightMargin = value; }
            public override float? TopMargin { get => !this.owner.LeftToRight ? 0 : base.TopMargin; set => base.TopMargin = value; }
            public override float? BottomMargin { get => !this.owner.LeftToRight ? this.owner.Height : base.BottomMargin; set => base.BottomMargin = value; }
            public override float Width
            {
                get
                {
                    if (this.owner == null) { return 0; }
                    return this.owner.SliderSize.Width;
                }
            }
            public override float Height
            {
                get
                {
                    if (this.owner == null) { return 0; }
                    return this.owner.SliderSize.Height;
                }
            }
            public override DUIBorder Border
            {
                get
                {
                    return new DUIBorder(Color.Transparent, 0);
                }
            }
            public SliderBody(DUITrackBar owner)
            {
                this.owner = owner;
            }
            public override EffectMargin CanEffectMargin
            {
                get
                {
                    return EffectMargin.Other;
                }
            }
            public override System.Windows.Forms.Cursor GetCursor(System.Drawing.PointF mouseLocation)
            {
                return Cursors.Default;
            }
            protected override void DrawBounds(DUIPaintEventArgs e)
            {
                //base.DrawBounds(e);
            }
            protected override void DrawVertex(DUIPaintEventArgs e)
            {
                //base.DrawVertex(e);
            }
            public override void OnMouseDown(DUIMouseEventArgs e)
            {
                base.OnMouseDown(e);
                this.isMouseDown = true;
                this.Invalidate();
            }
            public override void OnMouseUp(DUIMouseEventArgs e)
            {
                base.OnMouseUp(e);
                this.isMouseDown = false;
                this.Invalidate();
            }
            public override void OnPaint(DUIPaintEventArgs e)
            {
                this.owner.DrawSliderBody(e);
            }
        }
    }
}
