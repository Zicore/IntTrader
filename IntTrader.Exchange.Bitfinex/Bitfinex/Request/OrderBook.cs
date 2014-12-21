using IntTrader.API.Currency;
using Newtonsoft.Json;

namespace IntTrader.API.Exchange.Bitfinex.Request
{
    public class OrderBook : BitfinexRequestBase
    {
        public OrderBook(PairBase symbol)
        {
            RequestUri = "/v1/book/" + symbol.Name;
        }

        private int _limitBids = 500;
        private int _limitAsks = 500;

        [JsonProperty("limit_bids")]
        public int LimitBids
        {
            get { return _limitBids; }
            set { _limitBids = value; }
        }

        [JsonProperty("limit_asks")]
        public int LimitAsks
        {
            get { return _limitAsks; }
            set { _limitAsks = value; }
        }
    }
}
