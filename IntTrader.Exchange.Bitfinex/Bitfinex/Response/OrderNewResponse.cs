using System;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Transform;
using IntTrader.API.Converter;
using Newtonsoft.Json;

namespace IntTrader.API.Exchange.Bitfinex.Response
{
    public class OrderNewResponse : OrderStatusResponse, IOrderNew
    {
        private String _orderId;

        [JsonProperty("order_id")]
        public String OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        public CreateOrderModel Transform()
        {
            return new CreateOrderModel
            {
                OrderId = OrderId,
                AverageExecutionPrice = AverageExecutionPrice,
                DateTime = DateTimeConverter.ConvertTimestamp(Timestamp),
                Exchange = ExchangeString,
                ExecutedAmount = ExecutedAmount,
                IsCancelled = IsCancelled,
                IsLive = IsLive,
                OriginalAmount = OriginalAmount,
                Price = Price,
                RemainingAmount = RemainingAmount,
                Symbol = Symbol,
                Type = Type,
                WasForced = WasForced,
                OrderStatus = ConvertStatus(this),
            };
        }

        // To support the real status, who  knows if "Live and Canceled" is even possible
        private String ConvertStatus(OrderStatusResponse order)
        {
            String status = "";
            if (order.IsLive && !order.IsCancelled)
                status = "Live";

            if (order.IsLive && order.IsCancelled)
                status = "Live | Canceled";

            if (!order.IsLive && order.IsCancelled)
                status = "Canceled";

            if (order.WasForced)
                status += " | Forced";
            return status;
        }
    }
}
