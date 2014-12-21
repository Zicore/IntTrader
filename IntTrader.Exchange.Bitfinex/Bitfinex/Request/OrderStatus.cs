using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Response;
using IntTrader.API.Exchange.Bitfinex.Request.Authenticated;
using IntTrader.API.Exchange.Bitfinex.Response;
using Newtonsoft.Json;

namespace IntTrader.API.Exchange.Bitfinex.Request
{
    public class OrderStatus : AuthenticatedRequest
    {
        public OrderStatus(ExchangeBase exchange)
            : base(exchange)
        {
            RequestUri = "/v1/order/status";
        }

        private int _orderId;

        [JsonProperty("order_id")]
        public int OrderId
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
