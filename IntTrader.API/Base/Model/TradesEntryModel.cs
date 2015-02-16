using System;
using IntTrader.API.Base.Exchange.Orders;

namespace IntTrader.API.Base.Model
{
    public class TradesEntryModel
    {
        private DateTime _timestamp;
        private String _orderId;
        private decimal _price;
        private decimal _amount;
        private OrderSide _side;
        private String _type;
        private String _info;

        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        public String TimestampString
        {
            get { return Timestamp.ToString("HH:mm:ss"); }
        }

        public string OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public OrderSide Side
        {
            get { return _side; }
            set { _side = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Info
        {
            get { return _info; }
            set { _info = value; }
        }
    }
}
