using System;
using IntTrader.API.Base.Exchange;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Exchange.Orders;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Request;
using IntTrader.API.Base.Response;
using IntTrader.API.Base.Settings;
using IntTrader.API.Base.Transform;
using IntTrader.API.Converter;
using IntTrader.API.Currency;
using IntTrader.API.Event;
using IntTrader.API.Exchange.Bitfinex.Request;
using IntTrader.API.Exchange.Bitfinex.Request.Authenticated;
using IntTrader.API.Exchange.Bitfinex.Response;

namespace IntTrader.API.Exchange.Bitfinex
{
    public sealed class BitfinexExchange : ExchangeBase
    {
        public BitfinexExchange(ExchangeManager exchangeManager)
            : base(exchangeManager)
        {
            Name = "Bitfinex";
            ExchangeAPI = new ExchangeAPI
                {
                    Name = Name
                };

            PublicFunctions.Add(APIFunction.RequestOrderBook);
            PublicFunctions.Add(APIFunction.RequestTicker);
            PublicFunctions.Add(APIFunction.RequestTrades);

            AvailableFunctions.Add(APIFunction.RequestTicker);
            AvailableFunctions.Add(APIFunction.RequestBalances);
            AvailableFunctions.Add(APIFunction.RequestTrades);
            AvailableFunctions.Add(APIFunction.RequestOrderBook);
            AvailableFunctions.Add(APIFunction.RequestOpenOrders);
            AvailableFunctions.Add(APIFunction.RequestNewOrder);
            AvailableFunctions.Add(APIFunction.CancelOrder);

            OrderSides.Add(OrderSide.Buy.Name, OrderSide.Buy);
            OrderSides.Add(OrderSide.Sell.Name, OrderSide.Sell);

            DefaultOrderType = OrderExchangeLimit;

            AddOrderType(OrderExchangeMarket);
            AddOrderType(OrderExchangeLimit);
            AddOrderType(OrderExchangeStop);
            AddOrderType(OrderExchangeTrailingStop);
            AddOrderType(OrderExchangeFillOrKill);

            //OrderTypes.Add(new OrderType("market", "market"));
            //OrderTypes.Add(new OrderType("limit", "limit"));
            //OrderTypes.Add(new OrderType("stop", "stop"));
            //OrderTypes.Add(new OrderType("trailing-stop", "trailing-stop"));
            //OrderTypes.Add(new OrderType("fill-or-kill", "fill-or-kill"));

            PairManager.AddSupportedPair(PairBase.BTCUSD, "btcusd");
            PairManager.AddSupportedPair(PairBase.LTCUSD, "ltcusd");
            PairManager.AddSupportedPair(PairBase.LTCBTC, "ltcbtc");
            PairManager.AddSupportedPair(PairBase.DRKUSD, "drkusd");
            PairManager.AddSupportedPair(PairBase.DRKBTC, "drkbtc");
        }

        public static readonly ExchangeType ExchangeTypeBitfinex = new ExchangeType("bitfinex", "Bitfinex");
        public static readonly ExchangeType ExchangeTypeBitstamp = new ExchangeType("bitstamp", "Bitstamp");
        public static readonly ExchangeType ExchangeTypeAll = new ExchangeType("all", "All");

        public static readonly OrderType OrderExchangeMarket = new OrderType("exchange market", "exchange market");
        public static readonly OrderType OrderExchangeLimit = new OrderType("exchange limit", "exchange limit");
        public static readonly OrderType OrderExchangeStop = new OrderType("exchange stop", "exchange stop");
        public static readonly OrderType OrderExchangeTrailingStop = new OrderType("exchange trailing-stop", "exchange trailing-stop");
        public static readonly OrderType OrderExchangeFillOrKill = new OrderType("exchange fill-or-kill", "exchange fill-or-kill");

        public override ExchangeType DefaultExchangeType
        {
            get
            {
                return ExchangeTypeBitfinex;
            }
        }

        // --------------------------------------------------------------------

        public override OrderBookModel RequestOrderBook(PairBase symbol)
        {
            var s = ConvertPair(symbol);
            var book = new OrderBook(s);
            return book.Request<OrderBookResponse>().Transform();
        }

        public override TickerModel RequestTicker(PairBase pair)
        {
            var s = ConvertPair(pair);
            return new Ticker(s).Request<TickerResponse>().Transform();
        }

        public override OpenOrdersModel RequestOpenOrders()
        {
            return new Orders(this).Request<OrdersResponse>().Transform();
        }

        public override OpenOrderEntryModel RequestNewOrder(CreateOrderRequestBase orderNewRequest)
        {
            var order = new CreateOrderRequest(this)
                {
                    Amount = DecimalConverter.Convert(orderNewRequest.Amount),
                    Price = DecimalConverter.Convert(orderNewRequest.Price),
                    IsHidden = orderNewRequest.IsHidden,
                    Exchange = orderNewRequest.ExchangeType.Value,
                    Type = orderNewRequest.OrderType.Value,
                    Side = orderNewRequest.Side.Value,
                    Symbol = orderNewRequest.Pair
                };

            var response = order.Request<CreateOrderResponse>();

            return OnCreateOrder(response, response);
        }

        public override BalanceModel RequestBalances()
        {
            return new Balance(this).Request<BalanceResponse>().Transform();
        }

        public override CancelOrderModel CancelOrder(CancelOrderRequestBase orderCancelRequest)
        {
            var orderCancel = new OrderCancel(this)
                {
                    OrderId = long.Parse(orderCancelRequest.OrderId)
                };

            return orderCancel.Request<CancelOrderResponse>().Transform();
        }

        public override TradesModel RequestTrades(PairBase pair)
        {
            var request = new TradesRequest(pair);
            return request.Request<TradesResponse>().Transform();
        }
    }
}
