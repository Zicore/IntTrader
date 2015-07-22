using System;
using System.Globalization;
using IntTrader.API.Base.Exchange.Base;
using Newtonsoft.Json;

namespace IntTrader.API.Exchange.Kraken.Request.Authenticated
{
    public class CreateOrderRequest : AuthenticatedRequest
    {
        public CreateOrderRequest(ExchangeBase exchange)
            : base(exchange)
        {
            RequestUri = "/0/private/AddOrder";
        }

        private String _pair;
        private String _type; //buy/sell
        private String _orderType;
        //ordertype = order type:
        //market
        //limit (price = limit price)
        //stop-loss (price = stop loss price)
        //take-profit (price = take profit price)
        //stop-loss-profit (price = stop loss price, price2 = take profit price)
        //stop-loss-profit-limit (price = stop loss price, price2 = take profit price)
        //stop-loss-limit (price = stop loss trigger price, price2 = triggered limit price)
        //take-profit-limit (price = take profit trigger price, price2 = triggered limit price)
        //trailing-stop (price = trailing stop offset)
        //trailing-stop-limit (price = trailing stop offset, price2 = triggered limit offset)
        //stop-loss-and-limit (price = stop loss price, price2 = limit price)
        private String _price;
        private String _price2;//secondary price (optional.  dependent upon ordertype)
        private decimal _volume;
        private String _leverage = "none";
        private String _oflags = null;
        private String _starttm = "0";
        private String _expiretm = "0";
        private String _userref;
        private bool _validate;

        [JsonProperty("pair")]
        public string Pair
        {
            get { return _pair; }
            set { _pair = value; }
        }

        [JsonProperty("type")]
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        [JsonProperty("ordertype")]
        public string OrderType
        {
            get { return _orderType; }
            set { _orderType = value; }
        }

        [JsonProperty("price")]
        public String Price
        {
            get { return _price; }
            set { _price = value; }
        }

        [JsonProperty("price2")]
        public String Price2
        {
            get { return _price2; }
            set { _price2 = value; }
        }

        [JsonProperty("volume")]
        public String VolumenString
        {
            get { return Volume.ToString(CultureInfo.InvariantCulture); }
            set { Volume = decimal.Parse(value, CultureInfo.InvariantCulture); }
        }

        [JsonIgnore]
        public decimal Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        [JsonProperty("leverage")]
        public string Leverage
        {
            get { return _leverage; }
            set { _leverage = value; }
        }

        [JsonProperty("oflags", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Oflags
        {
            get { return _oflags; }
            set { _oflags = value; }
        }

        [JsonProperty("starttm")]
        public string Starttm
        {
            get { return _starttm; }
            set { _starttm = value; }
        }

        [JsonProperty("expiretm")]
        public string Expiretm
        {
            get { return _expiretm; }
            set { _expiretm = value; }
        }

        [JsonProperty("userref")]
        public string Userref
        {
            get { return _userref; }
            set { _userref = value; }
        }

        [JsonProperty("validate", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Validate
        {
            get { return _validate; }
            set { _validate = value; }
        }

    }
}
