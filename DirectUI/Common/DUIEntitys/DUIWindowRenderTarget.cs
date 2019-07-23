using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIWindowRenderTarget : DUIRenderTarget
    {
        private SharpDX.Direct2D1.Factory direct2D1factory = null;
        /// <summary> 渲染窗口
        /// </summary>
        internal SharpDX.Direct2D1.WindowRenderTarget windowRenderTarget;
        public IntPtr Handle => this.windowRenderTarget.Hwnd;
        public override SharpDX.Direct2D1.RenderTarget RenderTarget => this.windowRenderTarget;
        public DUIWindowRenderTarget(IntPtr handle)
        {
            Size size = System.Windows.Forms.Control.FromHandle(handle).Size;
            this.direct2D1factory = new SharpDX.Direct2D1.Factory(SharpDX.Direct2D1.FactoryType.SingleThreaded);
            SharpDX.Direct2D1.HwndRenderTargetProperties hwndRenderTargetProperties = new SharpDX.Direct2D1.HwndRenderTargetProperties()
            {
                Hwnd = handle,
                PixelSize = new SharpDX.Size2(size.Width, size.Height),
                PresentOptions = SharpDX.Direct2D1.PresentOptions.None
            };
            SharpDX.Direct2D1.PixelFormat pixelFormat = new SharpDX.Direct2D1.PixelFormat(SharpDX.DXGI.Format.Unknown, SharpDX.Direct2D1.AlphaMode.Premultiplied);
            //强制设置DPI为96，96（默认值），无意间发现有的人居然会去修改系统的dpi
            SharpDX.Direct2D1.RenderTargetProperties renderTargetProperties = new SharpDX.Direct2D1.RenderTargetProperties(SharpDX.Direct2D1.RenderTargetType.Default, pixelFormat, 96, 96, SharpDX.Direct2D1.RenderTargetUsage.None, SharpDX.Direct2D1.FeatureLevel.Level_DEFAULT);
            //初始化，在_d2dFactory创建渲染缓冲区并与Target绑定
            //SharpDX.Direct2D1.PixelFormat pf = new SharpDX.Direct2D1.PixelFormat(SharpDX.DXGI.Format.Unknown, SharpDX.Direct2D1.AlphaMode.Premultiplied);
            this.windowRenderTarget = new SharpDX.Direct2D1.WindowRenderTarget(direct2D1factory, renderTargetProperties, hwndRenderTargetProperties);
            this.windowRenderTarget.AntialiasMode = SharpDX.Direct2D1.AntialiasMode.Aliased;
            this.windowRenderTarget.TextAntialiasMode = SharpDX.Direct2D1.TextAntialiasMode.Aliased;
        }
        public override void Resize(Size size)
        {
            this.windowRenderTarget.Resize(new SharpDX.Size2(size.Width, size.Height));
        }
        public override void Dispose()
        {
            base.Dispose();
            this.direct2D1factory?.Dispose();
            this.direct2D1factory = null;
            this.windowRenderTarget?.Dispose();
            this.windowRenderTarget = null;
            GC.Collect();
        }
    }
}
