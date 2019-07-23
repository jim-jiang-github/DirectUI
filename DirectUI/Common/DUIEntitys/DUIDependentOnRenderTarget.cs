using DirectUI.Core;
using SharpDX.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    /// <summary> 依赖RenderTarget的对象,例如DUIImage,DUIBitmap等
    /// </summary>
    public abstract class DUIDependentOnRenderTarget : IDisposable
    {
        protected bool isNewRenderTarget = false;
        private DUIRenderTarget renderTarget = null;
        internal protected DUIRenderTarget RenderTarget
        {
            get { return renderTarget; }
            set
            {
                if (renderTarget != value)
                {
                    renderTarget?.DependentOnRenderTargets.Remove(this);
                    renderTarget = value;
                    value.DependentOnRenderTargets.Add(this);
                    isNewRenderTarget = true;
                }
            }
        }
        public virtual void Dispose()
        {
            renderTarget?.DependentOnRenderTargets.Remove(this);
        }
        public abstract void DisposeDx();
    }
}
