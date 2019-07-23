using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace DirectUI.Proxy
{
    public class AOPInterceptAchieveBase
    {
        public virtual System.Runtime.Remoting.Messaging.IMessage DoMethod(MarshalByRefObject target, IMethodCallMessage msg)
        {
            return RemotingServices.ExecuteMessage(target, msg);
        }
    }
}
