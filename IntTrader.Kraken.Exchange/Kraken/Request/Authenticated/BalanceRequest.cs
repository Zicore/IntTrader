using IntTrader.API.Base.Exchange.Base;

namespace IntTrader.API.Exchange.Kraken.Request.Authenticated
{
    public class BalanceRequest : AuthenticatedRequest
    {
        public BalanceRequest(ExchangeBase exchange)
            : base(exchange)
        {
            RequestUri = "/0/private/Balance";
        }

        //public BalanceResponse RequestSolid()
        //{
        //    var rs = Request();
        //    return new BalanceResponse { Items = Deserialize<List<BalanceEntry>>(rs) };
        //}
    }
}
