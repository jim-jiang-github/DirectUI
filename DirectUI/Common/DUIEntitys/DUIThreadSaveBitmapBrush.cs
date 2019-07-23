using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    /// <summary> 一个线程安全的画笔类
    /// </summary>
    public class DUIThreadSafeBitmapBrush : IDisposable
    {
        private readonly static object lockObj = new object();
        private Size size = Size.Empty;
        private Size lastSize = Size.Empty;
        private Func<Size> sizeFunc = null;
        private bool forceRefresh = false;
        private Action<DUIGraphics, Size> drawAction = null;
        private DUIBitmapBrush bitmapBrush = null;
        /// <summary> 画笔对象 调用时自动刷新
        /// </summary>
        private DUIBitmapBrush BitmapBrush
        {
            get
            {
                if (bitmapBrush == null || this.lastSize != this.Size || this.forceRefresh)
                {
                    this.forceRefresh = false;
                    this.lastSize = this.Size;
                    if (bitmapBrush != null)
                    {
                        bitmapBrush.Dispose();
                        bitmapBrush = null;
                    }
                    if (this.lastSize.Width != 0 && this.lastSize.Height != 0)
                    {
                        Bitmap bmp = new Bitmap(this.Size.Width, this.Size.Height);
                        using (DUIGraphics g = Graphics.FromImage(bmp))
                        {
                            if (drawAction != null)
                            {
                                drawAction.Invoke(g, this.Size);
                            }
                        }
                        bitmapBrush = new DUIBitmapBrush(bmp);
                    }
                }
                return bitmapBrush;
            }
        }
        /// <summary> 赋值新的尺寸，如果尺寸变化则画笔将会被刷新
        /// </summary>
        public Size Size
        {
            get => this.sizeFunc == null ? this.size : this.sizeFunc.Invoke();
            set
            {
                this.size = value;
            }
        }
        /// <summary> 
        /// </summary>
        /// <param name="size">画笔初始尺寸</param>
        /// <param name="drawAction">绘图函数</param>
        public DUIThreadSafeBitmapBrush(Size size, Action<DUIGraphics, Size> drawAction)
        {
            this.lastSize = size;
            this.Size = size;
            this.drawAction = drawAction;
        }
        /// <summary> 
        /// </summary>
        /// <param name="size">画笔初始尺寸</param>
        /// <param name="drawAction">绘图函数</param>
        public DUIThreadSafeBitmapBrush(Func<Size> sizeFunc, Action<DUIGraphics, Size> drawAction)
        {
            this.sizeFunc = sizeFunc;
            this.drawAction = drawAction;
        }
        public DUIThreadSafeBitmapBrush(Action<DUIGraphics, Size> drawAction)
        {
            this.drawAction = drawAction;
        }
        /// <summary> 绘制图像
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        public void Draw(DUIGraphics g, RectangleF rect)
        {
            lock (lockObj)
            {
                if (this.BitmapBrush == null) { return; }
                if (rect.Width == 0 || rect.Height == 0) { return; }
                g.TranslateTransform(rect.X, rect.Y);
                g.FillRectangle(this.BitmapBrush, new RectangleF(0, 0, rect.Width, rect.Height));
                g.TranslateTransform(-rect.X, -rect.Y);
            }
        }
        /// <summary> 下次调用的时候强制刷新笔刷
        /// </summary>
        public void CallRefresh()
        {
            this.forceRefresh = true;
        }
        #region IDisposable
        public void Dispose()
        {
            if (this.bitmapBrush != null) { this.bitmapBrush.Dispose(); }
        }
        #endregion
    }
}
