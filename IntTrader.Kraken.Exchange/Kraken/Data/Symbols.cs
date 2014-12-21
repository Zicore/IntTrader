using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace IntTrader.API.Exchange.Kraken.Data
{
    public class Symbols
    {
        public Symbols()
        {

        }

        private List<KrakenPair> _items = new List<KrakenPair>();

        public List<KrakenPair> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public void Parse(String json)
        {
            JToken token = JToken.Parse(json);
            foreach (var t in token)
            {
                var s = new KrakenPair
                {
                    Name = t.ToObject<string>()
                };
                Items.Add(s);
            }
        }
    }
}
