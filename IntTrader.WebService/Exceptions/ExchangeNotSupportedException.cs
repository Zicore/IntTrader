using System;

namespace IntTrader.WebService.Exceptions
{
    class ExchangeNotSupportedException : Exception
    {
        public ExchangeNotSupportedException(String exchange)
        {
            Exchange = exchange;
        }

        private String _exchange;

        public string Exchange
        {
            get { return _exchange; }
            set { _exchange = value; }
        }
    }
}
