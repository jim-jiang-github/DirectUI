using DirectUI.Common;
using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Controls
{
    /// <summary> 开关状态
    /// </summary>
    /// <param name="switchState"></param>
    public delegate void SwitchChangeHandler(bool switchState);
    public class DUISwitch : DUIControl
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
        private Color switchColor = Color.White;
        private Color switchOnColor = Color.LightGreen;
        private Color switchOffColor = Color.LightPink;
        private string switchOnText = "ON";
        private string switchOffText = "OFF";
        private bool isSwitchOn = false;
        #endregion
        #region 属性
        /// <summary> 开关按钮颜色
        /// </summary>
        public Color SwitchColor
        {
            get => switchColor;
            set
            {
                switchColor = value;
                this.Invalidate();
            }
        }
        /// <summary> 开关打开的颜色
        /// </summary>
        public Color SwitchOnColor
        {
            get => switchOnColor;
            set
            {
                switchOnColor = value;
                this.Invalidate();
            }
        }
        /// <summary> 开关关闭的颜色
        /// </summary>
        public Color SwitchOffColor
        {
            get => switchOffColor;
            set
            {
                switchOffColor = value;
                this.Invalidate();
            }
        }
        /// <summary> 开关打开的文字
        /// </summary>
        public string SwitchOnText
        {
            get => switchOnText;
            set
            {
                switchOnText = value;
                this.Invalidate();
            }
        }
        /// <summary> 开关关闭的文字
        /// </summary>
        public string SwitchOffText
        {
            get => switchOffText;
            set
            {
                switchOffText = value;
                this.Invalidate();
            }
        }
        /// <summary> 开关是否打开
        /// </summary>
        public bool IsSwitchOn
        {
            get => isSwitchOn;
            set
            {
                isSwitchOn = value;
                this.Invalidate();
            }
        }
        #endregion
        public DUISwitch()
        {
            this.ForeColor = Color.White;
            this.Font = new DUIFont("黑体", 12, FontStyle.Bold);
            this.Width = 50;
            this.Height = 20;
        }
        #region 重写
        public override void OnMouseClick(DUIMouseEventArgs e)
        {
            base.OnMouseClick(e);
            this.IsSwitchOn = !this.IsSwitchOn;
            this.OnSwitchChange(this.IsSwitchOn);
        }
        public override void OnPaintBackground(DUIPaintEventArgs e)
        {
            e.Graphics.SmoothingMode = DUISmoothingMode.AntiAlias; //抗锯齿
            e.Graphics.TextRenderingHint = DUITextRenderingHint.AntiAlias;
            using (DUISolidBrush sbBack = new DUISolidBrush(this.IsSwitchOn ? this.SwitchOnColor : this.SwitchOffColor))
            using (DUISolidBrush sbSwitch = new DUISolidBrush(this.SwitchColor))
            using (DUISolidBrush sbText = new DUISolidBrush(this.ForeColor))
            {
                e.Graphics.FillRoundedRectangle(sbBack, this.ClientRectangle, this.ClientSize.Height / 2);
                float switchRadius = this.ClientSize.Height / 2;
                e.Graphics.FillEllipse(sbSwitch, new RectangleF(this.IsSwitchOn ? 0 : this.ClientSize.Width - switchRadius * 2, 0, switchRadius * 2, switchRadius * 2));
                string text = this.IsSwitchOn ? this.SwitchOnText : this.SwitchOffText;
                SizeF textSize = e.Graphics.MeasureString(text, this.Font);
                PointF textLocation = new PointF((this.ClientSize.Width - switchRadius * 2 - textSize.Width) / 2 + (this.IsSwitchOn ? switchRadius * 2 : 0), (this.ClientSize.Height - textSize.Height) / 2);
                e.Graphics.DrawString(text, this.Font, sbText, textLocation);
                if (this.BorderWidth > 0)
                {
                    using (DUIPen pen = new DUIPen(this.BorderColor, this.BorderWidth))
                    {
                        e.Graphics.DrawRoundedRectangle(pen, this.ClientRectangle, this.ClientSize.Height / 2);
                    }
                }
            }
        }
        #endregion
    }
}
