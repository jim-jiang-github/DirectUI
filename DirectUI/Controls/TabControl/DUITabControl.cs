using DirectUI.Collection;
using DirectUI.Common;
using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace DirectUI.Controls
{
    public class DUITabControl : DUIControl
    {
        #region 变量
        private DUITabTitle tabTitle = null;
        private DUITabPage selectPage = null;
        private DUITabPageCollection tabPages = null;
        #endregion
        #region 属性
        public DUITabPageCollection TabPages => this.tabPages ?? (this.tabPages = new DUITabPageCollection(this));
        public DUITabPage SelectedPage => this.selectPage;
        public virtual SizeF TabTitleSize => new SizeF(160, 48);
        public virtual float TitleHeight => TabTitleSize.Height;
        #endregion
        public DUITabControl()
        {
            this.tabTitle = new DUITabTitle(this);
            this.tabPages = new DUITabPageCollection(this);
            this.DUIControls.Add(this.tabTitle);
        }
        #region 函数
        /// <summary> 选择下一页
        /// </summary>
        public void NextPage()
        {
            int selectIndex = this.TabPages.IndexOf(this.SelectedPage);
            if (selectIndex < this.TabPages.Count - 1)
            {
                this.SelectPage(this.TabPages[selectIndex + 1]);
            }
        }
        /// <summary> 选择上一页
        /// </summary>
        public void PrevPage()
        {
            int selectIndex = this.TabPages.IndexOf(this.SelectedPage);
            if (selectIndex > 0)
            {
                this.SelectPage(this.TabPages[selectIndex - 1]);
            }
        }
        public void SelectPage(DUITabPage tabPage)
        {
            this.selectPage = tabPage;
            this.Invalidate();
        }
        internal protected virtual void DrawTabPage(DUITabPage tabPage, DUIPaintEventArgs e)
        {
        }
        protected virtual void DrawTabTitle(DUIPaintEventArgs e)
        {
        }
        protected virtual void DrawTabTitle(DUITabPage tabPage, DUIPaintEventArgs e)
        {
            using (DUISolidBrush sb = new DUISolidBrush(tabPage.Selected ? Color.FromArgb(36, 107, 225) : Color.FromArgb(47, 52, 55)))
            {
                e.Graphics.FillRectangle(sb, new RectangleF(tabPage.TitleX, tabPage.TitleY, this.TabTitleSize.Width, this.TabTitleSize.Height));
            }
            SizeF sizeF = e.Graphics.MeasureString(tabPage.Text, this.Font);
            using (DUISolidBrush brush = new DUISolidBrush(Color.FromArgb(220, 224, 225)))
            {
                e.Graphics.DrawString(tabPage.Text, this.Font, brush, new PointF(tabPage.TitleX + this.TabTitleSize.Width / 2 - sizeF.Width / 2, tabPage.TitleY + this.TabTitleSize.Height / 2 - sizeF.Height / 2));
            }
        }
        protected virtual void ResetTabTitle()
        {
            for (int i = 0; i < this.TabPages.Count; i++)
            {
                DUITabPage item = this.TabPages[i];
                item.TitleX = this.ClientSize.Width / 2 - (this.TabPages.Count * this.TabTitleSize.Width) / 2 + i * this.TabTitleSize.Width;
                item.TitleY = 0;
            }
        }
        #endregion
        #region 重写
        public override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.ResetTabTitle();
        }
        #endregion

        public class DUITabTitle : DUIScrollableControl
        {
            private DUITabControl owner = null;
            public override float Height => this.owner == null ? 0 : this.owner.TitleHeight;
            public DUITabTitle(DUITabControl owner)
            {
                this.owner = owner;
                this.Dock = System.Windows.Forms.DockStyle.Top;
            }
            public override void OnMouseDown(DUIMouseEventArgs e)
            {
                base.OnMouseDown(e);
                DUITabPage tabPage = this.owner.TabPages.OfType<DUITabPage>().FirstOrDefault(t => new RectangleF(t.TitleX, t.TitleY, this.owner.TabTitleSize.Width, this.owner.TabTitleSize.Height).Contains(e.Location));
                if (tabPage != null)
                {
                    this.owner.SelectPage(tabPage);
                }
            }
            public override void OnPaintBackground(DUIPaintEventArgs e)
            {
                base.OnPaintBackground(e);
                this.owner.DrawTabTitle(e);
                foreach (DUITabPage tabPage in this.owner.TabPages)
                {
                    this.owner.DrawTabTitle(tabPage, e);
                }
            }
        }
        public class DUITabPageCollection : DUIItemCollection<DUITabControl, DUITabPage>
        {
            public DUITabPageCollection(DUITabControl owner)
                : base(owner)
            {
            }
            public override void Add(DUITabPage item)
            {
                item.TabControl = this.owner;
                base.Add(item);
                this.owner.DUIControls.Add(item);
                this.owner.ResetTabTitle();
            }
            public override void Remove(DUITabPage item)
            {
                base.Remove(item);
                this.owner.DUIControls.Remove(item);
                this.owner.ResetTabTitle();
            }
            public override void Clear()
            {
                base.Clear();
                foreach (DUITabPage item in this)
                {
                    this.owner.DUIControls.Remove(item);
                }
                this.owner.ResetTabTitle();
            }
        }
    }
}
