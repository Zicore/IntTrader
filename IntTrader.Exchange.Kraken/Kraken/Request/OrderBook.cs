using IntTrader.API.Currency;
using Newtonsoft.Json;

namespace IntTrader.API.Exchange.Kraken.Request
{
    public class OrderBook : KrakenRequestBase
    {
        public OrderBook(PairBase symbol)
        {
            RequestUri = "/0/public/Depth?pair=" + symbol.Name + "&count=" + Limit;
        }

        private int _limit = 500;

        [JsonProperty("count")]
        public int Limit
        {
            get { return _limit; }
            set { _limit = value; }
        }
    }
}
