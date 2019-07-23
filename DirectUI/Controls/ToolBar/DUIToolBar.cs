using DirectUI.Collection;
using DirectUI.Controls;
using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DirectUI.Controls
{
    public class DUIToolBar : DUIControl
    {
        private DUIToolBarItemCollection items = null;
        public DUIToolBarItemCollection Items
        {
            get { return items; }
        }
        public DUIToolBar()
        {
            this.Height = 20;
            this.Dock = System.Windows.Forms.DockStyle.Top;
            this.BorderWidth = 0;
            this.items = new DUIToolBarItemCollection(this);
        }
        public class DUIToolBarItemCollection : DUIItemCollection<DUIToolBar, DUIToolBarItem>
        {
            private int space = 2;
            public DUIToolBarItemCollection(DUIToolBar owner) : base(owner) { }
            public override void Add(DUIToolBarItem item)
            {
                item.Y = space;
                item.Width = this.owner.Height - space * 2;
                item.Height = this.owner.Height - space * 2;
                this.owner.DUIControls.Add(item);
                base.Add(item);
                for (int i = 0; i < this.Count; i++)
                {
                    this[i].X = (this.owner.Height - space) * i + space;
                }
            }
            public override void Remove(DUIToolBarItem value)
            {
                this.owner.DUIControls.Remove(value);
                base.Remove(value);
                for (int i = 0; i < this.Count; i++)
                {
                    this[i].X = (this.owner.Height - space) * i + space;
                }
            }
            public override void Insert(int index, DUIToolBarItem item)
            {
                item.Y = space;
                item.Width = this.owner.Height - space * 2;
                item.Height = this.owner.Height - space * 2;
                this.owner.DUIControls.Insert(index, item);
                base.Insert(index, item);
                for (int i = 0; i < this.Count; i++)
                {
                    this[i].X = (this.owner.Height - space) * i + space;
                }
            }
            public override void Clear()
            {
                this.owner.DUIControls.OfType<DUIToolBarItem>().ToList().ForEach(d =>
                {
                    this.owner.DUIControls.Remove(d);
                });
                base.Clear();
            }
        }
    }
}
