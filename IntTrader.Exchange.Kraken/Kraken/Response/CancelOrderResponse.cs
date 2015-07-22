using IntTrader.API.Base.Model;
using IntTrader.API.Base.Transform;
using Newtonsoft.Json;

namespace IntTrader.API.Exchange.Kraken.Response
{
    public class CancelOrderResponse : KrakenResponse, ICancelOrder
    {
        CancelOrderInfo _cancelOrderInfo = new CancelOrderInfo();

        [JsonProperty("result")]
        public CancelOrderInfo CancelOrderInfo
        {
            get { return _cancelOrderInfo; }
            set { _cancelOrderInfo = value; }
        }

        public CancelOrderModel Transform()
        {
            var m = new CancelOrderModel();
            m.OrdersCanceled = CancelOrderInfo.Count;
            m.OrdersPending = CancelOrderInfo.Pending;
            return m;
        }
    }

    public class CancelOrderInfo
    {
        private int _count;
        private int _pending;

        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        public int Pending
        {
            get { return _pending; }
            set { _pending = value; }
        }
    }
}
