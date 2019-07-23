using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectUI.Common
{
    // 摘要: 
    //     为 System.Windows.Forms.Control.Layout 事件提供数据。无法继承此类。
    public sealed class DUILayoutEventArgs
    {
        private DUIControl affectedControl = null;
        private string affectedProperty = string.Empty;
        //
        // 摘要: 
        //     获取受此更改影响的控件。
        //
        // 返回结果: 
        //     受此更改影响的子 System.Windows.Forms.Control。
        public DUIControl AffectedControl { get { return affectedControl; } }
        //
        // 摘要: 
        //     获取受此更改影响的属性。
        //
        // 返回结果: 
        //     受此更改影响的属性。
        public string AffectedProperty { get { return affectedProperty; } }
        public DUILayoutEventArgs(DUIControl affectedControl, string affectedProperty)
        {
            this.affectedControl = affectedControl;
            this.affectedProperty = affectedProperty;
        }
    }
}
