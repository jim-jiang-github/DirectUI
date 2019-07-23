using System;

namespace DirectUI.Win32.Const
{
    internal class SC
    {
        internal const int SC_SIZE = 0xF000;
        internal const int SC_MOVE = 0xF010;
        internal const int SC_MINIMIZE = 0xF020;
        internal const int SC_MAXIMIZE = 0xF030;
        internal const int SC_NEXTWINDOW = 0xF040;
        internal const int SC_PREVWINDOW = 0xF050;
        internal const int SC_CLOSE = 0xF060;
        internal const int SC_VSCROLL = 0xF070;
        internal const int SC_HSCROLL = 0xF080;
        internal const int SC_MOUSEMENU = 0xF090;
        internal const int SC_KEYMENU = 0xF100;
        internal const int SC_ARRANGE = 0xF110;
        internal const int SC_RESTORE = 0xF120;
        internal const int SC_TASKLIST = 0xF130;
        internal const int SC_SCREENSAVE = 0xF140;
        internal const int SC_HOTKEY = 0xF150;
        internal const int SC_DoubleRESTORE = 61730;//双击还原

        internal const int SC_DEFAULT = 0xF160;
        internal const int SC_MONITORPOWER = 0xF170;
        internal const int SC_CONTEXTHELP = 0xF180;
        internal const int SC_SEPARATOR = 0xF00F;

        internal const int SCF_ISSECURE = 0x00000001;

        internal const int SC_ICON = SC_MINIMIZE;
        internal const int SC_ZOOM = SC_MAXIMIZE;
    }
}
