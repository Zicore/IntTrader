
using System;

namespace IntTrader.API.Base.Request
{
    public class CancelOrderRequestBase
    {
        private String _orderId;
        public String OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }
    }
}
