using DirectUI.Collection;
using DirectUI.Common;
using DirectUI.Share;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace DirectUI.Core
{
    #region 委托
    public delegate void DUIControlHandler(object sender, DUIControlEventArgs e);
    public delegate void DUIControlLocationChangingHandler(object sender, DUIControlLocationChangingEventArgs e);
    public delegate void DUIControlSizeChangingHandler(object sender, DUIControlSizeChangingEventArgs e);
    public delegate void DUIControlBoundsChangingHandler(object sender, DUIControlBoundsChangingEventArgs e);
    public delegate void DUIControlCenterChangingHandler(object sender, DUIControlCenterChangingEventArgs e);
    public delegate void DUIControlSkewChangingHandler(object sender, DUIControlSkewChangingEventArgs e);
    public delegate void DUIControlScaleChangingHandler(object sender, DUIControlScaleChangingEventArgs e);
    public delegate void DUIControlRotateAngleChangingHandler(object sender, DUIControlRotateAngleChangingEventArgs e);
    public delegate void DUIControlAnyChangingHandler(object sender, DUIControlAnyChangingEventArgs e);
    public delegate void DUIParentHandler(object sender, DUIParentEventArgs e);
    public delegate void DUIParentLocationChangingHandler(object sender, DUIParentLocationChangingEventArgs e);
    public delegate void DUIParentSizeChangingHandler(object sender, DUIParentSizeChangingEventArgs e);
    public delegate void DUIParentBoundsChangingHandler(object sender, DUIParentBoundsChangingEventArgs e);
    public delegate void DUIParentCenterChangingHandler(object sender, DUIParentCenterChangingEventArgs e);
    public delegate void DUIParentSkewChangingHandler(object sender, DUIParentSkewChangingEventArgs e);
    public delegate void DUIParentScaleChangingHandler(object sender, DUIParentScaleChangingEventArgs e);
    public delegate void DUIParentRotateAngleChangingHandler(object sender, DUIParentRotateAngleChangingEventArgs e);
    public delegate void DUIParentAnyChangingHandler(object sender, DUIParentAnyChangingEventArgs e);
    public delegate void DUILayoutEventHandler(object sender, DUILayoutEventArgs e);
    public delegate void DUILocationChangingEventHandler(object sender, DUILocationChangingEventArgs e);
    public delegate void DUIResizingHandler(object sender, DUISizeChangingEventArgs e);
    public delegate void DUIBoundsChangingEventHandler(object sender, DUIBoundsChangingEventArgs e);
    public delegate void DUISkewChangingHandler(object sender, DUISkewChangingEventArgs e);
    public delegate void DUIScaleChangingHandler(object sender, DUIScaleChangingEventArgs e);
    public delegate void DUICenterChangingHandler(object sender, DUICenterChangingEventArgs e);
    public delegate void DUIRotateAngleChangingHandler(object sender, DUIRotateAngleChangingEventArgs e);
    public delegate void DUIAnyChangingHandler(object sender, DUIAnyChangingEventArgs e);
    public delegate void DUIParentChangingEventHandler(object sender, DUIParentChangingEventArgs e);
    public delegate void DUIPaintEventHandler(object sender, DUIPaintEventArgs e);
    public delegate void DUIMouseEventHandler(object sender, DUIMouseEventArgs e);
    #endregion
    //[AOPProxyAttribute(typeof(AOPMethodsTakesTime))] //记录DUIGraphics函数调用耗时的代理
    public class DUIControl
    {
        #region 静态
        #region 静态属性
        /// <summary> 获取控件的默认字体。 返回结果: 控件的默认 DUIFont。根据用户的操作系统以及系统的本地区域性设置的不同，返回的值也将不同。
        /// </summary>
        public static DUIFont DefaultFont { get { return new DUIFont(Control.DefaultFont.FontFamily, Control.DefaultFont.Size, Control.DefaultFont.Style); } }
        /// <summary> 获取控件的默认背景色。 返回结果:控件的默认背景 System.Drawing.Color。默认为 System.Drawing.SystemColors.Control。
        /// </summary>
        public static Color DefaultBackColor { get { return Control.DefaultBackColor; } }
        /// <summary> 获取控件的默认前景色。 返回结果:控件的默认前景 System.Drawing.Color。默认为 System.Drawing.SystemColors.ControlText。
        /// </summary>
        public static Color DefaultForeColor { get { return Control.DefaultForeColor; } }
        /// <summary> 获取一个值，该值指示哪一个修改键（Shift、Ctrl 和 Alt）处于按下的状态。 返回结果:System.Windows.Forms.Keys 值的按位组合。默认为 System.Windows.Forms.Keys.None。
        /// </summary>
        public static Keys ModifierKeys { get { return Control.ModifierKeys; } }
        /// <summary> 获取一个值，该值指示哪一个鼠标按钮处于按下的状态。 返回结果:System.Windows.Forms.MouseButtons 枚举值的按位组合。默认为 System.Windows.Forms.MouseButtons.None。
        /// </summary>
        public static MouseButtons MouseButtons { get { return Control.MouseButtons; } }
        /// <summary> 获取鼠标光标的位置（以屏幕坐标表示）。 返回结果:一个 System.Drawing.Point，它包含鼠标光标相对于屏幕左上角的坐标。
        /// </summary>
        public static Point MousePosition { get { return Control.MousePosition; } }
        #endregion
        #endregion
        #region 事件
        /// <summary> 在 TabIndex 属性值更改时发生。
        /// </summary>
        public event EventHandler TabIndexChanged;
        /// <summary> 在 Text 属性值更改时发生。
        /// </summary>
        public event EventHandler TextChanged;
        /// <summary> 在 Visible 属性值更改时发生。
        /// </summary>
        public event EventHandler VisibleChanged;
        /// <summary> 在 Enabled 属性值更改时发生。
        /// </summary>
        public event EventHandler EnabledChanged;
        /// <summary> 在 Font 属性值更改时发生。
        /// </summary>
        public event EventHandler FontChanged;
        /// <summary> 在 ForeColor 属性值更改时发生。
        /// </summary>
        public event EventHandler ForeColorChanged;
        /// <summary> 在 BackColor 属性值更改时发生。
        /// </summary>
        public event EventHandler BackColorChanged;
        /// <summary> 在 BackgroundImage 属性值更改时发生。
        /// </summary>
        public event EventHandler BackgroundImageChanged;
        /// <summary> 在 ContextMenuStrip 属性值更改时发生。
        /// </summary>
        public event EventHandler ContextMenuStripChanged;
        /// <summary> 在 Dock 属性值更改时发生。
        /// </summary>
        public event EventHandler DockChanged;
        /// <summary> 在 Anchor 属性值更改时发生。
        /// </summary>
        public event EventHandler AnchorChanged;
        /// <summary> 在为控件创建句柄时发生。
        /// </summary>
        public event EventHandler HandleCreated;
        /// <summary> 在控件应重新定位其子控件时发生。
        /// </summary>
        public event DUILayoutEventHandler Layout;
        /// <summary> 在 Parent 属性值更改时发生。
        /// </summary>
        public event EventHandler ParentChanged;
        /// <summary> 在 Parent 属性值更改时发生。
        /// </summary>
        public event DUIParentChangingEventHandler ParentChanging;
        /// <summary> 当 Region 属性的值更改时发生。
        /// </summary>
        public event EventHandler RegionChanged;
        /// <summary> 在调整控件大小时发生。
        /// </summary>
        public event DUIResizingHandler Resizing;
        /// <summary> 在调整控件大小时发生。
        /// </summary>
        public event EventHandler Resize;
        /// <summary> 在 Location 属性值更改时发生。
        /// </summary>
        public event DUILocationChangingEventHandler LocationChanging;
        /// <summary> 在 Location 属性值更改时发生。
        /// </summary>
        public event EventHandler LocationChanged;
        /// <summary> 在 Bounds 属性值更改时发生。
        /// </summary>
        public event DUIBoundsChangingEventHandler BoundsChanging;
        /// <summary> 在 Bounds 属性值更改时发生。
        /// </summary>
        public event EventHandler BoundsChanged;
        /// <summary> 在 Center 属性值更改时发生。
        /// </summary>
        public event DUICenterChangingHandler CenterChanging;
        /// <summary> 在 Center 属性值更改时发生。
        /// </summary>
        public event EventHandler CenterChanged;
        /// <summary> 在 Skew 属性值更改时发生。
        /// </summary>
        public event DUISkewChangingHandler SkewChanging;
        /// <summary> 在 Skew 属性值更改时发生。
        /// </summary>
        public event EventHandler SkewChanged;
        /// <summary> 在 Scale 属性值更改时发生。
        /// </summary>
        public event DUIScaleChangingHandler ScaleChanging;
        /// <summary> 在 Scale 属性值更改时发生。
        /// </summary>
        public event EventHandler ScaleChanged;
        /// <summary> 在 RotateAngle 属性值更改时发生。
        /// </summary>
        public event DUIRotateAngleChangingHandler RotateAngleChanging;
        /// <summary> 在 RotateAngle 属性值更改时发生。
        /// </summary>
        public event EventHandler RotateAngleChanged;
        /// <summary> 在 RotateAngle 属性值更改时发生。
        /// </summary>
        public event DUIAnyChangingHandler AnyChanging;
        /// <summary> 在 RotateAngle 属性值更改时发生。
        /// </summary>
        public event EventHandler AnyChanged;
        /// <summary> 在控件的显示需要重绘时发生。
        /// </summary>
        public event InvalidateEventHandler Invalidated;
        /// <summary> 在鼠标单击该控件时发生。
        /// </summary>
        public event DUIMouseEventHandler MouseClick;
        /// <summary> 当用鼠标双击控件时发生。
        /// </summary>
        public event DUIMouseEventHandler MouseDoubleClick;
        /// <summary> 当鼠标指针位于控件上并按下鼠标键时发生。
        /// </summary>
        public event DUIMouseEventHandler MouseDown;
        /// <summary> 在鼠标指针进入控件时发生。
        /// </summary>
        public event EventHandler MouseEnter;
        /// <summary> 在鼠标指针停放在控件上时发生。
        /// </summary>
        public event EventHandler MouseHover; //目前这个方法还没有想到合适的方式去实现
        public event EventHandler MouseLeave;
        /// <summary> 在鼠标指针移到控件上时发生。
        /// </summary>
        public event DUIMouseEventHandler MouseMove;
        /// <summary> 在鼠标指针在控件上并释放鼠标键时发生。
        /// </summary>
        public event DUIMouseEventHandler MouseUp;
        /// <summary> 在移动鼠标滚轮并且控件有焦点时发生。
        /// </summary>
        public event DUIMouseEventHandler MouseWheel;
        /// <summary> 在控件有焦点的情况下按下键时发生。
        /// </summary>
        public event KeyEventHandler KeyDown;
        /// <summary> 在控件有焦点的情况下按下键时发生。
        /// </summary>
        public event KeyPressEventHandler KeyPress;
        /// <summary> 在控件有焦点的情况下释放键时发生。
        /// </summary>
        public event KeyEventHandler KeyUp;
        /// <summary> 在控件接收焦点时发生。
        /// </summary>
        public event EventHandler GotFocus;
        /// <summary> 当控件失去焦点时发生。
        /// </summary>
        public event EventHandler LostFocus;
        /// <summary> 在将新控件添加到 DUIControlCollection 时发生。
        /// </summary>
        public event DUIControlHandler ControlAdded;
        /// <summary> 在从 DUIControlCollection 移除控件时发生。
        /// </summary>
        public event DUIControlHandler ControlRemoved;
        /// <summary> 在从 DUIControlCollection 中的控件Location 属性值更改时发生。
        /// </summary>
        public event DUIControlHandler ControlLocationChanged;
        /// <summary> 在从 DUIControlCollection 中的控件Size 属性值更改时发生。
        /// </summary>
        public event DUIControlHandler ControlSizeChanged;
        /// <summary> 在从 DUIControlCollection 中的控件Bounds 属性值更改时发生。
        /// </summary>
        public event DUIControlHandler ControlBoundsChanged;
        /// <summary> 在从 DUIControlCollection 中的控件Center 属性值更改时发生。
        /// </summary>
        public event DUIControlHandler ControlCenterChanged;
        /// <summary> 在从 DUIControlCollection 中的控件Skew 属性值更改时发生。
        /// </summary>
        public event DUIControlHandler ControlSkewChanged;
        /// <summary> 在从 DUIControlCollection 中的控件Scale 属性值更改时发生。
        /// </summary>
        public event DUIControlHandler ControlScaleChanged;
        /// <summary> 在从 DUIControlCollection 中的控件RotateAngle 属性值更改时发生。
        /// </summary>
        public event DUIControlHandler ControlRotateAngleChanged;
        /// <summary> 在从 DUIControlCollection 中的控件任何(Bounds、Center、RotateAngle)属性值更改时发生。
        /// </summary>
        public event DUIControlHandler ControlAnyChanged;
        /// <summary> 在从 DUIControlCollection 中的控件Location 属性值更改时发生。
        /// </summary>
        public event DUIControlLocationChangingHandler ControlLocationChanging;
        /// <summary> 在从 DUIControlCollection 中的控件Size 属性值更改时发生。
        /// </summary>
        public event DUIControlSizeChangingHandler ControlSizeChanging;
        /// <summary> 在从 DUIControlCollection 中的控件Bounds 属性值更改时发生。
        /// </summary>
        public event DUIControlBoundsChangingHandler ControlBoundsChanging;
        /// <summary> 在从 DUIControlCollection 中的控件Center 属性值更改时发生。
        /// </summary>
        public event DUIControlCenterChangingHandler ControlCenterChanging;
        /// <summary> 在从 DUIControlCollection 中的控件Skew 属性值更改时发生。
        /// </summary>
        public event DUIControlSkewChangingHandler ControlSkewChanging;
        /// <summary> 在从 DUIControlCollection 中的控件Scale 属性值更改时发生。
        /// </summary>
        public event DUIControlScaleChangingHandler ControlScaleChanging;
        /// <summary> 在从 DUIControlCollection 中的控件RotateAngle 属性值更改时发生。
        /// </summary>
        public event DUIControlRotateAngleChangingHandler ControlRotateAngleChanging;
        /// <summary> 在从 DUIControlCollection 中的控件任何(Bounds、Center、RotateAngle)属性值更改时发生。
        /// </summary>
        public event DUIControlAnyChangingHandler ControlAnyChanging;
        /// <summary> 在从 DUIParentCollection 中的控件Location 属性值更改时发生。
        /// </summary>
        public event DUIParentHandler ParentLocationChanged;
        /// <summary> 在从 DUIParentCollection 中的控件Size 属性值更改时发生。
        /// </summary>
        public event DUIParentHandler ParentSizeChanged;
        /// <summary> 在从 DUIParentCollection 中的控件Bounds 属性值更改时发生。
        /// </summary>
        public event DUIParentHandler ParentBoundsChanged;
        /// <summary> 在从 DUIParentCollection 中的控件Center 属性值更改时发生。
        /// </summary>
        public event DUIParentHandler ParentCenterChanged;
        /// <summary> 在从 DUIParentCollection 中的控件Skew 属性值更改时发生。
        /// </summary>
        public event DUIParentHandler ParentSkewChanged;
        /// <summary> 在从 DUIParentCollection 中的控件Scale 属性值更改时发生。
        /// </summary>
        public event DUIParentHandler ParentScaleChanged;
        /// <summary> 在从 DUIParentCollection 中的控件RotateAngle 属性值更改时发生。
        /// </summary>
        public event DUIParentHandler ParentRotateAngleChanged;
        /// <summary> 在从 DUIParentCollection 中的控件任何(Bounds、Center、RotateAngle)属性值更改时发生。
        /// </summary>
        public event DUIParentHandler ParentAnyChanged;
        /// <summary> 在从 DUIParentCollection 中的控件Location 属性值更改时发生。
        /// </summary>
        public event DUIParentLocationChangingHandler ParentLocationChanging;
        /// <summary> 在从 DUIParentCollection 中的控件Size 属性值更改时发生。
        /// </summary>
        public event DUIParentSizeChangingHandler ParentSizeChanging;
        /// <summary> 在从 DUIParentCollection 中的控件Bounds 属性值更改时发生。
        /// </summary>
        public event DUIParentBoundsChangingHandler ParentBoundsChanging;
        /// <summary> 在从 DUIParentCollection 中的控件Center 属性值更改时发生。
        /// </summary>
        public event DUIParentCenterChangingHandler ParentCenterChanging;
        /// <summary> 在从 DUIParentCollection 中的控件Skew 属性值更改时发生。
        /// </summary>
        public event DUIParentSkewChangingHandler ParentSkewChanging;
        /// <summary> 在从 DUIParentCollection 中的控件Scale 属性值更改时发生。
        /// </summary>
        public event DUIParentScaleChangingHandler ParentScaleChanging;
        /// <summary> 在从 DUIParentCollection 中的控件RotateAngle 属性值更改时发生。
        /// </summary>
        public event DUIParentRotateAngleChangingHandler ParentRotateAngleChanging;
        /// <summary> 在从 DUIParentCollection 中的控件任何(Bounds、Center、RotateAngle)属性值更改时发生。
        /// </summary>
        public event DUIParentAnyChangingHandler ParentAnyChanging;
        /// <summary> 在重绘控件时发生。
        /// </summary>
        public event DUIPaintEventHandler Paint;
        /// <summary> 在重绘控件背景时发生。
        /// </summary>
        public event DUIPaintEventHandler PaintBackground;
        /// <summary> 在所有绘图操作完成之后发生,此绘图在子控件之上
        /// </summary>
        public event DUIPaintEventHandler PaintForeground;
        public virtual void OnTabIndexChanged(EventArgs e)
        {
            if (TabIndexChanged != null)
            {
                TabIndexChanged(this, e);
            }
        }
        public virtual void OnTextChanged(EventArgs e)
        {
            if (TextChanged != null)
            {
                TextChanged(this, e);
            }
        }
        public virtual void OnVisibleChanged(EventArgs e)
        {
            if (VisibleChanged != null)
            {
                VisibleChanged(this, e);
            }
        }
        public virtual void OnEnabledChanged(EventArgs e)
        {
            if (EnabledChanged != null)
            {
                EnabledChanged(this, e);
            }
        }
        public virtual void OnFontChanged(EventArgs e)
        {
            if (FontChanged != null)
            {
                FontChanged(this, e);
            }
        }
        public virtual void OnForeColorChanged(EventArgs e)
        {
            if (ForeColorChanged != null)
            {
                ForeColorChanged(this, e);
            }
        }
        public virtual void OnBackColorChanged(EventArgs e)
        {
            if (BackColorChanged != null)
            {
                BackColorChanged(this, e);
            }
        }
        public virtual void OnBackgroundImageChanged(EventArgs e)
        {
            if (BackgroundImageChanged != null)
            {
                BackgroundImageChanged(this, e);
            }
        }
        public virtual void OnContextMenuStripChanged(EventArgs e)
        {
            if (ContextMenuStripChanged != null)
            {
                ContextMenuStripChanged(this, e);
            }
        }
        public virtual void OnDockChanged(EventArgs e)
        {
            if (DockChanged != null)
            {
                DockChanged(this, e);
            }
        }
        public virtual void OnAnchorChanged(EventArgs e)
        {
            if (AnchorChanged != null)
            {
                AnchorChanged(this, e);
            }
        }
        public virtual void OnHandleCreated(EventArgs e)
        {
            if (HandleCreated != null)
            {
                HandleCreated(this, e);
            }
        }
        public virtual void OnLayout()
        {
            if (Layout != null)
            {
                Layout(this, null);
            }
        }
        public virtual void OnParentChanged(EventArgs e)
        {
            if (ParentChanged != null)
            {
                ParentChanged(this, e);
            }
        }
        public virtual void OnParentChanging(DUIParentChangingEventArgs e)
        {
            if (ParentChanging != null)
            {
                ParentChanging(this, e);
            }
        }
        public virtual void OnRegionChanged(EventArgs e)
        {
            if (RegionChanged != null)
            {
                RegionChanged(this, e);
            }
        }
        public virtual void OnResizing(DUISizeChangingEventArgs e)
        {
            if (Resizing != null)
            {
                Resizing(this, e);
            }
        }
        public virtual void OnResize(EventArgs e)
        {
            OnLayout();
            if (Resize != null)
            {
                Resize(this, e);
            }
        }
        public virtual void OnLocationChanging(DUILocationChangingEventArgs e)
        {
            if (LocationChanging != null)
            {
                LocationChanging(this, e);
            }
        }
        public virtual void OnLocationChanged(EventArgs e)
        {
            if (LocationChanged != null)
            {
                LocationChanged(this, e);
            }
        }
        public virtual void OnBoundsChanging(DUIBoundsChangingEventArgs e)
        {
            if (BoundsChanging != null)
            {
                BoundsChanging(this, e);
            }
        }
        public virtual void OnBoundsChanged(EventArgs e)
        {
            if (BoundsChanged != null)
            {
                BoundsChanged(this, e);
            }
        }
        public virtual void OnCenterChanging(DUICenterChangingEventArgs e)
        {
            if (CenterChanging != null)
            {
                CenterChanging(this, e);
            }
        }
        public virtual void OnCenterChanged(EventArgs e)
        {
            if (CenterChanged != null)
            {
                CenterChanged(this, e);
            }
        }
        public virtual void OnSkewChanging(DUISkewChangingEventArgs e)
        {
            if (SkewChanging != null)
            {
                SkewChanging(this, e);
            }
        }
        public virtual void OnSkewChanged(EventArgs e)
        {
            if (SkewChanged != null)
            {
                SkewChanged(this, e);
            }
        }
        public virtual void OnScaleChanging(DUIScaleChangingEventArgs e)
        {
            if (ScaleChanging != null)
            {
                ScaleChanging(this, e);
            }
        }
        public virtual void OnScaleChanged(EventArgs e)
        {
            if (ScaleChanged != null)
            {
                ScaleChanged(this, e);
            }
        }
        public virtual void OnRotateAngleChanging(DUIRotateAngleChangingEventArgs e)
        {
            if (RotateAngleChanging != null)
            {
                RotateAngleChanging(this, e);
            }
        }
        public virtual void OnRotateAngleChanged(EventArgs e)
        {
            if (RotateAngleChanged != null)
            {
                RotateAngleChanged(this, e);
            }
        }
        public virtual void OnAnyChanging(DUIAnyChangingEventArgs e)
        {
            if (AnyChanging != null)
            {
                AnyChanging(this, e);
            }
        }
        public virtual void OnAnyChanged(EventArgs e)
        {
            if (AnyChanged != null)
            {
                AnyChanged(this, e);
            }
        }
        public virtual void OnInvalidated(InvalidateEventArgs e)
        {
            if (Invalidated != null)
            {
                Invalidated(this, e);
            }
        }
        public virtual void OnMouseEnter(EventArgs e)
        {
            if (MouseEnter != null)
            {
                MouseEnter(this, e);
            }
        }
        public virtual void OnMouseHover(EventArgs e)
        {
            if (MouseHover != null)
            {
                MouseHover(this, e);
            }
        }
        public virtual void OnMouseLeave(EventArgs e)
        {
            if (MouseLeave != null)
            {
                MouseLeave(this, e);
            }
        }
        public virtual void OnMouseClick(DUIMouseEventArgs e)
        {
            if (MouseClick != null)
            {
                MouseClick(this, e);
            }
        }
        /// <summary> 只要鼠标操作进入范围都会触发鼠标点击
        /// </summary>
        /// <param name="e">鼠标事件</param>
        protected virtual void AlwaysMouseClick(DUIMouseEventArgs e)
        {
        }
        public virtual void OnMouseDoubleClick(DUIMouseEventArgs e)
        {
            if (MouseDoubleClick != null)
            {
                MouseDoubleClick(this, e);
            }
        }
        /// <summary> 只要鼠标操作进入范围都会触发鼠标双击
        /// </summary>
        /// <param name="e">鼠标事件</param>
        protected virtual void AlwaysMouseDoubleClick(DUIMouseEventArgs e)
        {
        }
        public virtual void OnMouseDown(DUIMouseEventArgs e)
        {
            if (MouseDown != null)
            {
                MouseDown(this, e);
            }
        }
        /// <summary> 只要鼠标操作进入范围都会触发鼠标按下
        /// </summary>
        /// <param name="e">鼠标事件</param>
        protected virtual void AlwaysMouseDown(DUIMouseEventArgs e)
        {
        }
        public virtual void OnMouseMove(DUIMouseEventArgs e)
        {
            if (MouseMove != null)
            {
                MouseMove(this, e);
            }
        }
        /// <summary> 只要鼠标操作进入范围都会触发鼠标移动
        /// </summary>
        /// <param name="e">鼠标事件</param>
        protected virtual void AlwaysMouseMove(DUIMouseEventArgs e)
        {
        }
        public virtual void OnMouseUp(DUIMouseEventArgs e)
        {
            if (MouseUp != null)
            {
                MouseUp(this, e);
            }
        }
        /// <summary> 只要鼠标操作进入范围都会触发鼠标弹起
        /// </summary>
        /// <param name="e">鼠标事件</param>
        protected virtual void AlwaysMouseUp(DUIMouseEventArgs e)
        {
        }
        public virtual void OnMouseWheel(DUIMouseEventArgs e)
        {
            if (MouseWheel != null)
            {
                MouseWheel(this, e);
            }
        }
        /// <summary> 只要鼠标操作进入范围都会触发鼠标滚动
        /// </summary>
        /// <param name="e">鼠标事件</param>
        protected virtual void AlwaysMouseWheel(DUIMouseEventArgs e)
        {
        }
        public virtual void OnKeyDown(KeyEventArgs e)
        {
            if (KeyDown != null)
            {
                KeyDown(this, e);
            }
        }
        public virtual void OnKeyPress(KeyPressEventArgs e)
        {
            if (KeyPress != null)
            {
                KeyPress(this, e);
            }
        }
        public virtual void OnKeyUp(KeyEventArgs e)
        {
            if (KeyUp != null)
            {
                KeyUp(this, e);
            }
        }
        public virtual void OnGotFocus(EventArgs e)
        {
            if (GotFocus != null)
            {
                GotFocus(this, e);
            }
        }
        public virtual void OnLostFocus(EventArgs e)
        {
            if (LostFocus != null)
            {
                LostFocus(this, e);
            }
        }
        public virtual void OnControlAdded(DUIControlEventArgs e)
        {
            if (ControlAdded != null)
            {
                ControlAdded(this, e);
            }
        }
        public virtual void OnControlRemoved(DUIControlEventArgs e)
        {
            if (ControlRemoved != null)
            {
                ControlRemoved(this, e);
            }
        }
        public virtual void OnControlLocationChanged(DUIControlEventArgs e)
        {
            if (ControlLocationChanged != null)
            {
                ControlLocationChanged(this, e);
            }
        }
        public virtual void OnControlSizeChanged(DUIControlEventArgs e)
        {
            if (ControlSizeChanged != null)
            {
                ControlSizeChanged(this, e);
            }
        }
        public virtual void OnControlBoundsChanged(DUIControlEventArgs e)
        {
            if (ControlBoundsChanged != null)
            {
                ControlBoundsChanged(this, e);
            }
        }
        public virtual void OnControlCenterChanged(DUIControlEventArgs e)
        {
            if (ControlCenterChanged != null)
            {
                ControlCenterChanged(this, e);
            }
        }
        public virtual void OnControlSkewChanged(DUIControlEventArgs e)
        {
            if (ControlSkewChanged != null)
            {
                ControlSkewChanged(this, e);
            }
        }
        public virtual void OnControlScaleChanged(DUIControlEventArgs e)
        {
            if (ControlScaleChanged != null)
            {
                ControlScaleChanged(this, e);
            }
        }
        public virtual void OnControlRotateAngleChanged(DUIControlEventArgs e)
        {
            if (ControlRotateAngleChanged != null)
            {
                ControlRotateAngleChanged(this, e);
            }
        }
        public virtual void OnControlAnyChanged(DUIControlEventArgs e)
        {
            if (ControlAnyChanged != null)
            {
                ControlAnyChanged(this, e);
            }
        }
        public virtual void OnControlLocationChanging(DUIControlLocationChangingEventArgs e)
        {
            if (ControlLocationChanging != null)
            {
                ControlLocationChanging(this, e);
            }
        }
        public virtual void OnControlSizeChanging(DUIControlSizeChangingEventArgs e)
        {
            if (ControlSizeChanging != null)
            {
                ControlSizeChanging(this, e);
            }
        }
        public virtual void OnControlBoundsChanging(DUIControlBoundsChangingEventArgs e)
        {
            if (ControlBoundsChanging != null)
            {
                ControlBoundsChanging(this, e);
            }
        }
        public virtual void OnControlCenterChanging(DUIControlCenterChangingEventArgs e)
        {
            if (ControlCenterChanging != null)
            {
                ControlCenterChanging(this, e);
            }
        }
        public virtual void OnControlSkewChanging(DUIControlSkewChangingEventArgs e)
        {
            if (ControlSkewChanging != null)
            {
                ControlSkewChanging(this, e);
            }
        }
        public virtual void OnControlScaleChanging(DUIControlScaleChangingEventArgs e)
        {
            if (ControlScaleChanging != null)
            {
                ControlScaleChanging(this, e);
            }
        }
        public virtual void OnControlRotateAngleChanging(DUIControlRotateAngleChangingEventArgs e)
        {
            if (ControlRotateAngleChanging != null)
            {
                ControlRotateAngleChanging(this, e);
            }
        }
        public virtual void OnControlAnyChanging(DUIControlAnyChangingEventArgs e)
        {
            if (ControlAnyChanging != null)
            {
                ControlAnyChanging(this, e);
            }
        }
        public virtual void OnParentLocationChanged(DUIParentEventArgs e)
        {
            if (ParentLocationChanged != null)
            {
                ParentLocationChanged(this, e);
            }
        }
        public virtual void OnParentSizeChanged(DUIParentEventArgs e)
        {
            if (ParentSizeChanged != null)
            {
                ParentSizeChanged(this, e);
            }
        }
        public virtual void OnParentBoundsChanged(DUIParentEventArgs e)
        {
            if (ParentBoundsChanged != null)
            {
                ParentBoundsChanged(this, e);
            }
        }
        public virtual void OnParentCenterChanged(DUIParentEventArgs e)
        {
            if (ParentCenterChanged != null)
            {
                ParentCenterChanged(this, e);
            }
        }
        public virtual void OnParentSkewChanged(DUIParentEventArgs e)
        {
            if (ParentSkewChanged != null)
            {
                ParentSkewChanged(this, e);
            }
        }
        public virtual void OnParentScaleChanged(DUIParentEventArgs e)
        {
            if (ParentScaleChanged != null)
            {
                ParentScaleChanged(this, e);
            }
        }
        public virtual void OnParentRotateAngleChanged(DUIParentEventArgs e)
        {
            if (ParentRotateAngleChanged != null)
            {
                ParentRotateAngleChanged(this, e);
            }
        }
        public virtual void OnParentAnyChanged(DUIParentEventArgs e)
        {
            if (ParentAnyChanged != null)
            {
                ParentAnyChanged(this, e);
            }
        }
        public virtual void OnParentLocationChanging(DUIParentLocationChangingEventArgs e)
        {
            if (ParentLocationChanging != null)
            {
                ParentLocationChanging(this, e);
            }
        }
        public virtual void OnParentSizeChanging(DUIParentSizeChangingEventArgs e)
        {
            if (ParentSizeChanging != null)
            {
                ParentSizeChanging(this, e);
            }
        }
        public virtual void OnParentBoundsChanging(DUIParentBoundsChangingEventArgs e)
        {
            if (ParentBoundsChanging != null)
            {
                ParentBoundsChanging(this, e);
            }
        }
        public virtual void OnParentCenterChanging(DUIParentCenterChangingEventArgs e)
        {
            if (ParentCenterChanging != null)
            {
                ParentCenterChanging(this, e);
            }
        }
        public virtual void OnParentSkewChanging(DUIParentSkewChangingEventArgs e)
        {
            if (ParentSkewChanging != null)
            {
                ParentSkewChanging(this, e);
            }
        }
        public virtual void OnParentScaleChanging(DUIParentScaleChangingEventArgs e)
        {
            if (ParentScaleChanging != null)
            {
                ParentScaleChanging(this, e);
            }
        }
        public virtual void OnParentRotateAngleChanging(DUIParentRotateAngleChangingEventArgs e)
        {
            if (ParentRotateAngleChanging != null)
            {
                ParentRotateAngleChanging(this, e);
            }
        }
        public virtual void OnParentAnyChanging(DUIParentAnyChangingEventArgs e)
        {
            if (ParentAnyChanging != null)
            {
                ParentAnyChanging(this, e);
            }
        }
        public virtual void OnPaint(DUIPaintEventArgs e)
        {
            if (this.HFlipping || this.VFlipping)
            {
                e.Graphics.TranslateTransform(this.HFlipping ? this.ClientSize.Width / 2 : 0, this.VFlipping ? this.ClientSize.Height / 2 : 0);
                e.Graphics.ScaleTransform(this.HFlipping ? -1 : 1, this.VFlipping ? -1 : 1);
                e.Graphics.TranslateTransform(this.HFlipping ? -this.ClientSize.Width / 2 : 0, this.VFlipping ? -this.ClientSize.Height / 2 : 0);
            }
            if (Paint != null)
            {
                Paint(this, e);
            }
        }
        public virtual void OnPaintBackground(DUIPaintEventArgs e)
        {
            if (this.HFlipping || this.VFlipping)
            {
                e.Graphics.TranslateTransform(this.HFlipping ? this.Width / 2 : 0, this.VFlipping ? this.Height / 2 : 0);
                e.Graphics.ScaleTransform(this.HFlipping ? -1 : 1, this.VFlipping ? -1 : 1);
                e.Graphics.TranslateTransform(this.HFlipping ? -this.Width / 2 : 0, this.VFlipping ? -this.Height / 2 : 0);
            }
            using (DUIPen borderPen = new DUIPen(this.Border.BorderColor, this.BorderWidth))
            {
                e.Graphics.DrawRectangle(borderPen, new RectangleF(this.BorderWidth / 2F, this.BorderWidth / 2F, this.Width - this.BorderWidth, this.Height - this.BorderWidth));
            }
            using (DUISolidBrush backBrush = new DUISolidBrush(this.BackColor))
            {
                e.Graphics.FillRectangle(backBrush, new RectangleF(this.BorderWidth, this.BorderWidth, this.ClientSize.Width, this.ClientSize.Height));
            }
            if (this.BackgroundImage != null)
            {
                e.Graphics.DrawImage(this.BackgroundImage, new RectangleF(this.BorderWidth, this.BorderWidth, this.ClientSize.Width, this.ClientSize.Height));
            }
            if (PaintBackground != null)
            {
                PaintBackground(this, e);
            }
        }
        public virtual void OnPaintForeground(DUIPaintEventArgs e)
        {
            if (this.HFlipping || this.VFlipping)
            {
                e.Graphics.TranslateTransform(this.HFlipping ? this.Width / 2 : 0, this.VFlipping ? this.Height / 2 : 0);
                e.Graphics.ScaleTransform(this.HFlipping ? -1 : 1, this.VFlipping ? -1 : 1);
                e.Graphics.TranslateTransform(this.HFlipping ? -this.Width / 2 : 0, this.VFlipping ? -this.Height / 2 : 0);
            }
            if (PaintForeground != null)
            {
                PaintForeground(this, e);
            }
        }
        protected virtual void WndProc(ref Message m)
        {

        }
        protected virtual void DefWndProc(ref Message m)
        {

        }
        #endregion
        #region 私有变量
        private bool canFocus = true;
        private bool canMouseThrough = false;
        private bool canMouseSelected = true;
        private int layoutSuspendCount = 0;
        private bool hFlipping = false;
        private bool vFlipping = false;
        internal float x = 0;
        internal float y = 0;
        internal float centerX = 0;
        internal float centerY = 0;
        internal float rotate = 0;
        internal float width = 100;
        internal float height = 100;
        internal float skewX = 0;
        internal float skewY = 0;
        internal float scaleX = 1;
        internal float scaleY = 1;
        private int tabIndex = -1;
        private bool visible = true;
        private bool enabled = true;
        private bool isHandleCreated = false;
        internal Color borderColor = Color.Black;
        internal float borderWidth = 0;
        private AnchorStyles anchor = AnchorStyles.Left | AnchorStyles.Top;
        private DockStyle dock = DockStyle.None;
        private DUIFont font = null;
        private Color foreColor = DefaultForeColor;
        private Color backColor = DefaultBackColor;
        private DUIImage backgroundImage = null;
        private DUIControl dUIParent = null;
        protected DUIControlCollection dUIControls = null;
        private string text = string.Empty;
        private string name = string.Empty;
        private Region region = new Region();
        private float radius = 0;
        private static readonly object syncRoot = new object();
        private Queue<Action> loadAction = new Queue<Action>();
        #endregion
        #region 属性
        /// <summary> 是否水平翻转
        /// </summary>
        public bool HFlipping
        {
            get
            {
                return hFlipping;
            }
            set
            {
                if (hFlipping != value)
                {
                    hFlipping = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary> 是否垂直翻转
        /// </summary>
        public bool VFlipping
        {
            get
            {
                return vFlipping;
            }
            set
            {
                if (vFlipping != value)
                {
                    vFlipping = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary> 获取当前控件的矩阵
        /// </summary>
        public DUIMatrix Matrix
        {
            get
            {
                DUIMatrix matrix = new DUIMatrix();
                matrix.Rotate(this.Rotate);
                matrix.Skew(this.SkewX, this.SkewY);
                matrix.Scale(this.ScaleX, this.ScaleY);
                return matrix;
            }
        }
        /// <summary> 获取当前控件的逆向矩阵
        /// </summary>
        public DUIMatrix MatrixReverse
        {
            get
            {
                DUIMatrix matrix = new DUIMatrix();
                matrix.Scale(1 / this.ScaleX, 1 / this.ScaleY);
                matrix.Skew(-this.SkewX, -this.SkewY);
                float sx = (float)Math.Cos(Math.Atan(this.SkewY));
                float sy = (float)Math.Cos(Math.Atan(this.SkewX));
                //matrix.Scale(sx, sy);
                var s = (float)(-this.SkewX * this.SkewY) + 1;
                matrix.Scale(1 / s, 1 / s);
                matrix.Rotate(-this.Rotate);
                return matrix;
            }
        }
        internal virtual bool CanLayout
        {
            get
            {
                return this.DUIParent == null ? (layoutSuspendCount == 0) : (layoutSuspendCount == 0) && this.DUIParent.CanLayout;
            }
        }
        public virtual IntPtr Handle
        {
            get
            {
                if (this.DUIParent == null) { return IntPtr.Zero; }
                return this.DUIParent.Handle;
            }
        }
        #region 设置边界
        /// <summary> X坐标
        /// </summary>
        public virtual float X
        {
            get
            {
                return x;
            }
            set
            {
                SetBounds(value, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, DUIBoundsSpecified.X);
            }
        }
        /// <summary> Y坐标
        /// </summary>
        public virtual float Y
        {
            get
            {
                return y;
            }
            set
            {
                SetBounds(0, value, 0, 0, 0, 0, 0, 0, 0, 0, 0, DUIBoundsSpecified.Y);
            }
        }
        /// <summary> 宽度
        /// </summary>
        public virtual float Width
        {
            get
            {
                return this.width;
            }
            set
            {
                SetBounds(0, 0, value, 0, 0, 0, 0, 0, 0, 0, 0, DUIBoundsSpecified.Width);
            }
        }
        /// <summary> 高度
        /// </summary>
        public virtual float Height
        {
            get
            {
                return this.height;
            }
            set
            {
                SetBounds(0, 0, 0, value, 0, 0, 0, 0, 0, 0, 0, DUIBoundsSpecified.Height);
            }
        }
        /// <summary> 切变值，与X轴的角度（弧度）
        /// </summary>
        public float SkewX
        {
            get { return skewX; }
            set
            {
                SetBounds(0, 0, 0, 0, 0, 0, 0, value, 0, 0, 0, DUIBoundsSpecified.SkewX);
            }
        }
        /// <summary> 切变值，与Y轴的角度（弧度）
        /// </summary>
        public float SkewY
        {
            get { return skewY; }
            set
            {
                SetBounds(0, 0, 0, 0, 0, 0, 0, 0, value, 0, 0, DUIBoundsSpecified.SkewY);
            }
        }
        /// <summary> 倾斜
        /// </summary>
        public virtual PointF Skew
        {
            get { return new PointF(SkewX, SkewY); }
            set
            {
                SetBounds(0, 0, 0, 0, 0, 0, 0, value.X, value.Y, 0, 0, DUIBoundsSpecified.Skew);
            }
        }
        public virtual float ScaleX
        {
            get { return scaleX; }
            set
            {
                SetBounds(0, 0, 0, 0, 0, 0, 0, 0, 0, value, 0, DUIBoundsSpecified.ScaleX);
            }
        }
        public virtual float ScaleY
        {
            get { return scaleY; }
            set
            {
                SetBounds(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, value, DUIBoundsSpecified.ScaleY);
            }
        }
        /// <summary> 缩放
        /// </summary>
        public virtual PointF Scale
        {
            get { return new PointF(ScaleX, ScaleY); }
            set
            {
                SetBounds(0, 0, 0, 0, 0, 0, 0, 0, 0, value.X, value.Y, DUIBoundsSpecified.Scale);
            }
        }
        /// <summary> 位置
        /// </summary>
        public virtual PointF Location
        {
            get { return new PointF(X, Y); }
            set
            {
                SetBounds(value.X, value.Y, 0, 0, 0, 0, 0, 0, 0, 0, 0, DUIBoundsSpecified.Location);
            }
        }
        /// <summary> 尺寸
        /// </summary>
        public virtual SizeF Size
        {
            get { return new SizeF(Width, Height); }
            set
            {
                SetBounds(0, 0, value.Width, value.Height, 0, 0, 0, 0, 0, 0, 0, DUIBoundsSpecified.Size);
            }
        }
        /// <summary> 边界
        /// </summary>
        public virtual RectangleF Bounds
        {
            get { return new RectangleF(Location, Size); }
            set
            {
                SetBounds(value.X, value.Y, value.Width, value.Height, 0, 0, 0, 0, 0, 0, 0, DUIBoundsSpecified.Bounds);
            }
        }
        /// <summary> centerX坐标
        /// </summary>
        public virtual float CenterX
        {
            get
            {
                return centerX;
            }
            set
            {
                SetBounds(0, 0, 0, 0, value, 0, 0, 0, 0, 0, 0, DUIBoundsSpecified.CenterX);
            }
        }
        /// <summary> CenterY坐标
        /// </summary>
        public virtual float CenterY
        {
            get
            {
                return centerY;
            }
            set
            {
                SetBounds(0, 0, 0, 0, 0, value, 0, 0, 0, 0, 0, DUIBoundsSpecified.CenterY);
            }
        }
        /// <summary> 旋转中心点
        /// </summary>
        public virtual PointF Center
        {
            get { return new PointF(CenterX, CenterY); }
            set
            {
                SetBounds(0, 0, 0, 0, value.X, value.Y, 0, 0, 0, 0, 0, DUIBoundsSpecified.Center);
            }
        }
        /// <summary> 旋转中心点2
        /// </summary>
        public virtual PointF Center2
        {
            get { return PointF.Empty; }
            set
            {
            }
        }
        /// <summary> 旋转角度
        /// </summary>
        public virtual float Rotate
        {
            get { return rotate; }
            set
            {
                SetBounds(0, 0, 0, 0, 0, 0, value, 0, 0, 0, 0, DUIBoundsSpecified.Rotate);
            }
        }
        public virtual float Left
        {
            get
            {
                return this.X;
            }
        }
        public virtual float Top
        {
            get
            {
                return this.Y;
            }
        }
        public virtual float Right
        {
            get
            {
                return this.X + this.Width;
            }
        }
        public virtual float Bottom
        {
            get
            {
                return this.Y + this.Height;
            }
        }
        #endregion
        /// <summary> 获取或设置一个值，该值指示控件是否可以被鼠标穿透
        /// </summary>
        public virtual bool CanMouseThrough
        {
            get { return canMouseThrough; }
            set { canMouseThrough = value; }
        }
        /// <summary> 获取或设置一个值，该值指示控件是否可以被鼠标选中
        /// </summary>
        public virtual bool CanMouseSelected
        {
            get { return canMouseSelected; }
            set { canMouseSelected = value; }
        }
        public int TabIndex
        {
            get { return tabIndex == -1 ? 0 : tabIndex; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("TabIndex");
                }
                if (tabIndex != value)
                {
                    tabIndex = value;
                    OnTabIndexChanged(EventArgs.Empty);
                }
            }
        }
        public virtual string Text
        {
            get
            {
                return (text == null) ? "" : text;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                if (value == Text)
                {
                    return;
                }
                if (text != value)
                {
                    text = value;
                    this.Invalidate();
                    OnTextChanged(EventArgs.Empty);
                }
            }
        }
        public virtual string ToolTip { get; set; }
        public virtual string Name
        {
            get
            {
                return (name == null) ? "" : name;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                if (value == Name)
                {
                    return;
                }
                name = value;
            }
        }
        /// <summary> 边框颜色
        /// </summary>
        public virtual Color BorderColor
        {
            get { return borderColor; }
            set
            {
                if (borderColor != value)
                {
                    borderColor = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary> 边框宽度
        /// </summary>
        public virtual float BorderWidth
        {
            get { return borderWidth; }
            set
            {
                if (borderWidth != value)
                {
                    float offset = borderWidth - value;
                    borderWidth = value;
                    this.Invalidate();
                }
            }
        }
        public virtual DUIBorder Border
        {
            get { return new DUIBorder(BorderColor, BorderWidth); }
            set
            {
                if (borderColor != value.BorderColor || borderWidth != value.BorderWidth)
                {
                    borderColor = value.BorderColor;
                    borderWidth = value.BorderWidth;
                    this.Invalidate();
                }
            }
        }
        public virtual bool Visible
        {
            get
            {
                //if (this.Width == 0 || this.Height == 0)
                //{
                //    return false;
                //}
                return visible;
            }
            set
            {
                if (visible != value)
                {
                    visible = value;
                    if (this.DUIParent != null)
                    {
                        LayoutEngine.DoLayoutDock(new DUILayoutEventArgs(this.DUIParent, "Visible"));
                    }
                    this.Invalidate();
                    OnVisibleChanged(EventArgs.Empty);
                }
            }
        }
        public virtual bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                if (enabled != value)
                {
                    enabled = value;
                    this.Invalidate();
                    OnEnabledChanged(EventArgs.Empty);
                }
            }
        }
        public bool IsHandleCreated { get { return this.isHandleCreated; } }
        public AnchorStyles Anchor
        {
            get { return anchor; }
            set
            {
                if (anchor != value)
                {
                    anchor = value;
                    dock = DockStyle.None;
                    if (this.DUIParent != null)
                    {
                        LayoutEngine.DoLayoutDock(new DUILayoutEventArgs(this.DUIParent, "Anchor"));
                    }
                    OnAnchorChanged(EventArgs.Empty);
                }
            }
        }
        public DockStyle Dock
        {
            get { return dock; }
            set
            {
                if (dock != value)
                {
                    dock = value;
                    switch (value)
                    {
                        case DockStyle.None:
                            break;
                        default:
                            anchor = AnchorStyles.Left | AnchorStyles.Top;
                            break;
                    }
                    if (this.DUIParent != null)
                    {
                        LayoutEngine.DoLayoutDock(new DUILayoutEventArgs(this.DUIParent, "Dock"));
                    }
                    OnDockChanged(EventArgs.Empty);
                }
            }
        }
        public virtual DUIFont Font
        {
            get
            {
                if (font == null)
                {
                    if (this.DUIParent == null)
                    {
                        return DefaultFont;
                    }
                    return this.DUIParent.Font;
                }
                return font;
            }
            set
            {
                if (font != value)
                {
                    font = value;
                    this.Invalidate();
                    OnFontChanged(EventArgs.Empty);
                }
            }
        }
        public virtual Color ForeColor
        {
            get { return foreColor; }
            set
            {
                if (foreColor != value)
                {
                    foreColor = value;
                    this.Invalidate();
                    OnForeColorChanged(EventArgs.Empty);
                }
            }
        }
        public virtual Color BackColor
        {
            get { return backColor; }
            set
            {
                if (backColor != value)
                {
                    backColor = value;
                    this.Invalidate();
                    OnBackColorChanged(EventArgs.Empty);
                }
            }
        }
        public virtual DUIImage BackgroundImage
        {
            get { return backgroundImage; }
            set
            {
                if (backgroundImage != value)
                {
                    backgroundImage = value;
                    this.Invalidate();
                    OnBackgroundImageChanged(EventArgs.Empty);
                }
            }
        }
        public virtual bool CanFocus
        {
            get
            {
                if (!IsHandleCreated) { return false; }
                return (canFocus && Visible && Enabled);
            }
            set { canFocus = value; }
        }
        /// <summary> 该控件是否有焦点
        /// </summary>
        public bool Focused
        {
            get
            {
                if (this.DUIControlShare == null) { return false; }
                return this.DUIControlShare.FocusedDUIControl == this;
            }
        }
        /// <summary> 该控件或者子控件是否有焦点
        /// </summary>
        public bool AnyFocused
        {
            get
            {
                if (this.DUIControlShare == null) { return false; }
                return this.DUIControlShare.FocusedDUIControl == this ||
                    GetAllChild(true).Contains(this.DUIControlShare.FocusedDUIControl);
            }
        }
        public bool TopMost
        {
            get
            {
                if (this.DUIControlShare == null)
                {
                    return false;
                }
                return this.DUIControlShare.TopMostDUIControl == this;
            }
            set
            {
                if (value)
                {
                    if (this.DUIControlShare != null)
                    {
                        DUIControlShare.TopMostDUIControl = this;
                    }
                    else
                    {
                        loadAction.Enqueue(() =>
                        {
                            if (this.DUIControlShare != null)
                            {
                                DUIControlShare.TopMostDUIControl = this;
                            }
                        });
                    }
                }
                else
                {
                    if (this.DUIControlShare != null)
                    {
                        if (DUIControlShare.TopMostDUIControl == this)
                        {
                            DUIControlShare.TopMostDUIControl = null;
                        }
                    }
                    else
                    {
                        loadAction.Enqueue(() =>
                        {
                            if (DUIControlShare.TopMostDUIControl == this)
                            {
                                DUIControlShare.TopMostDUIControl = null;
                            }
                        });
                    }
                }
            }
        }
        public virtual Cursor Cursor
        {
            get
            {
                if (this.DUIParent == null)
                {
                    return Cursors.Default;
                }
                return this.DUIParent.Cursor;
            }
            set
            {
                if (this.DUIParent != null)
                {
                    this.DUIParent.Cursor = value;
                }
            }
        }
        public virtual DUIControl DUIParent
        {
            get { return dUIParent; }
            set
            {
                if (dUIParent != value)
                {
                    var oldParent = dUIParent;
                    if (value != null)
                    {
                        value.DUIControls.Add(this);
                    }
                    else
                    {
                        if (dUIParent != null)
                        {
                            dUIParent.DUIControls.Remove(this);
                        }
                    }
                    dUIParent = value;
                    this.OnParentChanging(new DUIParentChangingEventArgs(dUIParent, oldParent));
                    this.OnParentChanged(EventArgs.Empty);
                }
            }
        }
        public DUIControl FocusedControl
        {
            get { return this.DUIControlShare.FocusedDUIControl; }
        }
        public Region Region
        {
            get
            {
                return region;
            }
            set
            {
                if (region != value)
                {
                    region = value;
                    OnRegionChanged(EventArgs.Empty);
                }
            }
        }
        /// <summary> 获取Control经过Matrix变化的ClientBounds
        /// </summary>
        public RectangleF MatrixBounds
        {
            get
            {
                using (GraphicsPath graphicsPath = new GraphicsPath())
                using (DUIMatrix dUIMatrix = new DUIMatrix())
                {
                    dUIMatrix.Translate(this.X, this.Y);
                    dUIMatrix.Translate(this.BorderWidth, this.BorderWidth);
                    dUIMatrix.Translate(this.CenterX, this.CenterY);
                    dUIMatrix.Multiply(this.Matrix);
                    dUIMatrix.Translate(-this.CenterX, -this.CenterY);
                    dUIMatrix.Translate(-this.BorderWidth, -this.BorderWidth);
                    graphicsPath.AddRectangle(new RectangleF(0, 0, this.Width, this.Height));
                    graphicsPath.Transform(dUIMatrix);
                    return graphicsPath.GetBounds();
                }
            }
        }
        /// <summary> 获取Control经过Matrix变化的ClientBounds
        /// </summary>
        public RectangleF MatrixClientBounds
        {
            get
            {
                using (GraphicsPath graphicsPath = new GraphicsPath())
                using (DUIMatrix dUIMatrix = new DUIMatrix())
                {
                    dUIMatrix.Translate(this.ClientBounds.X, this.ClientBounds.Y);
                    dUIMatrix.Translate(this.CenterX, this.CenterY);
                    dUIMatrix.Multiply(this.Matrix);
                    dUIMatrix.Translate(-this.CenterX, -this.CenterY);
                    graphicsPath.AddRectangle(new RectangleF(0, 0, this.ClientSize.Width, this.ClientSize.Height));
                    graphicsPath.Transform(dUIMatrix);
                    return graphicsPath.GetBounds();
                }
            }
        }
        /// <summary> 客户端矩形，不包含坐标值
        /// </summary>
        public virtual RectangleF ClientRectangle { get { return new RectangleF(0, 0, ClientSize.Width, ClientSize.Height); } }
        /// <summary> 客户端区域的矩形，包含坐标值
        /// </summary>
        public virtual RectangleF ClientBounds
        {
            get { return new RectangleF(this.X + this.BorderWidth, this.Y + this.BorderWidth, this.ClientSize.Width, this.ClientSize.Height); }
            set
            {
                this.X = value.X - BorderWidth;
                this.Y = value.Y - BorderWidth;
                this.Width = value.Width + BorderWidth * 2;
                this.Height = value.Height + BorderWidth * 2;
            }
        }
        /// <summary> 客户端尺寸
        /// </summary>
        public virtual SizeF ClientSize { get { return new SizeF(this.Width - this.BorderWidth * 2, this.Height - this.BorderWidth * 2); } }
        /// <summary> 可显示区域，包含超出控件部分的区域
        /// </summary>
        public virtual RectangleF DisplayRectangle
        {
            get
            {
                return new RectangleF(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            }
        }
        /// <summary> 控件的圆角半径
        /// </summary>
        public virtual float Radius
        {
            get { return radius; }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                if (radius != value)
                {
                    radius = value;
                    this.Invalidate();
                };
            }
        }
        public virtual ContextMenuStrip ContextMenuStrip
        {
            get
            {
                if (this.DUIParent != null)
                {
                    return this.DUIParent.ContextMenuStrip;
                }
                return null;
            }
            set
            {
                this.DUIParent.ContextMenuStrip = value;
                OnContextMenuStripChanged(EventArgs.Empty);
            }
        }
        internal virtual DUIControlShare DUIControlShare
        {
            get
            {
                try
                {
                    if (DUIParent == null) { return null; }
                    return DUIParent.DUIControlShare;
                }
                catch (Exception ex)
                {
                    Log.DUILog.GettingLog(ex);
                    return null;
                }
            }
        }
        public object Tag { get; set; }
        /// <summary> 子控件集
        /// </summary>
        public DUIControlCollection DUIControls
        {
            get
            {
                if (dUIControls == null)
                {
                    dUIControls = this.CreateControlsInstance();
                }
                return dUIControls;
            }
        }
        /// <summary> 分配给控件的 DUIControlCollection 的新实例
        /// </summary>
        /// <returns></returns>
        protected virtual DUIControlCollection CreateControlsInstance()
        {
            return new DUIControlCollection(this);
        }
        /// <summary> 是否超出了父容器的显示范围
        /// </summary>
        internal virtual bool IsOutOfParentArea
        {
            get
            {
                if (this.DUIParent == null)
                {
                    return true;
                }
                else
                {
                    RectangleF rect = new RectangleF(
                        -this.DUIParent.DisplayRectangle.X,
                        -this.DUIParent.DisplayRectangle.Y,
                        this.DUIParent.ClientSize.Width,
                        this.DUIParent.ClientSize.Height);
                    return !rect.IntersectsWith(RotateTools.RectangleRotate(this.Bounds, this.PointToParent(this.Center), this.Rotate)) || this.DUIParent.IsOutOfParentArea;
                }
            }
        }
        /// <summary> 子控件是否超出显示范围
        /// </summary>
        /// <param name="child">子控件</param>
        /// <returns></returns>
        internal virtual bool IsOutOfArea(DUIControl child)
        {
            if (this.IsDescendant(child))
            {
                RectangleF rect = new RectangleF(
                    -this.DisplayRectangle.X,
                    -this.DisplayRectangle.Y,
                    this.ClientSize.Width,
                    this.ClientSize.Height);
                return !rect.IntersectsWith(RotateTools.RectangleRotate(child.Bounds, child.PointToParent(child.Center), child.Rotate));
            }
            return true;
        }
        /// <summary> 点是否超出显示范围
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        internal protected virtual bool IsPointOutOfArea(PointF p)
        {
            RectangleF rect = new RectangleF(
                -this.DisplayRectangle.X,
                -this.DisplayRectangle.Y,
                this.ClientSize.Width,
                this.ClientSize.Height);
            return !rect.Contains(p);
        }
        #endregion
        #region 构造函数
        public DUIControl()
        {
            //dUIControls = new DUIControlCollection(this);
        }
        #endregion
        #region 函数
        public virtual void SetClientBounds(float x, float y, float width, float height, float centerX, float centerY, float rotateAngle, float skewX, float skewY, float scaleX, float scaleY, DUIBoundsSpecified specified, bool invalidate = true)
        {
            x = x - this.borderWidth;
            y = y - this.borderWidth;
            SetBounds(x, y, width, height, centerX, centerY, rotateAngle, skewX, skewY, scaleX, scaleY, specified, invalidate);
        }
        public virtual void SetBounds(float x, float y, float width, float height, float centerX, float centerY, float rotateAngle, float skewX, float skewY, float scaleX, float scaleY, DUIBoundsSpecified specified, bool invalidate = true)
        {
            if ((specified & DUIBoundsSpecified.X) == DUIBoundsSpecified.None) x = this.x;
            if ((specified & DUIBoundsSpecified.Y) == DUIBoundsSpecified.None) y = this.y;
            if ((specified & DUIBoundsSpecified.Width) == DUIBoundsSpecified.None) width = this.width;
            if ((specified & DUIBoundsSpecified.Height) == DUIBoundsSpecified.None) height = this.height;
            if ((specified & DUIBoundsSpecified.CenterX) == DUIBoundsSpecified.None) centerX = this.centerX;
            if ((specified & DUIBoundsSpecified.CenterY) == DUIBoundsSpecified.None) centerY = this.centerY;
            if ((specified & DUIBoundsSpecified.Rotate) == DUIBoundsSpecified.None) rotateAngle = this.rotate;
            if ((specified & DUIBoundsSpecified.SkewX) == DUIBoundsSpecified.None) skewX = this.skewX;
            if ((specified & DUIBoundsSpecified.SkewY) == DUIBoundsSpecified.None) skewY = this.skewY;
            if ((specified & DUIBoundsSpecified.ScaleX) == DUIBoundsSpecified.None) scaleX = this.scaleX;
            if ((specified & DUIBoundsSpecified.ScaleY) == DUIBoundsSpecified.None) scaleY = this.scaleY;
            if (this.x != x ||
                this.y != y ||
                this.width != width ||
                this.height != height ||
                this.centerX != centerX ||
                this.centerY != centerY ||
                this.rotate != rotateAngle ||
                this.skewX != skewX ||
                this.skewY != skewY ||
                this.scaleX != scaleX ||
                this.scaleY != scaleY)
            {
                UpdateBounds(x, y, width, height, centerX, centerY, rotateAngle, skewX, skewY, scaleX, scaleY, invalidate);
            }
        }
        protected virtual void UpdateBounds(float x, float y, float width, float height, float centerX, float centerY, float rotateAngle, float skewX, float skewY, float scaleX, float scaleY, bool invalidate = true)
        {
            RectangleF lastBounds = new RectangleF(this.x, this.y, this.width, this.height);
            PointF lastCenter = new PointF(this.centerX, this.centerY);
            PointF lastSkew = new PointF(this.skewX, this.skewY);
            PointF lastScale = new PointF(this.scaleX, this.scaleY);
            float lastRotateAngle = this.rotate;
            Region lastRegion = new Region(new RectangleF(this.X, this.Y, this.Width, this.Height));
            Matrix lastMatrix = new Matrix();
            lastMatrix.RotateAt(this.Rotate, new PointF(this.ClientBounds.X + this.CenterX, this.ClientBounds.Y + this.CenterY));
            lastRegion.Transform(lastMatrix);
            bool newLocation = this.x != x || this.y != y;
            bool newSize = this.width != width || this.height != height;
            bool newCenter = this.centerX != centerX || this.centerY != centerY;
            bool newSkew = this.skewX != skewX || this.skewY != skewY;
            bool newScale = this.scaleX != scaleX || this.scaleY != scaleY;
            bool newRotateAngle = this.rotate != rotateAngle;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.centerX = centerX;
            this.centerY = centerY;
            this.skewX = skewX;
            this.skewY = skewY;
            this.scaleX = scaleX;
            this.scaleY = scaleY;
            this.rotate = rotateAngle;
            Region currentRegion = new Region(this.Bounds);
            Matrix currentMatrix = new Matrix();
            currentMatrix.RotateAt(this.Rotate, new PointF(this.ClientBounds.X + this.CenterX, this.ClientBounds.Y + this.CenterY));
            currentRegion.Transform(currentMatrix);
            if ((newLocation || newSize || newCenter || newRotateAngle || newSkew || newScale) && invalidate)
            {
                lastRegion.Union(currentRegion);
                this.Invalidate(lastRegion);
            }
            if (newLocation)
            {
                this.UpdateLocationChanging(lastBounds);
            }
            if (newSize)
            {
                this.UpdateResizing(lastBounds);
            }
            if (newLocation || newSize)
            {
                this.UpdateBoundsChanging(lastBounds);
            }
            if (newCenter)
            {
                this.UpdateCenterChanging(lastCenter);
            }
            if (newSkew)
            {
                this.UpdateSkewChanging(lastSkew);
            }
            if (newScale)
            {
                this.UpdateScaleChanging(lastScale);
            }
            if (newRotateAngle)
            {
                this.UpdateRotateAngleChanging(lastRotateAngle);
            }
            if ((newLocation || newSize || newCenter || newRotateAngle || newSkew) && invalidate)
            {
                this.UpdateAnyChanging(lastBounds, lastCenter, lastSkew, lastScale, lastRotateAngle);
            }
            if (newLocation)
            {
                this.UpdateLocationChanged();
            }
            if (newSize)
            {
                this.UpdateResize();
            }
            if (newLocation || newSize)
            {
                this.UpdateBoundsChanged();
            }
            if (newCenter)
            {
                this.UpdateCenterChanged();
            }
            if (newSkew)
            {
                this.UpdateSkewChanged();
            }
            if (newScale)
            {
                this.UpdateScaleChanged();
            }
            if (newRotateAngle)
            {
                this.UpdateRotateAngleChanged();
            }
            if ((newLocation || newSize || newCenter || newSkew || newScale || newRotateAngle) && invalidate)
            {
                this.UpdateAnyChanged();
            }
        }
        #region Update
        protected virtual void UpdateLocationChanging(RectangleF lastBounds)
        {
            OnLocationChanging(new DUILocationChangingEventArgs(new PointF(this.x, this.y), lastBounds.Location));
            this.DUIParent?.OnControlLocationChanging(new DUIControlLocationChangingEventArgs(this, new PointF(this.x, this.y), lastBounds.Location));
            foreach (DUIControl control in this.DUIControls)
            {
                this.DUIParent?.OnParentLocationChanging(new DUIParentLocationChangingEventArgs(this, new PointF(this.x, this.y), lastBounds.Location));
            }
            LayoutEngine.DoLayoutDock(new DUILayoutEventArgs(this.DUIParent, "OnResizing"));
        }
        protected virtual void UpdateResizing(RectangleF lastBounds)
        {
            OnResizing(new DUISizeChangingEventArgs(new SizeF(this.width, this.height), lastBounds.Size));
            this.DUIParent?.OnControlSizeChanging(new DUIControlSizeChangingEventArgs(this, new SizeF(this.width, this.height), lastBounds.Size));
            foreach (DUIControl control in this.DUIControls)
            {
                this.DUIParent?.OnParentSizeChanging(new DUIParentSizeChangingEventArgs(this, new SizeF(this.width, this.height), lastBounds.Size));
            }
            LayoutEngine.DoLayoutDock(new DUILayoutEventArgs(this.DUIParent, "OnResizing"));
            LayoutEngine.DoLayoutDock(new DUILayoutEventArgs(this, "OnResizing"), lastBounds.Size);
        }
        protected virtual void UpdateBoundsChanging(RectangleF lastBounds)
        {
            OnBoundsChanging(new DUIBoundsChangingEventArgs(new RectangleF(this.x, this.y, this.width, this.height), lastBounds));
            this.DUIParent?.OnControlBoundsChanging(new DUIControlBoundsChangingEventArgs(this, new RectangleF(this.x, this.y, this.width, this.height), lastBounds));
            foreach (DUIControl control in this.DUIControls)
            {
                this.DUIParent?.OnParentBoundsChanging(new DUIParentBoundsChangingEventArgs(this, new RectangleF(this.x, this.y, this.width, this.height), lastBounds));
            }
        }
        protected virtual void UpdateCenterChanging(PointF lastCenter)
        {
            OnCenterChanging(new DUICenterChangingEventArgs(new PointF(this.centerX, this.centerY), lastCenter));
            this.DUIParent?.OnControlCenterChanging(new DUIControlCenterChangingEventArgs(this, new PointF(this.centerX, this.centerY), lastCenter));
            foreach (DUIControl control in this.DUIControls)
            {
                this.DUIParent?.OnParentCenterChanging(new DUIParentCenterChangingEventArgs(this, new PointF(this.centerX, this.centerY), lastCenter));
            }
        }
        protected virtual void UpdateSkewChanging(PointF lastSkew)
        {
            OnSkewChanging(new DUISkewChangingEventArgs(new PointF(this.skewX, this.skewY), lastSkew));
            this.DUIParent?.OnControlSkewChanging(new DUIControlSkewChangingEventArgs(this, new PointF(this.skewX, this.skewY), lastSkew));
            foreach (DUIControl control in this.DUIControls)
            {
                this.DUIParent?.OnParentSkewChanging(new DUIParentSkewChangingEventArgs(this, new PointF(this.skewX, this.skewY), lastSkew));
            }
        }
        protected virtual void UpdateScaleChanging(PointF lastScale)
        {
            OnScaleChanging(new DUIScaleChangingEventArgs(new PointF(this.scaleX, this.scaleY), lastScale));
            this.DUIParent?.OnControlScaleChanging(new DUIControlScaleChangingEventArgs(this, new PointF(this.scaleX, this.scaleY), lastScale));
            foreach (DUIControl control in this.DUIControls)
            {
                this.DUIParent?.OnParentScaleChanging(new DUIParentScaleChangingEventArgs(this, new PointF(this.scaleX, this.scaleY), lastScale));
            }
        }
        protected virtual void UpdateRotateAngleChanging(float lastRotateAngle)
        {
            OnRotateAngleChanging(new DUIRotateAngleChangingEventArgs(this.rotate, lastRotateAngle));
            this.DUIParent?.OnControlRotateAngleChanging(new DUIControlRotateAngleChangingEventArgs(this, this.rotate, lastRotateAngle));
            foreach (DUIControl control in this.DUIControls)
            {
                this.DUIParent?.OnParentRotateAngleChanging(new DUIParentRotateAngleChangingEventArgs(this, this.rotate, lastRotateAngle));
            }
        }
        protected virtual void UpdateAnyChanging(RectangleF lastBounds, PointF lastCenter, PointF lastSkew, PointF lastScale, float lastRotateAngle)
        {
            OnAnyChanging(new DUIAnyChangingEventArgs(new RectangleF(this.x, this.y, this.width, this.height), lastBounds, new PointF(this.centerX, this.centerY), lastCenter, new PointF(this.skewX, this.skewY), lastSkew, new PointF(this.scaleX, this.scaleY), lastScale, this.rotate, lastRotateAngle));
            this.DUIParent?.OnControlAnyChanging(new DUIControlAnyChangingEventArgs(this, new RectangleF(this.x, this.y, this.width, this.height), lastBounds, new PointF(this.centerX, this.centerY), lastCenter, new PointF(this.skewX, this.skewY), lastSkew, new PointF(this.scaleX, this.scaleY), lastScale, this.rotate, lastRotateAngle));
            foreach (DUIControl control in this.DUIControls)
            {
                this.DUIParent?.OnParentAnyChanging(new DUIParentAnyChangingEventArgs(this, new RectangleF(this.x, this.y, this.width, this.height), lastBounds, new PointF(this.centerX, this.centerY), lastCenter, new PointF(this.skewX, this.skewY), lastSkew, new PointF(this.scaleX, this.scaleY), lastScale, this.rotate, lastRotateAngle));
            }
        }
        protected virtual void UpdateLocationChanged()
        {
            OnLocationChanged(EventArgs.Empty);
            this.DUIParent?.OnControlLocationChanged(new DUIControlEventArgs(this));
            foreach (DUIControl control in this.DUIControls)
            {
                this.DUIParent?.OnParentLocationChanged(new DUIParentEventArgs(this));
            }
        }
        protected virtual void UpdateResize()
        {
            OnResize(EventArgs.Empty);
            this.DUIParent?.OnControlSizeChanged(new DUIControlEventArgs(this));
            foreach (DUIControl control in this.DUIControls)
            {
                this.DUIParent?.OnParentSizeChanged(new DUIParentEventArgs(this));
            }
        }
        protected virtual void UpdateBoundsChanged()
        {
            OnBoundsChanged(EventArgs.Empty);
            if (this.DUIParent != null)
            {
                this.DUIParent?.OnControlBoundsChanged(new DUIControlEventArgs(this));
            }
            foreach (DUIControl control in this.DUIControls)
            {
                this.DUIParent?.OnParentBoundsChanged(new DUIParentEventArgs(this));
            }
        }
        protected virtual void UpdateCenterChanged()
        {
            OnCenterChanged(EventArgs.Empty);
            this.DUIParent?.OnControlCenterChanged(new DUIControlEventArgs(this));
            foreach (DUIControl control in this.DUIControls)
            {
                this.DUIParent?.OnParentCenterChanged(new DUIParentEventArgs(this));
            }
        }
        protected virtual void UpdateSkewChanged()
        {
            OnSkewChanged(EventArgs.Empty);
            this.DUIParent?.OnControlSkewChanged(new DUIControlEventArgs(this));
            foreach (DUIControl control in this.DUIControls)
            {
                this.DUIParent?.OnParentSkewChanged(new DUIParentEventArgs(this));
            }
        }
        protected virtual void UpdateScaleChanged()
        {
            OnScaleChanged(EventArgs.Empty);
            this.DUIParent?.OnControlScaleChanged(new DUIControlEventArgs(this));
            foreach (DUIControl control in this.DUIControls)
            {
                this.DUIParent?.OnParentScaleChanged(new DUIParentEventArgs(this));
            }
        }
        protected virtual void UpdateRotateAngleChanged()
        {
            OnRotateAngleChanged(EventArgs.Empty);
            this.DUIParent?.OnControlRotateAngleChanged(new DUIControlEventArgs(this));
            foreach (DUIControl control in this.DUIControls)
            {
                this.DUIParent?.OnParentRotateAngleChanged(new DUIParentEventArgs(this));
            }
        }
        protected virtual void UpdateAnyChanged()
        {
            OnAnyChanged(EventArgs.Empty);
            this.DUIParent?.OnControlAnyChanged(new DUIControlEventArgs(this));
            foreach (DUIControl control in this.DUIControls)
            {
                this.DUIParent?.OnParentAnyChanged(new DUIParentEventArgs(this));
            }
        }
        #endregion
        public void BringToFront()
        {
            if (this.DUIParent != null)
            {
                this.DUIParent.DUIControls.SetChildIndex(this, -1);
            }
        }
        public void SendToBack()
        {
            if (this.DUIParent != null)
            {
                this.DUIParent.DUIControls.SetChildIndex(this, 0);
            }
        }
        public virtual void Invalidate()
        {
            //if (this.DUIParent != null && this.CanLayout)
            //{
            //    this.DUIParent.Invalidate();
            //}
            Region region = new Region(this.Bounds);
            Matrix matrix = new Matrix();
            matrix.RotateAt(this.Rotate, new PointF(this.ClientBounds.X + this.CenterX, this.ClientBounds.Y + this.CenterY));
            region.Transform(matrix);
            this.Invalidate(region);
        }
        public virtual void Invalidate(Region r)
        {
            if (this.DUIParent != null && this.CanLayout)
            {
                if (this.DUIParent is DUIScaleableControl dUIScaleableControlParent)
                {
                    Matrix m = new Matrix();
                    m.Translate(dUIScaleableControlParent.CenterX, dUIScaleableControlParent.CenterY);
                    m.Scale(dUIScaleableControlParent.Scaling, dUIScaleableControlParent.Scaling);
                    r.Transform(m);
                    r.Translate(this.DUIParent.X, this.DUIParent.Y);
                    r.Intersect(new Region(this.DUIParent.Bounds));
                    this.DUIParent.Invalidate(r);
                }
                else
                {
                    r.Translate(this.DUIParent.X, this.DUIParent.Y);
                    r.Intersect(new Region(this.DUIParent.Bounds));
                    this.DUIParent.Invalidate(r);
                }
            }
        }
        public void Invalidate(RectangleF rect)
        {
            Region region = new Region(rect);
            Matrix matrix = new Matrix();
            matrix.RotateAt(this.Rotate, new PointF(this.ClientBounds.X + this.CenterX, this.ClientBounds.Y + this.CenterY));
            region.Transform(matrix);
            this.Invalidate(new Region(rect));
        }
        /// <summary> 在拥有此控件的基础窗口句柄的线程上执行指定的委托
        /// </summary>
        /// <param name="method">包含要在控件的线程上下文中调用的方法的委托</param>
        /// <returns>正在被调用的委托的返回值，或者如果委托没有返回值，则为 null</returns>
        public virtual object Invoke(Delegate method)
        {
            if (this.DUIParent != null)
            {
                return this.DUIParent.Invoke(method);
            }
            return null;
        }
        /// <summary> 在拥有控件的基础窗口句柄的线程上，用指定的参数列表执行指定委托
        /// </summary>
        /// <param name="method">一个方法委托，它采用的参数的数量和类型与 args 参数中所包含的相同</param>
        /// <param name="args">作为指定方法的参数传递的对象数组。如果此方法没有参数，该参数可以是 null</param>
        /// <returns>System.Object，它包含正被调用的委托返回值；如果该委托没有返回值，则为 null</returns>
        public virtual object Invoke(Delegate method, params object[] args)
        {
            if (this.DUIParent != null)
            {
                return this.DUIParent.Invoke(method, args);
            }
            return null;
        }
        public virtual void SynchronizationContextSend(Action act)
        {
            if (this.DUIParent != null)
            {
                this.DUIParent.SynchronizationContextSend(act);
            }
        }
        public virtual void SynchronizationContextPost(Action act)
        {
            if (this.DUIParent != null)
            {
                this.DUIParent.SynchronizationContextPost(act);
            }
        }
        public void Refresh()
        {
            LayoutEngine.DoLayout(new DUILayoutEventArgs(this, "Refresh"));
        }
        public virtual bool Focus()
        {
            if (CanFocus)
            {
                if (this.DUIControlShare.FocusedDUIControl != this)
                {
                    if (this.DUIControlShare.FocusedDUIControl != null)
                    {
                        DUIControl lastFocusedDUIControl = this.DUIControlShare.FocusedDUIControl;
                        this.DUIControlShare.FocusedDUIControl = null;
                        lastFocusedDUIControl.OnLostFocus(EventArgs.Empty);
                        lastFocusedDUIControl.Invalidate();
                    }
                    this.DUIControlShare.FocusedDUIControl = this;
                    this.DUIControlShare.FocusedDUIControl.Invalidate();
                    this.OnGotFocus(EventArgs.Empty);
                }
                return true;
            }
            return false;
        }
        /// <summary> 获取当前控件下的子控件
        /// </summary>
        /// <param name="recursion">是否递归获取子控件的子控件</param>
        /// <returns></returns>
        public List<DUIControl> GetAllChild(bool recursion)
        {
            List<DUIControl> dUIControls = new List<DUIControl>();
            if (this.dUIControls == null) { return dUIControls; }
            foreach (DUIControl dUIControl in this.dUIControls)
            {
                dUIControls.Add(dUIControl);
                if (recursion)
                {
                    dUIControls.AddRange(dUIControl.GetAllChild(recursion));
                }
            }
            return dUIControls;
        }
        /// <summary> 通过点坐标获取子控件
        /// </summary>
        /// <param name="p">点坐标</param>
        /// <returns>子控件</returns>
        public virtual DUIControl GetChildAtPoint(PointF p)
        {
            if (this.DUIControlShare.TopMostDUIControl != null && this.DUIControlShare.TopMostDUIControl.Visible && this.DUIControls.Contains(this.DUIControlShare.TopMostDUIControl) && this.DUIControlShare.TopMostDUIControl.CanMouseSelected && this.DUIControlShare.TopMostDUIControl.ContainPoint(this.DUIControlShare.TopMostDUIControl.PointFromParent(p)))
            {
                return this.DUIControlShare.TopMostDUIControl;
            }
            if (this.IsPointOutOfArea(p))
            {
                return null;
            }
            else
            {
                return this.DUIControls.OfType<DUIControl>().Where(d => d.Visible && d.CanMouseSelected).ToList().LastOrDefault(d => d.ContainPoint(d.PointFromParent(p)));
            }
        }
        /// <summary> 判断是否包含这个AnchorStyles
        /// </summary>
        /// <param name="anchor">AnchorStyles</param>
        /// <returns></returns>
        private bool ContainAnchor(AnchorStyles anchor)
        {
            return (this.Anchor & anchor) == anchor;
        }
        /// <summary> 控件区域是否包含这个点坐标
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        internal protected virtual bool ContainPoint(PointF p)
        {
            RectangleF rect = new RectangleF(-this.BorderWidth, -this.BorderWidth, this.Width, this.Height);
            return rect.Contains(p);
        }
        public virtual DUIGraphics CreateGraphics()
        {
            if (this.DUIParent != null)
            {
                return this.DUIParent.CreateGraphics();
            }
            return null;
        }
        /// <summary> 挂起界面布局
        /// </summary>
        public virtual void SuspendLayout()
        {
            this.layoutSuspendCount++;
        }
        /// <summary> 恢复挂起的界面布局
        /// </summary>
        public virtual void ResumeLayout()
        {
            ResumeLayout(true);
        }
        /// <summary> 恢复挂起的界面布局
        /// </summary>
        /// <param name="performLayout">是否刷新界面</param>
        public virtual void ResumeLayout(bool performLayout)
        {
            this.layoutSuspendCount--;
            if (performLayout)
            {
                LayoutEngine.DoLayoutDock(new DUILayoutEventArgs(this, "ResumeLayout"), false);
            }
            this.Invalidate();
        }
        internal bool IsDescendant(DUIControl descendant)
        {
            DUIControl control = descendant;
            while (control != null)
            {
                if (control == this)
                    return true;
                control = control.DUIParent;
            }
            return false;
        }
        internal virtual void ShowToolTip(string content)
        {
            if (this.DUIParent != null)
            {
                this.DUIParent.ShowToolTip(content);
            }
        }
        internal virtual void HideToolTip()
        {
            if (this.DUIParent != null)
            {
                this.DUIParent.HideToolTip();
            }
        }
        #region 坐标转换
        /// <summary> 用相对父控件的中心坐标来设置中心坐标
        /// </summary>
        /// <param name="center"></param>
        public void SetCenter(PointF center)
        {
            DUIMatrix matrix = new DUIMatrix();
            matrix.Translate(this.ClientBounds.X + this.CenterX, this.ClientBounds.Y + this.CenterY);
            matrix *= this.Matrix;
            matrix.TranslateReverse(this.ClientBounds.X + this.CenterX, this.ClientBounds.Y + this.CenterY);
            PointF p = MatrixTools.PointAfterMatrix(this.ClientBounds.Location, matrix);
            matrix.Reset();
            matrix.Translate(center);
            matrix *= this.MatrixReverse;
            matrix.TranslateReverse(center);
            p = MatrixTools.PointAfterMatrix(p, matrix);
            this.SetBounds(p.X - this.BorderWidth, p.Y - this.BorderWidth, 0, 0, center.X - p.X, center.Y - p.Y, 0, 0, 0, 0, 0, DUIBoundsSpecified.Location | DUIBoundsSpecified.Center);
        }
        /// <summary> 将指定屏幕点的位置计算成工作区坐标
        /// </summary>
        /// <param name="p">要转换的屏幕坐标 System.Drawing.PointF</param>
        /// <returns>一个 System.Drawing.PointF，它表示转换后的 System.Drawing.PointF、p（以工作区坐标表示）。</returns>
        public virtual PointF PointToClient(PointF p)
        {
            if (DUIParent == null) { return p; }
            return PointFromParent(DUIParent.PointToClient(p));
        }
        /// <summary> 将指定工作区点的位置计算成屏幕坐标
        /// </summary>
        /// <param name="p">要转换的工作区坐标 System.Drawing.PointF</param>
        /// <returns>一个 System.Drawing.PointF，它表示转换后的 System.Drawing.PointF、p（以屏幕坐标表示）。</returns>
        public virtual PointF PointToScreen(PointF p)
        {
            p = PointToParent(p);
            if (DUIParent == null) { return p; }
            return DUIParent.PointToScreen(p);
        }
        /// <summary> 将指定工作区点的位置计算成父控件的坐标
        /// </summary>
        /// <param name="p">要转换的工作区坐标 System.Drawing.PointF。</param>
        /// <returns>一个 System.Drawing.PointF，它表示转换后的 System.Drawing.PointF、p（以屏幕坐标表示）。</returns>
        public virtual PointF PointToParent(PointF p)
        {
            DUIMatrix matrix = new DUIMatrix();
            matrix.Translate(this.Center);
            matrix *= this.Matrix;
            matrix.TranslateReverse(this.Center);
            var matrixPoint = MatrixTools.PointAfterMatrix(p, matrix);
            return new PointF(this.ClientBounds.X + matrixPoint.X, this.ClientBounds.Y + matrixPoint.Y);
        }
        /// <summary> 将最基类的父容器的坐标转为此坐标
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public virtual PointF PointFromBaseParent(PointF p)
        {
            if (this.DUIParent == null) { return p; }
            return this.DUIParent.PointFromBaseParent(PointFromParent(p));
        }
        /// <summary> 将此坐标转为最基类的父容器的坐标
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public virtual PointF PointToBaseParent(PointF p)
        {
            if (this.DUIParent == null) { return p; }
            return this.DUIParent.PointToBaseParent(PointToParent(p));
        }
        /// <summary> 计算指定屏幕矩形的大小和位置（以工作区坐标表示）。
        /// </summary>
        /// <param name="r">要转换的屏幕坐标 System.Drawing.RectangleF</param>
        /// <returns>一个 System.Drawing.RectangleF，它表示转换后的 System.Drawing.RectangleF、r（以工作区坐标表示）。</returns>
        public virtual RectangleF RectangleToClient(RectangleF r)
        {
            return new RectangleF(PointToClient(r.Location), r.Size);
        }
        /// <summary> 计算指定工作区矩形的大小和位置（以屏幕坐标表示）。
        /// </summary>
        /// <param name="r">要转换的工作区坐标 System.Drawing.RectangleF</param>
        /// <returns>一个 System.Drawing.RectangleF，它表示转换后的 System.Drawing.RectangleF、p（以屏幕坐标表示）。</returns>
        public virtual RectangleF RectangleToScreen(RectangleF r)
        {
            return new RectangleF(PointToScreen(r.Location), r.Size);
        }
        public virtual RectangleF RectangleToParent(RectangleF r)
        {
            return new RectangleF(PointToParent(r.Location), r.Size);
        }
        public virtual RectangleF RectangleToBaseParent(RectangleF r)
        {
            return new RectangleF(this.DUIParent.PointToBaseParent(PointToParent(r.Location)), new SizeF(r.Width, r.Height));
        }
        /// <summary> 将父控件的位置计算成当前控件的坐标
        /// </summary>
        /// <param name="p">要转换的工作区坐标 System.Drawing.PointF。</param>
        /// <returns>一个 System.Drawing.PointF，它表示转换后的 System.Drawing.PointF、p（以屏幕坐标表示）。</returns>
        public virtual PointF PointFromParent(PointF p)
        {
            DUIMatrix matrix = new DUIMatrix();
            PointF center = new PointF(this.Center.X + this.ClientBounds.X, this.Center.Y + this.ClientBounds.Y);
            matrix.Translate(center);
            matrix *= this.MatrixReverse;
            matrix.TranslateReverse(center);
            var matrixPoint = MatrixTools.PointAfterMatrix(p, matrix);
            return new PointF(matrixPoint.X - this.ClientBounds.X, matrixPoint.Y - this.ClientBounds.Y);
        }
        #endregion
        #region 执行鼠标动作
        #region AlwaysMouse
        /// <summary> 递归触发鼠标点击
        /// </summary>
        /// <param name="dUIControl">触发控件</param>
        /// <param name="e">鼠标事件</param>
        protected void AlwaysMouseClick(DUIControl dUIControl, DUIMouseEventArgs e)
        {
            dUIControl.AlwaysMouseClick(e);
            if (dUIControl.DUIParent != null)
            {
                AlwaysMouseClick(dUIControl.DUIParent, new DUIMouseEventArgs(e.Button, e.Clicks, dUIControl.PointToParent(e.Location), e.Delta));
            }
        }
        /// <summary> 递归触发鼠标双击
        /// </summary>
        /// <param name="dUIControl">触发控件</param>
        /// <param name="e">鼠标事件</param>
        protected void AlwaysMouseDoubleClick(DUIControl dUIControl, DUIMouseEventArgs e)
        {
            dUIControl.AlwaysMouseDoubleClick(e);
            if (dUIControl.DUIParent != null)
            {
                AlwaysMouseDoubleClick(dUIControl.DUIParent, new DUIMouseEventArgs(e.Button, e.Clicks, dUIControl.PointToParent(e.Location), e.Delta));
            }
        }
        /// <summary> 递归触发鼠标按下
        /// </summary>
        /// <param name="dUIControl">触发控件</param>
        /// <param name="e">鼠标事件</param>
        protected void AlwaysMouseDown(DUIControl dUIControl, DUIMouseEventArgs e)
        {
            dUIControl.AlwaysMouseDown(e);
            if (dUIControl.DUIParent != null)
            {
                AlwaysMouseDown(dUIControl.DUIParent, new DUIMouseEventArgs(e.Button, e.Clicks, dUIControl.PointToParent(e.Location), e.Delta));
            }
        }
        /// <summary> 递归触发鼠标移动
        /// </summary>
        /// <param name="dUIControl">触发控件</param>
        /// <param name="e">鼠标事件</param>
        protected void AlwaysMouseMove(DUIControl dUIControl, DUIMouseEventArgs e)
        {
            dUIControl.AlwaysMouseMove(e);
            if (dUIControl.DUIParent != null)
            {
                AlwaysMouseMove(dUIControl.DUIParent, new DUIMouseEventArgs(e.Button, e.Clicks, dUIControl.PointToParent(e.Location), e.Delta));
            }
        }
        /// <summary> 递归触发鼠标弹起
        /// </summary>
        /// <param name="dUIControl">触发控件</param>
        /// <param name="e">鼠标事件</param>
        protected void AlwaysMouseUp(DUIControl dUIControl, DUIMouseEventArgs e)
        {
            dUIControl.AlwaysMouseUp(e);
            if (dUIControl.DUIParent != null)
            {
                AlwaysMouseUp(dUIControl.DUIParent, new DUIMouseEventArgs(e.Button, e.Clicks, dUIControl.PointToParent(e.Location), e.Delta));
            }
        }
        /// <summary> 递归触发鼠标滚动
        /// </summary>
        /// <param name="dUIControl">触发控件</param>
        /// <param name="e">鼠标事件</param>
        protected void AlwaysMouseWheel(DUIControl dUIControl, DUIMouseEventArgs e)
        {
            dUIControl.AlwaysMouseWheel(e);
            if (dUIControl.DUIParent != null)
            {
                AlwaysMouseWheel(dUIControl.DUIParent, new DUIMouseEventArgs(e.Button, e.Clicks, dUIControl.PointToParent(e.Location), e.Delta));
            }
        }
        #endregion
        #region PerformMouse
        /// <summary> 执行MouseEnter
        /// </summary>
        /// <param name="e"></param>
        public virtual void PerformMouseEnter(EventArgs e)
        {
            OnMouseEnter(e);
        }
        /// <summary> 执行MouseLeave
        /// </summary>
        /// <param name="e"></param>
        public virtual void PerformMouseLeave(EventArgs e)
        {
            OnMouseLeave(e);
        }
        /// <summary> 执行MouseHover
        /// </summary>
        /// <param name="e"></param>
        public virtual void PerformMouseHover(EventArgs e)
        {
            OnMouseHover(e);
        }
        /// <summary> 执行MouseClick
        /// </summary>
        /// <param name="e"></param>
        public virtual void PerformMouseClick(DUIMouseEventArgs e)
        {
            OnMouseClick(e);
        }
        /// <summary> 执行MouseDoubleClick
        /// </summary>
        /// <param name="e"></param>
        public virtual void PerformMouseDoubleClick(DUIMouseEventArgs e)
        {
            OnMouseDoubleClick(e);
        }
        /// <summary> 执行MouseDown
        /// </summary>
        /// <param name="e"></param>
        public virtual void PerformMouseDown(DUIMouseEventArgs e)
        {
            this.Focus();
            this.DUIControlShare.MouseDownDUIControls.Add(new DUIMouseStateEventArgs(this, e.Button));
            this.OnMouseDown(e);
        }
        /// <summary> 执行MouseMove
        /// </summary>
        /// <param name="e"></param>
        public virtual void PerformMouseMove(DUIMouseEventArgs e)
        {
            this.OnMouseMove(e);
            //var mouseDownDUIControls = this.DUIControlShare.MouseDownDUIControls.Where(d => d.Button == e.Button).ToList();
            //if (mouseDownDUIControls.Count == 0)
            //{
            //    if (this.DUIControlShare.MouseMoveDUIControl != this)
            //    {
            //        if (this.DUIControlShare.MouseMoveDUIControl != null)
            //        {
            //            this.DUIControlShare.MouseMoveDUIControl.DoMouseLeave(EventArgs.Empty);
            //        }
            //        this.DUIControlShare.MouseMoveDUIControl = this; //记录下鼠标移动的控件
            //        this.DUIControlShare.MouseMoveDUIControl.DoMouseEnter(EventArgs.Empty);
            //    }
            //    OnMouseMove(e);
            //}
            //else
            //{
            //    if (mouseDownDUIControls[0].AffectedControl == this)
            //    {
            //        OnMouseMove(e);
            //    }
            //}
        }
        /// <summary> 执行MouseUp
        /// </summary>
        /// <param name="e"></param>
        public virtual void PerformMouseUp(DUIMouseEventArgs e)
        {
            OnMouseUp(e);
            var mouseDownDUIControls = this.DUIControlShare.MouseDownDUIControls.Where(c => c.Button == e.Button).ToList();
            if (mouseDownDUIControls.Count == 0) { return; }
            if (mouseDownDUIControls[0].AffectedControl == this)
            {
                OnMouseUp(e);
                var delete = this.DUIControlShare.MouseDownDUIControls.Where(d => d.Button == e.Button).FirstOrDefault(dd => dd.AffectedControl == this);
                if (delete != null)
                {
                    this.DUIControlShare.MouseDownDUIControls.Remove(delete);
                }
            }
        }
        /// <summary> 执行MouseWheel
        /// </summary>
        /// <param name="e"></param>
        public virtual void PerformMouseWheel(DUIMouseEventArgs e)
        {
            if (!this.Visible || !this.Enabled) { return; }
            if (this.DUIControlShare.FocusedDUIControl == null) { return; }
            if (this == this.DUIControlShare.FocusedDUIControl)
            {
                OnMouseWheel(e);
            }
        }
        #endregion
        #region InvokeOn
        /// <summary> 为指定的控件引发 MouseClick 事件。
        /// </summary>
        /// <param name="dUIControl">要将事件分配到的DUIControl</param>
        /// <param name="e">包含事件数据的DUIMouseEventArgs</param>
        protected void InvokeOnMouseClick(DUIControl dUIControl, DUIMouseEventArgs e)
        {
            if (dUIControl.DUIControlShare == null || dUIControl.DUIControlShare.MouseDownDUIControls.Where(d => d.Button == e.Button).Select(d => d.AffectedControl).Contains(dUIControl))
            {
                dUIControl.OnMouseClick(e);
                if (dUIControl.DUIParent != null && dUIControl.CanMouseThrough)
                {
                    InvokeOnMouseClick(dUIControl.DUIParent, new DUIMouseEventArgs(e.Button, e.Clicks, dUIControl.PointToParent(e.Location), e.Delta));
                }
            }
        }
        /// <summary> 为指定的控件引发 MouseDoubleClick 事件。
        /// </summary>
        /// <param name="dUIControl">要将事件分配到的DUIControl</param>
        /// <param name="e">包含事件数据的DUIMouseEventArgs</param>
        protected void InvokeOnMouseDoubleClick(DUIControl dUIControl, DUIMouseEventArgs e)
        {
            dUIControl.OnMouseDoubleClick(e);
            if (dUIControl.DUIParent != null && dUIControl.CanMouseThrough)
            {
                InvokeOnMouseDoubleClick(dUIControl.DUIParent, new DUIMouseEventArgs(e.Button, e.Clicks, dUIControl.PointToParent(e.Location), e.Delta));
            }
        }
        /// <summary> 为指定的控件引发 MouseDown 事件。
        /// </summary>
        /// <param name="dUIControl">要将事件分配到的DUIControl</param>
        /// <param name="e">包含事件数据的DUIMouseEventArgs</param>
        protected void InvokeOnMouseDown(DUIControl dUIControl, DUIMouseEventArgs e)
        {
            dUIControl.DUIControlShare.MouseDownDUIControls.Add(new DUIMouseStateEventArgs(dUIControl, e.Button));
            dUIControl.OnMouseDown(e);
            if (dUIControl.DUIParent != null && dUIControl.CanMouseThrough)
            {
                InvokeOnMouseDown(dUIControl.DUIParent, new DUIMouseEventArgs(e.Button, e.Clicks, dUIControl.PointToParent(e.Location), e.Delta));
            }
        }
        /// <summary> 为指定的控件引发 MouseMove 事件。
        /// </summary>
        /// <param name="dUIControl">要将事件分配到的DUIControl</param>
        /// <param name="e">包含事件数据的DUIMouseEventArgs</param>
        protected void InvokeOnMouseMove(DUIControl dUIControl, DUIMouseEventArgs e)
        {
            dUIControl.OnMouseMove(e);
            if (dUIControl.DUIParent != null && dUIControl.CanMouseThrough)
            {
                InvokeOnMouseMove(dUIControl.DUIParent, new DUIMouseEventArgs(e.Button, e.Clicks, dUIControl.PointToParent(e.Location), e.Delta));
            }
        }
        /// <summary> 为指定的控件引发 MouseUp 事件。
        /// </summary>
        /// <param name="dUIControl">要将事件分配到的DUIControl</param>
        /// <param name="e">包含事件数据的DUIMouseEventArgs</param>
        protected void InvokeOnMouseUp(DUIControl dUIControl, DUIMouseEventArgs e)
        {
            if (dUIControl.DUIControlShare == null || dUIControl.DUIControlShare.MouseDownDUIControls.Where(d => d.Button == e.Button).Select(d => d.AffectedControl).Contains(dUIControl))
            {
                dUIControl.OnMouseUp(e);
                var delete = dUIControl.DUIControlShare.MouseDownDUIControls.Where(d => d.Button == e.Button).FirstOrDefault(dd => dd.AffectedControl == dUIControl);
                if (delete != null)
                {
                    this.DUIControlShare.MouseDownDUIControls.Remove(delete);
                }
                if (dUIControl.DUIParent != null && dUIControl.CanMouseThrough)
                {
                    InvokeOnMouseUp(dUIControl.DUIParent, new DUIMouseEventArgs(e.Button, e.Clicks, dUIControl.PointToParent(e.Location), e.Delta));
                }
            }
        }
        /// <summary> 为指定的控件引发 MouseWheel 事件。
        /// </summary>
        /// <param name="dUIControl">要将事件分配到的DUIControl</param>
        /// <param name="e">包含事件数据的DUIMouseEventArgs</param>
        protected void InvokeOnMouseWheel(DUIControl dUIControl, DUIMouseEventArgs e)
        {
            dUIControl.OnMouseWheel(e);
            if (dUIControl.DUIParent != null && dUIControl.CanMouseThrough)
            {
                InvokeOnMouseWheel(dUIControl.DUIParent, new DUIMouseEventArgs(e.Button, e.Clicks, dUIControl.PointToParent(e.Location), e.Delta));
            }
        }
        #endregion
        #endregion
        #endregion
        #region 窗体发出的消息,先被子DUI控件接收，然后由下发给DUI子控件
        internal virtual void DoHandleCreated(EventArgs e)
        {
            this.isHandleCreated = true;
            //this.oldClientSize = this.Size;
            while (loadAction.Count > 0)
            {
                loadAction.Dequeue().Invoke();
            }
            OnHandleCreated(e);
            //LayoutEngine.DoLayout(new DUILayoutEventArgs(this, "HandleCreated"));
            foreach (DUIControl dUIControl in this.DUIControls)
            {
                dUIControl.DoHandleCreated(e);
            }
        }
        internal virtual void DoLayout(DUILayoutEventArgs e)
        {
            OnLayout();
        }
        internal virtual void DoResize(EventArgs e)
        {
            OnResize(e);
        }
        internal virtual void DoLocationChanged(EventArgs e)
        {
            OnLocationChanged(e);
        }
        internal virtual void DoMouseEnter(EventArgs e)
        {
            if (!this.Visible || !this.Enabled) { return; }
            OnMouseEnter(e);
            if (this.DUIParent != null && this.CanMouseThrough)
            {
                this.DUIParent.DoMouseEnter(e);
            }
            if (!string.IsNullOrWhiteSpace(this.ToolTip))
            {
                ShowToolTip(this.ToolTip);
            }
        }
        /// <summary> 有的时候点击或者双击之类的动作让窗体变换位置，响应不到Leave事件
        /// </summary>
        internal virtual void DoMouseLeave()
        {
            if (this.DUIControlShare.MouseMoveDUIControl != null && !this.DUIControlShare.MouseMoveDUIControl.RectangleToScreen(this.DUIControlShare.MouseMoveDUIControl.Bounds).Contains(DUIControl.MousePosition))
            {
                this.DUIControlShare.MouseMoveDUIControl.DoMouseLeave(EventArgs.Empty);
                this.DUIControlShare.MouseMoveDUIControl = null;
            }
        }
        internal virtual void DoMouseLeave(EventArgs e)
        {
            //if (!this.Visible || !this.Enabled) { return; }
            OnMouseLeave(e);
            if (this.DUIParent != null && this.CanMouseThrough)
            {
                this.DUIParent.DoMouseLeave(e);
            }
            if (!string.IsNullOrWhiteSpace(this.ToolTip))
            {
                HideToolTip();
            }
        }
        internal virtual void DoMouseHover(EventArgs e)
        {
            if (!this.Visible || !this.Enabled) { return; }
            OnMouseHover(e);
        }
        internal virtual void DoMouseClick(DUIMouseEventArgs e)
        {
            if (!this.Visible || !this.Enabled) { return; }
            DUIControl dUIControl = this.GetChildAtPoint(e.Location);
            if (dUIControl == null)
            {
                this.AlwaysMouseClick(this, e);
                this.InvokeOnMouseClick(this, e);
            }
            else
            {
                dUIControl.DoMouseClick(new DUIMouseEventArgs(e.Button, e.Clicks, dUIControl.PointFromParent(e.Location), e.Delta));
            }
        }
        internal virtual void DoMouseDoubleClick(DUIMouseEventArgs e)
        {
            if (!this.Visible || !this.Enabled) { return; }
            DUIControl dUIControl = this.GetChildAtPoint(e.Location);
            if (dUIControl == null)
            {
                this.AlwaysMouseDoubleClick(this, e);
                this.InvokeOnMouseDoubleClick(this, e);
            }
            else
            {
                dUIControl.DoMouseDoubleClick(new DUIMouseEventArgs(e.Button, e.Clicks, dUIControl.PointFromParent(e.Location), e.Delta));
            }
        }
        internal virtual void DoMouseDown(DUIMouseEventArgs e)
        {
            if (!this.Visible || !this.Enabled) { return; }
            DUIControl dUIControl = this.GetChildAtPoint(e.Location);
            if (dUIControl == null)
            {
                this.Focus();
                this.AlwaysMouseDown(this, e);
                this.InvokeOnMouseDown(this, e);
            }
            else
            {
                dUIControl.DoMouseDown(new DUIMouseEventArgs(e.Button, e.Clicks, dUIControl.PointFromParent(e.Location), e.Delta));
            }
        }
        internal virtual void DoMouseMove(DUIMouseEventArgs e)
        {
            if (!this.Visible || !this.Enabled) { return; }
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
                    this.AlwaysMouseMove(this, e);
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
                    this.AlwaysMouseMove(this, e);
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
        internal virtual void DoMouseUp(DUIMouseEventArgs e)
        {
            if (!this.Visible || !this.Enabled) { return; }
            var mouseDownDUIControls = this.DUIControlShare.MouseDownDUIControls.Where(c => c.Button == e.Button).ToList();
            if (mouseDownDUIControls.Count == 0) { return; }
            if (mouseDownDUIControls[0].AffectedControl == this)
            {
                this.AlwaysMouseUp(this, e);
                this.InvokeOnMouseUp(this, e);
            }
            else
            {
                this.DUIControls.OfType<DUIControl>().Where(d => d.Visible).ToList().ForEach(d =>
                {
                    d.DoMouseUp(new DUIMouseEventArgs(e.Button, e.Clicks, d.PointFromParent(e.Location), e.Delta));
                });
            }
        }
        internal virtual void DoMouseWheel(DUIMouseEventArgs e)
        {
            if (!this.Visible || !this.Enabled) { return; }
            if (this.DUIControlShare.FocusedDUIControl == null) { return; }
            if (this == this.DUIControlShare.FocusedDUIControl)
            {
                this.AlwaysMouseWheel(this, e);
                this.InvokeOnMouseWheel(this, e);
            }
            else
            {
                if (this.DUIControlShare.FocusedDUIControl == this)
                {
                    this.AlwaysMouseWheel(this, e);
                    this.InvokeOnMouseWheel(this, e);
                }
                else
                {
                    this.DUIControls.OfType<DUIControl>().Where(d => d.Visible).ToList().ForEach(d =>
                    {
                        d.DoMouseWheel(new DUIMouseEventArgs(e.Button, e.Clicks, d.PointFromParent(e.Location), e.Delta));
                    });
                }
            }
        }
        internal virtual void DoKeyDown(KeyEventArgs e)
        {
            if (!this.Visible || !this.Enabled) { return; }
            if (this.DUIControlShare.FocusedDUIControl == null) { return; }
            if (this == this.DUIControlShare.FocusedDUIControl)
            {
                this.OnKeyDown(e);
            }
            else
            {
                foreach (DUIControl d in this.DUIControls)
                {
                    d.DoKeyDown(e);
                    //if (d == this.DUIControlShare.FocusedDUIControl)
                    //{
                    //    if (d.CanMouseThrough)
                    //    {
                    //        this.OnKeyDown(e);
                    //    }
                    //}
                }
            }
        }
        internal virtual void DoKeyPress(KeyPressEventArgs e)
        {
            if (!this.Visible || !this.Enabled) { return; }
            if (this.DUIControlShare.FocusedDUIControl == null) { return; }
            if (this == this.DUIControlShare.FocusedDUIControl)
            {
                this.OnKeyPress(e);
            }
            else
            {
                foreach (DUIControl d in this.DUIControls)
                {
                    d.DoKeyPress(e);
                    //if (d == this.DUIControlShare.FocusedDUIControl)
                    //{
                    //    if (d.CanMouseThrough)
                    //    {
                    //        this.OnKeyPress(e);
                    //    }
                    //}
                }
            }
        }
        internal virtual void DoKeyUp(KeyEventArgs e)
        {
            if (!this.Visible || !this.Enabled) { return; }
            if (this.DUIControlShare.FocusedDUIControl == null) { return; }
            if (this == this.DUIControlShare.FocusedDUIControl)
            {
                this.OnKeyUp(e);
            }
            else
            {
                foreach (DUIControl d in this.DUIControls)
                {
                    d.DoKeyUp(e);
                    //if (d == this.DUIControlShare.FocusedDUIControl)
                    //{
                    //    if (d.CanMouseThrough)
                    //    {
                    //        this.OnKeyUp(e);
                    //    }
                    //}
                }
            }
        }
        internal virtual void DoPaint(DUIPaintEventArgs e)
        {
            DUIGraphicsState backupGraphicsState = e.Graphics.Save();
            e.Graphics.TranslateTransform(this.X, this.Y); //偏移一下坐标系将控件的坐标定义为坐标系原点
            PointF center = new PointF(this.BorderWidth + this.CenterX, this.BorderWidth + this.CenterY);
            e.Graphics.RotateTransform(this.Rotate, center);
            e.Graphics.SkewTransform(this.Skew, center);
            e.Graphics.ScaleTransform(this.Scale, center);
            e.Graphics.PushLayer(this.Width, this.Height); //背景图层
            #region OnPaintBackground
            DUIGraphicsState backupOnPaintBackgroundGraphicsState = e.Graphics.Save();
            OnPaintBackground(new DUIPaintEventArgs(e.Graphics, new RectangleF(0, 0, this.Width, this.Height))); //绘制背景
            e.Graphics.Restore(backupOnPaintBackgroundGraphicsState);
            #endregion
            if (this.BorderWidth != 0)
            {
                e.Graphics.PopLayer();
                e.Graphics.TranslateTransform(this.BorderWidth, this.BorderWidth); //偏移一个边框的坐标系
                e.Graphics.PushLayer(this.ClientSize.Width, this.ClientSize.Height); //背景图层
            }
            #region OnPaint
            DUIGraphicsState backupOnPaintGraphicsState = e.Graphics.Save();
            OnPaint(new DUIPaintEventArgs(e.Graphics, new RectangleF(0, 0, ClientSize.Width, this.ClientSize.Height))); //先让子类画图
            e.Graphics.Restore(backupOnPaintGraphicsState);
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
            e.Graphics.TranslateTransform(-this.BorderWidth, -this.BorderWidth);
            e.Graphics.PopLayer();
            #region OnPaintForeground
            DUIGraphicsState backupOnPaintForegroundGraphicsState = e.Graphics.Save();
            OnPaintForeground(new DUIPaintEventArgs(e.Graphics, new RectangleF(0, 0, this.Width, this.Height))); //先让子类画图
            e.Graphics.Restore(backupOnPaintForegroundGraphicsState);
            #endregion
            e.Graphics.Restore(backupGraphicsState);
        }
        internal virtual void DoPaintBackground(DUIPaintEventArgs e)
        {
            if (this.Visible)
            {
                OnPaintBackground(new DUIPaintEventArgs(e.Graphics, new RectangleF(0, 0, this.Width, this.Height))); //让子类画的背景
            }
        }
        internal virtual void DoWndProc(ref Message m)
        {
            WndProc(ref m);
            try
            {
                foreach (DUIControl d in this.DUIControls)
                {
                    d.DoWndProc(ref m);
                }
            }
            catch (Exception ex)
            {
                Log.DUILog.GettingLog(ex);
            }
        }
        internal virtual void DoDefWndProc(ref Message m)
        {
            DefWndProc(ref m);
            try
            {
                foreach (DUIControl d in this.DUIControls)
                {
                    d.DoDefWndProc(ref m);
                }
            }
            catch (Exception ex)
            {
                Log.DUILog.GettingLog(ex);
            }
        }
        #endregion
        #region 子控件集合定义
        /// <summary> 子控件集合
        /// </summary>
        public class DUIControlCollection : DUIItemCollection<DUIControl, DUIControl>
        {
            public DUIControlCollection(DUIControl owner)
                : base(owner)
            {
            }
            public override void Add(DUIControl item)
            {
                if (item == null) { return; }
                if (item.DUIParent == owner)
                {
                    item.SendToBack();
                    return;
                }
                if (item.DUIParent != null)
                {
                    item.DUIParent.DUIControls.Remove(item);
                }
                base.Add(item);
                if (item.tabIndex == -1)
                {
                    int nextTabIndex = 0;
                    for (int c = 0; c < (Count - 1); c++)
                    {
                        int t = this[c].tabIndex;
                        if (nextTabIndex <= t)
                        {
                            nextTabIndex = t + 1;
                        }
                    }
                    item.TabIndex = nextTabIndex;
                }
                owner.SuspendLayout();
                var oldParent = item.dUIParent;
                item.dUIParent = owner;
                item.OnParentChanging(new DUIParentChangingEventArgs(owner, null));
                item.OnParentChanged(EventArgs.Empty);
                if (this.owner.IsHandleCreated)
                {
                    if (!item.IsHandleCreated) { item.DoHandleCreated(EventArgs.Empty); }
                }
                owner.ResumeLayout(false);
                //owner.Invalidate();
                if (owner.IsHandleCreated)
                {
                    LayoutEngine.DoLayoutDock(new DUILayoutEventArgs(owner, "SetDisplayRectangleLocation"));
                }
                this.owner.OnControlAdded(new DUIControlEventArgs(item));
            }
            public override void Remove(DUIControl item)
            {
                if (item == null) { return; }
                if (item.DUIParent == owner)
                {
                    item.isHandleCreated = false;
                    if (item.DUIControlShare.FocusedDUIControl == item)
                    {
                        item.DUIControlShare.FocusedDUIControl = null;
                    }
                    base.Remove(item);
                    item.dUIParent = null;
                    owner.Invalidate();
                    LayoutEngine.DoLayoutDock(new DUILayoutEventArgs(owner, "Remove"));
                    this.owner.OnControlRemoved(new DUIControlEventArgs(item));
                }
            }
            public override void RemoveAt(int index)
            {
                Remove(this[index]);
            }
            public override void Insert(int index, DUIControl item)
            {
                Add(item);
                int i = IndexOf(item);
                MoveDUIControl(item, i, index);
            }
            public override void Clear()
            {
                owner.SuspendLayout();
                if (Count != 0)
                {
                    while (Count != 0)
                    {
                        RemoveAt(Count - 1);
                    }
                }
                owner.ResumeLayout();
            }
            public int GetChildIndex(DUIControl child)
            {
                return GetChildIndex(child, true);
            }
            protected virtual int GetChildIndex(DUIControl child, bool throwException)
            {
                int index = IndexOf(child);
                if (index == -1 && throwException)
                {
                    //throw new ArgumentException("这个控件没有子控件");
                }
                return index;
            }
            internal virtual void SetChildIndexInternal(DUIControl child, int newIndex)
            {
                if (child == null)
                {
                    throw new ArgumentNullException("child");
                }
                int currentIndex = GetChildIndex(child);
                if (currentIndex == newIndex)
                {
                    return;
                }
                if (newIndex >= Count || newIndex == -1)
                {
                    newIndex = Count - 1;
                }
                MoveDUIControl(child, currentIndex, newIndex);
                LayoutEngine.DoLayoutDock(new DUILayoutEventArgs(owner, "SetChildIndexInternal"));
            }
            /// <summary> Copy .Net4.5源码
            /// </summary>
            /// <param name="child"></param>
            /// <param name="fromIndex"></param>
            /// <param name="toIndex"></param>
            private void MoveDUIControl(DUIControl child, int fromIndex, int toIndex)
            {
                int delta = toIndex - fromIndex;
                switch (delta)
                {
                    case -1:
                    case 1:
                        this[fromIndex] = this[toIndex];
                        break;
                    default:
                        int start = 0;
                        int dest = 0;
                        if (delta > 0)
                        {
                            start = fromIndex + 1;
                            dest = fromIndex;
                        }
                        else
                        {
                            start = toIndex;
                            dest = toIndex + 1;
                            delta = -delta;
                        }
                        if (start < dest)
                        {
                            start = start + delta;
                            dest = dest + delta;
                            for (; delta > 0; delta--)
                            {
                                this[--dest] = this[--start];
                            }
                        }
                        else
                        {
                            for (; delta > 0; delta--)
                            {
                                this[dest++] = this[start++];
                            }
                        }
                        break;
                }
                this[toIndex] = child;
            }
            public virtual void SetChildIndex(DUIControl child, int newIndex)
            {
                SetChildIndexInternal(child, newIndex);
            }
        }
        #endregion
    }
}
