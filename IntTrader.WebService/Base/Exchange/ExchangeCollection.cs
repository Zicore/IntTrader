using System;
using System.Collections.Generic;
using IntTrader.API.Base.Exchange.Base;
using NLog;

namespace IntTrader.WebService.Base.Exchange
{
    public class ExchangeCollection
    {
        private readonly Logger Log = LogManager.GetCurrentClassLogger();

        Dictionary<String, ExchangeBase> _items = new Dictionary<String, ExchangeBase>();

        public Dictionary<String, ExchangeBase> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public void Add(ExchangeBase exchange)
        {
            if (!Items.ContainsKey(exchange.Name.ToLower()))
            {
                Items.Add(exchange.Name.ToLower(), exchange);

                Log.Info("Adding Exchange: {0}", exchange.Name);
            }
        }
    }
}
