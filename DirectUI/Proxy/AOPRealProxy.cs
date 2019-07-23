using DirectUI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;
using System.Text;

namespace DirectUI.Proxy
{
    public class AOPRealProxy : RealProxy
    {
        private Type type;
        private MarshalByRefObject target;
        private AOPInterceptAchieveBase iAOPInterceptAchieve;
        public AOPRealProxy(Type type, MarshalByRefObject target, AOPInterceptAchieveBase iAOPInterceptAchieve)
            : base(type)
        {
            this.type = type;
            this.target = target;
            this.iAOPInterceptAchieve = iAOPInterceptAchieve;
        }
        public override System.Runtime.Remoting.Messaging.IMessage Invoke(System.Runtime.Remoting.Messaging.IMessage msg)
        {
            if (msg is IConstructionCallMessage constructionCallMessage)
            {
                RealProxy defaultProxy = RemotingServices.GetRealProxy(this.target);
                //如果不做下面这一步，_targetDict[targetTypeName]还是一个没有直正实例化被代理对象的透明代理，  
                //这样的话，会导致没有直正构建对象。  
                defaultProxy.InitializeServerObject(constructionCallMessage);
                //本类是一个RealProxy，它可通过GetTransparentProxy函数得到透明代理  
                return EnterpriseServicesHelper.CreateConstructionReturnMessage(constructionCallMessage, (MarshalByRefObject)GetTransparentProxy());
            }
            else if (msg is IMethodCallMessage methodCallMessage)
            {
                if (iAOPInterceptAchieve != null)
                {
                    return iAOPInterceptAchieve.DoMethod(this.target, methodCallMessage);
                }
            }
            return RemotingServices.ExecuteMessage(this.target, (IMethodCallMessage)msg);
        }
    }
}
