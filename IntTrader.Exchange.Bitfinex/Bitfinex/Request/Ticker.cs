using IntTrader.API.Currency;

namespace IntTrader.API.Exchange.Bitfinex.Request
{
    public class Ticker : BitfinexRequestBase
    {
        public Ticker(PairBase symbol)
        {
            RequestUri = "/v1/ticker/" + symbol.Name;
        }
    }
}
