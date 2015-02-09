using System.Collections.ObjectModel;
using System.Windows.Media;
using IntTrader.API.Base.Exchange;
using IntTrader.Service;
using IntTrader.ViewModel;

namespace IntTrader.Controls.Dashboard
{
    public class DashboardViewModel : ExchangeManagerViewModel
    {
        public MainViewModel MainViewModel { get; set; }
        public UpdateController UpdateController { get; private set; }
        public ObservableCollection<DashboardEntryViewModel> DashboardItems { get; set; }

        public DashboardViewModel(ExchangeManager exchangeManager, MainViewModel mainViewModel)
            : base(exchangeManager)
        {
            //Foreground = Brushes.OrangeRed;
            MainViewModel = mainViewModel;
            Header = "Dashboard";
            LoadDashboardItems();
        }

        public override bool IsActive
        {
            get { return MainViewModel.SelectedTab == this; }
        }

        private void LoadDashboardItems()
        {
            DashboardItems = new ObservableCollection<DashboardEntryViewModel>();
            foreach (var exchange in MainViewModel.Exchanges)
            {
                foreach (var pair in exchange.Exchange.PairManager.SupportedPairs)
                {
                    var entry = new DashboardEntryViewModel(exchange, this, MainViewModel);
                    entry.UpdatePair(pair.Value);
                    DashboardItems.Add(entry);
                }
            }
        }

        public DashboardEntryViewModel SelectedDashboardItem
        {
            get { return null; }
            set
            {
                if (value != null)
                {
                    MainViewModel.SelectedTab = value.ExchangeViewModel;
                    value.ExchangeViewModel.Pairs.Select(value.Ticker.Pair);
                }
            }
        }
    }
}
