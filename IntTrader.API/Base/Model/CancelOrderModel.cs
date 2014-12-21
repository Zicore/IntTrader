using System;

namespace IntTrader.API.Base.Model
{
    public class CancelOrderModel : ResponseModelBase
    {
        private String _orderId = "";

        private int _ordersCanceled = 0;
        private int _ordersPending = 0;

        public int OrdersCanceled
        {
            get { return _ordersCanceled; }
            set { _ordersCanceled = value; }
        }

        public int OrdersPending
        {
            get { return _ordersPending; }
            set { _ordersPending = value; }
        }

        public string OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        OpenOrderEntryModel _openOrder = new OpenOrderEntryModel();

        public OpenOrderEntryModel OpenOrder
        {
            get { return _openOrder; }
            set { _openOrder = value; }
        }
    }
}
