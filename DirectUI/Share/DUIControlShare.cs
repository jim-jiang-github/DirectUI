using DirectUI.Collection;
using DirectUI.Common;
using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectUI.Share
{
    /// <summary> DUI窗体下的控件共享的信息
    /// </summary>
    internal class DUIControlShare
    {
        //private AlwaysGetMouseEventCollection alwaysGetMouseEvents = null;
        //private AlwaysGetKeyEventCollection alwaysGetKeyEvents = null;
        /// <summary> 获得焦点的DUIControl
        /// </summary>
        internal DUIControl FocusedDUIControl { get; set; }
        /// <summary> 置顶的控件
        /// </summary>
        internal DUIControl TopMostDUIControl { get; set; }
        private List<DUIMouseStateEventArgs> mouseDownDUIControls = new List<DUIMouseStateEventArgs>();

        internal List<DUIMouseStateEventArgs> MouseDownDUIControls
        {
            get { return mouseDownDUIControls; }
        }
        /// <summary> 鼠标移动过的DUIControl
        /// </summary>
        public DUIControl MouseMoveDUIControl { get; set; }
    }
}
