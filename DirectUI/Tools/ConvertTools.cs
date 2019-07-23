using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DirectUI
{
    internal class ConvertTools
    {
        /// <summary> 中国式四舍五入
        /// </summary>
        /// <param name="d">原数</param>
        /// <returns>结果</returns>
        public static int ChineseRounding(double d)
        {
            //return Math.Round(d, 2, MidpointRounding.AwayFromZero); 这个可以转小数点
            return Convert.ToInt32(Math.Round(d, MidpointRounding.AwayFromZero));
        }
    }
}
