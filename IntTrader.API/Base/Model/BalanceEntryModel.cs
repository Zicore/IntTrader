using System;

namespace IntTrader.API.Base.Model
{
    public class BalanceEntryModel
    {
        private String _currencyKey;
        private String _walletType;
        private decimal _amount;
        private decimal _available;

        public string CurrencyKey
        {
            get { return _currencyKey; }
            set { _currencyKey = value; }
        }

        public string WalletType
        {
            get { return _walletType; }
            set { _walletType = value; }
        }

        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public decimal Available
        {
            get { return _available; }
            set { _available = value; }
        }
    }
}
