using System;
using IntTrader.API.Base.Exchange.Base;
using Newtonsoft.Json;

namespace IntTrader.API.Exchange.Bitfinex.Request.Authenticated
{
    public enum Side
    {
        Buy = 1,
        Sell = 2
    }

    public enum Exchange
    {
        Bitfinex = 1,
        Bitstamp = 2,
        All
    }

    //public enum OrderType
    //{
    //    Market = 1,
    //    Limit = 2,
    //    Stop = 3,
    //    TrailingStop = 4,
    //    FillOrKill = 5,
    //    ExchangeMarket = 6,
    //    ExchangeLimit = 7,
    //    ExchangeStop = 8,
    //    ExchangeTrailingStop = 9,
    //    ExchangeFillOrKill = 10
    //}

    public class OrderNew : AuthenticatedRequest
    {
        public OrderNew(ExchangeBase exchange)
            : base(exchange)
        {
            RequestUri = "/v1/order/new";
        }

        private String _symbol;
        private String _amount;
        private String _price;
        private String _exchange;
        private string _side;
        private string _type;
        private bool _isHidden = false;

        [JsonProperty("symbol")]
        public string Symbol
        {
            get { return _symbol; }
            set { _symbol = value; }
        }

        [JsonProperty("amount")]
        public String Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        [JsonProperty("price")]
        public String Price
        {
            get { return _price; }
            set { _price = value; }
        }

        [JsonProperty("exchange")]
        public string Exchange
        {
            get { return _exchange; }
            set { _exchange = value; }
        }

        [JsonProperty("side")]
        public string Side
        {
            get { return _side; }
            set { _side = value; }
        }

        [JsonProperty("type")]
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        [JsonProperty("hidden")]
        public bool IsHidden
        {
            get { return _isHidden; }
            set { _isHidden = value; }
        }

        //  "market" / "limit" / "stop" / "trailing-stop" / "fill-or-kill" /
        //  "exchange market" / "exchange limit" / "exchange stop" / "exchange trailing-stop" / "exchange fill-or-kill".
        //public static string TypeToString(OrderType type)
        //{
        //    switch (type)
        //    {
        //        case OrderType.Market:
        //            return "market";
        //        case OrderType.Limit:
        //            return "limit";
        //        case OrderType.Stop:
        //            return "stop";
        //        case OrderType.TrailingStop:
        //            return "trailing-stop";
        //        case OrderType.FillOrKill:
        //            return "fill-or-kill";
        //        case OrderType.ExchangeMarket:
        //            return "exchange market";
        //        case OrderType.ExchangeLimit:
        //            return "exchange limit";
        //        case OrderType.ExchangeStop:
        //            return "exchange stop";
        //        case OrderType.ExchangeTrailingStop:
        //            return "exchange trailing-stop";
        //        case OrderType.ExchangeFillOrKill:
        //            return "exchange fill-or-kill";
        //        default:
        //            return "exchange limit";
        //    }
        //}

        //public static Exchange Convert(BittyTradeLib.Request.ExchangeType exchange)
        //{
        //    switch (exchange)
        //    {
        //        case BittyTradeLib.Request.ExchangeType.Bitfinex:
        //            return API.Request.Exchange.Bitfinex;
        //        case BittyTradeLib.Request.ExchangeType.Bitstamp:
        //            return API.Request.Exchange.Bitstamp;
        //        case BittyTradeLib.Request.ExchangeType.All:
        //            return API.Request.Exchange.All;
        //        default:
        //            return API.Request.Exchange.Bitfinex;
        //    }
        //}

        //public static OrderType Convert(BittyTradeLib.Request.OrderType orderType)
        //{
        //    switch (orderType)
        //    {
        //        case BittyTradeLib.Request.OrderType.ExchangeLimit:
        //            return OrderType.ExchangeLimit;
        //        case BittyTradeLib.Request.OrderType.ExchangeMarket:
        //            return OrderType.ExchangeMarket;
        //        case BittyTradeLib.Request.OrderType.ExchangeStop:
        //            return OrderType.ExchangeStop;
        //        case BittyTradeLib.Request.OrderType.ExchangeTrailingStop:
        //            return OrderType.ExchangeTrailingStop;
        //        case BittyTradeLib.Request.OrderType.ExchangeFillOrKill:
        //            return OrderType.ExchangeFillOrKill;

        //        case BittyTradeLib.Request.OrderType.Limit:
        //            return OrderType.Limit;
        //        case BittyTradeLib.Request.OrderType.Market:
        //            return OrderType.Market;
        //        case BittyTradeLib.Request.OrderType.Stop:
        //            return OrderType.Stop;
        //        case BittyTradeLib.Request.OrderType.TrailingStop:
        //            return OrderType.TrailingStop;
        //        case BittyTradeLib.Request.OrderType.FillOrKill:
        //            return OrderType.FillOrKill;
        //        default:
        //            return OrderType.ExchangeLimit;
        //    }
        //}

        //public static Side Convert(BittyTradeLib.Request.Side side)
        //{
        //    switch (side)
        //    {
        //        case BittyTradeLib.Request.Side.Buy:
        //            return API.Request.Side.Buy;
        //        case BittyTradeLib.Request.Side.Sell:
        //            return API.Request.Side.Sell;

        //        default:
        //            return API.Request.Side.Buy;
        //    }
        //}
    }
}
