using DirectUI.Common;
using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirectUISample
{
    public partial class FmCompositeSample_Cubes : Form
    {
        private DUIScrollableContainer dUIScrollableContainer = new DUIScrollableContainer() { Dock = DockStyle.Fill, BackColor = Color.YellowGreen };
        public FmCompositeSample_Cubes()
        {
            InitializeComponent();
            this.duiNativeControl1.DUIControls.Add(dUIScrollableContainer);
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                this.dUIScrollableContainer.AddDisplayCube(new DisplayCube()
                {
                    BackColor = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255))
                });
            }
        }
    }
    public class DUIScrollableContainer : DUIScrollableControl
    {
        private DisplayCubeEditor displayCubeEditor;
        internal List<DisplayCube> displayCubes = new List<DisplayCube>();

        internal DisplayCube Selected { get; set; }
        public DisplayCube[] DisplayCubes => displayCubes.ToArray();
        public override float MinDisplayWidth => DisplayCubes.Length == 0 ? 0 : DisplayCubes.Max(d => d.Right);
        public override float MinDisplayHeight => DisplayCubes.Length == 0 ? 0 : DisplayCubes.Max(d => d.Bottom);

        public DUIScrollableContainer()
        {
            displayCubeEditor = new DisplayCubeEditor(this) { Visible = false };
            this.DUIControls.Add(displayCubeEditor);
        }

        public void AddDisplayCube(DisplayCube displayCube)
        {
            displayCube.Owner = this;
            displayCube.ZIndex = displayCubes.Count;
            displayCubes.Add(displayCube);
        }
        public override void OnPaint(DUIPaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.TranslateTransform(this.DisplayRectangle.X, this.DisplayRectangle.Y);
            foreach (var displayCube in displayCubes
                .Where(d => !d.Selected)
                .Concat(new DisplayCube[] { this.Selected })
                .Where(d => d != null))
            {
                using (DUISolidBrush brush = new DUISolidBrush(displayCube.BackColor))
                {
                    e.Graphics.FillRectangle(brush, displayCube.Bounds);
                    e.Graphics.DrawRectangle(DUIPens.Black, displayCube.Bounds);
                }
            }
            e.Graphics.TranslateTransform(-this.DisplayRectangle.X, -this.DisplayRectangle.Y);
        }
        public override void OnMouseMove(DUIMouseEventArgs e)
        {
            base.OnMouseMove(e);
            displayCubeEditor.BindingDisplayCube(displayCubes.FirstOrDefault(d => d.Bounds.Contains(e.Location)));
        }
    }
    public class DisplayCubeEditor : DUIEditableControl
    {
        private DUIScrollableContainer owner;
        private DisplayCube bingingDisplayCube = null;
        public override float BorderWidth => 2;
        public override EffectMargin CanEffectMargin => EffectMargin.Other;
        public DisplayCubeEditor(DUIScrollableContainer owner)
        {
            this.owner = owner;
        }
        public void BindingDisplayCube(DisplayCube displayCube)
        {
            this.bingingDisplayCube = displayCube;
            if (displayCube == null)
            {
                this.owner.Selected = null;
                this.Visible = false;
            }
            else
            {
                displayCube.SetSelect();
                this.ClientBounds = displayCube.Bounds;
                this.Visible = true;
            }
            this.Invalidate();
        }
        public override void OnLocationChanging(DUILocationChangingEventArgs e)
        {
            base.OnLocationChanging(e);
            if (MouseButtons == MouseButtons.Left)
            {
                this.bingingDisplayCube?.SetLocation(this.ClientBounds.Location);
            }
        }
        public override void OnMouseUp(DUIMouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.bingingDisplayCube?.SetLocation(this.ClientBounds.Location);
            this.BindingDisplayCube(null);
        }
    }
    public class DisplayCube
    {
        private static Size cubeSize = new Size(100, 50);
        private static int space = 10;
        private float x;
        private float y;
        public int ZIndex { get; set; }
        public float X
        {
            get
            {
                if (Selected)
                {
                    return x;
                }
                return ZIndex % (int)((this.Owner.ClientSize.Width - space * 2) / (cubeSize.Width + space)) * (cubeSize.Width + space) + space;
            }
            set => x = value;
        }
        public float Y
        {
            get
            {
                if (Selected)
                {
                    return y;
                }
                return ZIndex / (int)((this.Owner.ClientSize.Width - space * 2) / (cubeSize.Width + space)) * (cubeSize.Height + space) + space;
            }
            set => y = value;
        }
        public float Width => cubeSize.Width;
        public float Height => cubeSize.Height;
        public PointF Location => new PointF(X, Y);
        public SizeF Size => new SizeF(Width, Height);
        public RectangleF Bounds => new RectangleF(Location, Size);
        public float Left => X;
        public float Right => X + Width;
        public float Top => Y;
        public float Bottom => Y + Height;
        public Color BackColor { get; set; }
        internal DUIScrollableContainer Owner { get; set; }
        public bool Selected => Owner.Selected == this;
        internal void SetSelect()
        {
            this.x = this.X;
            this.y = this.Y;
            Owner.Selected = this;
        }
        internal void SetLocation(PointF location)
        {
            this.X = location.X;
            this.Y = location.Y;
            var displayCube = Owner.displayCubes.Where(d => d != this).FirstOrDefault(d =>
            {
                RectangleF bounds = this.Bounds;
                bounds.Intersect(d.Bounds);
                return bounds.Width > cubeSize.Width / 2 && bounds.Height > cubeSize.Height / 2;
            });
            if (displayCube != null)
            {
                int zIndex = this.ZIndex;
                this.ZIndex = displayCube.ZIndex;
                displayCube.ZIndex = zIndex;
            }
        }
    }
}
