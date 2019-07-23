using System;

namespace DirectUI.Win32.Const
{
    internal class HITTEST
    {
        /// <summary>
        /// On the screen background or on a dividing line between windows 
        /// (same as HTNOWHERE; except that the DefWindowProc function produces a system beep to indicate an error).
        /// </summary>
        internal const int HTERROR = (-2);
        /// <summary>
        /// In a window currently covered by another window in the same thread 
        /// (the message will be sent to underlying windows in the same thread until one of them returns a code that is not HTTRANSPARENT).
        /// </summary>
        internal const int HTTRANSPARENT = (-1);
        /// <summary>
        /// On the screen background or on a dividing line between windows.
        /// </summary>
        internal const int HTNOWHERE = 0;
        /// <summary>In a client area.</summary>
        internal const int HTCLIENT = 1;
        /// <summary>In a title bar.</summary>
        internal const int HTCAPTION = 2;
        /// <summary>In a window menu or in a Close button in a child window.</summary>
        internal const int HTSYSMENU = 3;
        /// <summary>In a size box (same as HTSIZE).</summary>
        internal const int HTGROWBOX = 4;
        /// <summary>In a menu.</summary>
        internal const int HTMENU = 5;
        /// <summary>In a horizontal scroll bar.</summary>
        internal const int HTHSCROLL = 6;
        /// <summary>In the vertical scroll bar.</summary>
        internal const int HTVSCROLL = 7;
        /// <summary>In a Minimize button.</summary>
        internal const int HTMINBUTTON = 8;
        /// <summary>In a Maximize button.</summary>
        internal const int HTMAXBUTTON = 9;
        /// <summary>In the left border of a resizable window 
        /// (the user can click the mouse to resize the window horizontally).</summary>
        internal const int HTLEFT = 10;
        /// <summary>
        /// In the right border of a resizable window 
        /// (the user can click the mouse to resize the window horizontally).
        /// </summary>
        internal const int HTRIGHT = 11;
        /// <summary>In the upper-horizontal border of a window.</summary>
        internal const int HTTOP = 12;
        /// <summary>In the upper-left corner of a window border.</summary>
        internal const int HTTOPLEFT = 13;
        /// <summary>In the upper-right corner of a window border.</summary>
        internal const int HTTOPRIGHT = 14;
        /// <summary>	In the lower-horizontal border of a resizable window 
        /// (the user can click the mouse to resize the window vertically).</summary>
        internal const int HTBOTTOM = 15;
        /// <summary>In the lower-left corner of a border of a resizable window 
        /// (the user can click the mouse to resize the window diagonally).</summary>
        internal const int HTBOTTOMLEFT = 16;
        /// <summary>	In the lower-right corner of a border of a resizable window 
        /// (the user can click the mouse to resize the window diagonally).</summary>
        internal const int HTBOTTOMRIGHT = 17;
        /// <summary>In the border of a window that does not have a sizing border.</summary>
        internal const int HTBORDER = 18;

        internal const int HTOBJECT = 19;
        /// <summary>In a Close button.</summary>
        internal const int HTCLOSE = 20;
        /// <summary>In a Help button.</summary>
        internal const int HTHELP = 21;
    }
}
