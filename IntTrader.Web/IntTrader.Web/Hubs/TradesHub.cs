using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntTrader.API.Base.Exchange;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;
using Microsoft.AspNet.SignalR;

namespace IntTrader.Web.Hubs
{
    public class TradesHub : Hub
    {
        public void RequestTrades(String exchange, String pair)
        {
            var command = ExchangeManager.Functions[APIFunction.RequestTrades];
            var result = WebService.Broker.Execute(exchange, command, pair) as TradesModel;

            if (result != null) Clients.Caller.update(exchange, result.Items);
        }
    }
}
