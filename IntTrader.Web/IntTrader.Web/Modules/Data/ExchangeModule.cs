using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using IntTrader.API.Base.Exchange;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;
using IntTrader.WebService.Base;
using Nancy;
using Nancy.Extensions;
using Nancy.Routing;
using Newtonsoft.Json;

namespace IntTrader.Web.Modules.Data
{
    public class ExchangeModule : NancyModule
    {

        public ExchangeModule()
        {

            Get["/{exchange}/ticker/{pair}"] = _ =>
            {
                if (Request.IsLocal())
                {
                    var command = ExchangeManager.Functions[APIFunction.RequestTicker];
                    var result = WebService.Broker.Execute(_.exchange.ToString(), command, _.pair.ToString());
                    var response = (Response)JsonConvert.SerializeObject(result);
                    response.ContentType = "application/json";
                    return response;
                }
                return HttpStatusCode.Forbidden;
            };

            Get["/{exchange}/balance"] = _ =>
            {
                if (Request.IsLocal())
                {
                    var command = ExchangeManager.Functions[APIFunction.RequestBalances];
                    ResponseModelBase responseModel;
                    if (WebService.Broker.TryExecute(out responseModel, _.exchange.ToString(), command))
                    {
                        var response = (Response)JsonConvert.SerializeObject(responseModel);
                        response.ContentType = "application/json";
                        return response;
                    }

                }
                return HttpStatusCode.Forbidden;
            };

            Get["/{exchange}/neworder/{side}/{type}/{pair}/{amount:decimal}/{price:decimal}"] = _ =>
            {
                if (Request.IsLocal())
                {
                    var command = ExchangeManager.Functions[APIFunction.RequestNewOrder];
                    ResponseModelBase responseModel;
                    if (WebService.Broker.TryExecute(out responseModel, (String)_.exchange, command, (String)_.side, (String)_.type, (String)_.pair, (decimal)_.amount, (decimal)_.price))
                    {
                        var response = (Response)JsonConvert.SerializeObject(responseModel);
                        response.ContentType = "application/json";
                        return response;
                    }
                }
                return HttpStatusCode.Forbidden;
            };
        }

    }
}
