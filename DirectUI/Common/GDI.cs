using DirectUI.Win32;
using DirectUI.Win32.Struct;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    public sealed class GDI : IDisposable
    {
        private IntPtr hDC = IntPtr.Zero;
        private GDI(IntPtr hDC)
        {
            this.hDC = hDC;
        }

        public static GDI FromHdc(IntPtr hDC)
        {
            return new GDI(hDC);
        }

        public void DrawHDC(IntPtr hDC, Rectangle destRect, Rectangle srcRect)
        {
            #region AlphaBlend
            BLENDFUNCTION bf = new BLENDFUNCTION(DirectUI.Win32.Const.AC.AC_SRC_OVER, 0x0, 255, DirectUI.Win32.Const.AC.AC_SRC_ALPHA);
            DirectUI.Win32.NativeMethods.AlphaBlend(
                this.hDC,
                destRect.X,
                destRect.Y,
                destRect.Width,
                destRect.Height,
                hDC,
                srcRect.X,
                srcRect.Y,
                srcRect.Width,
                srcRect.Height,
                bf);
            #endregion
        }

        public void Dispose()
        {
        }
    }
}
