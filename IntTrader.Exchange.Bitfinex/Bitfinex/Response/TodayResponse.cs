using Newtonsoft.Json;

namespace IntTrader.API.Exchange.Bitfinex.Response
{
    public class TodayResponse : BitfinexResponse
    {
        public TodayResponse()
        {

        }

        //low (price)
        //high (price)
        //volume (price)

        private decimal _low;
        private decimal _high;
        private decimal _volume;

        [JsonProperty("low")]
        public decimal Low
        {
            get { return _low; }
            set { _low = value; }
        }

        [JsonProperty("high")]
        public decimal High
        {
            get { return _high; }
            set { _high = value; }
        }

        [JsonProperty("volume")]
        public decimal Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }
    }
}
