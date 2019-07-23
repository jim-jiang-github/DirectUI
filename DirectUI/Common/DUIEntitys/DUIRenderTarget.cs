using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public abstract class DUIRenderTarget : IDisposable
    {
        private DUIDependentOnRenderTargetCollection dependentOnRenderTargets = null;
        internal DUIDependentOnRenderTargetCollection DependentOnRenderTargets => this.dependentOnRenderTargets ?? (this.dependentOnRenderTargets = new DUIDependentOnRenderTargetCollection(this));
        public abstract SharpDX.Direct2D1.RenderTarget RenderTarget { get; }
        public IntPtr NativePointer => this.RenderTarget.NativePointer;
        public virtual float DpiX { get { return 96; } }
        public virtual float DpiY { get { return 96; } }
        public virtual void BeginDraw()
        {
            this.RenderTarget.BeginDraw();
        }
        public virtual void Resize(Size size)
        {
        }
        public virtual void EndDraw()
        {
            this.RenderTarget.EndDraw();
        }
        public virtual void Dispose()
        {
            this.DependentOnRenderTargets.Clear();
        }
        public static implicit operator SharpDX.Direct2D1.RenderTarget(DUIRenderTarget dUIRenderTarget)
        {
            return dUIRenderTarget.RenderTarget;
        }
        public class DUIDependentOnRenderTargetCollection : Collection.DUIItemCollection<DUIRenderTarget, DUIDependentOnRenderTarget>
        {
            private object lockObj = new object();
            public DUIDependentOnRenderTargetCollection(DUIRenderTarget owner) : base(owner)
            {
            }
            public override void Add(DUIDependentOnRenderTarget item)
            {
                lock (lockObj)
                {
                    base.Add(item);
                }
            }
            public override void Remove(DUIDependentOnRenderTarget item)
            {
                lock (lockObj)
                {
                    base.Remove(item);
                }
            }
            public override void Clear()
            {
                lock (lockObj)
                {
                    foreach (DUIDependentOnRenderTarget t in this)
                    {
                        t.DisposeDx();
                    }
                    base.Clear();
                }
            }
        }
    }
}
