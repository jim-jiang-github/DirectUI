using System;
using System.Runtime.InteropServices;

namespace DirectUI.Win32.Com
{
    [ComImport(), ComVisible(true),
        Guid("B722BCCB-4E68-101B-A2BC-00AA00404770"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IOleCommandTarget
    {

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int QueryStatus(
            [In] IntPtr pguidCmdGroup,
            [In, MarshalAs(UnmanagedType.U4)] uint cCmds,
            [In, Out, MarshalAs(UnmanagedType.Struct)] ref tagOLECMD prgCmds,
            //This parameter must be IntPtr, as it can be null
            [In, Out] IntPtr pCmdText);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int Exec(
            //[In] ref Guid pguidCmdGroup,
            //have to be IntPtr, since null values are unacceptable
            //and null is used as default group!
            [In] IntPtr pguidCmdGroup,
            [In, MarshalAs(UnmanagedType.U4)] uint nCmdID,
            [In, MarshalAs(UnmanagedType.U4)] uint nCmdexecopt,
            [In] IntPtr pvaIn,
            [In, Out] IntPtr pvaOut);
    }
}
