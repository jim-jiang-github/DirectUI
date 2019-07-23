using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Core
{
    public abstract class DUIScrollProperties
    {
        private float thickness = 10;
        internal float value = 0;
        protected DUIScrollableControl parent;
        internal bool visible = false;
        private bool autoVisible = true;
        private bool enabled = true;
        protected DUIScrollProperties(DUIScrollableControl container)
        {
            this.parent = container;
        }
        /// <summary> Gets or sets a bool value controlling whether the scrollbar is enabled.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                if (parent.AutoScroll)
                {
                    return;
                }
                if (value != enabled)
                {
                    enabled = value;
                }
            }
        }
        internal abstract float PageSize { get; }
        internal abstract float HorizontalDisplayPosition { get; }
        internal abstract float VerticalDisplayPosition { get; }
        public float Value
        {
            get
            {
                return value;
            }
            set
            {
                if (this.value != value)
                {
                    if (value < 0)
                    {
                        value = 0;
                    }
                    this.value = value;
                }
            }
        }
        protected virtual SizeF ParentClientSize
        {
            get
            {
                return new SizeF(this.parent.Width - this.parent.Border.BorderWidth * 2, this.parent.Height - this.parent.Border.BorderWidth * 2);
            }
        }
        public abstract RectangleF ClientRectangle { get; }
        public abstract RectangleF BodyRectangle { get; }
        public float Thickness
        {
            get { return thickness; }
            set
            {
                if (thickness != value)
                {
                    thickness = value;
                    this.parent.Invalidate();
                }
            }
        }
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                if (parent.AutoScroll)
                {
                    return;
                }
                if (value != visible)
                {
                    visible = value;
                }
            }
        }
        public bool AutoVisible
        {
            get { return autoVisible; }
            set
            {
                if (autoVisible != value)
                {
                    autoVisible = value;
                }
            }
        }
    }
}
