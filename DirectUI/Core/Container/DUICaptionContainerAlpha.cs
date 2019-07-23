using DirectUI.Common;
using DirectUI.Share;
using DirectUI.Win32.Const;
using DirectUI.Win32.Struct;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DirectUI.Core
{
    /// <summary> 标题栏里面DUIControl的容器
    /// </summary>
    internal class DUICaptionContainerAlpha : DUICaptionContainer
    {
        #region 变量
        #endregion
        #region 属性
        public override Bitmap ControlThumbnail
        {
            get
            {
                return this.Owner.ControlThumbnail;
            }
        }
        /// <summary> 所在的DUISkinFormAlpha
        /// </summary>
        private DUISkinFormAlpha Owner { get { return this.owner as DUISkinFormAlpha; } }
        #endregion
        #region 重写
        public override void Invalidate(Region r)
        {
            synchronizationContext.Send((obj) =>
            {
                if (this.CanLayout && this.owner.IsHandleCreated)
                {
                    this.CaptionInvalidate(r);
                    this.BorderInvalidate(r);
                    this.Owner.SetBits();
                    this.Owner.dUIContainer.Invalidate();
                }
            }, null);
        }
        #endregion
        public DUICaptionContainerAlpha(DUISkinFormAlpha owner)
        {
            this.owner = owner;
            this.lastClientSize = this.Size;
            this.dUIControlShare = new DUIControlShare();
            synchronizationContext = SynchronizationContext.Current;
            #region 窗体事件
            this.owner.HandleCreated += (s, e) =>
            {
                this.DoHandleCreated(e);
                LayoutEngine.DoLayout(new DUILayoutEventArgs(this, "HandleCreated"));
            };
            this.owner.Resize += (s, e) => { this.DoResize(e); };
            this.owner.Layout += (s, e) =>
            {
                if (this.Width != 0 || this.Height != 0)
                {
                    if (this.lastClientSize != this.Size)
                    {
                        LayoutEngine.DoLayoutDock(new DUILayoutEventArgs(this, "Layout"), this.lastClientSize);
                        this.lastClientSize = this.Size;
                    }
                }
                this.DoLayout(new DUILayoutEventArgs(this, "Layout"));
            };
            #endregion
            this.Owner.Load += (s, e) =>
            {
                //记录坐标和尺寸
                this.Owner.lastSysCommandLocation = this.owner.Location;
                this.Owner.lastSysCommandSize = this.owner.Size;
            };
            this.Owner.ResizeEnd += (s, e) =>
            {
                if (this.Owner.WindowState == FormWindowState.Normal)
                {
                    //记录坐标和尺寸
                    this.Owner.lastSysCommandLocation = this.owner.Location;
                    this.Owner.lastSysCommandSize = this.owner.Size;
                }
            };
        }
        #region 函数
        private Color shadowColor = Color.Black;
        private int shadowWidth = 4;
        private int radius = 6;
        #region 绘画阴影方法1
        /// <summary>
        /// 四边阴影的颜色。[0]为阴影内沿颜色，[1]为阴影外沿颜色
        /// </summary>
        public Color[] ShadowColors = { Color.FromArgb(60, Color.Black), Color.Transparent };
        /// <summary>
        /// 圆角阴影的颜色。[0]为阴影内沿颜色，[1]为阴影外沿颜色。
        /// 注：一般来讲，圆角阴影内沿的颜色应当比四边阴影内沿的颜色更深，才会有更好的显示效果。此值应当根据您的实际情况而定。
        /// </summary>
        /// <remarks>由于给扇面上渐变时，起点并不是准确的扇面内弧，因此扇面的内沿颜色可能应比四边的内沿颜色深</remarks>
        public Color[] CornerColors = { Color.FromArgb(155, Color.Black), Color.Transparent };

        /// <summary>
        /// 绘制四角、四边的阴影
        /// </summary>
        /// <param name="g"></param>
        private void DrawShadow(Graphics g)
        {
            /* 阴影分为9宫格，5为内部背景图部分
             *  1   2   3
             *  4   5   6
             *  7   8   9
             */

            //赋新值
            ShadowColors[0] = Color.FromArgb(60, shadowColor);
            int S = shadowWidth;
            int A = S <= 4 ? 180 : S <= 15 ? 155 : S <= 50 ? 140 : S <= 100 ? 135 : 180;
            CornerColors[0] = Color.FromArgb(A, shadowColor);
            // 四角正方形边长 = 圆角半径 + 阴影宽度
            Size corSize = new Size(shadowWidth + radius, shadowWidth + radius);

            // 左侧、右侧渐变的尺寸
            Size gradientSize_LR = new Size(shadowWidth, this.Size.Height - corSize.Height * 2);

            // 顶部、底部渐变的尺寸
            Size gradientSize_TB = new Size(this.Size.Width - corSize.Width * 2, shadowWidth);

            // 绘制四边
            DrawLines(g, corSize, gradientSize_LR, gradientSize_TB);

            // 绘制四角
            DrawCorners(g, corSize);
        }

        /// <summary>
        /// 绘制四角的阴影
        /// </summary>
        /// <param name="g"></param>
        /// <param name="corSize">圆角区域正方形的大小</param>
        /// <returns></returns>
        private void DrawCorners(Graphics g, Size corSize)
        {
            /*
             * 四个角，每个角都是一个扇面
             * 画图时扇面由外弧、内弧以及两段的连接线构成图形
             * 然后在内弧中间附近向外做渐变
             *
             * 阴影分为9宫格，5为内部背景图部分
             *  1   2   3
             *  4   5   6
             *  7   8   9
             */
            Action<int> DrawCorenerN = (n) =>
            {
                using (GraphicsPath gp = new GraphicsPath())
                {
                    // 扇面外沿、内沿曲线的尺寸
                    Size sizeOutSide = new Size(corSize.Width * 2, corSize.Height * 2);
                    Size sizeInSide = new Size(radius * 2, radius * 2);

                    // 扇面外沿、内沿曲线的位置
                    Point locationOutSide, locationInSide;
                    // 当圆角半径小于MinCornerRadius时，内沿不绘制曲线，而以线段绘制近似值。该线段绘制方向是从p1指向p2。
                    Point p1, p2;

                    // 渐变起点位置
                    PointF brushCenter;

                    // 扇面起始角度
                    float startAngle;

                    // 根据四个方位不同，确定扇面的位置、角度及渐变起点位置
                    switch (n)
                    {
                        case 1:
                            locationOutSide = new Point(0, 0);
                            startAngle = 180;
                            brushCenter = new PointF((float)sizeOutSide.Width - sizeInSide.Width * 0.5f, (float)sizeOutSide.Height - sizeInSide.Height * 0.5f);
                            p1 = new Point(corSize.Width, shadowWidth);
                            p2 = new Point(shadowWidth, corSize.Height);
                            break;

                        case 3:
                            locationOutSide = new Point(this.Width - sizeOutSide.Width, 0);
                            startAngle = 270;
                            brushCenter = new PointF((float)locationOutSide.X + sizeInSide.Width * 0.5f, (float)sizeOutSide.Height - sizeInSide.Height * 0.5f);
                            p1 = new Point(this.Width - shadowWidth, corSize.Height);
                            p2 = new Point(this.Width - corSize.Width, shadowWidth);
                            break;

                        case 7:
                            locationOutSide = new Point(0, this.Height - sizeOutSide.Height);
                            startAngle = 90;
                            brushCenter = new PointF((float)sizeOutSide.Width - sizeInSide.Width * 0.5f, (float)locationOutSide.Y + sizeInSide.Height * 0.5f);
                            p1 = new Point(shadowWidth, this.Height - corSize.Height);
                            p2 = new Point(corSize.Width, this.Height - shadowWidth);
                            break;

                        default:
                            locationOutSide = new Point(this.Width - sizeOutSide.Width, this.Height - sizeOutSide.Height);
                            startAngle = 0;
                            brushCenter = new PointF((float)locationOutSide.X + sizeInSide.Width * 0.5f, (float)locationOutSide.Y + sizeInSide.Height * 0.5f);
                            p1 = new Point(this.Width - corSize.Width, this.Height - shadowWidth);
                            p2 = new Point(this.Width - shadowWidth, this.Height - corSize.Height);
                            break;
                    }

                    // 扇面外沿曲线
                    Rectangle recOutSide = new Rectangle(locationOutSide, sizeOutSide);

                    // 扇面内沿曲线的位置
                    locationInSide = new Point(locationOutSide.X + (sizeOutSide.Width - sizeInSide.Width) / 2, locationOutSide.Y + (sizeOutSide.Height - sizeInSide.Height) / 2);

                    // 扇面内沿曲线
                    Rectangle recInSide = new Rectangle(locationInSide, sizeInSide);

                    // 将扇面添加到形状，以备绘制
                    gp.AddArc(recOutSide, startAngle, 90);

                    if (radius > 3)
                    {
                        gp.AddArc(recInSide, startAngle + 90, -90);
                    }
                    else
                    {
                        gp.AddLine(p1, p2);
                    }

                    // 使用渐变笔刷
                    using (PathGradientBrush shadowBrush = new PathGradientBrush(gp))
                    {
                        Color[] colors = new Color[2];
                        float[] positions = new float[2];
                        ColorBlend sBlend = new ColorBlend();
                        // 扇面外沿色
                        colors[0] = CornerColors[1];
                        // 扇面内沿色
                        colors[1] = CornerColors[0];
                        positions[0] = 0.0f;
                        positions[1] = 1.0f;
                        sBlend.Colors = colors;
                        sBlend.Positions = positions;

                        shadowBrush.InterpolationColors = sBlend;
                        shadowBrush.CenterPoint = brushCenter;
                        // 上色中心点
                        g.FillPath(shadowBrush, gp);
                    }
                }
            };

            DrawCorenerN(1);
            DrawCorenerN(3);
            DrawCorenerN(7);
            DrawCorenerN(9);
        }

        /// <summary>
        /// 绘制上下左右四边的阴影
        /// </summary>
        /// <param name="g"></param>
        /// <param name="corSize"></param>
        /// <param name="gradientSize_LR"></param>
        /// <param name="gradientSize_TB"></param>
        private void DrawLines(Graphics g, Size corSize, Size gradientSize_LR, Size gradientSize_TB)
        {
            Rectangle rect2 = new Rectangle(new Point(corSize.Width, 0), gradientSize_TB);
            Rectangle rect4 = new Rectangle(new Point(0, corSize.Width), gradientSize_LR);
            Rectangle rect6 = new Rectangle(new Point(this.Size.Width - shadowWidth, corSize.Width), gradientSize_LR);
            Rectangle rect8 = new Rectangle(new Point(corSize.Width, this.Size.Height - shadowWidth), gradientSize_TB);

            using (
                LinearGradientBrush brush2 = new LinearGradientBrush(rect2, ShadowColors[1], ShadowColors[0], LinearGradientMode.Vertical),
                 brush4 = new LinearGradientBrush(rect4, this.ShadowColors[1], this.ShadowColors[0], LinearGradientMode.Horizontal),
                 brush6 = new LinearGradientBrush(rect6, this.ShadowColors[0], this.ShadowColors[1], LinearGradientMode.Horizontal),
                 brush8 = new LinearGradientBrush(rect8, this.ShadowColors[0], this.ShadowColors[1], LinearGradientMode.Vertical)
            )
            {
                g.FillRectangle(brush2, rect2);
                g.FillRectangle(brush4, rect4);
                g.FillRectangle(brush6, rect6);
                g.FillRectangle(brush8, rect8);
            }
        }
        #endregion

        //#region 绘画阴影方法2
        //private Bitmap DrawBlurBorder()
        //{
        //    return (Bitmap)DrawOutsetShadow(shadowColor, new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height));
        //}
        //private Image DrawOutsetShadow(Color color, Rectangle shadowCanvasArea)
        //{
        //    Rectangle rOuter = shadowCanvasArea;
        //    Rectangle rInner = new Rectangle(shadowWidth - 1, shadowWidth - 1, Main.Width + 1, Main.Height + 1);

        //    Bitmap img = new Bitmap(rOuter.Width, rOuter.Height, PixelFormat.Format32bppArgb);
        //    Graphics g = Graphics.FromImage(img);
        //    using (Pen bgPen = new Pen(Color.FromArgb(30, color)))
        //    {
        //        bgPen.Width = shadowWidth * 2 - 2;
        //        g.DrawRectangle(bgPen, rOuter);
        //    }
        //    using (Pen bgPen = new Pen(Color.FromArgb(60, color)))
        //    {
        //        g.DrawRectangle(bgPen, rInner);
        //    }
        //    g.Flush();
        //    g.Dispose();
        //    return img;
        //}
        //#endregion
        #endregion
    }
}
