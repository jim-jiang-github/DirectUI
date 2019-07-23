using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public class DUIPen : DUIDependentOnRenderTarget
    {
        private SharpDX.Direct2D1.SolidColorBrush dxBrush = null;
        private SharpDX.Direct2D1.StrokeStyle dxStrokeStyle = null;
        private System.Drawing.Pen pen = null;
        private SharpDX.Direct2D1.StrokeStyleProperties strokeStyleProperties = new SharpDX.Direct2D1.StrokeStyleProperties();
        private SharpDX.Direct2D1.SolidColorBrush DxBrush
        {
            get
            {
                if (dxBrush == null || isNewRenderTarget)
                {
                    if (this.RenderTarget != null)
                    {
                        if (dxBrush != null)
                        {
                            dxBrush.Dispose();
                            dxBrush = null;
                        }
                        dxBrush = (SharpDX.Direct2D1.SolidColorBrush)DxConvert.ToBrush(this.RenderTarget, this.pen);
                        isNewRenderTarget = false;
                    }
                }
                return dxBrush;
            }
        }
        private SharpDX.Direct2D1.StrokeStyle DxStrokeStyle
        {
            get
            {
                if (dxStrokeStyle == null || isNewRenderTarget
                    //|| this.strokeStyleProperties.DashCap != (SharpDX.Direct2D1.CapStyle)this.DashCap
                    //|| this.strokeStyleProperties.DashOffset != this.DashOffset
                    //|| this.strokeStyleProperties.DashStyle != (SharpDX.Direct2D1.DashStyle)this.DashStyle
                    //|| this.strokeStyleProperties.EndCap != (SharpDX.Direct2D1.CapStyle)this.EndCap
                    //|| this.strokeStyleProperties.LineJoin != (SharpDX.Direct2D1.LineJoin)this.LineJoin
                    //|| this.strokeStyleProperties.MiterLimit != this.MiterLimit
                    //|| this.strokeStyleProperties.StartCap != (SharpDX.Direct2D1.CapStyle)this.StartCap
                    )
                {
                    if (this.RenderTarget != null)
                    {
                        this.strokeStyleProperties.DashCap = DxConvert.ToCapStyle(this.DashCap);
                        this.strokeStyleProperties.DashOffset = this.DashOffset;
                        this.strokeStyleProperties.DashStyle = (SharpDX.Direct2D1.DashStyle)this.DashStyle;
                        this.strokeStyleProperties.EndCap = DxConvert.ToCapStyle(this.EndCap);
                        this.strokeStyleProperties.LineJoin = (SharpDX.Direct2D1.LineJoin)this.LineJoin;
                        this.strokeStyleProperties.MiterLimit = this.MiterLimit;
                        this.strokeStyleProperties.StartCap = DxConvert.ToCapStyle(this.StartCap);
                        if (dxStrokeStyle != null)
                        {
                            dxStrokeStyle.Dispose();
                            dxStrokeStyle = null;
                        }
                        dxStrokeStyle = DxConvert.ToStrokeStyle(this.RenderTarget, this.strokeStyleProperties);
                        isNewRenderTarget = false;
                    }
                }
                return dxStrokeStyle;
            }
        }
        /// <summary> 是否是默认的StyleProperties
        /// </summary>
        public bool IsDefaultStyleProperties
        {
            get
            {
                return (SharpDX.Direct2D1.CapStyle)this.DashCap == SharpDX.Direct2D1.CapStyle.Flat &&
                    this.DashOffset == 0 &&
                    (SharpDX.Direct2D1.DashStyle)this.DashStyle == SharpDX.Direct2D1.DashStyle.Solid &&
                    (SharpDX.Direct2D1.CapStyle)this.EndCap == SharpDX.Direct2D1.CapStyle.Flat &&
                    (SharpDX.Direct2D1.LineJoin)this.LineJoin == SharpDX.Direct2D1.LineJoin.Miter &&
                    this.MiterLimit == 10 &&
                    (SharpDX.Direct2D1.CapStyle)this.StartCap == SharpDX.Direct2D1.CapStyle.Flat;
            }
        }
        public Color Color
        {
            get { return this.pen.Color; }
            set
            {
                this.pen.Color = value;
                if (this.dxBrush != null)
                {
                    var c = DxConvert.ToColor4(value);
                    this.dxBrush.Color = c;
                    //if (this.dxBrush != null && this.dxBrush.Color != c)
                    //{
                    //    this.dxBrush.Color = c;
                    //}
                }
            }
        }
        /// <summary> 获取或设置用在短划线终点的线帽样式，这些短划线构成通过此 DUIPen 绘制的虚线。
        /// </summary>
        public DashCap DashCap
        {
            get { return this.pen.DashCap; }
            set
            {
                this.pen.DashCap = value;
            }
        }
        /// <summary> 获取或设置直线的起点到短划线图案起始处的距离。
        /// </summary>
        public float DashOffset
        {
            get { return this.pen.DashOffset; }
            set { this.pen.DashOffset = value; }
        }
        /// <summary> 获取或设置用于通过此 DUIPen 绘制的虚线的样式。
        /// </summary>
        public DashStyle DashStyle
        {
            get { return this.pen.DashStyle; }
            set { this.pen.DashStyle = value; }
        }
        /// <summary> 获取或设置要在通过此 DUIPen 绘制的直线终点使用的线帽样式。
        /// </summary>
        public LineCap EndCap
        {
            get { return this.pen.EndCap; }
            set { this.pen.EndCap = value; }
        }
        /// <summary> 获取或设置通过此 DUIPen 绘制的两条连续直线的端点的联接样式。
        /// </summary>
        public LineJoin LineJoin
        {
            get { return this.pen.LineJoin; }
            set { this.pen.LineJoin = value; }
        }
        /// <summary> 获取或设置斜接角上联接宽度的限制。
        /// </summary>
        public float MiterLimit
        {
            get { return this.pen.MiterLimit; }
            set { this.pen.MiterLimit = value; }
        }
        /// <summary> 获取或设置在通过此 DUIPen 绘制的直线起点使用的线帽样式。
        /// </summary>
        public LineCap StartCap
        {
            get { return this.pen.StartCap; }
            set { this.pen.StartCap = value; }
        }
        /// <summary> 获取或设置此 DUIPen 的宽度，以用于绘图的 DUIGraphics 对象为单位。
        /// </summary>
        public float Width
        {
            get { return this.pen.Width; }
            set
            {
                this.pen.Width = value;
            }
        }
        /// <summary> 用指定的 System.Drawing.Brush 初始化 System.Drawing.Pen 类的新实例
        /// </summary>
        /// <param name="brush">一个 System.Drawing.Brush，确定该 System.Drawing.Pen 的填充属性。</param>
        public DUIPen(Brush brush)
        {
            this.pen = new Pen(brush);
        }
        /// <summary> 用指定颜色初始化 System.Drawing.Pen 类的新实例。
        /// </summary>
        /// <param name="color">System.Drawing.Color 结构，指示此 System.Drawing.Pen 的颜色。</param>
        public DUIPen(Color color)
        {
            this.pen = new Pen(color);
        }
        /// <summary> 使用指定的 System.Drawing.Brush 和 System.Drawing.Pen.Width 初始化 System.Drawing.Pen类的新实例。
        /// </summary>
        /// <param name="brush">一个 System.Drawing.Brush，决定此 System.Drawing.Pen 的特征。</param>
        /// <param name="width">新 System.Drawing.Pen 的宽度。</param>
        public DUIPen(Brush brush, float width)
        {
            this.pen = new Pen(brush, width);
        }
        /// <summary> 用指定的 System.Drawing.Color 和 System.Drawing.Pen.Width 属性初始化 System.Drawing.Pen类的新实例。
        /// </summary>
        /// <param name="color">System.Drawing.Color 结构，指示此 System.Drawing.Pen 的颜色。</param>
        /// <param name="width">指示此 System.Drawing.Pen 的宽度的值。</param>
        public DUIPen(Color color, float width)
        {
            this.pen = new Pen(color, width);
        }

        public static implicit operator Pen(DUIPen dUIPen)
        {
            return dUIPen.pen;
        }

        public static implicit operator SharpDX.Direct2D1.Brush(DUIPen dUIPen)
        {
            return dUIPen.DxBrush;
        }

        public static implicit operator SharpDX.Direct2D1.StrokeStyle(DUIPen dUIPen)
        {
            return dUIPen.DxStrokeStyle;
        }
        public override void Dispose()
        {
            if (this.dxBrush != null)
            {
                this.dxBrush.Dispose();
            }
            if (this.pen != null)
            {
                this.pen.Dispose();
            }
            base.Dispose();
        }
        public override void DisposeDx()
        {
            if (this.dxBrush != null)
            {
                this.dxBrush.Dispose();
            }
        }
    }
}
