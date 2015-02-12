using System;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Exchange.Orders;

namespace IntTrader.API.Base.Request
{
    public class CreateOrderRequestBase
    {
        private String _pair;
        private decimal _amount;
        private decimal _price;
        private decimal _price2;
        private ExchangeType _exchangeType;
        private OrderSide _side;
        private OrderType _ordertype;
        private bool _isHidden = false;
        private bool _validate = false;

        public string Pair
        {
            get { return _pair; }
            set { _pair = value; }
        }

        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public decimal Price2
        {
            get { return _price2; }
            set { _price2 = value; }
        }

        public ExchangeType ExchangeType
        {
            get { return _exchangeType; }
            set { _exchangeType = value; }
        }

        public OrderSide Side
        {
            get { return _side; }
            set { _side = value; }
        }

        public OrderType OrderType
        {
            get { return _ordertype; }
            set { _ordertype = value; }
        }

        public bool IsHidden
        {
            get { return _isHidden; }
            set { _isHidden = value; }
        }

        public bool Validate
        {
            get { return _validate; }
            set { _validate = value; }
        }
    }
}