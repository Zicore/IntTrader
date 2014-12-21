using System;

namespace IntTrader.API.Exceptions
{
    public class PairNotSupportedException : Exception
    {
        private String _pairKey;

        public string PairKey
        {
            get { return _pairKey; }
            set { _pairKey = value; }
        }
    }
}
