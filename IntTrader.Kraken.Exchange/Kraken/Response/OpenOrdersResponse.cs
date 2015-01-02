using System;
using System.Collections.Generic;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Transform;
using IntTrader.API.Converter;
using IntTrader.API.Currency;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IntTrader.API.Exchange.Kraken.Response
{
    public class OpenOrdersResponse : KrakenResponse, IOrders
    {
        private List<OpenOrderEntry> _items = new List<OpenOrderEntry>();

        [JsonConverter(typeof(OpenOrdersConverter))]
        [JsonProperty("result")]
        public List<OpenOrderEntry> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public OpenOrdersModel Transform()
        {
            var m = new OpenOrdersModel();

            foreach (var item in Items)
            {
                m.Orders.Add(item.Transform());
            }

            return m;
        }
    }

    public class OpenOrderEntry
    {
        public OpenOrderEntry()
        {

        }

        private String _txid; //array of order info in open array with txid as the key
        private String _refid;//Referral order transaction id that created this order
        private String _userref;//user reference id
        private String _status; // Pending,open,closed,canceled,expired
        private String _opentm;//unix timestamp of when order was placed
        private String _starttm;//unix timestamp of order start time (or 0 if not set)
        private String _expiretm;//unix timestamp of order end time (or 0 if not set)
        private OpenOrderEntryDescription _description = new OpenOrderEntryDescription();//order description info
        private decimal _vol;// volume of order (base currency unless viqc set in oflags)
        private decimal _volExec;//volume executed (base currency unless viqc set in oflags)
        private decimal _cost; //total cost (quote currency unless unless viqc set in oflags)
        private decimal _fee;//total fee (quote currency)
        private decimal _price;//average price (quote currency unless viqc set in oflags)
        private decimal _stopprice; //stop price (quote currency, for trailing stops)
        private decimal _limitprice;//triggered limit price (quote currency, when limit based order type triggered)
        private List<string> _misc = new List<string>();
        // comma delimited list of miscellaneous info
        //      stopped = triggered by stop price
        //      touched = triggered by touch price
        //      liquidated = liquidation
        //      partial = partial fill

        List<string> _oflags = new List<string>(); // comma delimited list of order flags
        //      viqc = volume in quote currency
        //      plbc = prefer profit/loss in base currency
        //      nompp = no market price protection

        List<string> _trades = new List<string>(); //  array of trade ids related to order (if trades info requested and data available)

        // ------------------------------

        [JsonProperty("txid")]
        public string Txid
        {
            get { return _txid; }
            set { _txid = value; }
        }

        [JsonProperty("refid")]
        public string Refid
        {
            get { return _refid; }
            set { _refid = value; }
        }

        [JsonProperty("userref")]
        public string Userref
        {
            get { return _userref; }
            set { _userref = value; }
        }

        [JsonProperty("status")]
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [JsonProperty("opentm")]
        public string Opentm
        {
            get { return _opentm; }
            set { _opentm = value; }
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

        [JsonProperty("descr")]
        public OpenOrderEntryDescription Description
        {
            get { return _description; }
            set { _description = value; }
        }

        [JsonProperty("vol")]
        public decimal Vol
        {
            get { return _vol; }
            set { _vol = value; }
        }

        [JsonProperty("volexec")]
        public decimal VolExec
        {
            get { return _volExec; }
            set { _volExec = value; }
        }

        [JsonProperty("cost")]
        public decimal Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }

        [JsonProperty("fee")]
        public decimal Fee
        {
            get { return _fee; }
            set { _fee = value; }
        }

        [JsonProperty("price")]
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        [JsonProperty("stopprice")]
        public decimal Stopprice
        {
            get { return _stopprice; }
            set { _stopprice = value; }
        }

        [JsonProperty("limitprice")]
        public decimal Limitprice
        {
            get { return _limitprice; }
            set { _limitprice = value; }
        }

        [JsonProperty("misc")]
        public List<string> Misc
        {
            get { return _misc; }
            set { _misc = value; }
        }

        [JsonProperty("oflags")]
        public List<string> Oflags
        {
            get { return _oflags; }
            set { _oflags = value; }
        }

        [JsonProperty("trades")]
        public List<string> Trades
        {
            get { return _trades; }
            set { _trades = value; }
        }

        public OpenOrderEntryModel Transform()
        {
            var m = new OpenOrderEntryModel
                {
                    OrderId = Txid,
                    OrderStatus = Status,
                    AverageExecutionPrice = Price,
                    DateTime = DateTimeConverter.ConvertTimestamp(Opentm),
                    OriginalAmount = Vol,
                    ExecutedAmount = VolExec,
                    Price = Description.Price,
                    RemainingAmount = Vol,
                    Symbol = Description.Pair,
                    Type = Description.Type,
                };

            return m;
        }
    }

    public class OpenOrderEntryDescription
    {
        private String _pair;//asset pair
        private String _type;//type of order (buy/sell)
        private String _ordertype;//order type (See Add standard order) https://www.kraken.com/help/api#add-standard-order
        private decimal _price;//primary price
        private decimal _price2;//secondary price
        private String _leverage;//amount of leverage
        private long _position;//position tx id to close (if order is positional)
        private String _order;//order description
        private String _close;//conditional close order description (if conditional close set)

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
        public string Ordertype
        {
            get { return _ordertype; }
            set { _ordertype = value; }
        }

        [JsonProperty("price")]
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        [JsonProperty("price2")]
        public decimal Price2
        {
            get { return _price2; }
            set { _price2 = value; }
        }

        [JsonProperty("leverage")]
        public String Leverage
        {
            get { return _leverage; }
            set { _leverage = value; }
        }

        [JsonProperty("position")]
        public long Position
        {
            get { return _position; }
            set { _position = value; }
        }

        [JsonProperty("order")]
        public string Order
        {
            get { return _order; }
            set { _order = value; }
        }

        [JsonProperty("close")]
        public string Close
        {
            get { return _close; }
            set { _close = value; }
        }
    }

    public class OpenOrdersConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(List<OpenOrderEntry>));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var entries = new List<OpenOrderEntry>();
            JObject jobject = JObject.Load(reader);
            var obj = jobject["open"] as JObject;
            if (obj != null)
            {
                foreach (var k in obj)
                {
                    var entry = k.Value.ToObject<OpenOrderEntry>();
                    entry.Txid = k.Key;
                    entries.Add(entry);
                }
            }

            return entries;
        }
    }
}
