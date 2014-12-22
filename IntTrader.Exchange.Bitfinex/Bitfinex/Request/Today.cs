using IntTrader.API.Currency;

namespace IntTrader.API.Exchange.Bitfinex.Request
{
    public class Today : BitfinexRequestBase
    {
        public Today(PairBase symbol)
        {
            RequestUri = "/v1/today/" + symbol.Name;
        }
    }
}
