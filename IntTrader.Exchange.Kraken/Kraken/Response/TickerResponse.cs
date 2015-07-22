using System;
using System.Linq;
using System.Collections.Generic;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Transform;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IntTrader.API.Exchange.Kraken.Response
{
    public class TickerResponse : KrakenResponse, ITicker
    {
        //    <pair_name> = pair name
        //a = ask array(<price>, <lot volume>),
        //b = bid array(<price>, <lot volume>),
        //c = last trade closed array(<price>, <lot volume>),
        //v = volume array(<today>, <last 24 hours>),
        //p = volume weighted average price array(<today>, <last 24 hours>),
        //t = number of trades array(<today>, <last 24 hours>),
        //l = low array(<today>, <last 24 hours>),
        //h = high array(<today>, <last 24 hours>),
        //o = today's opening price

        //[JsonConverter(typeof(TickerResponseConverter))]
        [JsonProperty(PropertyName = "result")]
        public Dictionary<String, TickerResponseResult> TickerResponseResult { get; set; }

        public TickerModel Transform()
        {
            if (TickerResponseResult == null)
            {
                return new TickerModel() { ResponseState = ResponseState.Error };
            }
            var ticker = TickerResponseResult.Values.First();


            var rs = new TickerModel
                {
                    DateTime = DateTime.Now,
                    Ask = ticker.Ask[0],
                    AskVolume = ticker.Ask[1],
                    Bid = ticker.Bid[0],
                    BidVolume = ticker.Bid[1],
                    LastPrice = ticker.LastTradeClosed[0],
                    LastPriceVolume = ticker.LastTradeClosed[1],
                    VolumeToday = ticker.Volume[0],
                    Volume24Hour = ticker.Volume[1],
                    VolumeAverageToday = ticker.VolumeWeightedAveragePrice[0],
                    VolumeAverage24Hour = ticker.VolumeWeightedAveragePrice[1],
                    LowToday = ticker.Low[0],
                    Low24Hour = ticker.Low[1],
                    HighToday = ticker.High[0],
                    High24Hour = ticker.High[1],
                    NumberOfTradesToday = ticker.NumberOfTrades[0],
                    NumberOfTrades24Hour = ticker.NumberOfTrades[1],
                    OpeningPriceToday = ticker.OpeningPriceToday
                };
            return rs;
        }
        //public TickerModel Transform()
        //{
        //    return new TickerModel
        //        {
        //            //Ask = TickerResponseResult.
        //        };
        //}
    }

    public class TickerResponseConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(TickerResponseResult));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Object)
            {
                return token.ToObject<TickerResponseResult>();
            }
            return null;
        }
    }

    public class TickerResponseResult
    {
        List<Decimal> _ask = new List<decimal>();
        List<Decimal> _bid = new List<decimal>();
        List<Decimal> _lastTradeClosed = new List<decimal>();
        List<Decimal> _volume = new List<decimal>();
        List<Decimal> _volumeWeightedAveragePrice = new List<decimal>();
        List<Decimal> _numberOfTrades = new List<decimal>();
        List<Decimal> _low = new List<decimal>();
        List<Decimal> _high = new List<decimal>();

        [JsonProperty("a")]
        public List<decimal> Ask
        {
            get { return _ask; }
            set { _ask = value; }
        }

        [JsonProperty("b")]
        public List<decimal> Bid
        {
            get { return _bid; }
            set { _bid = value; }
        }

        [JsonProperty("c")]
        public List<decimal> LastTradeClosed
        {
            get { return _lastTradeClosed; }
            set { _lastTradeClosed = value; }
        }

        [JsonProperty("v")]
        public List<decimal> Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        [JsonProperty("p")]
        public List<decimal> VolumeWeightedAveragePrice
        {
            get { return _volumeWeightedAveragePrice; }
            set { _volumeWeightedAveragePrice = value; }
        }

        [JsonProperty("t")]
        public List<decimal> NumberOfTrades
        {
            get { return _numberOfTrades; }
            set { _numberOfTrades = value; }
        }

        [JsonProperty("l")]
        public List<decimal> Low
        {
            get { return _low; }
            set { _low = value; }
        }

        [JsonProperty("h")]
        public List<decimal> High
        {
            get { return _high; }
            set { _high = value; }
        }

        [JsonProperty("o")]
        public decimal OpeningPriceToday { get; set; }
    }
}
