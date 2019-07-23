using DirectUI.Collection;
using DirectUI.Common;
using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Controls
{
    public abstract class DUICheckBoxBase : DUIControl
    {
        #region 事件
        //
        // 摘要: 
        //     当 System.Windows.Forms.CheckBox.Checked 属性的值更改时发生。
        public event EventHandler CheckedChanged;
        protected virtual void OnCheckedChanged(EventArgs e)
        {
            if (CheckedChanged != null)
            {
                CheckedChanged(this, e);
            }
        }
        #endregion
        #region 变量
        private int checkStateIndex = 0;
        private DUICheckStateCollection checkStates = null;
        #endregion
        #region 属性
        /// <summary> 选中状态的集合
        /// </summary>
        protected DUICheckStateCollection CheckStates
        {
            get { return checkStates; }
        }
        /// <summary> 选中状态的序号
        /// </summary>
        public int CheckStateIndex
        {
            get { return checkStateIndex; }
            set { checkStateIndex = value; }
        }
        /// <summary> 当前选择的状态
        /// </summary>
        protected DUICheckState CurrentCheckState
        {
            get
            {
                return CheckStates[CheckStateIndex];
            }
        }
        #endregion
        public DUICheckBoxBase()
        {
            this.checkStates = new DUICheckStateCollection(this);
        }
        #region 重写
        public override void OnPaint(DUIPaintEventArgs e)
        {
            //e.Graphics.FillRectangle(Brushes.Bisque, e.ClipRectangle);
            base.OnPaint(e);
            CurrentCheckState.DrawCheckState(e);
            SizeF sizef = e.Graphics.MeasureString(this.Text, this.Font);
            using (DUISolidBrush brush = new DUISolidBrush(this.ForeColor))
            {
                e.Graphics.DrawString(this.Text, this.Font, brush, new PointF(CurrentCheckState.Bounds.Right, this.Height / 2 - sizef.Height / 2 - 2));
            }
        }
        public override void OnMouseUp(DUIMouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (this.ClientRectangle.Contains(e.Location))
            {
                CheckStateIndex++;
                if (CheckStateIndex > this.CheckStates.Count - 1)
                {
                    CheckStateIndex = 0;
                }
                SetCheckState();
            }
        }
        #endregion
        #region 函数
        protected abstract void SetCheckState();
        #endregion
        #region 枚举状态集合对象
        public class DUICheckStateCollection : DUIItemCollection<DUICheckBoxBase, DUICheckState>
        {
            public DUICheckStateCollection(DUICheckBoxBase owner)
                : base(owner)
            {

            }
        }
        #endregion
    }
}
