using System.Collections.Generic;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Exchange.Bitfinex.Request.Authenticated;
using IntTrader.API.Exchange.Bitfinex.Response;

namespace IntTrader.API.Exchange.Bitfinex.Request
{
    public class Orders : AuthenticatedRequest
    {
        public Orders(ExchangeBase exchange)
            : base(exchange)
        {
            RequestUri = "/v1/orders";
        }
    }
}
