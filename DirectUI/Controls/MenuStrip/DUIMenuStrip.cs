using DirectUI.Collection;
using DirectUI.Common;
using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DirectUI.Controls
{
    public class DUIMenuStrip : DUIControl
    {
        private ToolStripItem lastToolStripItem = null;
        private bool isMouseEnter = false;
        private DUIToolStripItemCollection items = null;
        internal ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

        public DUIToolStripItemCollection Items
        {
            get { return items; }
        }
        public DUIMenuStrip()
        {
            this.BorderWidth = 0;
            this.BackColor = Color.Transparent;
            this.items = new DUIToolStripItemCollection(this);
            contextMenuStrip.VisibleChanged += (s, e) =>
            {
                if (!contextMenuStrip.Visible)
                {
                    ToolStripMenuItem toolStripMenuItem = contextMenuStrip.Tag as ToolStripMenuItem;
                    if (toolStripMenuItem != null)
                    {
                        while (contextMenuStrip.Items.Count > 0)
                        {
                            toolStripMenuItem.DropDownItems.Add(contextMenuStrip.Items[0]);
                        }
                    }
                }
            };
            //this.Dock = DockStyle.Top;
        }
        internal SizeF TextSize(string text)
        {
            using (Graphics g = Graphics.FromHwnd(this.Handle))
            {
                StringFormat format = (StringFormat)StringFormat.GenericTypographic.Clone();
                format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
                SizeF sf = g.MeasureString(text, this.Font, new PointF(0, 0), format);
                //sf.Height = this.Font.GetHeight();
                return sf;
                //return Size.Ceiling(sf);
            }
        }
        public override float Width
        {
            get
            {
                if (this.Items == null || this.Items.Count == 0) { return 0; }
                return this.Items.OfType<ToolStripItem>().Sum(t => t.Width) + 2;
                //return base.Width;
            }
        }
        public override float Height
        {
            get
            {
                return (int)Math.Ceiling(this.Font.GetHeight()) + 2;
                //return base.Width;
            }
        }
        public override void OnPaint(DUIPaintEventArgs e)
        {
            base.OnPaint(e);
            StringFormat format = (StringFormat)StringFormat.GenericTypographic.Clone();
            format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
            foreach (ToolStripItem tsi in this.Items)
            {
                if (tsi.Enabled)
                {
                    Rectangle tsiBounds = GetBounds(tsi);
                    if (isMouseEnter && lastToolStripItem == tsi)
                    {
                        //e.Graphics.DrawRectangle(Pens.Black, new Rectangle(tsiBounds.X, tsiBounds.Y, tsiBounds.Width - 1, this.Height - 1));
                    }
                    using (DUISolidBrush brush = new DUISolidBrush(this.ForeColor))
                    {
                        e.Graphics.DrawString(tsi.Text, this.Font, brush, new PointF(tsiBounds.X + tsiBounds.Width / 2 - this.TextSize(tsi.Text).Width / 2, 0));
                    }
                }
            }
        }
        public override void OnMouseDown(DUIMouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                //contextMenuStrip.Tag = this;
                if (contextMenuStrip.Visible)
                {
                    contextMenuStrip.Hide();
                }
                else
                {
                    ToolStripMenuItem toolStripMenuItem = (lastToolStripItem as ToolStripMenuItem);
                    if (toolStripMenuItem == null) { return; }
                    contextMenuStrip.Tag = toolStripMenuItem;
                    if (toolStripMenuItem.DropDownItems.Count == 0)
                    {
                        return;
                    }
                    while (toolStripMenuItem.DropDownItems.Count > 0)
                    {
                        contextMenuStrip.Items.Add(toolStripMenuItem.DropDownItems[0]);
                    }
                    Rectangle tsiBounds = GetBounds(toolStripMenuItem);
                    contextMenuStrip.Show(Point.Ceiling(this.PointToScreen(new PointF(tsiBounds.X, this.Height))));
                }
            }
        }
        public override void OnMouseMove(DUIMouseEventArgs e)
        {
            base.OnMouseMove(e);
            foreach (ToolStripItem tsi in this.Items)
            {
                if (tsi.Enabled)
                {
                    Rectangle tsiBounds = GetBounds(tsi);
                    if (isMouseEnter && tsiBounds.Contains(Point.Ceiling(this.PointToClient(DUIControl.MousePosition))) && lastToolStripItem != tsi)
                    {
                        lastToolStripItem = tsi;
                        this.Invalidate();
                    }
                }
            }
        }
        public override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.isMouseEnter = false;
            this.Invalidate();
        }
        public override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.isMouseEnter = true;
            this.Invalidate();
        }
        private Rectangle GetBounds(ToolStripItem toolStripItem)
        {
            int x = 1;
            for (int i = 0; i < this.Items.Count; i++)
            {
                if (this.Items[i].Enabled)
                {
                    if (this.Items[i] == toolStripItem)
                    {
                        return new Rectangle(x, 0, toolStripItem.Width, toolStripItem.Height);
                    }
                    x += this.Items[i].Width;
                }
            }
            return Rectangle.Empty;
        }
        public class DUIToolStripItemCollection : DUIItemCollection<DUIMenuStrip, ToolStripItem>
        {
            public DUIToolStripItemCollection(DUIMenuStrip owner) : base(owner) { }

            public override void Add(ToolStripItem item)
            {
                //this.owner.contextMenuStrip.Items.Add(item);
                base.Add(item);
                item.TextChanged += (s, e) =>
                {
                    item.Width = (int)Math.Ceiling(this.owner.TextSize(item.Text).Width) + 20;
                };
                item.Width = (int)Math.Ceiling(this.owner.TextSize(item.Text).Width) + 20;
            }
            public override void Remove(ToolStripItem item)
            {
                //this.owner.contextMenuStrip.Items.Remove(item);
                base.Remove(item);
            }
            public override void Insert(int index, ToolStripItem item)
            {
                //this.owner.contextMenuStrip.Items.Insert(index, item);
                base.Insert(index, item);
            }
            public override void RemoveAt(int index)
            {
                //this.owner.contextMenuStrip.Items.RemoveAt(index);
                base.RemoveAt(index);
            }
            public override void Clear()
            {
                //this.owner.contextMenuStrip.Items.Clear();
                base.Clear();
            }
            //private void ResetLocation() 
            //{
            //    int x = 1;
            //    foreach (ToolStripItem toolStripItem in this)
            //    {
            //        x += toolStripItem.Width;
            //    }
            //}
        }
    }
}
