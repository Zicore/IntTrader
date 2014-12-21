using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace IntTrader.API.Exchange.Bitfinex.Data
{
    public class Symbols
    {
        public Symbols()
        {

        }

        private List<BitfinexSymbol> _items = new List<BitfinexSymbol>();

        public List<BitfinexSymbol> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public void Parse(String json)
        {
            JToken token = JToken.Parse(json);
            foreach (var t in token)
            {
                var s = new BitfinexSymbol
                {
                    Name = t.ToObject<string>()
                };
                Items.Add(s);
            }
        }
    }
}
