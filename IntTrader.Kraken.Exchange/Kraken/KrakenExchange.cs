using System;
using IntTrader.API.Base.Exchange;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Exchange.Orders;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Request;
using IntTrader.API.Base.Settings;
using IntTrader.API.Converter;
using IntTrader.API.Currency;
using IntTrader.API.Event;
using IntTrader.API.Exchange.Kraken.Request;
using IntTrader.API.Exchange.Kraken.Request.Authenticated;
using IntTrader.API.Exchange.Kraken.Response;

namespace IntTrader.API.Exchange.Kraken
{
    public sealed class KrakenExchange : ExchangeBase
    {
        public KrakenExchange(ExchangeManager exchangeManager)
            : base(exchangeManager)
        {
            Name = "Kraken";
            ExchangeAPI = new ExchangeAPI
            {
                Name = Name
            };

            PublicFunctions.Add(APIFunction.RequestOrderBook);
            PublicFunctions.Add(APIFunction.RequestTicker);
            PublicFunctions.Add(APIFunction.RequestTrades);

            AvailableFunctions.Add(APIFunction.RequestTicker);
            AvailableFunctions.Add(APIFunction.RequestOrderBook);
            AvailableFunctions.Add(APIFunction.RequestBalances);
            AvailableFunctions.Add(APIFunction.RequestOpenOrders);
            AvailableFunctions.Add(APIFunction.RequestNewOrder);

            OrderSides.Add(OrderSide.Buy);
            OrderSides.Add(OrderSide.Sell);

            DefaultOrderType = new OrderType("limit", "limit");
            OrderTypes.Add(new OrderType("market", "market"));
            OrderTypes.Add(DefaultOrderType); // limit
            OrderTypes.Add(new OrderType("stop-loss", "stop Loss"));
            OrderTypes.Add(new OrderType("take-profit", "take profit"));
            OrderTypes.Add(new OrderType("stop-loss-profit", "stop-loss-profit"));
            OrderTypes.Add(new OrderType("stop-loss-profit-limit", "stop-loss-profit-limit"));
            OrderTypes.Add(new OrderType("stop-loss-limit", "stop-loss-limit"));
            OrderTypes.Add(new OrderType("trailing-stop", "trailing-stop"));
            OrderTypes.Add(new OrderType("trailing-stop-limit", "trailing-stop-limit"));
            OrderTypes.Add(new OrderType("stop-loss-and-limit", "stop-loss-and-limit"));

            PairManager.AddSupportedPair(PairBase.BTCEUR, "XBTEUR");
            PairManager.AddSupportedPair(PairBase.LTCEUR, "LTCEUR");
            PairManager.AddSupportedPair(PairBase.BTCUSD, "XBTUSD");
            PairManager.AddSupportedPair(PairBase.LTCUSD, "LTCUSD");
            PairManager.AddSupportedPair(PairBase.BTCLTC, "XBTLTC");
            PairManager.AddSupportedPair(PairBase.BTCXRP, "XBTXRP");
        }

        public override TickerModel RequestTicker(PairBase pair)
        {
            var s = ConvertPair(pair);
            return new Ticker(s).Request<TickerResponse>().Transform();
        }

        public override OrderBookModel RequestOrderBook(PairBase pair)
        {
            var s = ConvertPair(pair);
            var book = new OrderBook(s);
            return book.Request<OrderBookResponse>().Transform();
        }

        public override BalanceModel RequestBalances()
        {
            var b = new BalanceRequest(this);
            var model = b.Request<BalanceResponse>().Transform();
            return model;
        }

        public override OpenOrdersModel RequestOpenOrders()
        {
            var o = new OpenOrdersRequest(this);
            var model = o.Request<OpenOrdersResponse>().Transform();
            return model;
        }

        public override OpenOrderEntryModel RequestNewOrder(CreateOrderRequestBase orderNewRequest)
        {
            var order = new CreateOrderRequest(this)
            {
                Volume = orderNewRequest.Amount,
                Price = DecimalConverter.Convert(orderNewRequest.Price),
                OrderType = orderNewRequest.OrderType.Value,
                Type = orderNewRequest.Side.Value,
                Pair = orderNewRequest.Pair,
                Validate = orderNewRequest.Validate,
            };

            var response = order.Request<CreateOrderResponse>();

            return OnCreateOrder(response, response);
        }


        public override CancelOrderModel CancelOrder(CancelOrderRequestBase orderCancelRequest)
        {
            var orderCancel = new CancelOrderRequest(this)
            {
                Txid = orderCancelRequest.OrderId
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
