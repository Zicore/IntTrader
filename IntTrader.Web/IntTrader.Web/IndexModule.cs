using IntTrader.API.Base.Exchange;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;

namespace IntTrader.Web
{
    using Nancy;

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters =>
            {
                var command = ExchangeManager.Functions[APIFunction.RequestTicker];
                var result = WebService.Broker.Execute("bitfinex", command, "btcusd") as TickerModel;

                return View["index", result];
            };
        }
    }
}