using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace DirectUI.Win32.Com
{
    [ComVisible(true), ComImport(),
    Guid("00000122-0000-0000-C000-000000000046"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDropTarget
    {
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int DragEnter(
            [In, MarshalAs(UnmanagedType.Interface)] System.Runtime.InteropServices.ComTypes.IDataObject pDataObj,
            [In, MarshalAs(UnmanagedType.U4)] uint grfKeyState,
            [In, MarshalAs(UnmanagedType.Struct)] tagPOINT pt,
            [In, Out, MarshalAs(UnmanagedType.U4)] ref uint pdwEffect);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int DragOver(
            [In, MarshalAs(UnmanagedType.U4)] uint grfKeyState,
            [In, MarshalAs(UnmanagedType.Struct)] tagPOINT pt,
            [In, Out, MarshalAs(UnmanagedType.U4)] ref uint pdwEffect);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int DragLeave();

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int Drop(
            [In, MarshalAs(UnmanagedType.Interface)] System.Runtime.InteropServices.ComTypes.IDataObject pDataObj,
            [In, MarshalAs(UnmanagedType.U4)] uint grfKeyState,
            [In, MarshalAs(UnmanagedType.Struct)] tagPOINT pt,
            [In, Out, MarshalAs(UnmanagedType.U4)] ref uint pdwEffect);
    }
}
