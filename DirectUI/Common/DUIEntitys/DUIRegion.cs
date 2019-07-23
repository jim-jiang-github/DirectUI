using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIRegion : DUIDependentOnRenderTarget
    {
        private List<Action<SharpDX.Direct2D1.PathGeometry, SharpDX.Direct2D1.GeometrySink>> actions = new List<Action<SharpDX.Direct2D1.PathGeometry, SharpDX.Direct2D1.GeometrySink>>();
        private SharpDX.Direct2D1.RectangleGeometry rectangleGeometry = null;
        protected SharpDX.Direct2D1.PathGeometry dxGeometry = null;
        protected System.Drawing.Region region = null;
        protected virtual SharpDX.Direct2D1.PathGeometry DxGeometry
        {
            get
            {
                if (dxGeometry == null || isNewRenderTarget)
                {
                    if (this.RenderTarget != null)
                    {
                        this.rectangleGeometry = new SharpDX.Direct2D1.RectangleGeometry(RenderTarget.RenderTarget.Factory, DxConvert.ToRectF(0, 0, 0, 0));
                        this.dxGeometry = new SharpDX.Direct2D1.PathGeometry(this.RenderTarget.RenderTarget.Factory);
                        SharpDX.Direct2D1.GeometrySink geometrySink = this.dxGeometry.Open();
                        this.actions.ForEach(a => a.Invoke(this.dxGeometry, geometrySink));
                        geometrySink.Close();
                        this.isNewRenderTarget = false;
                    }
                }
                return dxGeometry;
            }
        }
        public DUIRegion()
        {
            this.region = new Region();
        }
        /// <summary> 基于指定的 System.Drawing.Rectangle 结构初始化一个新的 DUIRegion。
        /// </summary>
        /// <param name="rect">一个 System.Drawing.Rectangle 结构，用于定义新 DUIRegion 的内部</param>
        public DUIRegion(Rectangle rect)
        {
            this.region = new Region(rect);
            this.actions.Add((dxGeometry, geometrySink) =>
            {
                SharpDX.Direct2D1.RectangleGeometry rectangleGeometry = new SharpDX.Direct2D1.RectangleGeometry(RenderTarget.RenderTarget.Factory, DxConvert.ToRectF(rect));
                this.rectangleGeometry.Combine(rectangleGeometry, SharpDX.Direct2D1.CombineMode.Union, geometrySink);
            });
        }
        /// <summary> 基于指定的 System.Drawing.RectangleF 结构初始化一个新的 DUIRegion。
        /// </summary>
        /// <param name="rect">一个 System.Drawing.RectangleF 结构，用于定义新 DUIRegion 的内部</param>
        public DUIRegion(RectangleF rect)
        {
            this.region = new Region(rect);
            this.actions.Add((dxGeometry, geometrySink) =>
            {
                SharpDX.Direct2D1.RectangleGeometry rectangleGeometry = new SharpDX.Direct2D1.RectangleGeometry(RenderTarget.RenderTarget.Factory, DxConvert.ToRectF(rect));
                this.rectangleGeometry.Combine(rectangleGeometry, SharpDX.Direct2D1.CombineMode.Union, geometrySink);
            });
        }
        /// <summary> 更新此 DUIRegion，以仅包含其内部与指定的 System.Drawing.Rectangle 结构不相交的部分。
        /// </summary>
        /// <param name="rect">要从此 DUIRegion 中排除的 System.Drawing.Rectangle 结构</param>
        public void Exclude(Rectangle rect)
        {
            this.region.Exclude(rect);
            this.actions.Add((dxGeometry, geometrySink) =>
            {
                SharpDX.Direct2D1.RectangleGeometry rectangleGeometry = new SharpDX.Direct2D1.RectangleGeometry(RenderTarget.RenderTarget.Factory, DxConvert.ToRectF(rect));
                this.rectangleGeometry.Combine(rectangleGeometry, SharpDX.Direct2D1.CombineMode.Exclude, geometrySink);
            });
        }
        /// <summary> 更新此 DUIRegion，以仅包含其内部与指定的 System.Drawing.RectangleF 结构不相交的部分。
        /// </summary>
        /// <param name="rect">要从此 DUIRegion 中排除的 System.Drawing.RectangleF 结构。</param>
        public void Exclude(RectangleF rect)
        {
            this.region.Exclude(rect);
            this.actions.Add((dxGeometry, geometrySink) =>
            {
                SharpDX.Direct2D1.RectangleGeometry rectangleGeometry = new SharpDX.Direct2D1.RectangleGeometry(RenderTarget.RenderTarget.Factory, DxConvert.ToRectF(rect));
                this.rectangleGeometry.Combine(rectangleGeometry, SharpDX.Direct2D1.CombineMode.Exclude, geometrySink);
            });
        }
        /// <summary> 将此 DUIRegion 更新为其自身与指定的 System.Drawing.Rectangle 结构的交集。
        /// </summary>
        /// <param name="rect">要与此 DUIRegion 相交的 System.Drawing.Rectangle 结构。</param>
        public void Intersect(Rectangle rect)
        {
            this.region.Intersect(rect);
            this.actions.Add((dxGeometry, geometrySink) =>
            {
                SharpDX.Direct2D1.RectangleGeometry rectangleGeometry = new SharpDX.Direct2D1.RectangleGeometry(RenderTarget.RenderTarget.Factory, DxConvert.ToRectF(rect));
                this.rectangleGeometry.Combine(rectangleGeometry, SharpDX.Direct2D1.CombineMode.Intersect, geometrySink);
            });
        }
        /// <summary> 将此 DUIRegion 更新为其自身与指定的 System.Drawing.RectangleF 结构的交集。
        /// </summary>
        /// <param name="rect">要与此 DUIRegion 相交的 System.Drawing.RectangleF 结构。</param>
        public void Intersect(RectangleF rect)
        {
            this.region.Intersect(rect);
            this.actions.Add((dxGeometry, geometrySink) =>
            {
                SharpDX.Direct2D1.RectangleGeometry rectangleGeometry = new SharpDX.Direct2D1.RectangleGeometry(RenderTarget.RenderTarget.Factory, DxConvert.ToRectF(rect));
                this.rectangleGeometry.Combine(rectangleGeometry, SharpDX.Direct2D1.CombineMode.Intersect, geometrySink);
            });
        }
        /// <summary> 将此 DUIRegion 更新为其自身与指定 System.Drawing.Rectangle 结构的并集。
        /// </summary>
        /// <param name="rect">要与此 DUIRegion 合并的 System.Drawing.Rectangle 结构。</param>
        public void Union(Rectangle rect)
        {
            this.region.Union(rect);
            this.actions.Add((dxGeometry, geometrySink) =>
            {
                SharpDX.Direct2D1.RectangleGeometry rectangleGeometry = new SharpDX.Direct2D1.RectangleGeometry(RenderTarget.RenderTarget.Factory, DxConvert.ToRectF(rect));
                this.rectangleGeometry.Combine(rectangleGeometry, SharpDX.Direct2D1.CombineMode.Union, geometrySink);
            });
        }
        /// <summary> 将此 DUIRegion 更新为其自身与指定 System.Drawing.RectangleF 结构的并集。
        /// </summary>
        /// <param name="rect">要与此 DUIRegion 合并的 System.Drawing.RectangleF 结构。</param>
        public void Union(RectangleF rect)
        {
            this.region.Union(rect);
            this.actions.Add((dxGeometry, geometrySink) =>
            {
                SharpDX.Direct2D1.RectangleGeometry rectangleGeometry = new SharpDX.Direct2D1.RectangleGeometry(RenderTarget.RenderTarget.Factory, DxConvert.ToRectF(rect));
                this.rectangleGeometry.Combine(rectangleGeometry, SharpDX.Direct2D1.CombineMode.Union, geometrySink);
            });
        }
        /// <summary> 将此 DUIRegion 更新为其自身与指定 System.Drawing.Rectangle 结构的并集减去这两者的交集。
        /// </summary>
        /// <param name="rect">要与此 DUIRegion 进行 Overload:DUIRegion.Xor 的 System.Drawing.Rectangle</param>
        public void Xor(Rectangle rect)
        {
            this.region.Xor(rect);
            this.actions.Add((dxGeometry, geometrySink) =>
            {
                SharpDX.Direct2D1.RectangleGeometry rectangleGeometry = new SharpDX.Direct2D1.RectangleGeometry(RenderTarget.RenderTarget.Factory, DxConvert.ToRectF(rect));
                this.rectangleGeometry.Combine(rectangleGeometry, SharpDX.Direct2D1.CombineMode.Xor, geometrySink);
            });
        }
        /// <summary> 将此 DUIRegion 更新为其自身与指定 System.Drawing.RectangleF 结构的并集减去这两者的交集。
        /// </summary>
        /// <param name="rect">要与此 DUIRegion 进行 DUIRegion.Xor(System.Drawing.Drawing2D.GraphicsPath)的 System.Drawing.RectangleF 结构。</param>
        public void Xor(RectangleF rect)
        {
            this.region.Xor(rect);
            this.actions.Add((dxGeometry, geometrySink) =>
            {
                SharpDX.Direct2D1.RectangleGeometry rectangleGeometry = new SharpDX.Direct2D1.RectangleGeometry(RenderTarget.RenderTarget.Factory, DxConvert.ToRectF(rect));
                this.rectangleGeometry.Combine(rectangleGeometry, SharpDX.Direct2D1.CombineMode.Xor, geometrySink);
            });
        }
        public static implicit operator Region(DUIRegion dUIRegion)
        {
            return dUIRegion.region;
        }

        public static implicit operator SharpDX.Direct2D1.Geometry(DUIRegion dUIRegion)
        {
            return dUIRegion.DxGeometry;
        }
        public override void Dispose()
        {
            if (this.dxGeometry != null)
            {
                this.dxGeometry.Dispose();
            }
            if (this.region != null)
            {
                this.region.Dispose();
            }
            if (this.rectangleGeometry != null)
            {
                this.rectangleGeometry.Dispose();
            }
            base.Dispose();
        }
        public override void DisposeDx()
        {
            if (this.dxGeometry != null)
            {
                this.dxGeometry.Dispose();
            }
            if (this.rectangleGeometry != null)
            {
                this.rectangleGeometry.Dispose();
            }
        }
    }
}
