using DirectUI.Collection;
using DirectUI.Common;
using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DirectUI.Controls.ScrollBar
{
    public abstract class DUIScrollBar : DUIControl
    {
        #region 事件
        /// <summary> 在通过鼠标或键盘操作移动滚动框后发生。
        /// </summary>
        public event DUIScrollEventHandler Scroll;
        /// <summary> 当通过 System.Windows.Forms.ScrollBar.Scroll 事件或以编程方式更改 System.Windows.Forms.ScrollBar.Value属性时发生。
        /// </summary>
        public event EventHandler ValueChanged;
        /// <summary> 引发 System.Windows.Forms.ScrollBar.Scroll 事件。
        /// </summary>
        /// <param name="se">一个 System.Windows.Forms.ScrollEventArgs，其中包含事件数据</param>
        protected virtual void OnScroll(DUIScrollEventArgs se)
        {
            if (Scroll != null)
            {
                Scroll(this, se);
            }
        }
        /// <summary> 引发 System.Windows.Forms.ScrollBar.ValueChanged 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.EventArgs。</param>
        protected virtual void OnValueChanged()
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, EventArgs.Empty);
            }
        }
        #endregion
        #region 变量
        protected bool display = false;
        private DUIBorder border = new DUIBorder(Color.Black, 0);
        private Color bodyColor = Color.Gray;
        private float minimum = 0;
        private float maximum = 100;
        private float smallChange = 1;
        private float largeChange = 10;
        private float value = 0;
        private ScrollOrientation scrollOrientation;
        private int wheelDelta = 10;
        private float thickness = 10;
        protected RectangleF displayRect = RectangleF.Empty;
        protected PointF mouseEffectScrollPoint = Point.Empty; //鼠标影响滚动条的点
        protected bool isMouseDownInScroll = false; //鼠标是否在滚动条处按下
        private DUIScrollableControl bindingScrollableControl = null;
        #endregion
        #region 属性
        public virtual Color BodyColor
        {
            get => this.bodyColor;
            set
            {
                this.bodyColor = value;
                this.Invalidate();
            }
        }

        public DUIScrollableControl BindingScrollableControl
        {
            get { return bindingScrollableControl; }
            set
            {
                if (bindingScrollableControl != value)
                {
                    bindingScrollableControl = value;
                    if (bindingScrollableControl != null)
                    {
                        bindingScrollableControl.ControlAdded -= ItemChanged;
                        bindingScrollableControl.ControlRemoved -= ItemChanged;
                        bindingScrollableControl.ControlBoundsChanged -= ItemChanged;
                        bindingScrollableControl.ControlBoundsChanging -= ItemChanging;
                        bindingScrollableControl.ControlAdded += ItemChanged;
                        bindingScrollableControl.ControlRemoved += ItemChanged;
                        bindingScrollableControl.ControlBoundsChanged += ItemChanged;
                        bindingScrollableControl.ControlBoundsChanging += ItemChanging;
                        bindingScrollableControl.Layout -= ItemLayout;
                        bindingScrollableControl.Layout += ItemLayout;
                        bindingScrollableControl.Scroll -= BindingScroll;
                        bindingScrollableControl.Scroll += BindingScroll;
                        bindingScrollableControl.DisplaySizeChanged -= DisplaySizeChanged;
                        bindingScrollableControl.DisplaySizeChanged += DisplaySizeChanged;
                        this.AdjustFormScrollbars();
                    }
                }
            }
        }

        private void DisplaySizeChanged(object sender, EventArgs e)
        {
            this.AdjustFormScrollbars();
        }

        void BindingScroll(object sender, DUIScrollEventArgs e)
        {
            BindingScrollValueChanged(e);
        }
        protected abstract void BindingScrollValueChanged(DUIScrollEventArgs e);
        void ItemLayout(object sender, DUILayoutEventArgs e)
        {
            this.AdjustFormScrollbars();
        }

        void ItemChanged(object sender, DUIControlEventArgs e)
        {
            this.AdjustFormScrollbars();
        }
        void ItemChanging(object sender, DUIControlBoundsChangingEventArgs e)
        {
            DUIScrollableControl dsc = sender as DUIScrollableControl;
            if (e.NewBounds.Right + dsc.RightAppend > dsc.DisplayRectangle.Width || e.NewBounds.Bottom + dsc.BottomAppend > dsc.DisplayRectangle.Height)
            {
                this.AdjustFormScrollbars();
            }
        }
        public override DUIBorder Border
        {
            get
            {
                return this.border;
            }
        }

        protected bool Display
        {
            get { return display; }
            set
            {
                display = value;
                this.Invalidate();
            }
        }

        public float Minimum
        {
            get { return minimum; }
            set { minimum = value; }
        }

        public float Maximum
        {
            get { return maximum; }
            set { maximum = value; }
        }

        public float SmallChange
        {
            get { return smallChange; }
            set { smallChange = value; }
        }

        public float LargeChange
        {
            get { return largeChange; }
            set { largeChange = value; }
        }

        public float Value
        {
            get { return this.value; }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                if (value > Math.Max(this.Maximum - this.LargeChange, 0))
                {
                    value = Math.Max(this.Maximum - this.LargeChange, 0);
                }
                if (this.value != value)
                {
                    this.value = value;
                    OnValueChanged();
                    this.Invalidate();
                }
            }
        }

        public ScrollOrientation ScrollOrientation
        {
            get { return scrollOrientation; }
            set { scrollOrientation = value; }
        }

        public int WheelDelta
        {
            get { return wheelDelta; }
            set { wheelDelta = value; }
        }
        public float Thickness
        {
            get { return thickness; }
            set { thickness = value; }
        }
        public abstract RectangleF BodyRectangle { get; }
        #endregion
        public abstract void AdjustFormScrollbars();
        public override void OnMouseWheel(DUIMouseEventArgs e)
        {
            float oldValue = this.Value;
            if (e.Delta > 0)
            {
                this.Value -= wheelDelta;
            }
            else
            {
                this.Value += wheelDelta;
            }
            DUIScrollEventArgs se = new DUIScrollEventArgs(ScrollEventType.SmallDecrement, oldValue, this.Value, ScrollOrientation.VerticalScroll);
            OnScroll(se);
            base.OnMouseWheel(e);
        }
        public override void OnMouseUp(DUIMouseEventArgs e)
        {
            this.mouseEffectScrollPoint = PointF.Empty;
            this.isMouseDownInScroll = false;
            base.OnMouseUp(e);
        }
        public override void OnPaintBackground(DUIPaintEventArgs e)
        {
            var backupSmoothingMode = e.Graphics.SmoothingMode;
            e.Graphics.SmoothingMode = DUISmoothingMode.AntiAlias;
            //base.OnPaintBackground(e);
            if (display)
            {
                using (DUISolidBrush backBrush = new DUISolidBrush(this.BackColor))
                {
                    e.Graphics.FillRectangle(backBrush, this.ClientRectangle);
                }
                using (DUISolidBrush bodyBrush = new DUISolidBrush(this.BodyColor))
                {
                    e.Graphics.FillRoundedRectangle(bodyBrush, this.BodyRectangle, 4);
                }
            }
            e.Graphics.SmoothingMode = backupSmoothingMode;
        }
    }
}
