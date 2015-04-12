using System.Globalization;
using System.Threading;
using IntTrader.API.Base.Exchange;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;
using IntTrader.Web.Model;
using Nancy;
using Nancy.ModelBinding;

namespace IntTrader.Web.Modules
{
    public class MarketModel : NancyModule
    {
        public MarketModel()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            Get["/markets/{exchange}"] = _ =>
            {
                var exchangeString = _.exchange;
                var exchange = WebService.Broker.GetExchange(exchangeString);

                return View["markets", WebService.CreateExchangeModel(exchange)];
            };

            Post["/markets"] = parameters =>
            {
                var newOrderQuery = this.Bind<NewOrderModel>();

                // TODO: create order
                return Negotiate.WithStatusCode(HttpStatusCode.OK);
            };
        }
    }
}