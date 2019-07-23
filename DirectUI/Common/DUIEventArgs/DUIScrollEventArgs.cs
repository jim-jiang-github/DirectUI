using DirectUI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DirectUI.Common
{
    /// <summary> 为 Scroll 事件提供数据
    /// </summary>
    public class DUIScrollEventArgs
    {
        private float newValue;
        private float oldValue;
        private ScrollOrientation scrollOrientation;
        private ScrollEventType type;
        /// <summary> 获取或设置滚动条的新 System.Windows.Forms.ScrollBar.Value
        /// </summary>
        public float NewValue
        {
            get { return newValue; }
            set { newValue = value; }
        }
        /// <summary> 获取滚动条的旧 System.Windows.Forms.ScrollBar.Value 值
        /// </summary>
        public float OldValue
        {
            get { return oldValue; }
        }
        /// <summary> 获取引发 Scroll 事件的滚动条方向
        /// </summary>
        public ScrollOrientation ScrollOrientation
        {
            get { return scrollOrientation; }
        }
        /// <summary> 获取所发生的滚动事件的类型
        /// </summary>
        public ScrollEventType Type
        {
            get { return type; }
        }
        /// <summary> 属性的给定值初始化 DUIScrollEventArgs 类的新实例
        /// </summary>
        /// <param name="type">System.Windows.Forms.ScrollEventType 值之一</param>
        /// <param name="newValue">滚动条的新值</param>
        public DUIScrollEventArgs(ScrollEventType type, float newValue)
        {
            this.type = type;
            this.newValue = newValue;
        }
        /// <summary> 属性的给定值初始化 DUIScrollEventArgs 类的新实例
        /// </summary>
        /// <param name="type">System.Windows.Forms.ScrollEventType 值之一</param>
        /// <param name="oldValue">滚动条的旧值</param>
        /// <param name="newValue">滚动条的新值</param>
        public DUIScrollEventArgs(ScrollEventType type, float oldValue, float newValue)
        {
            this.type = type;
            this.oldValue = oldValue;
            this.newValue = newValue;
        }
        /// <summary> 属性的给定值初始化 DUIScrollEventArgs 类的新实例
        /// </summary>
        /// <param name="type">System.Windows.Forms.ScrollEventType 值之一</param>
        /// <param name="newValue">滚动条的新值</param>
        /// <param name="scroll">System.Windows.Forms.ScrollOrientation 值之一</param>
        public DUIScrollEventArgs(ScrollEventType type, float newValue, ScrollOrientation scroll)
        {
            this.type = type;
            this.newValue = newValue;
            this.scrollOrientation = scroll;
        }
        /// <summary> 属性的给定值初始化 DUIScrollEventArgs 类的新实例
        /// </summary>
        /// <param name="type">System.Windows.Forms.ScrollEventType 值之一</param>
        /// <param name="oldValue">滚动条的旧值</param>
        /// <param name="newValue">滚动条的新值</param>
        /// <param name="scroll">System.Windows.Forms.ScrollOrientation 值之一</param>
        public DUIScrollEventArgs(ScrollEventType type, float oldValue, float newValue, ScrollOrientation scroll)
        {
            this.type = type;
            this.oldValue = oldValue;
            this.newValue = newValue;
            this.scrollOrientation = scroll;
        }
    }
}
