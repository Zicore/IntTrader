using IntTrader.API.Base.Model;
using IntTrader.API.Base.Transform;

namespace IntTrader.API.Exchange.Bitfinex.Response
{
    public class CancelOrderResponse : OrderResponseBase, ICancelOrder
    {


        public CancelOrderModel Transform()
        {
            var m = new CancelOrderModel();
            m.OrdersCanceled = 1;
            m.OrderId = this.Id;
            return m;
        }
    }
}
