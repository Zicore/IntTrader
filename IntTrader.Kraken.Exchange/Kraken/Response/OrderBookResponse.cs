using System.Collections.Generic;
using System.Linq;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Transform;
using Newtonsoft.Json;

namespace IntTrader.API.Exchange.Kraken.Response
{
    public class OrderBookResponse : KrakenResponse, IOrderBook
    {
        Dictionary<string, OrderBookResponseResult> _orderBookResponseResult = new Dictionary<string, OrderBookResponseResult>();

        [JsonProperty("result")]
        public Dictionary<string, OrderBookResponseResult> OrderBooks
        {
            get { return _orderBookResponseResult; }
            set { _orderBookResponseResult = value; }
        }

        public OrderBookModel Transform()
        {
            var firstOrderBook = OrderBooks.Values.First();
            var asks = new List<OrderBookEntryModel>();
            var bids = new List<OrderBookEntryModel>();

            firstOrderBook.Asks.ForEach(x => asks.Add(x.Transform()));
            firstOrderBook.Bids.ForEach(x => bids.Add(x.Transform()));

            var result = new OrderBookModel
            {
                Asks = new List<OrderBookEntryModel>(asks),
                Bids = new List<OrderBookEntryModel>(bids)
            };
            return result;
        }

        public class OrderBookResponseResult
        {
            private List<OrderBookEntry> _bids;
            private List<OrderBookEntry> _asks;

            [JsonProperty("bids")]
            public List<OrderBookEntry> Bids
            {
                get { return _bids; }
                set { _bids = value; }
            }

            [JsonProperty("asks")]
            public List<OrderBookEntry> Asks
            {
                get { return _asks; }
                set { _asks = value; }
            }
        }
    }
}
