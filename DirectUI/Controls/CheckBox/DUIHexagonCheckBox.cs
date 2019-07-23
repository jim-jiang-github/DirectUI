using DirectUI.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DirectUI.Controls
{
    public class DUIHexagonCheckBox : DUICheckBox
    {
        private Color checkBoxColor = Color.White;
        private DUICheckState checkedState = null;
        private DUICheckState unCheckedState = null;
        public Color CheckBoxColor
        {
            get { return checkBoxColor; }
            set { checkBoxColor = value; }
        }
        public override DUICheckState CheckedState
        {
            get
            {
                if (this.checkedState == null)
                {
                    this.checkedState = new DUIHexagonCheckedState(this);
                }
                return this.checkedState;
            }
        }
        public override DUICheckState UncheckedState
        {
            get
            {
                if (this.unCheckedState == null)
                {
                    this.unCheckedState = new DUIHexagonUncheckedState(this);
                }
                return this.unCheckedState;
            }
        }
        #region 选中状态
        public class DUIHexagonCheckedState : DUICheckState
        {
            #region 变量
            private DUIHexagonCheckBox owner = null;
            private DUIImage tickBlackImage = DUIImage.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("DirectUI.Resources.tickWhile.png"));
            #endregion
            #region 属性
            private PointF CenterPoint { get { return new PointF(Raduis, Raduis - 1); } }
            private float Raduis { get { return (float)(this.Height / 2f); } }
            private PointF[] HexagonPath
            {
                get
                {
                    PointF p1 = new PointF((float)(this.CenterPoint.X - Math.Cos(60 * Math.PI / 180) * this.Raduis), (float)(this.CenterPoint.Y - Math.Cos(30 * Math.PI / 180) * this.Raduis));
                    PointF p2 = new PointF((float)(this.CenterPoint.X + Math.Cos(60 * Math.PI / 180) * this.Raduis), (float)(this.CenterPoint.Y - Math.Cos(30 * Math.PI / 180) * this.Raduis));
                    PointF p3 = new PointF(this.CenterPoint.X + this.Raduis, this.CenterPoint.Y);
                    PointF p4 = new PointF((float)(this.CenterPoint.X + Math.Cos(60 * Math.PI / 180) * this.Raduis), (float)(this.CenterPoint.Y + Math.Cos(30 * Math.PI / 180) * this.Raduis));
                    PointF p5 = new PointF((float)(this.CenterPoint.X - Math.Cos(60 * Math.PI / 180) * this.Raduis), (float)(this.CenterPoint.Y + Math.Cos(30 * Math.PI / 180) * this.Raduis));
                    PointF p6 = new PointF(this.CenterPoint.X - this.Raduis, this.CenterPoint.Y);
                    PointF[] ps = new PointF[6] { p1, p2, p3, p4, p5, p6 };
                    return ps;
                }
            }
            #endregion
            public DUIHexagonCheckedState(DUIHexagonCheckBox owner)
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
                using (DUISolidBrush brush = new DUISolidBrush(this.owner.CheckBoxColor))
                {
                    e.Graphics.FillPolygon(brush, HexagonPath);
                }
                e.Graphics.DrawImage(tickBlackImage, new RectangleF(Bounds.X + 6, Bounds.Y + 6, Bounds.Width - 12, Bounds.Height - 12));
            }
            #endregion
        }
        #endregion
        #region 未选中状态
        public class DUIHexagonUncheckedState : DUICheckState
        {
            #region 变量
            private DUIHexagonCheckBox owner = null;
            #endregion
            #region 属性
            private PointF CenterPoint { get { return new PointF(Raduis, Raduis - 1); } }
            private float Raduis { get { return (float)(this.Height / 2f); } }
            private PointF[] HexagonPath
            {
                get
                {
                    PointF p1 = new PointF((float)(this.CenterPoint.X - Math.Cos(60 * Math.PI / 180) * this.Raduis), (float)(this.CenterPoint.Y - Math.Cos(30 * Math.PI / 180) * this.Raduis));
                    PointF p2 = new PointF((float)(this.CenterPoint.X + Math.Cos(60 * Math.PI / 180) * this.Raduis), (float)(this.CenterPoint.Y - Math.Cos(30 * Math.PI / 180) * this.Raduis));
                    PointF p3 = new PointF(this.CenterPoint.X + this.Raduis, this.CenterPoint.Y);
                    PointF p4 = new PointF((float)(this.CenterPoint.X + Math.Cos(60 * Math.PI / 180) * this.Raduis), (float)(this.CenterPoint.Y + Math.Cos(30 * Math.PI / 180) * this.Raduis));
                    PointF p5 = new PointF((float)(this.CenterPoint.X - Math.Cos(60 * Math.PI / 180) * this.Raduis), (float)(this.CenterPoint.Y + Math.Cos(30 * Math.PI / 180) * this.Raduis));
                    PointF p6 = new PointF(this.CenterPoint.X - this.Raduis, this.CenterPoint.Y);
                    PointF[] ps = new PointF[6] { p1, p2, p3, p4, p5, p6 };
                    return ps;
                }
            }
            #endregion
            public DUIHexagonUncheckedState(DUIHexagonCheckBox owner)
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
                e.Graphics.SmoothingMode = DUISmoothingMode.AntiAlias;
                using (DUISolidBrush brush = new DUISolidBrush(this.owner.CheckBoxColor))
                {
                    e.Graphics.FillPolygon(brush, HexagonPath);
                }
            }
            #endregion
        }
        #endregion
    }
}
