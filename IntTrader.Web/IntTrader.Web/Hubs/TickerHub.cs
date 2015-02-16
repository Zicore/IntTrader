using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IntTrader.API.Base.Exchange;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;
using IntTrader.WebService.Base;
using Microsoft.AspNet.SignalR;
using Nancy;
using Newtonsoft.Json;

namespace IntTrader.Web.Hubs
{
    public class TickerHub : Hub
    {
        public TickerHub()
        {

        }

        public void RequestTicker(String exchange, String pair)
        {
            var command = ExchangeManager.Functions[APIFunction.RequestTicker];

            exchange = exchange.ToLower();

            var pairBase = WebService.Broker.GetPair(exchange,pair);

            var result = WebService.Broker.Execute(exchange, command, pair) as TickerModel;

            if (result != null) Clients.Caller.update(exchange, String.Format("{0} {1}", result.LastPrice, pairBase.RightCurrency.Symbol));
        }
    }
}
