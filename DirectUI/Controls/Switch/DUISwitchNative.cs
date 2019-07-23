using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Controls
{
    public class DUISwitchNative : DUINativeControl
    {
        #region 事件
        /// <summary> 开关状态事件
        /// </summary>
        public event SwitchChangeHandler SwitchChange;
        protected virtual void OnSwitchChange(bool switchState)
        {
            if (SwitchChange != null)
            {
                SwitchChange(switchState);
            }
        }
        #endregion
        #region 变量
        private DUISwitch dUISwitch = new DUISwitch() { Dock = System.Windows.Forms.DockStyle.Fill };
        #endregion
        #region 属性
        public new Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value;
                dUISwitch.Font = new Common.DUIFont(base.Font);
            }
        }
        public new Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                dUISwitch.ForeColor = value;
            }
        }
        /// <summary> 边框颜色
        /// </summary>
        public virtual Color BorderColor { get => dUISwitch.BorderColor; set => dUISwitch.BorderColor = value; }
        /// <summary> 边框宽度
        /// </summary>
        public virtual float BorderWidth { get => dUISwitch.BorderWidth; set => dUISwitch.BorderWidth = value; }
        /// <summary> 开关按钮颜色
        /// </summary>
        public Color SwitchColor { get => dUISwitch.SwitchColor; set => dUISwitch.SwitchColor = value; }
        /// <summary> 开关打开的颜色
        /// </summary>
        public Color SwitchOnColor { get => dUISwitch.SwitchOnColor; set => dUISwitch.SwitchOnColor = value; }
        /// <summary> 开关关闭的颜色
        /// </summary>
        public Color SwitchOffColor { get => dUISwitch.SwitchOffColor; set => dUISwitch.SwitchOffColor = value; }
        /// <summary> 开关打开的文字
        /// </summary>
        public string SwitchOnText { get => dUISwitch.SwitchOnText; set => dUISwitch.SwitchOnText = value; }
        /// <summary> 开关关闭的文字
        /// </summary>
        public string SwitchOffText { get => dUISwitch.SwitchOffText; set => dUISwitch.SwitchOffText = value; }
        /// <summary> 开关是否打开
        /// </summary>
        public bool IsSwitchOn
        {
            get => dUISwitch.IsSwitchOn;
            set
            {
                dUISwitch.IsSwitchOn = value;
                this.Invalidate();
            }
        }
        #endregion
        public DUISwitchNative()
        {
            this.ForeColor = Color.White;
            this.Font = new Font("黑体", 12, FontStyle.Bold);
            this.Width = 50;
            this.Height = 20;
            this.dUISwitch.SwitchChange += (s) =>
            {
                OnSwitchChange(s);
            };
            this.DUIControls.Add(dUISwitch);
        }
    }
}
