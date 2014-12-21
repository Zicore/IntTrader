using IntTrader.API.Exchange.Bitfinex.Data;

namespace IntTrader.API.Exchange.Bitfinex.Request
{
    public class Today : BitfinexRequestBase
    {
        public Today(BitfinexSymbol symbol)
        {
            RequestUri = "/v1/today/" + symbol.Name;
        }
    }
}
