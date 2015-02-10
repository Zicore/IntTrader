using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntTrader.API.Base.Exchange;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.WebService.Base;
using Nancy;
using Newtonsoft.Json;

namespace IntTrader.Web.Modules.Data
{
    public class ExchangeModule : NancyModule
    {

        public ExchangeModule()
        {
            // would capture routes to /products/list sent as a GET request
            Get["/{exchange}/ticker/{pair}"] = _ =>
            {
                var command = ExchangeManager.Functions[APIFunction.RequestTicker];
                var result = WebService.Broker.Execute(_.exchange.ToString(), command, _.pair.ToString());
                var response = (Response)JsonConvert.SerializeObject(result);
                response.ContentType = "application/json";
                return response;
            };
        }
    }
}
