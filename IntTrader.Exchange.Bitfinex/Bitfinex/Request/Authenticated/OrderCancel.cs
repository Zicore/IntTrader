using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Response;
using IntTrader.API.Exchange.Bitfinex.Response;
using Newtonsoft.Json;

namespace IntTrader.API.Exchange.Bitfinex.Request.Authenticated
{
    public class OrderCancel : AuthenticatedRequest
    {
        public OrderCancel(ExchangeBase exchange)
            : base(exchange)
        {
            RequestUri = "/v1/order/cancel";
        }

        private long _orderId;

        [JsonProperty("order_id")]
        public long OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        public OrderStatusResponse Deserialize(ResponseData responseData)
        {
            return Deserialize<OrderStatusResponse>(responseData);
        }
    }
}
