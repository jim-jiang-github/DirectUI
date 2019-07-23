using DirectUI.Collection;
using DirectUI.Common;
using DirectUI.Controls;
using DirectUI.Win32;
using DirectUI.Win32.Const;
using DirectUI.Win32.Struct;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace DirectUI.Core
{
    public partial class DUISkinForm : DUIForm
    {
        #region 变量
        private bool closeBox = true;
        private int captionHeight = 60;
        private int borderWidth = 8;
        private Color captionBackgroundColor = Color.Transparent;
        private Color captionForegroundColor = Color.Black;
        private Color borderColor = Color.Transparent;
        protected SysButtonCollection sysButtons = null;
        protected SysButton sysButtonClose = null;
        protected SysButton sysButtonMax = null;
        protected SysButton sysButtonRestore = null;
        protected SysButton sysButtonMin = null;
        private bool showText = true;
        private CaptionDUIControlCollection captionDUIControl = null;
        //记录最大化最小化时候的坐标和尺寸
        internal Point lastSysCommandLocation = Point.Empty;
        internal Size lastSysCommandSize = Size.Empty;
        #endregion
        #region 属性
        public bool CloseBox
        {
            get { return closeBox; }
            set
            {
                closeBox = value;
                this.Invalidate();
            }
        }
        /// <summary> 是否显示Text
        /// </summary>
        public bool ShowText
        {
            get { return showText; }
            set
            {
                showText = value;
                this.Invalidate();
            }
        }
        /// <summary> 标题栏控件
        /// </summary>
        public CaptionDUIControlCollection CaptionDUIControl
        {
            get { return captionDUIControl; }
        }
        /// <summary> 边框颜色
        /// </summary>
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.dUIContainer.Invalidate();
            }
        }
        /// <summary> 边框宽度
        /// </summary>
        public int BorderWidth
        {
            get
            {
                if (this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.None) { return 0; }
                return borderWidth;
            }
            set
            {
                borderWidth = value;
                this.dUIContainer.Invalidate();
            }
        }
        /// <summary> 标题前景色
        /// </summary>
        public Color CaptionForegroundColor
        {
            get { return captionForegroundColor; }
            set
            {
                captionForegroundColor = value;
                this.dUIContainer.Invalidate();
            }
        }
        /// <summary> 标题背景色
        /// </summary>
        public Color CaptionBackgroundColor
        {
            get { return captionBackgroundColor; }
            set
            {
                captionBackgroundColor = value;
                this.dUIContainer.Invalidate();
            }
        }
        /// <summary> 标题高度
        /// </summary>
        public int CaptionHeight
        {
            get
            {
                if (this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.None) { return 0; }
                return captionHeight;
            }
            set
            {
                int lastCaptionHeight = captionHeight;
                captionHeight = value;
                this.dUIContainer.Invalidate();
                #region 设计器模式下，刷新客户端区域，还没有找到什么更好的方式来处理，暂时先这么弄
                var size = this.ClientSize;
                this.ClientSize = Size.Empty;
                this.ClientSize = new Size(size.Width, size.Height + lastCaptionHeight - captionHeight);
                #endregion
            }
        }
        internal int CaptionHeightInternal
        {
            //由于最大化之后会超出屏幕8个像素，所以在这里做一点修正
            get
            {
                if (this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.None) { return 0; }
                return this.WindowState == FormWindowState.Maximized ? this.CaptionHeight + 8 : this.CaptionHeight;
            }
        }
        internal int BorderWidthInternal
        {
            //由于最大化之后会超出屏幕8个像素，所以在这里做一点修正
            get
            {
                if (this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.None) { return 0; }
                return this.WindowState == FormWindowState.Maximized ? this.BorderWidth + 8 : this.BorderWidth;
            }
        }
        public new FormBorderStyle FormBorderStyle
        {
            get { return base.FormBorderStyle; }
            set
            {
                if (value == System.Windows.Forms.FormBorderStyle.None)
                {
                    base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                }
                else if (value == System.Windows.Forms.FormBorderStyle.FixedToolWindow)
                {
                    base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
                }
                else
                {
                    base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
                }
                this.Invalidate();
            }
        }
        #endregion
        protected override void Init()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            //指定控件的样式和行为
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);// 控件透明
            this.SetStyle(ControlStyles.UserPaint, true); //用户自行重绘
            this.dUIContainer = new DUICaptionContainer(this);
            this.captionDUIControl = new CaptionDUIControlCollection(this);
            this.sysButtons = new SysButtonCollection(this);
            this.sysButtonClose = new SysButtonClose() { Width = 42 };
            this.sysButtonMax = new SysButtonMax() { Width = 42 };
            this.sysButtonRestore = new SysButtonRestore() { Width = 42 };
            this.sysButtonMin = new SysButtonMin() { Width = 42 };
            this.sysButtons.Add(this.sysButtonClose);
            this.sysButtons.Add(this.sysButtonMax);
            this.sysButtons.Add(this.sysButtonRestore);
            this.sysButtons.Add(this.sysButtonMin);
            this.sysButtons.Add(new SysButtonSkin());
        }
        public new void Invalidate()
        {
            if (this.dUIContainer != null) { this.dUIContainer.Invalidate(); }
        }
        #region 重载
        /// <summary> 禁止关闭按钮
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                int CS_NOCLOSE = 0x200;
                CreateParams parameters = base.CreateParams;
                parameters.ClassStyle |= CS_NOCLOSE;
                return parameters;
            }
        }
        /// <summary> 客户端区域改变的时候，主窗体尺寸变化
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected override void SetClientSizeCore(int x, int y)
        {
            base.SetClientSizeCore(x, y);
            if (this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.None) { return; }
            this.Size = new Size(this.ClientSize.Width + this.BorderWidth * 2, this.ClientSize.Height + this.CaptionHeight + this.BorderWidth);
        }
        #endregion
        #region 函数
        internal virtual void DoMaximize()
        {
            if (!this.MaximizeBox) { return; }
            this.WindowState = FormWindowState.Maximized;
            this.dUIContainer.Invalidate();
        }
        internal virtual void DoMinimize()
        {
            if (!this.MinimizeBox) { return; }
            this.WindowState = FormWindowState.Minimized;
            this.dUIContainer.Invalidate();
        }
        internal virtual void DoNormal()
        {
            if (!this.MaximizeBox) { return; }
            this.WindowState = FormWindowState.Normal;
            this.Location = this.lastSysCommandLocation;
            this.Size = this.lastSysCommandSize;
            this.dUIContainer.Invalidate();
        }
        public virtual void OnCaptionPaint(DUIPaintEventArgs e)
        {
            using (SolidBrush captionBackgroundColorBrush = new SolidBrush(this.CaptionBackgroundColor == Color.Transparent ? this.BackColor : this.CaptionBackgroundColor))
            using (SolidBrush captionForegroundColorBrush = new SolidBrush(this.CaptionForegroundColor))
            {
                e.Graphics.FillRectangle(captionBackgroundColorBrush, e.ClipRectangle);
                if (this.ShowIcon)
                {
                    //e.Graphics.DrawIcon(this.Icon, new Rectangle(8, 4, 22, 22));
                }
                if (this.ShowText)
                {
                    #region 文字的上下左右四个方向都偏移一个像素,形成一个四周阴影的效果
                    PointF textLocation = new PointF(8 + (ShowIcon ? 22 : 0) + 4, 8);
                    e.Graphics.DrawString(this.Text, this.Font, Brushes.White, new PointF(textLocation.X - 1F, textLocation.Y + 1F));
                    e.Graphics.DrawString(this.Text, this.Font, Brushes.White, new PointF(textLocation.X + 1F, textLocation.Y - 1F));
                    e.Graphics.DrawString(this.Text, this.Font, Brushes.White, new PointF(textLocation.X - 1F, textLocation.Y - 1F));
                    e.Graphics.DrawString(this.Text, this.Font, Brushes.White, new PointF(textLocation.X + 1F, textLocation.Y + 1F));
                    e.Graphics.DrawString(this.Text, this.Font, Brushes.Black, textLocation);
                    #endregion
                }
            }
        }
        public virtual void OnBorderPaint(PaintEventArgs e)
        {
            using (SolidBrush borderColorBrush = new SolidBrush(this.BorderColor == Color.Transparent ? this.BackColor : this.BorderColor))
            {
                e.Graphics.FillRectangle(borderColorBrush, e.ClipRectangle);
            }
        }

        #endregion
        #region 消息拦截 WndProc
        //protected override void WndProc(ref Message m)
        //{
        //    if (dUIContainer != null)
        //    {
        //        dUIContainer.DoWndProc(ref m);
        //    }
        //    else
        //    {
        //        DefWndProcInternal(ref m);
        //    }
        //}
        internal override void DefWndProcInternal(ref Message m)
        {
            base.DefWndProcInternal(ref m);
        }
        #endregion
        #region 系统按钮基类,如果要自定义系统按钮,就继承这个基类重写
        internal protected abstract class SysButton : DUIButton
        {
            protected DUISkinForm owner = null;
            internal DUISkinForm Owner
            {
                get { return owner; }
                set { owner = value; }
            }
            /// <summary> 前一个SysButton
            /// </summary>
            internal SysButton PreSysButton { get; set; }
            public override PointF Location
            {
                get
                {
                    if (this.owner == null) { return Point.Empty; }
                    int y = this.owner.WindowState == FormWindowState.Maximized ? 8 : 0;
                    int x = this.owner.WindowState == FormWindowState.Maximized ? 8 : 0;
                    if (!this.Visible)
                    {
                        return PreSysButton == null ? new Point(0, y) : PreSysButton.Location;
                    }
                    return PreSysButton == null ? new PointF(this.owner.Width - this.Width - x - 8, y) : new PointF(PreSysButton.Location.X - this.Width, y);
                }
            }
            public override float X
            {
                get
                {
                    return Location.X;
                }
            }
            public override float Y
            {
                get
                {
                    return Location.Y;
                }
            }
            public SysButton()
            {
                this.Width = 32;
                this.Height = 18;
                Visible = true;
                this.BorderWidth = 0;
                this.Radius = 0;
                this.BackColor = Color.Transparent;
                this.BorderColor = Color.Transparent;
            }
        }
        #endregion
        #region 系统按钮集合对象
        public class CaptionDUIControlCollection : ItemCollection<DUISkinForm, DUIControl>
        {
            public CaptionDUIControlCollection(DUISkinForm owner)
                : base(owner)
            {
            }
            public override void Add(DUIControl item)
            {
                this.owner.dUIContainer.DUIControls.Add(item);
                base.Add(item);
            }
        }
        #endregion
        #region 系统按钮集合对象
        internal protected class SysButtonCollection : ItemCollection<DUISkinForm, SysButton>
        {
            public SysButtonCollection(DUISkinForm owner)
                : base(owner)
            {
            }
            public override void Add(SysButton item)
            {
                if (base.Count != 0)
                {
                    item.PreSysButton = base[base.Count - 1];
                }
                item.Owner = this.owner;
                this.owner.dUIContainer.DUIControls.Add(item);
                base.Add(item);
            }
        }
        #endregion
        #region 系统按钮-关闭
        internal protected class SysButtonClose : SysButton
        {
            private Image closeDownBackImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("DirectUI.Resources.closeDownBack.png"));
            private Image closeMouseBackImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("DirectUI.Resources.closeMouseBack.png"));
            private Image closeNormlBackImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("DirectUI.Resources.closeNormlBack.png"));
            public SysButtonClose()
            {
                this.MouseDownImage = closeDownBackImage;
                this.MouseHoverImage = closeMouseBackImage;
                this.MouseNormalImage = closeNormlBackImage;
                this.ToolTip = "关闭窗口";
            }
            public override bool Visible
            {
                get
                {
                    return base.Visible && this.owner.CloseBox;
                }
                set
                {
                    base.Visible = value;
                }
            }
            protected override void OnClick(EventArgs e)
            {
                this.owner.Close();
            }
        }
        #endregion
        #region 系统按钮-最大化
        internal protected class SysButtonMax : SysButton
        {
            private Image maxDownBackImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("DirectUI.Resources.maxDownBack.png"));
            private Image maxMouseBackImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("DirectUI.Resources.maxMouseBack.png"));
            private Image maxNormlBackImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("DirectUI.Resources.maxNormlBack.png"));
            public SysButtonMax()
            {
                this.MouseDownImage = maxDownBackImage;
                this.MouseHoverImage = maxMouseBackImage;
                this.MouseNormalImage = maxNormlBackImage;
            }
            public override bool Visible
            {
                get
                {
                    if (this.owner.WindowState == FormWindowState.Normal)
                    {
                        return base.Visible && this.owner.MaximizeBox;
                    }
                    return false;
                }
            }
            protected override void OnClick(EventArgs e)
            {
                this.owner.DoMaximize();
                //this.owner.WindowState = FormWindowState.Maximized;
                //this.owner.dUIContainer.Invalidate();
            }
        }
        #endregion
        #region 系统按钮-还原
        internal protected class SysButtonRestore : SysButton
        {
            private Image restoreDownBackImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("DirectUI.Resources.restoreDownBack.png"));
            private Image restoreMouseBackImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("DirectUI.Resources.restoreMouseBack.png"));
            private Image restoreNormlBackImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("DirectUI.Resources.restoreNormlBack.png"));
            public SysButtonRestore()
            {
                this.MouseDownImage = restoreDownBackImage;
                this.MouseHoverImage = restoreMouseBackImage;
                this.MouseNormalImage = restoreNormlBackImage;
            }
            public override bool Visible
            {
                get
                {
                    if (this.Owner.WindowState == FormWindowState.Maximized)
                    {
                        return base.Visible && this.owner.MaximizeBox;
                    }
                    return false;
                }
            }
            protected override void OnClick(EventArgs e)
            {
                this.owner.DoNormal();
                //this.owner.WindowState = FormWindowState.Normal;
                //this.owner.Location = this.owner.lastSysCommandLocation;
                //this.owner.Size = this.owner.lastSysCommandSize;
                //this.owner.dUIContainer.Invalidate();
            }
        }
        #endregion
        #region 系统按钮-最小化
        internal protected class SysButtonMin : SysButton
        {
            private Image minDownBackImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("DirectUI.Resources.miniDownBack.png"));
            private Image minMouseBackImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("DirectUI.Resources.miniMouseBack.png"));
            private Image minNormlBackImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("DirectUI.Resources.miniNormlBack.png"));
            public SysButtonMin()
            {
                this.MouseDownImage = minDownBackImage;
                this.MouseHoverImage = minMouseBackImage;
                this.MouseNormalImage = minNormlBackImage;
            }
            public override bool Visible
            {
                get
                {
                    return base.Visible && this.owner.MinimizeBox;
                }
            }
            protected override void OnClick(EventArgs e)
            {
                this.owner.DoMinimize();
                //this.owner.WindowState = FormWindowState.Minimized;
                //this.owner.dUIContainer.Invalidate();
            }
        }
        #endregion
        #region 系统按钮-换肤
        internal protected class SysButtonSkin : SysButton
        {
            private Image skinDownBackImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("DirectUI.Resources.skinDownBack.png"));
            private Image skinMouseBackImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("DirectUI.Resources.skinMouseBack.png"));
            private Image skinNormlBackImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("DirectUI.Resources.skinNormlBack.png"));
            public SysButtonSkin()
            {
                this.MouseDownImage = skinDownBackImage;
                this.MouseHoverImage = skinMouseBackImage;
                this.MouseNormalImage = skinNormlBackImage;
            }
        }
        #endregion
    }
}
