using SharpDX.Direct2D1;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DirectUI.Common
{
    public class DUIWICRenderTarget : DUIRenderTarget
    {
        internal static object lockObj = new object();
        private static readonly SharpDX.Direct2D1.Factory d2dFactory = new SharpDX.Direct2D1.Factory();
        private SharpDX.Direct2D1.WicRenderTarget wicRenderTarget = null;
        public override SharpDX.Direct2D1.RenderTarget RenderTarget => this.wicRenderTarget;
        public DUIWICRenderTarget(DUIBitmap image)
        {
            System.Threading.Monitor.Enter(lockObj);
            RenderTargetProperties renderTargetProperties = new RenderTargetProperties
            {
                Type = RenderTargetType.Default,
                DpiX = 96.0f,
                DpiY = 96.0f,
                PixelFormat = new PixelFormat(Format.Unknown, SharpDX.Direct2D1.AlphaMode.Unknown),
                Usage = RenderTargetUsage.None,
                MinLevel = FeatureLevel.Level_DEFAULT
            };
            this.wicRenderTarget = new SharpDX.Direct2D1.WicRenderTarget(d2dFactory, image, renderTargetProperties);
        }
        public override void Dispose()
        {
            base.Dispose();
            this.wicRenderTarget?.Dispose();
            this.wicRenderTarget = null;
            System.Threading.Monitor.Exit(lockObj);
        }
    }
}
