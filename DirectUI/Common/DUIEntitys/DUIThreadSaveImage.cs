using DirectUI.Common.DUIEntitys;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    /// <summary> 一个线程安全的image类
    /// </summary>
    public class DUIThreadSaveImage : IDisposable
    {
        private readonly static object lockObj = new object();
        private Size size = Size.Empty;
        private Size lastSize = Size.Empty;
        private Func<Size> sizeFunc = null;
        private bool callRefresh = false;
        private Action<DUIGraphics, Size> drawAction = null;
        private DUIImage image = null;
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
        public DUIThreadSaveImage(Size size, Action<DUIGraphics, Size> drawAction)
        {
            this.lastSize = size;
            this.Size = size;
            this.drawAction = drawAction;
        }
        /// <summary> 
        /// </summary>
        /// <param name="size">画笔初始尺寸</param>
        /// <param name="drawAction">绘图函数</param>
        public DUIThreadSaveImage(Func<Size> sizeFunc, Action<DUIGraphics, Size> drawAction)
        {
            this.sizeFunc = sizeFunc;
            this.drawAction = drawAction;
        }
        public DUIThreadSaveImage(Action<DUIGraphics, Size> drawAction)
        {
            this.drawAction = drawAction;
        }
        /// <summary> 同步绘制图像
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        public void DrawSync(DUIGraphics g, RectangleF rect)
        {
            if (rect.Width == 0 || rect.Height == 0) { return; }
            if (image == null || this.lastSize != this.Size || this.callRefresh)
            {
                this.SyncRefresh();
            }
            if (this.image == null) { return; }
            lock (lockObj)
            {
                g.DrawImage(this.image, new RectangleF(rect.X, rect.Y, rect.Width, rect.Height));
            }
        }
        /// <summary> 异步绘制图像
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        public void DrawAsync(DUIGraphics g, RectangleF rect, Action cache = null, Action loaded = null)
        {
            if (rect.Width == 0 || rect.Height == 0) { return; }
            if (image == null || this.lastSize != this.Size || this.callRefresh)
            {
                AsyncRefresh(loaded);
            }
            if (this.image == null)
            {
                if (cache != null) { cache.Invoke(); }
            }
            else
            {
                lock (lockObj)
                {
                    g.DrawImage(this.image, new RectangleF(rect.X, rect.Y, rect.Width, rect.Height));
                }
            }
        }
        /// <summary> 下次调用的时候刷新
        /// </summary>
        public void CallRefresh()
        {
            this.callRefresh = true;
        }
        /// <summary> 强制刷新
        /// </summary>
        private void AsyncRefresh(Action loadedDo = null)
        {
            lock (lockObj)
            {
                if (this.image != null)
                {
                    this.image.Dispose();
                    this.image = null;
                }
            }
            DUIThreadSaveActions.Add(SyncRefresh, loadedDo);
        }
        /// <summary> 强制刷新
        /// </summary>
        private void SyncRefresh()
        {
            DUIImage image = null;
            this.callRefresh = false;
            this.lastSize = this.Size;
            if (this.lastSize.Width != 0 && this.lastSize.Height != 0)
            {
                try
                {
                    image = DUIImage.FromImage(new Bitmap(this.Size.Width, this.Size.Height));
                }
                catch { }
                using (DUIGraphics g = Graphics.FromImage(image))
                {
                    if (drawAction != null)
                    {
                        drawAction.Invoke(g, this.Size);
                    }
                }
            }
            lock (lockObj)
            {
                if (this.image != null)
                {
                    this.image.Dispose();
                }
                this.image = image;
            }
        }
        #region IDisposable
        public void Dispose()
        {
            if (this.image != null) { this.image.Dispose(); }
        }
        #endregion
    }
}
