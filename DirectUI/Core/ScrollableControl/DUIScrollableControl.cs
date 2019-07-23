using DirectUI.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace DirectUI.Core
{
    public delegate void DUIScrollEventHandler(object sender, DUIScrollEventArgs e);
    public class DUIScrollableControl : DUIControl
    {
        #region 事件
        public event DUIScrollEventHandler Scroll;
        public event EventHandler DisplayRectangleChanged;
        public event EventHandler DisplayLocationChanged;
        public event EventHandler DisplaySizeChanged;
        public event EventHandler ClientSizeChanged;
        protected virtual void OnScroll(DUIScrollEventArgs se)
        {
            if (Scroll != null)
            {
                Scroll(this, se);
            }
        }
        protected virtual void OnDisplayRectangleChanged()
        {
            if (DisplayRectangleChanged != null)
            {
                DisplayRectangleChanged(this, EventArgs.Empty);
            }
        }
        protected virtual void OnDisplayLocationChanged()
        {
            if (DisplayLocationChanged != null)
            {
                DisplayLocationChanged(this, EventArgs.Empty);
            }
        }
        protected virtual void OnDisplaySizeChanged()
        {
            if (DisplaySizeChanged != null)
            {
                DisplaySizeChanged(this, EventArgs.Empty);
            }
        }
        protected virtual void OnClientSizeChanged()
        {
            if (ClientSizeChanged != null)
            {
                ClientSizeChanged(this, EventArgs.Empty);
            }
        }
        #endregion
        #region 变量
        private const int interval = 100;
        private LoopTool loop = new LoopTool(interval);
        protected internal RectangleF displayRect = RectangleF.Empty;
        private bool autoScroll = true;
        private bool outOfRangeAutoScroll = true;
        private bool hScroll = false;
        private bool vScroll = false;
        private DUIHScrollProperties horizontalScroll = null;
        private DUIVScrollProperties verticalScroll = null;
        private PointF mouseEffectScrollPoint = PointF.Empty; //鼠标影响滚动条的点
        private bool isMouseDownInHScroll = false; //鼠标是否在滚动条处按下
        private bool isMouseDownInVScroll = false; //鼠标是否在滚动条处按下
        private bool isMouseEnter = false;
        private DUIMouseEventArgs mouseMoveEventArgs = new DUIMouseEventArgs(System.Windows.Forms.MouseButtons.None, 0, Point.Empty, 0); //用于记录MouseMove的参数
        private float rightAppend = 0;
        private float bottomAppend = 0;
        private float minDisplayWidth = 0;
        private float minDisplayHeight = 0;
        #endregion
        #region 属性
        public DUIMouseEventArgs MouseMoveEventArgs { get => mouseMoveEventArgs; set => mouseMoveEventArgs = value; }
        public DUIHScrollProperties HorizontalScroll
        {
            get
            {
                if (horizontalScroll == null)
                {
                    horizontalScroll = new DUIHScrollProperties(this);
                }
                return horizontalScroll;
            }
        }

        public DUIVScrollProperties VerticalScroll
        {
            get
            {
                if (verticalScroll == null)
                {
                    verticalScroll = new DUIVScrollProperties(this);
                }
                return verticalScroll;
            }
        }
        public bool AutoScroll
        {
            get { return autoScroll; }
            set { autoScroll = value; }
        }
        /// <summary> 鼠标超出范围的时候是否会自动滚动
        /// </summary>
        public virtual bool OutOfRangeAutoScroll
        {
            get { return outOfRangeAutoScroll; }
            set { outOfRangeAutoScroll = value; }
        }
        protected virtual internal bool HScroll
        {
            get { return hScroll; }
            set
            {
                if (hScroll != value)
                {
                    hScroll = value;
                    this.Invalidate();
                }
            }
        }
        protected virtual internal bool VScroll
        {
            get { return vScroll; }
            set
            {
                if (vScroll != value)
                {
                    vScroll = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary> 横向滚动条的高度
        /// </summary>
        public float HThickness
        {
            get { return this.HorizontalScroll.Thickness; }
            set { this.HorizontalScroll.Thickness = value; }
        }
        /// <summary> 纵向滚动条的宽度
        /// </summary>
        public float VThickness
        {
            get { return this.VerticalScroll.Thickness; }
            set { this.VerticalScroll.Thickness = value; }
        }
        /// <summary> 滚动容器最右的控件追加的距离
        /// </summary>
        public virtual float RightAppend
        {
            get { return rightAppend; }
            set
            {
                if (rightAppend != value)
                {
                    rightAppend = value;
                    AdjustFormScrollbars();
                }
            }
        }
        /// <summary> 滚动容器最底的控件追加的距离
        /// </summary>
        public virtual float BottomAppend
        {
            get { return bottomAppend; }
            set
            {
                if (bottomAppend != value)
                {
                    bottomAppend = value;
                    AdjustFormScrollbars();
                }
            }
        }
        /// <summary> 最小显示宽度
        /// </summary>
        public virtual float MinDisplayWidth
        {
            get { return minDisplayWidth; }
            set
            {
                if (minDisplayWidth != value)
                {
                    minDisplayWidth = value;
                    AdjustFormScrollbars();
                    OnDisplaySizeChanged();
                }
            }
        }
        /// <summary> 最小显示高度
        /// </summary>
        public virtual float MinDisplayHeight
        {
            get { return minDisplayHeight; }
            set
            {
                if (minDisplayHeight != value)
                {
                    minDisplayHeight = value;
                    AdjustFormScrollbars();
                    OnDisplaySizeChanged();
                }
            }
        }
        #endregion
        public DUIScrollableControl()
        {
            loop.LoopTick += (s, index) =>
            {
                lock (this.mouseMoveEventArgs)
                {
                    if (this.Visible)
                    {
                        if (DUIControl.MouseButtons == System.Windows.Forms.MouseButtons.Left && this.mouseMoveEventArgs.Button == System.Windows.Forms.MouseButtons.Left && this.DisplayRectangle.Width > this.ClientSize.Width && this.mouseMoveEventArgs.X < -this.DisplayRectangle.X)
                        {
                            float offset = this.ScrollHOffset((this.mouseMoveEventArgs.X + this.DisplayRectangle.X) / (100 / interval), true);
                            offset = Math.Min(-20, offset);
                            this.mouseMoveEventArgs = new DUIMouseEventArgs(this.mouseMoveEventArgs.Button, this.mouseMoveEventArgs.Clicks, new PointF(this.mouseMoveEventArgs.X + offset, this.mouseMoveEventArgs.Y), this.mouseMoveEventArgs.Delta);
                            this.SynchronizationContextSend(() =>
                            {
                                this.ScrollMouseMove(this.mouseMoveEventArgs);
                            });
                        }
                        if (DUIControl.MouseButtons == System.Windows.Forms.MouseButtons.Left && this.mouseMoveEventArgs.Button == System.Windows.Forms.MouseButtons.Left && this.DisplayRectangle.Height > this.ClientSize.Height && this.mouseMoveEventArgs.Y < -this.DisplayRectangle.Y)
                        {
                            float offset = this.ScrollVOffset((this.mouseMoveEventArgs.Y + this.DisplayRectangle.Y) / (100 / interval), true);
                            offset = Math.Min(-20, offset);
                            this.mouseMoveEventArgs = new DUIMouseEventArgs(this.mouseMoveEventArgs.Button, this.mouseMoveEventArgs.Clicks, new PointF(this.mouseMoveEventArgs.X, this.mouseMoveEventArgs.Y + offset), this.mouseMoveEventArgs.Delta);
                            this.SynchronizationContextSend(() =>
                            {
                                this.ScrollMouseMove(this.mouseMoveEventArgs);
                            });
                        }
                        if (DUIControl.MouseButtons == System.Windows.Forms.MouseButtons.Left && this.mouseMoveEventArgs.Button == System.Windows.Forms.MouseButtons.Left && this.DisplayRectangle.Width > this.ClientSize.Width && this.mouseMoveEventArgs.X > (this.ClientSize.Width - this.DisplayRectangle.X))
                        {
                            float offset = this.ScrollHOffset((this.mouseMoveEventArgs.X - (this.ClientSize.Width - this.DisplayRectangle.X)) / (100F / interval), true);
                            offset = Math.Max(20, offset);
                            this.mouseMoveEventArgs = new DUIMouseEventArgs(this.mouseMoveEventArgs.Button, this.mouseMoveEventArgs.Clicks, new PointF(this.mouseMoveEventArgs.X + offset, this.mouseMoveEventArgs.Y), this.mouseMoveEventArgs.Delta);
                            this.SynchronizationContextSend(() =>
                            {
                                this.ScrollMouseMove(this.mouseMoveEventArgs);
                            });
                        }
                        if (DUIControl.MouseButtons == System.Windows.Forms.MouseButtons.Left && this.mouseMoveEventArgs.Button == System.Windows.Forms.MouseButtons.Left && this.DisplayRectangle.Height > this.ClientSize.Height && this.mouseMoveEventArgs.Y > (this.ClientSize.Height - this.DisplayRectangle.Y))
                        {
                            float offset = this.ScrollVOffset((this.mouseMoveEventArgs.Y - (this.ClientSize.Height - this.DisplayRectangle.Y)) / (100 / interval), true);
                            offset = Math.Max(20, offset);
                            this.mouseMoveEventArgs = new DUIMouseEventArgs(this.mouseMoveEventArgs.Button, this.mouseMoveEventArgs.Clicks, new PointF(this.mouseMoveEventArgs.X, this.mouseMoveEventArgs.Y + offset), this.mouseMoveEventArgs.Delta);
                            this.SynchronizationContextSend(() =>
                            {
                                this.ScrollMouseMove(this.mouseMoveEventArgs);
                            });
                        }
                    }
                }
            };
        }
        #region 重写
        public override SizeF ClientSize
        {
            get
            {
                return new SizeF(
                    base.ClientSize.Width - ((VScroll && this.VerticalScroll.AutoVisible) ? this.VerticalScroll.Thickness : 0),
                    base.ClientSize.Height - ((HScroll && this.HorizontalScroll.AutoVisible) ? this.HorizontalScroll.Thickness : 0));
            }
        }
        public override RectangleF DisplayRectangle
        {
            get
            {
                RectangleF rect = base.ClientRectangle;
                if (!displayRect.IsEmpty)
                {
                    rect.X = displayRect.X;
                    rect.Y = displayRect.Y;
                    if (HScroll)
                    {
                        rect.Width = displayRect.Width;
                    }
                    if (VScroll)
                    {
                        rect.Height = displayRect.Height;
                    }
                }
                return rect;
            }
        }
        protected override void AlwaysMouseDown(DUIMouseEventArgs e)
        {
            base.AlwaysMouseDown(e);
            this.AutoScrollStart();
        }
        protected override void AlwaysMouseMove(DUIMouseEventArgs e)
        {
            this.MouseMoveEventArgs = e;
            base.AlwaysMouseMove(e);
        }
        protected override void AlwaysMouseUp(DUIMouseEventArgs e)
        {
            base.AlwaysMouseUp(e);
            if (this.MouseMoveEventArgs != null)
            {
                this.MouseMoveEventArgs = new DUIMouseEventArgs(System.Windows.Forms.MouseButtons.None, this.MouseMoveEventArgs.Clicks, this.MouseMoveEventArgs.Location, this.MouseMoveEventArgs.Delta);
            }
            this.AutoScrollStop();
        }
        public override void OnControlAdded(DUIControlEventArgs e)
        {
            base.OnControlAdded(e);
            RectangleF oldDisplayRectangle = this.DisplayRectangle;
            AdjustFormScrollbars();
            if (oldDisplayRectangle.X != displayRect.X)
            {
                DUIScrollEventArgs se = new DUIScrollEventArgs(System.Windows.Forms.ScrollEventType.SmallDecrement, -oldDisplayRectangle.X, -displayRect.X, System.Windows.Forms.ScrollOrientation.HorizontalScroll);
                OnScroll(se);
            }
            if (oldDisplayRectangle.Y != displayRect.Y)
            {
                DUIScrollEventArgs se = new DUIScrollEventArgs(System.Windows.Forms.ScrollEventType.SmallDecrement, -oldDisplayRectangle.Y, -displayRect.Y, System.Windows.Forms.ScrollOrientation.HorizontalScroll);
                OnScroll(se);
            }
        }
        public override void OnControlRemoved(DUIControlEventArgs e)
        {
            base.OnControlRemoved(e);
            RectangleF oldDisplayRectangle = this.DisplayRectangle;
            AdjustFormScrollbars();
            if (oldDisplayRectangle.X != displayRect.X)
            {
                DUIScrollEventArgs se = new DUIScrollEventArgs(System.Windows.Forms.ScrollEventType.SmallDecrement, -oldDisplayRectangle.X, -displayRect.X, System.Windows.Forms.ScrollOrientation.HorizontalScroll);
                OnScroll(se);
            }
            if (oldDisplayRectangle.Y != displayRect.Y)
            {
                DUIScrollEventArgs se = new DUIScrollEventArgs(System.Windows.Forms.ScrollEventType.SmallDecrement, -oldDisplayRectangle.Y, -displayRect.Y, System.Windows.Forms.ScrollOrientation.HorizontalScroll);
                OnScroll(se);
            }
        }
        public override void OnControlBoundsChanged(DUIControlEventArgs e)
        {
            base.OnControlBoundsChanged(e);
            RectangleF oldDisplayRectangle = this.DisplayRectangle;
            AdjustFormScrollbars();
            //if (oldDisplayRectangle.X != displayRect.X)
            //{
            //    DUIScrollEventArgs se = new DUIScrollEventArgs(System.Windows.Forms.ScrollEventType.SmallDecrement, -oldDisplayRectangle.X, -displayRect.X, System.Windows.Forms.ScrollOrientation.HorizontalScroll);
            //    OnScroll(se);
            //}
            //if (oldDisplayRectangle.Y != displayRect.Y)
            //{
            //    DUIScrollEventArgs se = new DUIScrollEventArgs(System.Windows.Forms.ScrollEventType.SmallDecrement, -oldDisplayRectangle.Y, -displayRect.Y, System.Windows.Forms.ScrollOrientation.HorizontalScroll);
            //    OnScroll(se);
            //} 
            SyncScrollbars(true);
        }
        public override void OnControlBoundsChanging(DUIControlBoundsChangingEventArgs e)
        {
            base.OnControlBoundsChanging(e);
            if (e.NewBounds.Right + this.RightAppend > this.DisplayRectangle.Width || e.NewBounds.Bottom + this.BottomAppend > this.DisplayRectangle.Height)
            {
                RectangleF oldDisplayRectangle = this.DisplayRectangle;
                AdjustFormScrollbars();
                SyncScrollbars(true);
                if (oldDisplayRectangle.X != displayRect.X)
                {
                    DUIScrollEventArgs se = new DUIScrollEventArgs(System.Windows.Forms.ScrollEventType.SmallDecrement, -oldDisplayRectangle.X, -displayRect.X, System.Windows.Forms.ScrollOrientation.HorizontalScroll);
                    OnScroll(se);
                }
                if (oldDisplayRectangle.Y != displayRect.Y)
                {
                    DUIScrollEventArgs se = new DUIScrollEventArgs(System.Windows.Forms.ScrollEventType.SmallDecrement, -oldDisplayRectangle.Y, -displayRect.Y, System.Windows.Forms.ScrollOrientation.HorizontalScroll);
                    OnScroll(se);
                }
            }
        }
        public override void OnLayout()
        {
            base.OnLayout();
            AdjustFormScrollbars();
        }
        internal override void DoPaint(DUIPaintEventArgs e)
        {
            e.Graphics.TranslateTransform(this.X, this.Y); //偏移一下坐标系将控件的坐标定义为坐标系原点
            PointF center = new PointF(this.BorderWidth + this.CenterX, this.BorderWidth + this.CenterY);
            e.Graphics.RotateTransform(this.Rotate, center.X, center.Y);
            e.Graphics.SkewTransform(this.SkewX, this.SkewY, center.X, center.Y);
            e.Graphics.ScaleTransform(this.ScaleX, this.ScaleY, center.X, center.Y);
            e.Graphics.PushLayer(this.Width, this.Height); //背景图层
            #region OnPaintBackground
            var backupSmoothingMode = e.Graphics.SmoothingMode;
            var backupTextRenderingHint = e.Graphics.TextRenderingHint;
            var backupTransform = e.Graphics.Transform;
            OnPaintBackground(new DUIPaintEventArgs(e.Graphics, new RectangleF(0, 0, this.Width, this.Height))); //绘制背景
            e.Graphics.Transform = backupTransform;
            e.Graphics.TextRenderingHint = backupTextRenderingHint;
            e.Graphics.SmoothingMode = backupSmoothingMode;
            #endregion
            if (this.BorderWidth != 0)
            {
                e.Graphics.PopLayer();
                e.Graphics.TranslateTransform(this.BorderWidth, this.BorderWidth); //偏移一个边框的坐标系
                e.Graphics.PushLayer(this.ClientSize.Width, this.ClientSize.Height); //背景图层
            }
            #region OnPaint
            backupSmoothingMode = e.Graphics.SmoothingMode;
            backupTextRenderingHint = e.Graphics.TextRenderingHint;
            backupTransform = e.Graphics.Transform;
            OnPaint(new DUIPaintEventArgs(e.Graphics, new RectangleF(0, 0, ClientSize.Width, this.ClientSize.Height))); //先让子类画图
            e.Graphics.Transform = backupTransform;
            e.Graphics.TextRenderingHint = backupTextRenderingHint;
            e.Graphics.SmoothingMode = backupSmoothingMode;
            #endregion
            #region 子控件
            List<DUIControl> dUIControls = new List<DUIControl>();
            List<DUIControl> dUITopMostControls = new List<DUIControl>();
            foreach (DUIControl d in this.DUIControls)
            {
                if (d.Visible && !this.IsOutOfArea(d))
                {
                    if (d.TopMost)
                    {
                        dUITopMostControls.Add(d);
                    }
                    else
                    {
                        dUIControls.Add(d);
                    }
                }
            }
            dUITopMostControls.ForEach(t => { dUIControls.Add(t); });
            e.Graphics.TranslateTransform(this.DisplayRectangle.X, this.DisplayRectangle.Y);
            foreach (DUIControl dUIControl in dUIControls)
            {
                dUIControl.DoPaint(new DUIPaintEventArgs(e.Graphics, new RectangleF(0, 0, dUIControl.Width, dUIControl.Height)));
            }
            e.Graphics.TranslateTransform(-this.DisplayRectangle.X, -this.DisplayRectangle.Y);
            #endregion
            e.Graphics.TranslateTransform(-this.BorderWidth, -this.BorderWidth);
            e.Graphics.PopLayer();
            #region OnPaintForeground
            backupSmoothingMode = e.Graphics.SmoothingMode;
            backupTextRenderingHint = e.Graphics.TextRenderingHint;
            backupTransform = e.Graphics.Transform;
            OnPaintForeground(new DUIPaintEventArgs(e.Graphics, new RectangleF(0, 0, this.Width, this.Height))); //先让子类画图
            e.Graphics.Transform = backupTransform;
            e.Graphics.TextRenderingHint = backupTextRenderingHint;
            e.Graphics.SmoothingMode = backupSmoothingMode;
            #endregion
            DrawScrollBar(e);
            e.Graphics.ScaleTransform(1 / this.ScaleX, 1 / this.ScaleY, center.X, center.Y);
            e.Graphics.SkewTransform(-this.SkewX, -this.SkewY, center.X, center.Y);
            var s = (float)(Math.Tan(-this.SkewX) * Math.Tan(this.SkewY) + 1);
            e.Graphics.ScaleTransform(1 / s, 1 / s, center.X, center.Y);
            e.Graphics.RotateTransform(-this.Rotate, center.X, center.Y);
            e.Graphics.TranslateTransform(-this.X, -this.Y); //偏移一下坐标系将控件的坐标定义为坐标系原点
        }
        internal override void DoMouseClick(DUIMouseEventArgs e)
        {
            if (HScroll && this.HorizontalScroll.AutoVisible && HorizontalScroll.ClientRectangle.Contains(e.Location))
            {
                return;
            }
            if (VScroll && this.VerticalScroll.AutoVisible && VerticalScroll.ClientRectangle.Contains(e.Location))
            {
                return;
            }
            e = new DUIMouseEventArgs(e.Button, e.Clicks, PointToScroll(e.Location), e.Delta);
            base.DoMouseClick(e);
        }
        internal override void DoMouseDoubleClick(DUIMouseEventArgs e)
        {
            if (HScroll && this.HorizontalScroll.AutoVisible && HorizontalScroll.ClientRectangle.Contains(e.Location))
            {
                return;
            }
            if (VScroll && this.VerticalScroll.AutoVisible && VerticalScroll.ClientRectangle.Contains(e.Location))
            {
                return;
            }
            e = new DUIMouseEventArgs(e.Button, e.Clicks, PointToScroll(e.Location), e.Delta);
            base.DoMouseDoubleClick(e);
        }
        internal override void DoMouseDown(DUIMouseEventArgs e)
        {
            if (HScroll && this.HorizontalScroll.AutoVisible && HorizontalScroll.BodyRectangle.Contains(e.Location))
            {
                this.mouseEffectScrollPoint = e.Location;
                this.isMouseDownInHScroll = true;
                this.Focus();
                this.DUIControlShare.MouseDownDUIControls.Add(new Common.DUIMouseStateEventArgs(this, e.Button));
                this.Invalidate();
                return;
            }
            if (VScroll && this.VerticalScroll.AutoVisible && VerticalScroll.BodyRectangle.Contains(e.Location))
            {
                this.mouseEffectScrollPoint = e.Location;
                this.isMouseDownInVScroll = true;
                this.Focus();
                this.DUIControlShare.MouseDownDUIControls.Add(new Common.DUIMouseStateEventArgs(this, e.Button));
                this.Invalidate();
                return;
            }
            if (HScroll && this.HorizontalScroll.AutoVisible && HorizontalScroll.ClientRectangle.Contains(e.Location))
            {
                this.isMouseDownInVScroll = true;
                this.Invalidate();
                return;
            }
            if (VScroll && this.VerticalScroll.AutoVisible && VerticalScroll.ClientRectangle.Contains(e.Location))
            {
                this.isMouseDownInVScroll = true;
                this.Invalidate();
                return;
            }
            e = new DUIMouseEventArgs(e.Button, e.Clicks, PointToScroll(e.Location), e.Delta);
            base.DoMouseDown(e);
        }
        internal override void DoMouseMove(DUIMouseEventArgs e)
        {
            if (HScroll && this.HorizontalScroll.AutoVisible && isMouseDownInHScroll)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    ScrollHOffset((e.Location.X - this.mouseEffectScrollPoint.X) * this.DisplayRectangle.Width / this.ClientRectangle.Width);
                }
                this.mouseEffectScrollPoint = e.Location;
                //this.Invalidate();
                return;
            }
            if (VScroll && this.VerticalScroll.AutoVisible && isMouseDownInVScroll)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    ScrollVOffset((e.Location.Y - this.mouseEffectScrollPoint.Y) * this.DisplayRectangle.Height / this.ClientRectangle.Height);
                }
                this.mouseEffectScrollPoint = e.Location;
                //this.Invalidate();
                return;
            }
            if (HScroll && this.HorizontalScroll.AutoVisible && HorizontalScroll.ClientRectangle.Contains(e.Location))
            {
                //this.Invalidate();
                return;
            }
            if (VScroll && this.VerticalScroll.AutoVisible && VerticalScroll.ClientRectangle.Contains(e.Location))
            {
                //this.Invalidate();
                return;
            }
            e = new DUIMouseEventArgs(e.Button, e.Clicks, PointToScroll(e.Location), e.Delta);
            base.DoMouseMove(e);
        }
        internal override void DoMouseUp(DUIMouseEventArgs e)
        {
            isMouseDownInHScroll = false;
            isMouseDownInVScroll = false;
            e = new DUIMouseEventArgs(e.Button, e.Clicks, PointToScroll(e.Location), e.Delta);
            base.DoMouseUp(e);
        }
        internal override void DoMouseWheel(DUIMouseEventArgs e)
        {
            e = new DUIMouseEventArgs(e.Button, e.Clicks, PointToScroll(e.Location), e.Delta);
            if (this.DUIControlShare.FocusedDUIControl == null) { return; }
            if (this.AnyFocused)
            {
                this.OnMouseWheel(e);
            }
            else
            {
                this.DUIControls.OfType<DUIControl>().Where(d => d.Visible).ToList().ForEach(d =>
                {
                    d.DoMouseWheel(new DUIMouseEventArgs(e.Button, e.Clicks, d.PointFromParent(e.Location), e.Delta));
                    if (d.AnyFocused)
                    {
                        if (d.CanMouseThrough)
                        {
                            this.OnMouseWheel(e);
                        }
                    }
                });
            }
        }
        internal override void DoMouseEnter(EventArgs e)
        {
            base.DoMouseEnter(e);
            this.isMouseEnter = true;
            this.Invalidate();
        }
        internal override void DoMouseLeave(EventArgs e)
        {
            base.DoMouseLeave(e);
            this.isMouseEnter = false;
            this.Invalidate();
        }
        public override void OnMouseDown(DUIMouseEventArgs e)
        {
            base.OnMouseDown(e);
            //this.mouseDownY = e.Location.Y;
            //this.lastY = e.Location.Y;
            //this.mouseDowmTime = DateTime.Now;
        }
        //public override void OnMouseMove(MouseEventArgs e)
        //{
        //    base.OnMouseMove(e);
        //    if (e.Button == System.Windows.Forms.MouseButtons.Left && ArrowMouseScorll)
        //    {
        //        int y = e.Location.Y - this.lastY;
        //        this.lastY = e.Location.Y;
        //        this.DoVScroll(-y);
        //    }
        //}
        public override void OnMouseUp(DUIMouseEventArgs e)
        {
            this.mouseEffectScrollPoint = Point.Empty;
            this.isMouseDownInHScroll = false;
            this.isMouseDownInVScroll = false;
            base.OnMouseUp(e);
        }
        public override void OnMouseWheel(DUIMouseEventArgs e)
        {
            if (VScroll && this.VerticalScroll.AutoVisible)
            {
                ScrollVOffset(-e.Delta / 4);
            }
            else if (HScroll && this.HorizontalScroll.AutoVisible)
            {
                ScrollHOffset(e.Delta / 4);
            }
            base.OnMouseWheel(e);
        }
        #endregion
        #region 函数
        /// <summary> 鼠标超出控件的范围会动态的滚动至鼠标位置，自动滚动开启
        /// </summary>
        public void AutoScrollStart()
        {
            if (this.OutOfRangeAutoScroll)
            {
                loop.Start();
            }
        }
        /// <summary> 鼠标超出控件的范围会动态的滚动至鼠标位置，自动滚动关闭
        /// </summary>
        public void AutoScrollStop()
        {
            loop.Stop();
        }
        protected virtual void DrawScrollBar(DUIPaintEventArgs e)
        {
            var backupSmoothingMode = e.Graphics.SmoothingMode;
            e.Graphics.SmoothingMode = DUISmoothingMode.AntiAlias;
            if (this.HScroll && this.HorizontalScroll.Visible && this.HorizontalScroll.AutoVisible)
            {
                if (isMouseDownInHScroll)
                {
                    using (DUISolidBrush backBrush = new DUISolidBrush(Color.White))
                    {
                        e.Graphics.FillRectangle(backBrush, this.HorizontalScroll.ClientRectangle);
                    }
                }
                if (this.isMouseEnter || isMouseDownInHScroll)
                {
                    using (DUISolidBrush bodyBrush = new DUISolidBrush(Color.LightGray))
                    {
                        e.Graphics.FillRectangle(bodyBrush, this.HorizontalScroll.BodyRectangle);
                    }
                }
            }
            if (this.VScroll && this.VerticalScroll.Visible && this.VerticalScroll.AutoVisible)
            {
                if (isMouseDownInVScroll)
                {
                    using (DUISolidBrush backBrush = new DUISolidBrush(Color.White))
                    {
                        e.Graphics.FillRectangle(backBrush, this.VerticalScroll.ClientRectangle);
                    }
                }
                if (this.isMouseEnter || isMouseDownInVScroll)
                {
                    using (DUISolidBrush bodyBrush = new DUISolidBrush(Color.Gray))
                    {
                        e.Graphics.FillRoundedRectangle(bodyBrush, this.VerticalScroll.BodyRectangle, 4);
                    }
                }
            }
            e.Graphics.SmoothingMode = backupSmoothingMode;
        }
        public void ScrollMouseMove(DUIMouseEventArgs e)
        {
            var mouseDownDUIControls = this.DUIControlShare.MouseDownDUIControls.Where(d => d.Button == e.Button).ToList();
            if (mouseDownDUIControls.Count == 0)
            {
                DUIControl dUIControl = this.GetChildAtPoint(e.Location);
                if (dUIControl == null)
                {
                    if (this.DUIControlShare.MouseMoveDUIControl != this)
                    {
                        if (this.DUIControlShare.MouseMoveDUIControl != null)
                        {
                            this.DUIControlShare.MouseMoveDUIControl.DoMouseLeave(EventArgs.Empty);
                        }
                        this.DUIControlShare.MouseMoveDUIControl = this; //记录下鼠标移动的控件
                        this.DUIControlShare.MouseMoveDUIControl.DoMouseEnter(EventArgs.Empty);
                    }
                    this.InvokeOnMouseMove(this, e);
                }
                else
                {
                    dUIControl.DoMouseMove(new DUIMouseEventArgs(e.Button, e.Clicks, dUIControl.PointFromParent(e.Location), e.Delta));
                }
            }
            else
            {
                if (mouseDownDUIControls[0].AffectedControl == this)
                {
                    this.InvokeOnMouseMove(this, e);
                }
                else
                {
                    this.DUIControls.OfType<DUIControl>().Where(d => d.Visible).ToList().ForEach(d =>
                    {
                        d.DoMouseMove(new DUIMouseEventArgs(e.Button, e.Clicks, d.PointFromParent(e.Location), e.Delta));
                    });
                }
            }
        }
        /// <summary> 调整滚动条
        /// </summary>
        protected internal virtual void AdjustFormScrollbars()
        {
            RectangleF display = this.DisplayRectangle;
            if (!AutoScroll && (HScroll || VScroll))
            {
                SetVisibleScrollbars(false, false);
            }
            if (!AutoScroll)
            {
                RectangleF client = ClientRectangle;
                display.Width = client.Width;
                display.Height = client.Height;
            }
            else
            {
                ApplyScrollbarChanges(display);
            }
        }
        /// <summary> 应用滚动条的改变
        /// </summary>
        /// <param name="display"></param>
        /// <returns></returns>
        protected internal virtual void ApplyScrollbarChanges(RectangleF display)
        {
            bool needHscroll = false;
            bool needVscroll = false;
            RectangleF currentClient = ClientRectangle;
            RectangleF fullClient = currentClient;
            RectangleF minClient = fullClient;
            if (HScroll)
            {
                fullClient.Height += HThickness;
            }
            else
            {
                minClient.Height -= HThickness;
            }
            if (VScroll)
            {
                fullClient.Width += VThickness;
            }
            else
            {
                minClient.Width -= VThickness;
            }
            float maxX = minClient.Width;
            float maxY = minClient.Height;
            //if (DUIControls.Count == 0)
            {
                float ctlRight = Math.Max(this.MinDisplayWidth, maxX);
                float ctlBottom = Math.Max(this.MinDisplayHeight, maxY);
                if (ctlRight > maxX)
                {
                    needHscroll = true;
                    maxX = ctlRight;
                }
                if (ctlBottom > maxY)
                {
                    needVscroll = true;
                    maxY = ctlBottom;
                }
            }
            for (int i = 0; i < DUIControls.Count; i++)
            {
                bool watchHoriz = true;
                bool watchVert = true;
                DUIControl current = DUIControls[i];
                if (current != null && current.Visible)
                {
                    DUIControl richCurrent = (DUIControl)current;
                    switch (richCurrent.Dock)
                    {
                        case System.Windows.Forms.DockStyle.Top:
                            watchHoriz = false;
                            break;
                        case System.Windows.Forms.DockStyle.Left:
                            watchVert = false;
                            break;
                        case System.Windows.Forms.DockStyle.Right:
                            watchHoriz = false;
                            watchVert = false;
                            break;
                        default:
                            System.Windows.Forms.AnchorStyles anchor = richCurrent.Anchor;
                            if ((anchor & System.Windows.Forms.AnchorStyles.Right) == System.Windows.Forms.AnchorStyles.Right)
                            {
                                watchHoriz = false;
                            }
                            if ((anchor & System.Windows.Forms.AnchorStyles.Left) != System.Windows.Forms.AnchorStyles.Left)
                            {
                                watchHoriz = false;
                            }
                            if ((anchor & System.Windows.Forms.AnchorStyles.Bottom) == System.Windows.Forms.AnchorStyles.Bottom)
                            {
                                watchVert = false;
                            }
                            if ((anchor & System.Windows.Forms.AnchorStyles.Top) != System.Windows.Forms.AnchorStyles.Top)
                            {
                                watchVert = false;
                            }
                            break;
                    }
                    if (watchHoriz || watchVert)
                    {
                        RectangleF bounds = current.Bounds;
                        float ctlRight = Math.Max(this.MinDisplayWidth, bounds.X + bounds.Width + RightAppend);
                        float ctlBottom = Math.Max(this.MinDisplayHeight, bounds.Y + bounds.Height + BottomAppend);
                        if (ctlRight > maxX && watchHoriz)
                        {
                            needHscroll = true;
                            maxX = ctlRight;
                        }
                        if (ctlBottom > maxY && watchVert)
                        {
                            needVscroll = true;
                            maxY = ctlBottom;
                        }
                    }
                }
            }
            if (maxX <= fullClient.Width)
            {
                needHscroll = false;
            }
            if (maxY <= fullClient.Height)
            {
                needVscroll = false;
            }
            RectangleF clientToBe = fullClient;
            if (needHscroll)
            {
                clientToBe.Height -= HThickness;
            }
            if (needVscroll)
            {
                clientToBe.Width -= VThickness;
            }
            if (needHscroll && maxY > clientToBe.Height)
            {
                needVscroll = true;
            }
            if (needVscroll && maxX > clientToBe.Width)
            {
                needHscroll = true;
            }
            if (!needHscroll)
            {
                maxX = clientToBe.Width;
            }
            if (!needVscroll)
            {
                maxY = clientToBe.Height;
            }
            SetVisibleScrollbars(needHscroll, needVscroll);
            SetDisplayRectangleSize(maxX, maxY);
            //if (HScroll || VScroll)
            //{
            //    SetDisplayRectangleSize(maxX, maxY);
            //}
            //else
            //{
            //    SetDisplayRectangleSize(maxX, maxY);
            //}
            SyncScrollbars(true);
        }
        /// <summary> 设置滚动条的值，并触发事件
        /// </summary>
        /// <param name="autoScroll">是否自动滚动</param>
        /// <param name="triggerEvent">是否触发事件</param>
        protected internal virtual void SyncScrollbars(bool autoScroll, bool triggerEvent = true)
        {
            RectangleF displayRect = this.displayRect;
            if (autoScroll)
            {
                if (!IsHandleCreated)
                {
                    return;
                }
                if (HScroll && triggerEvent)
                {
                    if (-displayRect.X >= 0 && -displayRect.X < displayRect.Width)
                    {
                        if (displayRect.X != -HorizontalScroll.value)
                        {
                            HorizontalScroll.value = -displayRect.X;
                            DUIScrollEventArgs se = new DUIScrollEventArgs(System.Windows.Forms.ScrollEventType.SmallDecrement, -displayRect.X, HorizontalScroll.value, System.Windows.Forms.ScrollOrientation.HorizontalScroll);
                            OnScroll(se);
                        }
                    }
                }
                if (VScroll && triggerEvent)
                {
                    if (-displayRect.Y >= 0 && -displayRect.Y < displayRect.Height)
                    {
                        if (displayRect.Y != -VerticalScroll.value)
                        {
                            VerticalScroll.value = -displayRect.Y;
                            DUIScrollEventArgs se = new DUIScrollEventArgs(System.Windows.Forms.ScrollEventType.SmallDecrement, -displayRect.Y, VerticalScroll.value, System.Windows.Forms.ScrollOrientation.VerticalScroll);
                            OnScroll(se);
                        }
                    }
                }
            }
            if (this.HorizontalScroll.Visible)
            {
                HorizontalScroll.Value = -displayRect.X;
            }
            else
            {
                ResetScrollProperties(HorizontalScroll);
            }
            if (this.VerticalScroll.Visible)
            {
                VerticalScroll.Value = -displayRect.Y;
            }
            else
            {
                ResetScrollProperties(VerticalScroll);
            }
        }
        /// <summary> 设置滚动条是否可见
        /// </summary>
        /// <param name="horiz"></param>
        /// <param name="vert"></param>
        /// <returns></returns>
        private void SetVisibleScrollbars(bool horiz, bool vert)
        {
            bool needLayout = false;
            if (!horiz && HScroll || horiz && !HScroll || !vert && VScroll || vert && !VScroll)
            {
                needLayout = true;
            }
            if (needLayout)
            {
                float x = displayRect.X;
                float y = displayRect.Y;
                if (!horiz)
                {
                    x = 0;
                }
                if (!vert)
                {
                    y = 0;
                }
                SetDisplayRectangleLocation(x, y);
                HScroll = horiz;
                VScroll = vert;
                if (horiz)
                {
                    HorizontalScroll.visible = true;
                }
                else
                {
                    HorizontalScroll.visible = false;
                    ResetScrollProperties(HorizontalScroll);
                }
                if (vert)
                {
                    VerticalScroll.visible = true;
                }
                else
                {
                    VerticalScroll.visible = false;
                    ResetScrollProperties(VerticalScroll);
                }
            }
        }
        /// <summary> 重置滚动条的属性
        /// </summary>
        /// <param name="scrollProperties"></param>
        private void ResetScrollProperties(DUIScrollProperties scrollProperties)
        {
            scrollProperties.visible = false;
            scrollProperties.value = 0;
        }
        /// <summary> 设置DisplayRectangle
        /// </summary>
        /// <param name="x">DisplayRectangle的X</param>
        /// <param name="y">DisplayRectangle的Y</param>
        /// <param name="limited">是否限制</param>
        protected internal virtual void SetDisplayRectangleLocation(float x, float y, bool limited = true)
        {
            float xDelta = 0;
            float yDelta = 0;
            RectangleF client = ClientRectangle;
            RectangleF displayRectangle = displayRect;
            float minX = Math.Min(client.Width - displayRectangle.Width, 0);
            float minY = Math.Min(client.Height - displayRectangle.Height, 0);
            if (limited)
            {
                if (x > 0)
                {
                    x = 0;
                }
                if (y > 0)
                {
                    y = 0;
                }
                if (x < minX)
                {
                    x = minX;
                }
                if (y < minY)
                {
                    y = minY;
                }
            }
            if (displayRectangle.X != x)
            {
                xDelta = x - displayRectangle.X;
            }
            if (displayRectangle.Y != y)
            {
                yDelta = y - displayRectangle.Y;
            }
            if (displayRect.X != x || displayRect.Y != y)
            {
                displayRect.X = x;
                displayRect.Y = y;
                OnDisplayLocationChanged();
                OnDisplayRectangleChanged();
            }
        }
        /// <summary> 设置DisplayRectangle的尺寸
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        protected internal virtual void SetDisplayRectangleSize(float width, float height)
        {
            if (displayRect.Width != width || displayRect.Height != height)
            {
                displayRect.Width = width;
                displayRect.Height = height;
                OnDisplaySizeChanged();
                OnDisplayRectangleChanged();
            }
            float minX = ClientRectangle.Width - width;
            float minY = ClientRectangle.Height - height;
            if (minX > 0) minX = 0;
            if (minY > 0) minY = 0;
            float x = displayRect.X;
            float y = displayRect.Y;
            if (!HScroll)
            {
                x = 0;
            }
            if (!VScroll)
            {
                y = 0;
            }
            if (x < minX)
            {
                x = minX;
            }
            if (y < minY)
            {
                y = minY;
            }
            SetDisplayRectangleLocation(x, y);
        }
        /// <summary> 横向偏移坐标滚动到支持自动滚动的控件的视图中
        /// </summary>
        /// <param name="offsetValue">偏移值</param>
        /// <param name="limited">超出边界是否做限制</param>
        /// <returns>偏移距离</returns>
        public virtual float ScrollHOffset(float offsetValue, bool limited = false, bool triggerEvent = true)
        {
            float lastdisplayRectX = this.DisplayRectangle.X;
            float pos = -this.DisplayRectangle.X;
            float maxPos = -(this.ClientRectangle.Width - this.DisplayRectangle.Width);
            pos = Math.Max(pos + offsetValue, 0);
            pos = Math.Min(pos, maxPos);
            SetDisplayRectangleLocation(-pos, this.DisplayRectangle.Y, limited);
            SyncScrollbars(true, triggerEvent);
            this.Invalidate();
            return lastdisplayRectX - this.DisplayRectangle.X;
        }
        /// <summary> 纵向偏移坐标滚动到支持自动滚动的控件的视图中
        /// </summary>
        /// <param name="offsetValue">偏移值</param>
        /// <param name="limited">超出边界是否做限制</param>
        /// <returns>偏移距离</returns>
        public virtual float ScrollVOffset(float offsetValue, bool limited = false, bool triggerEvent = true)
        {
            float lastdisplayRectY = this.DisplayRectangle.Y;
            float pos = -this.DisplayRectangle.Y;
            float maxPos = -(this.ClientRectangle.Height - this.DisplayRectangle.Height);
            pos = Math.Max(pos + offsetValue, 0);
            pos = Math.Min(pos, maxPos);
            SetDisplayRectangleLocation(this.DisplayRectangle.X, -pos, limited);
            SyncScrollbars(true, triggerEvent);
            this.Invalidate();
            return lastdisplayRectY - this.DisplayRectangle.Y;
        }
        /// <summary> 将指定的点坐标滚动到支持自动滚动的控件的视图中
        /// </summary>
        /// <param name="x">要滚动到视图中的坐标</param>
        /// <param name="limited">超出边界是否做限制</param>
        /// <param name="viewRangeScroll">如果已在可视范围内，是否继续滚动</param>
        public float ScrollHIntoView(float x, bool limited = true, bool viewRangeScroll = true, bool triggerEvent = true)
        {
            float lastdisplayRectY = this.DisplayRectangle.Y;
            if (x > -this.DisplayRectangle.X && x < this.ClientRectangle.Width - this.DisplayRectangle.X && !viewRangeScroll)
            {
                x = -this.DisplayRectangle.Y;
            }
            SetDisplayRectangleLocation(-x, this.DisplayRectangle.Y, limited);
            SyncScrollbars(true, triggerEvent);
            this.Invalidate();
            return lastdisplayRectY - this.DisplayRectangle.Y;
        }
        /// <summary> 将指定的点坐标Y滚动到支持自动滚动的控件的视图中
        /// </summary>
        /// <param name="y">要滚动到视图中的坐标Y</param>
        /// <param name="limited">超出边界是否做限制</param>
        /// <param name="viewRangeScroll">如果已在可视范围内，是否继续滚动</param>
        public float ScrollVIntoView(float y, bool limited = true, bool viewRangeScroll = true, bool triggerEvent = true)
        {
            float lastdisplayRectY = this.DisplayRectangle.Y;
            if (y > -this.DisplayRectangle.Y && y < this.ClientRectangle.Height - this.DisplayRectangle.Y && !viewRangeScroll)
            {
                y = -this.DisplayRectangle.Y;
            }
            SetDisplayRectangleLocation(this.DisplayRectangle.X, -y, limited);
            SyncScrollbars(true, triggerEvent);
            this.Invalidate();
            return lastdisplayRectY - this.DisplayRectangle.Y;
        }
        #region 坐标转换
        /// <summary> 将实际坐标转为Scroll控件的坐标
        /// </summary>
        /// <param name="point">实际坐标</param>
        /// <returns>Scroll控件的坐标</returns>
        private PointF PointToScroll(PointF point) //这里总是出问题 下次遇到再说 这里MMTools时间轴测试过了都是没问题的
        {
            return new PointF(point.X - this.DisplayRectangle.X, point.Y - this.DisplayRectangle.Y);
        }
        /// <summary> 将Scroll控件的坐标转为实际坐标
        /// </summary>
        /// <param name="point">Scroll控件的坐标</param>
        /// <returns>实际坐标</returns>
        private PointF PointFromScroll(PointF point)
        {
            return new PointF(point.X + this.DisplayRectangle.X, point.Y + this.DisplayRectangle.Y);
        }
        public override PointF PointToClient(PointF p)
        {
            p = base.PointToClient(p);
            return PointFromScroll(p);
        }
        public override PointF PointToScreen(PointF p)
        {
            p = PointFromScroll(p);
            return base.PointToScreen(p);
        }
        public override PointF PointToParent(PointF p)
        {
            //p = PointFromScroll(p);
            return base.PointToParent(p);
        }
        public override PointF PointFromParent(PointF p)
        {
            //p = PointFromScroll(p);
            return base.PointFromParent(p);
        }
        public override PointF PointToBaseParent(PointF p)
        {
            p = PointFromScroll(p);
            return base.PointToBaseParent(p);
        }
        public override RectangleF RectangleToClient(RectangleF r)
        {
            r = new RectangleF(PointFromScroll(r.Location), r.Size);
            return base.RectangleToClient(r);
        }
        public override RectangleF RectangleToScreen(RectangleF r)
        {
            r = new RectangleF(PointFromScroll(r.Location), r.Size);
            return base.RectangleToScreen(r);
        }
        public override RectangleF RectangleToParent(RectangleF r)
        {
            r = new RectangleF(PointFromScroll(r.Location), r.Size);
            return base.RectangleToParent(r);
        }
        public override RectangleF RectangleToBaseParent(RectangleF r)
        {
            r = new RectangleF(PointFromScroll(r.Location), r.Size);
            return base.RectangleToBaseParent(r);
        }
        #endregion
        #region 定位坐标、控件
        /// <summary> 将指定的子控件滚动到支持自动滚动的控件的视图中。
        /// </summary>
        /// <param name="activeControl">要滚动到视图中的子控件</param>
        public void ScrollControlIntoView(DUIControl activeControl)
        {
            RectangleF client = ClientRectangle;
            if (IsDescendant(activeControl)
                && AutoScroll
                && (HScroll || VScroll)
                && activeControl != null
                && (client.Width > 0 && client.Height > 0))
            {
                PointF scrollLocation = ScrollToControl(activeControl);
                SetDisplayRectangleLocation(scrollLocation.X, scrollLocation.Y);
                SyncScrollbars(true);
                this.Invalidate();
            }
        }
        /// <summary> 找到控件在DUIScrollableControl上合适展示的点
        /// </summary>
        /// <param name="activeControl"></param>
        /// <returns></returns>
        protected virtual PointF ScrollToControl(DUIControl activeControl)
        {
            float x = this.DisplayRectangle.X;
            float y = this.DisplayRectangle.Y;
            if (activeControl.X < -this.DisplayRectangle.X)
            {
                x = -activeControl.X;
            }
            if (activeControl.Right > this.ClientRectangle.Width - this.DisplayRectangle.X)
            {
                x = -(activeControl.Right - this.ClientRectangle.Width);
            }
            if (activeControl.Y < -this.DisplayRectangle.Y)
            {
                y = -activeControl.Y;
            }
            if (activeControl.Bottom > this.ClientRectangle.Height - this.DisplayRectangle.Y)
            {
                y = -(activeControl.Bottom - this.ClientRectangle.Height);
            }
            return new PointF(x, y);
        }
        #endregion
        #endregion
    }
}
