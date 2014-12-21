using System;
using IntTrader.API.Currency;
using IntTrader.ViewModel;

namespace IntTrader.Controls.Balance
{
    public class BalanceEntryViewModel : ViewModelBase
    {
        private String _walletType;
        private CurrencyBase _currency;
        private decimal _amount;
        private decimal _available;

        public string Type
        {
            get { return _walletType; }
            set
            {
                _walletType = value;
                OnPropertyChanged("Type");
            }
        }

        public CurrencyBase Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

        public decimal Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged("Amount");
            }
        }

        public decimal Available
        {
            get { return _available; }
            set
            {
                _available = value;
                OnPropertyChanged("Available");
            }
        }
    }
}
