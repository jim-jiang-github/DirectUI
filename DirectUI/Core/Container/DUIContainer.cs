using DirectUI.Common;
using DirectUI.Log;
using DirectUI.Share;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirectUI.Core
{
    /// <summary> DUIControl的容器
    /// </summary>
    internal class DUIContainer : DUIControl
    {
        #region 变量
        protected int fPS = 60;
        /// <summary> 需要刷新的区域
        /// </summary>
        private Region invalidateRegion = new Region(Rectangle.Empty);
        protected ConcurrentStack<Action> invalidateStack = new ConcurrentStack<Action>(); //用作刷新的堆栈
        //protected Stack<Action> invalidateStack = new Stack<Action>(); //用作刷新的堆栈
        protected DUIGraphics dUIGraphics = null;
        /// <summary> 对话框气球
        /// </summary>
        protected ToolTip toolTip = new ToolTip() { IsBalloon = true };
        /// <summary> 所在的DUINativeControl
        /// </summary>
        internal protected DUINativeControl owner = null;
        protected SizeF lastClientSize = SizeF.Empty;
        /// <summary> 共享信息,用于记录焦点控件、置顶控件、按下控件、移动控件
        /// </summary>
        protected DUIControlShare dUIControlShare = null;
        protected SynchronizationContext synchronizationContext = null;
        #endregion
        #region 属性
        public override DUIFont Font
        {
            get
            {
                return new DUIFont(this.owner.Font.FontFamily, this.owner.Font.Size, this.owner.Font.Style);
            }
            set
            {
                base.Font = value;
            }
        }
        /// <summary> 刷新率
        /// </summary>
        public int FPS
        {
            get { return fPS; }
            set { fPS = value; }
        }
        #endregion
        #region 重写
        internal override bool CanLayout
        {
            get
            {
                return base.CanLayout;
            }
        }
        public override System.Drawing.Color BackColor
        {
            get
            {
                return this.owner.BackColor;
            }
        }
        internal override DUIControlShare DUIControlShare
        {
            get
            {
                return dUIControlShare;
            }
        }
        public override IntPtr Handle
        {
            get
            {
                return this.owner.Handle;
            }
        }
        public override float Width
        {
            get
            {
                return this.owner.ClientSize.Width;
            }
        }
        public override float Height
        {
            get
            {
                return this.owner.ClientSize.Height;
            }
        }
        public override System.Drawing.PointF PointToClient(System.Drawing.PointF p)
        {
            PointF rp = PointF.Empty;
            this.SynchronizationContextSend(() =>
            {
                rp = owner?.PointToClient(Point.Ceiling(p)) ?? PointF.Empty;
            });
            return rp;
        }
        public override System.Drawing.PointF PointToScreen(System.Drawing.PointF p)
        {
            PointF rp = PointF.Empty;
            this.SynchronizationContextSend(() =>
            {
                rp = owner?.PointToScreen(Point.Ceiling(p)) ?? PointF.Empty;
            });
            return rp;
        }
        public override System.Drawing.RectangleF RectangleToClient(System.Drawing.RectangleF r)
        {
            RectangleF rp = RectangleF.Empty;
            this.SynchronizationContextSend(() =>
            {
                rp = owner?.RectangleToClient(Rectangle.Ceiling(r)) ?? RectangleF.Empty;
            });
            return rp;
        }
        public override System.Drawing.RectangleF RectangleToScreen(System.Drawing.RectangleF r)
        {
            return owner.RectangleToScreen(Rectangle.Ceiling(r));
        }
        public override System.Drawing.PointF PointFromBaseParent(System.Drawing.PointF p)
        {
            return p;
        }
        public override System.Drawing.PointF PointToBaseParent(System.Drawing.PointF p)
        {
            return p;
        }
        public override string Text
        {
            get
            {
                return owner.Text;
            }
            set
            {
                this.SynchronizationContextSend(() =>
                {
                    owner.Text = value;
                });
            }
        }
        public override Cursor Cursor
        {
            get
            {
                return owner.Cursor;
            }
            set
            {
                this.SynchronizationContextSend(() =>
                {
                    owner.Cursor = value;
                });
            }
        }
        public override ContextMenuStrip ContextMenuStrip
        {
            get
            {
                return owner.ContextMenuStrip;
            }
            set
            {
                this.SynchronizationContextSend(() =>
                {
                    owner.ContextMenuStrip = value;
                });
            }
        }
        public override DUIGraphics CreateGraphics()
        {
            return this.dUIGraphics;
        }
        public override void Invalidate(Region r)
        {
            this.invalidateStack.Push(new Action(() =>
            {
                DoInvalidate();
            }));
        }
        public void DoInvalidate()
        {
            if (this.CanLayout && this.dUIGraphics != null)
            {
                long executeTime = TimeTools.GetExecuteTime(() =>
                {
                    try
                    {
                        this.dUIGraphics.BeginDraw(this.invalidateRegion);
                        this.dUIGraphics.Clear(this.BackColor);
                        this.DoPaint(new DUIPaintEventArgs(this.dUIGraphics, new RectangleF(0, 0, this.Width, this.Height)));
                        this.dUIGraphics.EndDraw();
                    }
                    catch (Exception ex)
                    {
                        this.dUIGraphics?.Dispose();
                        this.dUIGraphics = null;
                        this.dUIGraphics = DUIGraphics.FromControl(this.owner);
                        Log.DUILog.WriteLog(ex);
                    }
                });
                //Debug.WriteLine("DUIInvalidateExecuteTime:" + executeTime);
                //this.invalidateRegion = new Region(Rectangle.Empty);
                //sw.Stop();
                //Debug.WriteLine(sw.ElapsedMilliseconds + "|" + this.dUIGraphics.ClipBounds);
            }
        }
        public override object Invoke(System.Delegate method)
        {
            return this.owner.Invoke(method);
        }
        public override object Invoke(System.Delegate method, params object[] args)
        {
            return this.owner.Invoke(method, args);
        }
        public override void SynchronizationContextPost(Action act)
        {
            try
            {
                synchronizationContext.Post((obj) =>
                {
                    act?.Invoke();
                }, null);
            }
            catch (Exception ex)
            {
                Log.DUILog.GettingLog(ex);
            }
        }
        public override void SynchronizationContextSend(Action act)
        {
            try
            {
                synchronizationContext.Send((obj) =>
                {
                    act?.Invoke();
                }, null);
            }
            catch (Exception ex)
            {
                Log.DUILog.GettingLog(ex);
            }
        }
        internal override void ShowToolTip(string content)
        {
            this.SynchronizationContextSend(() =>
            {
                toolTip.SetToolTip(this.owner, content);
            });
        }
        internal override void HideToolTip()
        {
            this.SynchronizationContextSend(() =>
            {
                toolTip.RemoveAll();
            });
        }
        internal override bool IsOutOfParentArea
        {
            get
            {
                return false;
            }
        }
        public override void OnPaint(DUIPaintEventArgs e)
        {
            this.owner.OnPaint(e);
        }
        public override void OnPaintBackground(DUIPaintEventArgs e)
        {
            this.owner.OnPaintBackground(e);
        }
        public override void OnPaintForeground(DUIPaintEventArgs e)
        {
            this.owner.OnPaintForeground(e);
        }
        internal override void DoPaint(DUIPaintEventArgs e)
        {
            #region OnPaintBackground
            var backupSmoothingMode = e.Graphics.SmoothingMode;
            var backupTextRenderingHint = e.Graphics.TextRenderingHint;
            var backupTransform = e.Graphics.Transform;
            OnPaintBackground(new DUIPaintEventArgs(e.Graphics, new RectangleF(0, 0, this.Width, this.Height))); //绘制背景
            e.Graphics.Transform = backupTransform;
            e.Graphics.TextRenderingHint = backupTextRenderingHint;
            e.Graphics.SmoothingMode = backupSmoothingMode;
            #endregion
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
            foreach (DUIControl dUIControl in dUIControls)
            {
                dUIControl.DoPaint(new DUIPaintEventArgs(e.Graphics, new RectangleF(0, 0, dUIControl.Width, dUIControl.Height)));
            }
            #endregion
            #region OnPaintForeground
            backupSmoothingMode = e.Graphics.SmoothingMode;
            backupTextRenderingHint = e.Graphics.TextRenderingHint;
            backupTransform = e.Graphics.Transform;
            OnPaintForeground(new DUIPaintEventArgs(e.Graphics, new RectangleF(0, 0, this.Width, this.Height))); //先让子类画图
            e.Graphics.Transform = backupTransform;
            e.Graphics.TextRenderingHint = backupTextRenderingHint;
            e.Graphics.SmoothingMode = backupSmoothingMode;
            #endregion
        }
        #endregion
        public DUIContainer(DUINativeControl owner)
        {
            this.owner = owner;
            this.lastClientSize = this.Size;
            this.dUIControlShare = new DUIControlShare();
            synchronizationContext = SynchronizationContext.Current;
            InitD2D();
            RegeditFormEvent();
            RegeditMouseEvent();
            RegeditKeyboardEvent();
            RegeditPaintEvent();
            #region 新开一个线程来处理刷新
            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000 / this.FPS);
                    if (this.invalidateStack.TryPop(out Action action))
                    {
                        synchronizationContext.Send((obj) =>
                        {
                            action?.Invoke();
                        }, null);
                        this.invalidateStack.Clear();
                    }
                }
            })
            { IsBackground = true, Name = "DUIContainerInvalidate", Priority = ThreadPriority.Highest }.Start();
            #endregion
        }
        /// <summary> 初始化D2D
        /// </summary>
        protected virtual void InitD2D()
        {
            try
            {
                #region D2D初始化
                this.dUIGraphics?.Dispose();
                this.dUIGraphics = null;
                this.dUIGraphics = DUIGraphics.FromControl(this.owner);
                #endregion
            }
            catch (Exception ex)
            {
                Log.DUILog.GettingLog(ex);
                InitD2D();
            }
        }
        /// <summary> 注册窗体事件
        /// </summary>
        protected virtual void RegeditFormEvent()
        {
            #region 窗体事件
            this.owner.ParentChanged += (s, e) =>
            {
                if (this.owner.Parent != null)
                {
                    if (!this.IsHandleCreated)
                    {
                        this.DoHandleCreated(e);
                        if (this.Width != 0 || this.Height != 0)
                        {
                            if (this.lastClientSize != this.Size)
                            {
                                LayoutEngine.DoLayoutDock(new DUILayoutEventArgs(this, "Layout"), this.lastClientSize);
                                this.lastClientSize = this.Size;
                            }
                        }
                        LayoutEngine.DoLayout(new DUILayoutEventArgs(this, "HandleCreated"));
                    }
                }
            };
            //在这句“SharpDX.Direct2D1.HwndRenderTargetProperties hwndRenderTargetProperties = new SharpDX.Direct2D1.HwndRenderTargetProperties”的时候就已经创建了句柄了,但是这个事件又不会被触发
            //this.owner.HandleCreated += (s, e) =>
            //{
            //    this.DoHandleCreated(e);
            //    LayoutEngine.DoLayout(new DUILayoutEventArgs(this, "HandleCreated"));
            //};
            this.owner.Resize += (s, e) =>
            {
                try
                {
                    this.dUIGraphics?.Resize(this.Width, this.Height);
                }
                catch (Exception ex) //控件在Dockpanel里面以Document的形式展现的时候会报错 目前还不知道报错原因，就先这样处理吧
                {
                    Log.DUILog.GettingLog(ex);
                    this.dUIGraphics?.Dispose();
                    this.dUIGraphics = null;
                    InitD2D();
                }
                this.DoResize(e);
            };
            this.owner.HandleDestroyed += (s, e) =>
            {
                this.dUIGraphics?.Dispose();
                this.dUIGraphics = null;
            };
            this.owner.HandleCreated += (s, e) =>
            {
                InitD2D();
            };
            this.owner.Layout += (s, e) =>
            {
                if (this.Width != 0 || this.Height != 0)
                {
                    if (this.lastClientSize != this.Size)
                    {
                        LayoutEngine.DoLayoutDock(new DUILayoutEventArgs(this, "Layout"), this.lastClientSize);
                        this.lastClientSize = this.Size;
                    }
                }
                this.DoLayout(new DUILayoutEventArgs(this, "Layout"));
            };
            #endregion
        }
        /// <summary> 注册鼠标事件
        /// </summary>
        protected virtual void RegeditMouseEvent()
        {
            #region 鼠标事件
            this.owner.MouseClick += (s, e) =>
            {
                this.DoMouseClick(new DUIMouseEventArgs(e.Button, e.Clicks, e.X, e.Y, e.Delta));
            };
            this.owner.MouseDoubleClick += (s, e) => { this.DoMouseDoubleClick(new DUIMouseEventArgs(e.Button, e.Clicks, e.X, e.Y, e.Delta)); };
            this.owner.MouseDown += (s, e) =>
            {
                if (this.owner.Controls.OfType<Control>().ToList().Exists(c => c.Focused))
                {
                    #region 如果窗体上有Control控件，控件就会获得焦点，所以在窗体上按下鼠标的时候要让所有的控件失去焦点
                    Dictionary<Control, bool> controlEnables = new Dictionary<Control, bool>();
                    foreach (Control control in this.owner.Controls)
                    {
                        controlEnables.Add(control, control.Enabled);
                        control.Enabled = false;
                    }
                    this.owner.Focus();
                    foreach (KeyValuePair<Control, bool> controlEnable in controlEnables)
                    {
                        controlEnable.Key.Enabled = controlEnable.Value;
                    }
                    #endregion
                }
                this.DUIControlShare.MouseDownDUIControls.Clear(); this.DoMouseDown(new DUIMouseEventArgs(e.Button, e.Clicks, e.X, e.Y, e.Delta));
            };
            this.owner.MouseMove += (s, e) =>
            {
                this.DoMouseMove(new DUIMouseEventArgs(e.Button, e.Clicks, e.X, e.Y, e.Delta));
            };
            this.owner.MouseUp += (s, e) =>
            {
                this.DoMouseUp(new DUIMouseEventArgs(e.Button, e.Clicks, e.X, e.Y, e.Delta)); this.DUIControlShare.MouseDownDUIControls.Clear();
            };
            this.owner.MouseWheel += (s, e) => { this.DoMouseWheel(new DUIMouseEventArgs(e.Button, e.Clicks, e.X, e.Y, e.Delta)); };
            this.owner.MouseLeave += (s, e) =>
            {
                if (this.DUIControlShare.MouseMoveDUIControl != null)
                {
                    this.DUIControlShare.MouseMoveDUIControl.DoMouseLeave(EventArgs.Empty);
                    this.DUIControlShare.MouseMoveDUIControl = null;
                }
            };
            this.owner.LostFocus += (s, e) =>
            {
                if (this.DUIControlShare.FocusedDUIControl != null)
                {
                    DUIControl lastFocusedDUIControl = this.DUIControlShare.FocusedDUIControl;
                    this.DUIControlShare.FocusedDUIControl = null;
                    lastFocusedDUIControl.OnLostFocus(EventArgs.Empty);
                    lastFocusedDUIControl.Invalidate();
                }
            };
            #endregion
        }
        /// <summary> 注册键盘事件
        /// </summary>
        protected virtual void RegeditKeyboardEvent()
        {
            #region 键盘事件
            this.owner.KeyDown += (s, e) => { this.DoKeyDown(e); };
            this.owner.KeyPress += (s, e) => { this.DoKeyPress(e); };
            this.owner.KeyUp += (s, e) => { this.DoKeyUp(e); };
            #endregion
        }
        /// <summary> 注册绘图事件
        /// </summary>
        protected virtual void RegeditPaintEvent()
        {
            #region 绘图事件
            this.owner.Paint += (s, e) =>
            {
                //DoInvalidate(e.Graphics.Clip);
                //this.Invalidate(e.ClipRectangle);
            };
            #endregion
        }
    }
}
