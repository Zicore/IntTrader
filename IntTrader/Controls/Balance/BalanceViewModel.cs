using System;
using System.Collections.ObjectModel;
using System.Linq;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;
using IntTrader.API.Currency;
using IntTrader.ViewModel;

namespace IntTrader.Controls.Balance
{
    public class BalanceViewModel : ExchangeViewModelBase
    {
        public BalanceViewModel(ExchangeBase exchangeBase)
            : base(exchangeBase)
        {
            // Items.Add(new BalanceEntryViewModel { Available = 50000, Currency = CurrencyBase.USD, Type = "Exchange" });
        }

        public override void OnUpdate()
        {
            if (Exchange.IsAvailable(APIFunction.RequestBalances))
            {
                var balanceModel = Exchange.RequestBalances();
                Dispatch(() => Dispatch(balanceModel));
            }
        }

        public event EventHandler SelectedBalanceChanged;
        protected virtual void OnSelectedBalanceChanged()
        {
            EventHandler handler = SelectedBalanceChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private BalanceEntryViewModel _selectedBalanceEntry;
        public BalanceEntryViewModel SelectedBalanceEntry
        {
            get { return _selectedBalanceEntry; }
            set
            {
                _selectedBalanceEntry = value;
                if (_selectedBalanceEntry != null)
                {
                    OnSelectedBalanceChanged();
                }
            }
        }

        public BalanceEntryViewModel SelectedBalanceEntryReadOnly
        {
            get { return null; }
            set
            {
                SelectedBalanceEntry = value;
                OnPropertyChanged("SelectedBalanceEntryReadOnly");
            }
        }

        ObservableCollection<BalanceEntryViewModel> _items = new ObservableCollection<BalanceEntryViewModel>();
        public ObservableCollection<BalanceEntryViewModel> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        private void Dispatch(BalanceModel balanceModel)
        {
            var modelItems = balanceModel.Items.OrderByDescending(x => x.Available);
            Items.Clear();

            foreach (var balanceEntry in modelItems)
            {
                var balanceEntryViewModel = new BalanceEntryViewModel
                {
                    Amount = balanceEntry.Amount,
                    Available = balanceEntry.Available,
                    Currency = Exchange.CurrencyManager.GetCurrency(balanceEntry.CurrencyKey),
                    Type = balanceEntry.WalletType
                };
                if (balanceEntry.Amount > 0)
                {
                    Items.Add(balanceEntryViewModel);
                }
            }
        }
    }
}
