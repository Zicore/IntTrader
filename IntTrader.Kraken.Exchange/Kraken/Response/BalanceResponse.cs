using System;
using System.Collections.Generic;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Transform;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IntTrader.API.Exchange.Kraken.Response
{
    public class BalanceResponse : KrakenResponse, IBalance
    {
        List<BalanceResponseEntry> _balanceEntries = new List<BalanceResponseEntry>();

        [JsonConverter(typeof(BalanceEntryConverter))]
        [JsonProperty("result")]
        public List<BalanceResponseEntry> BalanceEntries
        {
            get { return _balanceEntries; }
            set { _balanceEntries = value; }
        }

        public BalanceModel Transform(ExchangeBase exchange)
        {
            var m = new BalanceModel();
            foreach (var b in BalanceEntries)
            {
                m.Items.Add(new BalanceEntryModel
                    {
                        CurrencyKey = b.Currency,
                        Amount = b.Value,
                        Available = b.Value,
                        Currency = exchange.PairManager.GetCurrency(b.Currency)
                    });
            }
            return m;
        }
    }

    public class BalanceResponseEntry
    {
        private String _currency = "";
        private decimal _value;

        public string Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

        [JsonProperty("value")]
        public decimal Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    public class BalanceEntryConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(List<BalanceResponseEntry>));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var entries = new List<BalanceResponseEntry>();
            JObject jObject = JObject.Load(reader);

            foreach (var keyValue in jObject)
            {
                var entry = new BalanceResponseEntry();

                entry.Currency = keyValue.Key;
                entry.Value = keyValue.Value.ToObject<decimal>();
                entries.Add(entry);
            }
            return entries;
        }
    }
}
