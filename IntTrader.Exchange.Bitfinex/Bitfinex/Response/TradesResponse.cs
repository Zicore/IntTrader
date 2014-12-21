using System;
using System.Collections.Generic;
using IntTrader.API.Base.Exchange.Orders;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Transform;
using IntTrader.API.Converter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IntTrader.API.Exchange.Bitfinex.Response
{
    [JsonConverter(typeof(TradeResponseConverter))]
    public class TradesResponse : BitfinexResponse, ITrades
    {
        private List<TradeEntry> _items = new List<TradeEntry>();

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

            return model;
        }
    }

    public class TradeEntry
    {
        //An array of dictionaries:
        //tid (integer)
        //timestamp (time)
        //price (price)
        //amount (decimal)
        //exchange (string)
        //type (string) "sell" or "buy" (can be "" if undetermined)

        private double _timestamp;
        private String _tid;
        private decimal _price;
        private decimal _amount;
        private String _exchange;
        private String _type;

        [JsonProperty("timestamp")]
        public double Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        [JsonProperty("tid")]
        public string Tid
        {
            get { return _tid; }
            set { _tid = value; }
        }

        [JsonProperty("price")]
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        [JsonProperty("amount")]
        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        [JsonProperty("exchange")]
        public string Exchange
        {
            get { return _exchange; }
            set { _exchange = value; }
        }

        [JsonProperty("type")]
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }


        public TradesEntryModel Transform()
        {
            var model = new TradesEntryModel
            {
                Timestamp = DateTimeConverter.ConvertTimestamp(Timestamp).ToLocalTime(),
                Amount = DecimalConverter.Normalize(Amount),
                Price = DecimalConverter.Normalize(Price),
                Info = Exchange,
                OrderId = Tid,
                Side = StringToOrderSide(Type)
            };

            return model;
        }

        private static OrderSide StringToOrderSide(String orderSide)
        {
            switch (orderSide)
            {
                case "buy":
                    return OrderSide.Buy;
                case "sell":
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
            return (objectType == typeof(TradesResponse));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            return new TradesResponse
                {
                    Items = token.ToObject<List<TradeEntry>>()
                };
        }
    }

}
