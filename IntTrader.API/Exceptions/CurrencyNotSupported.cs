using System;

namespace IntTrader.API.Exceptions
{
    public class CurrencyNotSupportedException : Exception
    {
        private String _currencyKey;

        public string CurrencyKey
        {
            get { return _currencyKey; }
            set { _currencyKey = value; }
        }
    }
}
