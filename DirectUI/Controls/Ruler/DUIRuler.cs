using DirectUI.Common;
using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Controls
{
    public class DUIRuler : DUIControl
    {
        private static readonly object lockObj = new object();
        private DUISolidBrush brush = null;
        private float dividingStartSpace = 3; //刻度距离开始坐标的间距
        private float wordStartSpace = 3; //文字距离开始坐标的间距
        private float dividingInterval = 5;//刻度间隔
        private float dividingHeight = 3;//刻度高度
        private DUIThreadSaveBitmapBrush dividingBrush = null;
        private float lastWidth = 0;
        private float lastDividingInterval = 5;
        private Color lastForeColor = Color.Black;
        public DUIThreadSaveBitmapBrush DividingBrush
        {
            get
            {
                dividingBrush.Size = new Size((int)(10 * this.DividingInterval), (int)this.ClientSize.Height);
                if (lastWidth != this.Width || lastDividingInterval != this.DividingInterval || lastForeColor != this.ForeColor)
                {
                    lastWidth = this.Width;
                    lastDividingInterval = this.DividingInterval;
                    lastForeColor = this.ForeColor;
                    dividingBrush.CallRefresh();
                }
                return dividingBrush;
            }
        }
        /// <summary> 刻度距离开始坐标的间距
        /// </summary>
        public virtual float DividingStartSpace
        {
            get { return dividingStartSpace; }
            set
            {
                dividingStartSpace = value;
                this.Invalidate();
            }
        }
        /// <summary> 文字距离开始坐标的间距
        /// </summary>
        public virtual float WordStartSpace
        {
            get { return wordStartSpace; }
            set
            {
                wordStartSpace = value;
                this.Invalidate();
            }
        }
        /// <summary> 刻度间隔
        /// </summary>
        public virtual float DividingInterval
        {
            get { return dividingInterval; }
            set
            {
                dividingInterval = value;
                this.Invalidate();
            }
        }
        /// <summary> 刻度高度
        /// </summary>
        public virtual float DividingHeight
        {
            get { return dividingHeight; }
            set
            {
                dividingHeight = value;
                this.Invalidate();
            }
        }
        public DUIRuler()
        {
            this.Height = 20;
            this.brush = new DUISolidBrush(this.ForeColor);
            this.dividingBrush = new DUIThreadSaveBitmapBrush(new Size(1, 1), (g, s) =>
            {
                using (DUIPen pen = new DUIPen(this.ForeColor))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (i == 0)
                        {
                            g.DrawLine(pen, new PointF(i * this.DividingInterval, this.ClientSize.Height), new PointF(i * this.DividingInterval, this.ClientSize.Height - 3 * DividingHeight));
                        }
                        else if (i == 5)
                        {
                            g.DrawLine(pen, new PointF(i * this.DividingInterval, this.ClientSize.Height), new PointF(i * this.DividingInterval, this.ClientSize.Height - 2 * DividingHeight));
                        }
                        else
                        {
                            g.DrawLine(pen, new PointF(i * this.DividingInterval, this.ClientSize.Height), new PointF(i * this.DividingInterval, this.ClientSize.Height - DividingHeight));
                        }
                    }
                }
            });
        }
        public virtual string DividingFormat(float dividing)
        {
            return ((int)dividing).ToString();
        }
        public virtual float DividingWidth(float dividing)
        {
            return ((int)dividing).ToString().Length * 5;
        }
        public override void OnPaintBackground(DUIPaintEventArgs e)
        {
            base.OnPaintBackground(e);
            brush.Color = this.ForeColor;
            float partWidth = this.DividingInterval * 10;
            e.Graphics.TranslateTransform(this.DividingStartSpace % partWidth, 0);
            this.DividingBrush.Draw(e.Graphics, new RectangleF(-partWidth, 0, this.ClientSize.Width + 2 * partWidth, this.ClientSize.Height));
            e.Graphics.TranslateTransform(this.WordStartSpace, 0);
            for (int i = 0; i < this.Width / partWidth + 1; i++)
            {
                e.Graphics.DrawString(DividingFormat(-this.DividingStartSpace / partWidth + i), this.Font, brush, new PointF(i * partWidth - DividingWidth(-this.DividingStartSpace / partWidth + i) / 2, this.ClientSize.Height - 3 * DividingHeight - 14));
            }
            e.Graphics.TranslateTransform(-this.WordStartSpace, 0);
            e.Graphics.TranslateTransform(-this.DividingStartSpace % partWidth, 0);
        }
    }
}
