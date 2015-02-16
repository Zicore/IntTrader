using System;
using IntTrader.API.Currency;
using Newtonsoft.Json;

namespace IntTrader.API.Exchange.Bitfinex.Request
{
    public class TradesRequest : BitfinexRequestBase
    {
        public TradesRequest(PairBase pair)
        {
            RequestUri = "/v1/trades/" + pair.Name;
        }

        private int _limit = 200;
        private String _timestamp;

        [JsonProperty("limit_trades")]
        public int Limit
        {
            get { return _limit; }
            set { _limit = value; }
        }

        [JsonProperty("timestamp")]
        public string Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }
    }
}
