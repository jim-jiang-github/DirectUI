using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectUI.Common
{
    public enum DUISmoothingMode
    {
        /// <summary> 指定不抗锯齿
        /// </summary>
        Default = 0,
        /// <summary> 指定抗锯齿的呈现
        /// </summary>
        AntiAlias = 1,
    }
}
