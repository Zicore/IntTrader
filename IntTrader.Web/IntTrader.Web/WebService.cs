using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntTrader.API.Base.Exchange;
using IntTrader.WebService.Base;

namespace IntTrader.Web
{
    public static class WebService
    {
        static WebService()
        {
            Broker = new WebBroker();
        }

        public static void LoadExchangeSettings(String hash)
        {
            Broker.ExchangeManager.LoadSettings(hash);
        }

        public static WebBroker Broker { get; set; }
    }
}
