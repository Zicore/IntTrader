using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IntTrader.API.Base.Exchange;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;
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
            var result = WebService.Broker.Execute(exchange, command, pair) as TickerModel;

            if (result != null) Clients.Caller.update(exchange, result.LastPrice);
        }
    }
}
