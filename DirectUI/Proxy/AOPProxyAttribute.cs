using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;

namespace DirectUI.Proxy
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AOPProxyAttribute : ProxyAttribute
    {
        private AOPInterceptAchieveBase aOPInterceptAchieveBase;

        public AOPInterceptAchieveBase AOPInterceptAchieveBase
        {
            get { return aOPInterceptAchieveBase; }
        }
        public AOPProxyAttribute()
        {

        }
        public AOPProxyAttribute(Type type)
        {
            object obj = Activator.CreateInstance(type);
            if (obj is AOPInterceptAchieveBase )
            {
                this.aOPInterceptAchieveBase = (AOPInterceptAchieveBase)obj;
            }
            else
            {
                throw new ArgumentException("特性的参数必须是实现IAOPInterceptAchieve接口的对象");
            }
        }

        //覆写CreateInstance函数，返回我们自建的代理  
        //该方法会在需要被代理的类在实例化时被创建  
        public override MarshalByRefObject CreateInstance(Type serverType)//serverType是被AOPAttribute修饰的类  
        {
            //未初始化的实例的默认透明代理  
            MarshalByRefObject target = base.CreateInstance(serverType); //得到未初始化的实例  
            //得到自定义的真实代理  
            AOPRealProxy rp = new AOPRealProxy(serverType, target, aOPInterceptAchieveBase);//new AOPProxyBase(serverType, _targetDict, _aopAchieveDict);  
            return (MarshalByRefObject)rp.GetTransparentProxy();
        }
    }  
}
