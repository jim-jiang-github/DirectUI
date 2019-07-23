using System;

namespace DirectUI.Win32.Const
{
    /// <summary>
    /// SetWindowPos Flags.
    /// </summary>
    internal enum SWP
    {
        SWP_NOSIZE = 0x0001,
        SWP_NOMOVE = 0x0002,
        SWP_NOZORDER = 0x0004,
        SWP_NOREDRAW = 0x0008,
        SWP_NOACTIVATE = 0x0010,
        SWP_FRAMECHANGED = 0x0020,//The frame changed: send WM_NCCALCSIZE
        SWP_SHOWWINDOW = 0x0040,
        SWP_HIDEWINDOW = 0x0080,
        SWP_NOCOPYBITS = 0x0100,
        SWP_NOOWNERZORDER = 0x0200,//Don't do owner Z ordering
        SWP_NOSENDCHANGING = 0x0400,//Don't send WM_WINDOWPOSCHANGING
        SWP_DEFERERASE = 0x2000,
        SWP_ASYNCWINDOWPOS = 0x4000,

        SWP_DRAWFRAME = SWP_FRAMECHANGED,
        SWP_NOREPOSITION = SWP_NOOWNERZORDER

//#if(WINVER0400) //>= 0x0400
//        SWP_DEFERERASE = 0x2000,
//        SWP_ASYNCWINDOWPOS = 0x4000,
//#endif
    }
}
