using DirectUI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectUI.Proxy
{
    /// <summary> 记录函数耗时的代理
    /// </summary>
    public class AOPMethodsTakesTime : AOPInterceptAchieveBase
    {
        public override System.Runtime.Remoting.Messaging.IMessage DoMethod(MarshalByRefObject target, System.Runtime.Remoting.Messaging.IMethodCallMessage msg)
        {
            string targetTypeName = Convert.ToString(msg.Properties["__MethodName"]).Split(',')[0];
            TakesTimeInfo takesTimeInfo = new TakesTimeInfo(targetTypeName);
            takesTimeInfo.Begin();
            System.Runtime.Remoting.Messaging.IMessage iMessage = base.DoMethod(target, msg);
            takesTimeInfo.End();
            TakesTime.TakesTimeInfos.Add(takesTimeInfo);
            return iMessage;
        }
    }
}
