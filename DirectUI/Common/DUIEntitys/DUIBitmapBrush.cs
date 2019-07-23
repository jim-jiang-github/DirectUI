using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIBitmapBrush : DUIBrush
    {
        private DUIImage image = null;
        private DUIImage lastImage = null;
        private DUIExtendMode lastDUIExtendMode = DUIExtendMode.Wrap;
        private DUIExtendMode dUIExtendMode = DUIExtendMode.Wrap;
        private float lastOpacity = 1F;
        private float opacity = 1F;
        private DUIMatrix matrix = new DUIMatrix();
        public DUIBitmapBrush(DUIImage image, DUIExtendMode dUIExtendMode = DUIExtendMode.Wrap, float opacity = 1F)
        {
            this.image = image;
            this.dUIExtendMode = dUIExtendMode;
            this.opacity = opacity;
        }
        public DUIBitmapBrush(Image image, DUIExtendMode dUIExtendMode = DUIExtendMode.Wrap, float opacity = 1F)
            : this(DUIImage.FromImage(image), dUIExtendMode, opacity)
        {
        }
        protected override SharpDX.Direct2D1.Brush DxBrush
        {
            get
            {
                if (dxBrush == null || isNewRenderTarget
                    || this.lastImage != this.image
                    || this.lastDUIExtendMode != this.dUIExtendMode
                    || this.lastOpacity != this.opacity)
                {
                    if (this.RenderTarget != null)
                    {
                        this.lastImage = this.image;
                        this.lastDUIExtendMode = this.dUIExtendMode;
                        this.lastOpacity = this.opacity;
                        if (dxBrush != null)
                        {
                            dxBrush.Dispose();
                            dxBrush = null;
                        }
                        dxBrush = DxConvert.ToBitmapBrush(this.RenderTarget, this);
                        isNewRenderTarget = false;
                    }
                }
                return dxBrush;
            }
        }
        public DUIMatrix Transform
        {
            get
            {
                return this.matrix.Clone();
            }
            set
            {
                this.matrix = value;
                this.DxBrush.Transform = this.matrix;
            }
        }
        public DUIImage Image
        {
            get { return this.image; }
            set { this.image = value; }
        }
        public DUIExtendMode ExtendMode
        {
            get { return dUIExtendMode; }
            set { dUIExtendMode = value; }
        }
        public float Opacity
        {
            get { return opacity; }
            set { opacity = value; }
        }
    }
}
