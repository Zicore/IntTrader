using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntTrader.WebService.Base;

namespace IntTrader.Web
{
    public static class WebService
    {
        static WebService()
        {
            Broker = new WebBroker();
        }

        public static WebBroker Broker { get; set; }
    }
}
