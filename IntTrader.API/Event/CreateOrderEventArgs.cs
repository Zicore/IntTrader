using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;

namespace IntTrader.API.Event
{
    public class CreateOrderEventArgs : EventArgs
    {
        private ExchangeBase _exchange;
        private CreateOrderModel _model;

        public CreateOrderEventArgs(ExchangeBase exchange, CreateOrderModel model)
        {
            _exchange = exchange;
            _model = model;
        }

        public ExchangeBase Exchange
        {
            get { return _exchange; }
            set { _exchange = value; }
        }


        public CreateOrderModel Model
        {
            get { return _model; }
            set { _model = value; }
        }
    }
}
