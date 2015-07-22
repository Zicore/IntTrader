using System;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Transform;
using IntTrader.API.Converter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IntTrader.API.Exchange.Kraken.Response
{
    [JsonConverter(typeof(OrderBookEntryConverter))]
    public class OrderBookEntry : IOrderBookEntry
    {
        //              "price": "0.0278",
        //            "amount": "20.0",
        //            "timestamp": "1395065891.0"

        private decimal _price;
        private decimal _amount;
        private String _timestamp;

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

        [JsonProperty("timestamp")]
        public string Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        public OrderBookEntryModel Transform()
        {
            return new OrderBookEntryModel { Amount = Amount, Price = Price, DateTime = DateTimeConverter.ConvertTimestamp(Timestamp) };
        }

    }

    public class OrderBookEntryConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(OrderBookEntry));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);

            var entry = new OrderBookEntry();

            entry.Price = token[0].ToObject<decimal>();
            entry.Amount = token[1].ToObject<decimal>();
            entry.Timestamp = token[2].ToObject<String>();

            return entry;
        }
    }
}
