/********************************************************************
 * * 使本项目源码前请仔细阅读以下协议内容，如果你同意以下协议才能使用本项目所有的功能,
 * * 否则如果你违反了以下协议，有可能陷入法律纠纷和赔偿，作者保留追究法律责任的权利。
 * * Copyright (C) 
 * * 作者： 煎饼的归宿    QQ：375324644   
 * * 请保留以上版权信息，否则作者将保留追究法律责任。
 * * 最后修改：BF-20150503UIWA
 * * 创建时间：2017/12/18 14:20:01
********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectUI.Log
{
    public class DUILog
    {
        public static event Action<Exception> LogRecord;
        private static void OnLogRecord(Exception ex)
        {
            if (LogRecord != null)
            {
                LogRecord(ex);
            }
        }
        public static void WriteLog(Exception ex)
        {
            OnLogRecord(ex);
        }
        public static void GettingLog(Exception ex)
        {
            //System.Diagnostics.Debug.WriteLine(ex);
        }
    }
}
