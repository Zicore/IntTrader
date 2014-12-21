using System;
using IntTrader.API.Currency;
using Newtonsoft.Json;

namespace IntTrader.API.Exchange.Kraken.Request
{
    public class TradesRequest : KrakenRequestBase
    {
        public TradesRequest(PairBase pair)
        {
            Pair = pair.Name;
            RequestUri = "/0/public/Trades";
        }

        //pair = asset pair to get trade data for
        //since = return trade data since given id (optional.  exclusive)

        private String _sinceOrderId;

        [JsonProperty("since")]
        public string SinceOrderId
        {
            get { return _sinceOrderId; }
            set { _sinceOrderId = value; }
        }

        private String _pair;

        [JsonProperty("pair")]
        public string Pair
        {
            get { return _pair; }
            set { _pair = value; }
        }
    }
}
