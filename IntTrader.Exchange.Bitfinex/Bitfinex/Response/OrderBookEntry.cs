using System;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Transform;
using IntTrader.API.Converter;
using Newtonsoft.Json;

namespace IntTrader.API.Exchange.Bitfinex.Response
{
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
}
