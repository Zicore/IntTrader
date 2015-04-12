using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntTrader.API.Base.Exchange;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.Web.Model;
using Microsoft.AspNet.SignalR;
using BalanceModel = IntTrader.API.Base.Model.BalanceModel;

namespace IntTrader.Web.Hubs
{
    public class BalanceHub : Hub
    {
        public BalanceHub()
        {

        }

        public void RequestBalance(String exchange, String pair)
        {
            var command = ExchangeManager.Functions[APIFunction.RequestBalances];

            exchange = exchange.ToLower();

            var pairBase = WebService.Broker.GetPair(exchange, pair);

            var result = WebService.Broker.Execute(exchange, command, pair) as BalanceModel;

            if (result != null)
            {
                Clients.Caller.update(exchange, result);
            }
        }
    }
}
