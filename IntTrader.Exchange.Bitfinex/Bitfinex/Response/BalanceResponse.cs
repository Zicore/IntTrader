using System;
using System.Collections.Generic;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Transform;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IntTrader.API.Exchange.Bitfinex.Response
{
    [JsonConverter(typeof(BalanceConverter))]
    public class BalanceResponse : BitfinexResponse, IBalance
    {
        private List<BalanceEntry> _items = new List<BalanceEntry>();

        public List<BalanceEntry> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public BalanceModel Transform()
        {
            var list = new List<BalanceEntryModel>();

            Items.ForEach(x => list.Add(x.Transform()));
            return new BalanceModel
            {
                Items = list
            };
        }
    }

    public class BalanceConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(BalanceResponse));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var json = JToken.Load(reader);
            BalanceResponse response = new BalanceResponse();
            response.Items = json.ToObject<List<BalanceEntry>>();
            return response;
        }
    }
}
