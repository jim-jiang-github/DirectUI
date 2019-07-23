using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DirectUI.Win32.Struct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct UPDATELAYEREDWINDOWINFO
    {
        internal Int32 _cbSize;
        internal IntPtr _hdcDst;
        internal POINT _pptDst;
        internal SIZE _psize;
        internal IntPtr _hdcSrc;
        internal POINT _pptSrc;
        internal Int32 _crKey;
        internal BLENDFUNCTION _pblend;
        internal Int32 _dwFlags;
        internal RECT _prcDirty;
        internal UPDATELAYEREDWINDOWINFO(Int32 cbSize, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pptSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags, ref RECT prcDirty)
        {
            _cbSize = cbSize;
            _hdcDst = hdcDst;
            _pptDst = pptDst;
            _psize = psize;
            _hdcSrc = hdcSrc;
            _pptSrc = pptSrc;
            _crKey = crKey;
            _pblend = pblend;
            _dwFlags = dwFlags;
            _prcDirty = prcDirty;
        }
    }
}
