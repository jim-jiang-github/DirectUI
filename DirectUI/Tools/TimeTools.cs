using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI
{
    internal class TimeTools
    {
        private static Stopwatch stopwatch = new Stopwatch();
        /// <summary> 获取函数的执行时间，单位毫秒
        /// </summary>
        /// <param name="action">执行的函数</param>
        /// <returns>执行时间</returns>
        public static long GetExecuteTime(Action action)
        {
            stopwatch.Restart();
            action.Invoke();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
    }
}
