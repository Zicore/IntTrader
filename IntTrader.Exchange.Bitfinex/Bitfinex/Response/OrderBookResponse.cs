using System.Collections.Generic;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Transform;
using Newtonsoft.Json;

namespace IntTrader.API.Exchange.Bitfinex.Response
{
    public class OrderBookResponse : BitfinexResponse, IOrderBook
    {
        public OrderBookResponse()
        {

        }

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

        public OrderBookModel Transform()
        {
            var asks = new List<OrderBookEntryModel>();
            var bids = new List<OrderBookEntryModel>();

            Asks.ForEach(x => asks.Add(x.Transform()));
            Bids.ForEach(x => bids.Add(x.Transform()));

            return new OrderBookModel
            {
                Asks = new List<OrderBookEntryModel>(asks),
                Bids = new List<OrderBookEntryModel>(bids)
            };
        }
    }
}
