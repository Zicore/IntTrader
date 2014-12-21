using System.Collections.Generic;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Exchange.Bitfinex.Response;

namespace IntTrader.API.Exchange.Bitfinex.Request.Authenticated
{
    public class Balance : AuthenticatedRequest
    {
        public Balance(ExchangeBase exchange)
            : base(exchange)
        {
            RequestUri = "/v1/balances";
        }

        //public BalanceResponse RequestSolid()
        //{
        //    var rs = Request();
        //    return new BalanceResponse { Items = Deserialize<List<BalanceEntry>>(rs) };
        //}
    }
}
