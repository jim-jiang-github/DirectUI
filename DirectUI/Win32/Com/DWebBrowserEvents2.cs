using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace DirectUI.Win32.Com
{
    [ComImport, Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D"), 
    TypeLibType(TypeLibTypeFlags.FHidden | TypeLibTypeFlags.FDispatchable), 
    InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    internal interface DWebBrowserEvents2
    {
        [PreserveSig,
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), 
        DispId(0x66)]
        void StatusTextChange([In, MarshalAs(UnmanagedType.BStr)] string Text);

        [PreserveSig, 
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), 
        DispId(0x6c)]
        void ProgressChange([In] int Progress, [In] int ProgressMax);

        [PreserveSig, 
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime),
        DispId(0x69)]
        void CommandStateChange([In] int Command, [In] bool Enable);

        [PreserveSig, 
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), 
        DispId(0x6a)]
        void DownloadBegin();

        [PreserveSig,
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime),
        DispId(0x68)]
        void DownloadComplete();

        [PreserveSig,
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime),
        DispId(0x71)]
        void TitleChange([In, MarshalAs(UnmanagedType.BStr)] string Text);

        [PreserveSig, 
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime),
        DispId(0x70)]
        void PropertyChange([In, MarshalAs(UnmanagedType.BStr)] string szProperty);

        [PreserveSig, 
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime),
        DispId(250)]
        void BeforeNavigate2(
            [In, MarshalAs(UnmanagedType.IDispatch)] object pDisp,
            [In, MarshalAs(UnmanagedType.Struct)] ref object URL, 
            [In, MarshalAs(UnmanagedType.Struct)] ref object Flags, 
            [In, MarshalAs(UnmanagedType.Struct)] ref object TargetFrameName, 
            [In, MarshalAs(UnmanagedType.Struct)] ref object PostData,
            [In, MarshalAs(UnmanagedType.Struct)] ref object Headers, 
            [In, Out] ref bool Cancel);

        [PreserveSig,
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime),
        DispId(0xfb)]
        void NewWindow2(
            [In, Out, MarshalAs(UnmanagedType.IDispatch)] ref object ppDisp, 
            [In, Out] ref bool Cancel);

        [PreserveSig,
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), 
        DispId(0xfc)]
        void NavigateComplete2(
            [In, MarshalAs(UnmanagedType.IDispatch)] object pDisp,
            [In, MarshalAs(UnmanagedType.Struct)] ref object URL);

        [PreserveSig, 
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime),
        DispId(0x103)]
        void DocumentComplete(
            [In, MarshalAs(UnmanagedType.IDispatch)] object pDisp,
            [In, MarshalAs(UnmanagedType.Struct)] ref object URL);

        [PreserveSig, 
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), 
        DispId(0xfd)]
        void OnQuit();

        [PreserveSig, 
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), 
        DispId(0xfe)]
        void OnVisible([In] bool Visible);

        [PreserveSig,
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime),
        DispId(0xff)]
        void OnToolBar([In] bool ToolBar);

        [PreserveSig, MethodImpl(
            MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime),
        DispId(0x100)]
        void OnMenuBar([In] bool MenuBar);

        [PreserveSig, 
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), 
        DispId(0x101)]
        void OnStatusBar([In] bool StatusBar);

        [PreserveSig, 
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime),
        DispId(0x102)]
        void OnFullScreen([In] bool FullScreen);

        [PreserveSig, 
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime),
        DispId(260)]
        void OnTheaterMode([In] bool TheaterMode);

        [PreserveSig, 
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime),
        DispId(0x106)]
        void WindowSetResizable([In] bool Resizable);

        [PreserveSig, 
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), 
        DispId(0x108)]
        void WindowSetLeft([In] int Left);

        [PreserveSig,
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime),
        DispId(0x109)]
        void WindowSetTop([In] int Top);

        [PreserveSig,
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime),
        DispId(0x10a)]
        void WindowSetWidth([In] int Width);

        [PreserveSig, 
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), 
        DispId(0x10b)]
        void WindowSetHeight([In] int Height);

        [PreserveSig, 
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), 
        DispId(0x107)]
        void WindowClosing([In] bool IsChildWindow, [In, Out] ref bool Cancel);

        [PreserveSig, 
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime),
        DispId(0x10c)]
        void ClientToHostWindow([In, Out] ref int CX, [In, Out] ref int CY);

        [PreserveSig, 
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime),
        DispId(0x10d)]
        void SetSecureLockIcon([In] int SecureLockIcon);

        [PreserveSig, 
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), 
        DispId(270)]
        void FileDownload([In, Out] ref bool Cancel);

        [PreserveSig,
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), 
        DispId(0x10f)]
        void NavigateError(
            [In, MarshalAs(UnmanagedType.IDispatch)] object pDisp,
            [In, MarshalAs(UnmanagedType.Struct)] ref object URL, 
            [In, MarshalAs(UnmanagedType.Struct)] ref object Frame, 
            [In, MarshalAs(UnmanagedType.Struct)] ref object StatusCode,
            [In, Out] ref bool Cancel);

        [PreserveSig, 
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), 
        DispId(0xe1)]
        void PrintTemplateInstantiation(
            [In, MarshalAs(UnmanagedType.IDispatch)] object pDisp);

        [PreserveSig, 
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), 
        DispId(0xe2)]
        void PrintTemplateTeardown([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp);

        [PreserveSig, 
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), 
        DispId(0xe3)]
        void UpdatePageStatus(
            [In, MarshalAs(UnmanagedType.IDispatch)] object pDisp,
            [In, MarshalAs(UnmanagedType.Struct)] ref object nPage,
            [In, MarshalAs(UnmanagedType.Struct)] ref object fDone);

        [PreserveSig,
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime),
        DispId(0x110)]
        void PrivacyImpactedStateChange([In] bool bImpacted);

        [PreserveSig, 
        MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), 
        DispId(0x111)]
        void NewWindow3(
            [In, Out, MarshalAs(UnmanagedType.IDispatch)] ref object ppDisp,
            [In, Out] ref bool Cancel, [In] uint dwFlags, 
            [In, MarshalAs(UnmanagedType.BStr)] string bstrUrlContext,
            [In, MarshalAs(UnmanagedType.BStr)] string bstrUrl);
    }
}
