using DirectUI.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectUI.Proxy
{
    public class TakesTimeInfo
    {
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        public string ClassName { get; set; }
        public double RenderMilliseconds { get; set; }
        public TakesTimeInfo()
        {

        }
        public TakesTimeInfo(string className)
        {
            this.ClassName = className;
        }
        public void Begin()
        {
            stopwatch.Start(); //  开始监视代码
        }
        public void End()
        {
            stopwatch.Stop(); //  停止监视
            this.RenderMilliseconds = stopwatch.Elapsed.TotalMilliseconds;
        }
    }
    public class TakesTimeInfoCollection : DUIItemCollection<TakesTimeInfo>
    {
        public List<TakesTimeInfo> OrderTakesTimeInfo
        {
            get { return this.OfType<TakesTimeInfo>().OrderByDescending(d => d.RenderMilliseconds).ToList(); }
        }
    }
}
