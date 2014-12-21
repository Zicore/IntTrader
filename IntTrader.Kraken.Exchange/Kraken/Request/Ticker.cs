using IntTrader.API.Currency;

namespace IntTrader.API.Exchange.Kraken.Request
{
    public class Ticker : KrakenRequestBase
    {
        public Ticker(PairBase symbol)
        {
            RequestUri = "/0/public/Ticker?pair=" + symbol.Name;
        }
    }
}
