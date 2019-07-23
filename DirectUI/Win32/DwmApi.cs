using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace DirectUI.Win32
{
    internal class DwmApi
    {
        /// <summary>
        /// 允许在指定的窗口模糊效果
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="pBlurBehind"></param>
        [DllImport("dwmapi.dll", PreserveSig = false)]
        internal static extern void DwmEnableBlurBehindWindow(IntPtr hWnd, DWM_BLURBEHIND pBlurBehind);

        /// <summary>
        /// 扩展到客户端区域的窗框
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="pMargins"></param>
        [DllImport("dwmapi.dll", PreserveSig = false)]
        internal static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, MARGINS pMargins);

        /// <summary>
        /// 获取一个值，指示是否启用DWM的组成。 应用程序可以收听组成状态变化的处理
        /// </summary>
        /// <returns></returns>
        [DllImport("dwmapi.dll", PreserveSig = false)]
        internal static extern bool DwmIsCompositionEnabled();

        /// <summary>
        /// 检索DWM的玻璃成分中使用当前的颜色。.应用程序可以听颜色的变化， 处理 WM_DWMCOLORIZATIONCOLORCHANGED通知
        /// </summary>
        /// <param name="pcrColorization"></param>
        /// <param name="pfOpaqueBlend"></param>
        [DllImport("dwmapi.dll", PreserveSig = false)]
        internal static extern void DwmGetColorizationColor(out int pcrColorization, [MarshalAs(UnmanagedType.Bool)]out bool pfOpaqueBlend);

        /// <summary>
        /// 启用或禁用DWM的组成
        /// </summary>
        /// <param name="bEnable"></param>
        [DllImport("dwmapi.dll", PreserveSig = false)]
        internal static extern void DwmEnableComposition(bool bEnable);

        /// <summary>
        /// 创建目标和源窗口之间的DWM缩略图关系
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        [DllImport("dwmapi.dll", PreserveSig = false)]
        internal static extern IntPtr DwmRegisterThumbnail(IntPtr dest, IntPtr source);

        /// <summary>
        /// 移除DWM缩略图关系DwmRegisterThumbnail功能
        /// </summary>
        /// <param name="hThumbnail"></param>
        [DllImport("dwmapi.dll", PreserveSig = false)]
        internal static extern void DwmUnregisterThumbnail(IntPtr hThumbnail);

        /// <summary>
        /// 更新的属性为DWM缩略图。
        /// </summary>
        /// <param name="hThumbnail"></param>
        /// <param name="props"></param>
        [DllImport("dwmapi.dll", PreserveSig = false)]
        internal static extern void DwmUpdateThumbnailProperties(IntPtr hThumbnail, DWM_THUMBNAIL_PROPERTIES props);

        /// <summary>
        /// 检索源DWM缩略图的大小
        /// </summary>
        /// <param name="hThumbnail"></param>
        /// <param name="size"></param>
        [DllImport("dwmapi.dll", PreserveSig = false)]
        internal static extern void DwmQueryThumbnailSourceSize(IntPtr hThumbnail, out Size size);

        [StructLayout(LayoutKind.Sequential)]
        internal class DWM_THUMBNAIL_PROPERTIES
        {
            internal uint dwFlags;
            internal RECT rcDestination;
            internal RECT rcSource;
            internal byte opacity;
            [MarshalAs(UnmanagedType.Bool)]
            internal bool fVisible;
            [MarshalAs(UnmanagedType.Bool)]
            internal bool fSourceClientAreaOnly;

            internal const uint DWM_TNP_RECTDESTINATION = 0x00000001;
            internal const uint DWM_TNP_RECTSOURCE = 0x00000002;
            internal const uint DWM_TNP_OPACITY = 0x00000001;
            internal const uint DWM_TNP_VISIBLE = 0x00000008;
            internal const uint DWM_TNP_SOURCECLIENTAREAONLY = 0x00000010;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class MARGINS
        {
            internal int cxLeftWidth, cxRightWidth, cyTopHeight, cyBottomHeight;

            internal MARGINS(int left, int top, int right, int bottom)
            {
                cxLeftWidth = left; cyTopHeight = top;
                cxRightWidth = right; cyBottomHeight = bottom;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class DWM_BLURBEHIND
        {
            internal uint dwFlags;
            [MarshalAs(UnmanagedType.Bool)]
            internal bool fEnable;
            internal IntPtr hRegionBlur;
            [MarshalAs(UnmanagedType.Bool)]
            internal bool fTransitionOnMaximized;

            internal const uint DWM_BB_ENABLE = 0x00000001;
            internal const uint DWM_BB_BLURREGION = 0x00000002;
            internal const uint DWM_BB_TRANSITIONONMAXIMIZED = 0x00000004;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            internal int left, top, right, bottom;

            internal RECT(int left, int top, int right, int bottom)
            {
                this.left = left; this.top = top; this.right = right; this.bottom = bottom;
            }
        }
    }
}
