using System;
using System.Collections.Generic;
using IntTrader.API.Base.Exchange.Orders;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Transform;
using IntTrader.API.Converter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IntTrader.API.Exchange.Kraken.Response
{
    public class TradesResponse : KrakenResponse, ITrades
    {
        public TradesResponse()
        {

        }

        List<TradeEntry> _items = new List<TradeEntry>();

        [JsonProperty("result")]
        [JsonConverter(typeof(TradeResponseConverter))]
        public List<TradeEntry> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public TradesModel Transform()
        {
            var model = new TradesModel();

            foreach (var tradeEntry in Items)
            {
                model.Items.Add(tradeEntry.Transform());
            }
            model.Items.Reverse();
            return model;
        }
    }

    public class TradeEntry
    {
        private decimal _price;
        private decimal _amount;
        private double _timestamp;
        private String _side;
        private String _type;
        private String _misc;

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public double Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        public string Side
        {
            get { return _side; }
            set { _side = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Misc
        {
            get { return _misc; }
            set { _misc = value; }
        }

        public TradesEntryModel Transform()
        {
            var model = new TradesEntryModel
                {
                    Price = DecimalConverter.Normalize(Price),
                    Amount = DecimalConverter.Normalize(Amount),
                    Timestamp = DateTimeConverter.ConvertTimestamp(Timestamp).ToLocalTime(),
                    Info = Misc,
                    Side = StringToOrderSide(Side),
                    Type = Type
                };

            return model;
        }

        private static OrderSide StringToOrderSide(String orderSide)
        {
            switch (orderSide)
            {
                case "b":
                    return OrderSide.Buy;
                case "s":
                    return OrderSide.Sell;
                default:
                    return OrderSide.Buy;
            }
        }
    }

    public class TradeResponseConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(List<TradeEntry>));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);

            List<TradeEntry> entries = new List<TradeEntry>();
            if (token.Type == JTokenType.Object)
            {
                var innerToken = token.First.First;


                if (innerToken.Type == JTokenType.Array)
                {
                    foreach (var t in innerToken)
                    {
                        var entry = new TradeEntry
                        {
                            Price = t[0].ToObject<decimal>(),
                            Amount = t[1].ToObject<decimal>(),
                            Timestamp = t[2].ToObject<double>(),
                            Side = t[3].ToObject<String>(),
                            Type = t[4].ToObject<String>(),
                            Misc = t[5].ToObject<String>()
                        };
                        entries.Add(entry);
                    }

                }
            }

            return entries;
        }
    }
}
