using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectUI.Proxy
{
    public class TakesTime
    {
        private static TakesTimeInfoCollection takesTimeInfos = new TakesTimeInfoCollection();

        public static TakesTimeInfoCollection TakesTimeInfos
        {
            get { return takesTimeInfos; }
        }
    }
}
