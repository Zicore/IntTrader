using IntTrader.API.Base.Exchange.Base;

namespace IntTrader.API.Exchange.Kraken.Request.Authenticated
{
    public class OpenOrdersRequest : AuthenticatedRequest
    {
        public OpenOrdersRequest(ExchangeBase exchange)
            : base(exchange)
        {
            RequestUri = "/0/private/OpenOrders";
        }

    }
}
