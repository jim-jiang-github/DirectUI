using System;

namespace DirectUI.Win32.Const
{
    /// <summary>
    /// drawstage flags
    /// values under 0x00010000 are reserved for global custom draw values.
    /// above that are for specific controls
    /// </summary>
    internal static class CDDS
    {
        internal const int CDDS_PREPAINT = 0x00000001;
        internal const int CDDS_POSTPAINT = 0x00000002;
        internal const int CDDS_PREERASE = 0x00000003;
        internal const int CDDS_POSTERASE = 0x00000004;
        // the 0x000010000 bit means it's individual item specific
        internal const int CDDS_ITEM = 0x00010000;
        internal const int CDDS_ITEMPREPAINT = (CDDS_ITEM | CDDS_PREPAINT);
        internal const int CDDS_ITEMPOSTPAINT = (CDDS_ITEM | CDDS_POSTPAINT);
        internal const int CDDS_ITEMPREERASE = (CDDS_ITEM | CDDS_PREERASE);
        internal const int CDDS_ITEMPOSTERASE = (CDDS_ITEM | CDDS_POSTERASE);

#if (_WIN32_IE0400) //>= 0x0400
        internal const int CDDS_SUBITEM = 0x00020000;
#endif
    }
}
