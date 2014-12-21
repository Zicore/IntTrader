using System;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Transform;
using IntTrader.API.Converter;
using Newtonsoft.Json;

namespace IntTrader.API.Exchange.Bitfinex.Response
{
    public class TickerResponse : BitfinexResponse, ITicker
    {
        public TickerResponse()
        {

        }
        //mid (price): (bid + ask) / 2
        //bid (price): Innermost bid.
        //ask (price): Innermost ask.
        //last_price (price) The price at which the last order executed.
        //timestamp (time) The timestamp at which this information was valid.

        private decimal _mid;
        private decimal _bid;
        private decimal _ask;
        private decimal _lastPrice;
        private String _timestamp;

        [JsonProperty("mid")]
        public decimal Mid
        {
            get { return _mid; }
            set { _mid = value; }
        }

        [JsonProperty("bid")]
        public decimal Bid
        {
            get { return _bid; }
            set { _bid = value; }
        }

        [JsonProperty("ask")]
        public decimal Ask
        {
            get { return _ask; }
            set { _ask = value; }
        }

        [JsonProperty("last_price")]
        public decimal LastPrice
        {
            get { return _lastPrice; }
            set { _lastPrice = value; }
        }

        [JsonProperty("timestamp")]
        public string Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        public TickerModel Transform()
        {
            return new TickerModel
                {
                    Ask = Ask,
                    Bid = Bid,
                    DateTime = DateTimeConverter.ConvertTimestamp(Timestamp),
                    LastPrice = LastPrice,
                    Mid = Mid
                };
        }
    }
}
