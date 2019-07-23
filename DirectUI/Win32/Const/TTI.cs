using System;

namespace DirectUI.Win32.Const
{
    /// <summary>
    /// ToolTip Icons possible wParam values for TTM_SETTITLE message.
    /// </summary>
    internal static class TTI
    {
        internal const int TTI_NONE = 0;
        internal const int TTI_INFO = 1; //(32512)
        internal const int TTI_WARNING = 2;
        internal const int TTI_ERROR = 3;

        //// values larger thant TTI_ERROR are assumed to be an HICON value
        internal const int TTI_INFO_LARGE = 4;
        internal const int TTI_WARNING_LARGE = 5;
        internal const int TTI_ERROR_LARGE = 6;
    }
}
