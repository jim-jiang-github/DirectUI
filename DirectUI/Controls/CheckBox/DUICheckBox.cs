using DirectUI.Common;
using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DirectUI.Controls
{
    public class DUICheckBox : DUICheckBoxBase
    {
        #region 变量
        private DUICheckState checkedState = null;
        private DUICheckState unCheckedState = null;
        private bool _checked = false;
        #endregion
        #region 属性
        public virtual bool Checked
        {
            get { return _checked; }
            set
            {
                if (_checked != value)
                {
                    _checked = value;
                    if (Checked)
                    {
                        this.CheckStateIndex = 0;
                    }
                    else
                    {
                        this.CheckStateIndex = 1;
                    }
                    this.Invalidate();
                    OnCheckedChanged(EventArgs.Empty);
                }
            }
        }
        public virtual DUICheckState CheckedState
        {
            get
            {
                if (this.checkedState == null)
                {
                    this.checkedState = new DUICheckedState(this);
                }
                return this.checkedState;
            }
        }
        public virtual DUICheckState UncheckedState
        {
            get
            {
                if (this.unCheckedState == null)
                {
                    this.unCheckedState = new DUIUncheckedState(this);
                }
                return this.unCheckedState;;
            }
        }
        #endregion
        public DUICheckBox()
        {
            this.Width = 18;
            this.Height = 18;
            this.CheckStates.Add(CheckedState);
            this.CheckStates.Add(UncheckedState);
            if (Checked)
            {
                this.CheckStateIndex = 0;
            }
            else
            {
                this.CheckStateIndex = 1;
            }
        }
        #region 重写
        public override DUIFont Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                this.Width = this.Height + DUITextRenderer.MeasureText(this.Text, this.Font).Width;
            }
        }
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                this.Width = this.Height + DUITextRenderer.MeasureText(this.Text, this.Font).Width;
            }
        }
        protected override void SetCheckState()
        {
            if (this.CurrentCheckState.CheckStateName == "Checked")
            {
                Checked = true;
            }
            else
            {
                Checked = false;
            }
        }
        #endregion
        #region 选中状态
        public class DUICheckedState : DUICheckState
        {
            #region 变量
            private DUICheckBox owner = null;
            private static DUIImage tickBlackImage = DUIImage.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("DirectUI.Resources.tickBlack.png"));
            #endregion
            #region 属性
            #endregion
            public DUICheckedState(DUICheckBox owner)
            {
                this.owner = owner;
            }
            #region 重写
            public override float X
            {
                get
                {
                    return 0;
                }
            }
            public override float Y
            {
                get
                {
                    return 0;
                }
            }
            public override float Width
            {
                get
                {
                    return this.owner.Height;
                }
            }
            public override float Height
            {
                get
                {
                    return this.owner.Height;
                }
            }
            public override string CheckStateName
            {
                get
                {
                    return "Checked";
                }
            }

            public override void DrawCheckState(DUIPaintEventArgs e)
            {
                using (DUIPen pen = new DUIPen(this.owner.ForeColor, 2))
                {
                    e.Graphics.DrawRectangle(pen, new RectangleF(Bounds.X + 3, Bounds.Y + 3, Bounds.Width - 6, Bounds.Height - 6));
                }
                e.Graphics.DrawImage(tickBlackImage, new RectangleF(Bounds.X + 5, Bounds.Y + 5, Bounds.Width - 10, Bounds.Height - 10));
            }
            #endregion
        }
        #endregion
        #region 未选中状态
        public class DUIUncheckedState : DUICheckState
        {
            #region 变量
            private DUICheckBox owner = null;
            #endregion
            #region 属性
            #endregion
            public DUIUncheckedState(DUICheckBox owner)
            {
                this.owner = owner;
            }
            #region 重写
            public override float X
            {
                get
                {
                    return 0;
                }
            }
            public override float Y
            {
                get
                {
                    return 0;
                }
            }
            public override float Width
            {
                get
                {
                    return this.owner.Height;
                }
            }
            public override float Height
            {
                get
                {
                    return this.owner.Height;
                }
            }
            public override string CheckStateName
            {
                get
                {
                    return "Unchecked";
                }
            }

            public override void DrawCheckState(DUIPaintEventArgs e)
            {
                using (DUIPen pen = new DUIPen(this.owner.ForeColor, 2))
                {
                    e.Graphics.DrawRectangle(pen, new RectangleF(Bounds.X + 3, Bounds.Y + 3, Bounds.Width - 6, Bounds.Height - 6));
                }
            }
            #endregion
        }
        #endregion
    }
}
