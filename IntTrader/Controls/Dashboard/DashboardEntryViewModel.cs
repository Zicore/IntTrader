using System;
using IntTrader.API.Currency;
using IntTrader.Controls.Exchange;
using IntTrader.Controls.Ticker;
using IntTrader.Service;
using IntTrader.ViewModel;

namespace IntTrader.Controls.Dashboard
{
    public class DashboardEntryViewModel : ExchangeViewModelBase
    {
        UpdateController _updateController = new UpdateController();
        public MainViewModel MainViewModel { get; set; }
        public DashboardViewModel DashboardViewModel { get; set; }

        public DashboardEntryViewModel(ExchangeViewModel exchangeViewModel, DashboardViewModel dashboardViewModel, MainViewModel mainViewModel)
            : base(exchangeViewModel.Exchange)
        {
            this.ExchangeViewModel = exchangeViewModel;
            this.MainViewModel = mainViewModel;
            this.DashboardViewModel = dashboardViewModel;
            this.Ticker = new TickerViewModel(Exchange);
            this.Header = exchangeViewModel.Header;
            _updateController.Register(String.Format("{0}DashboardTicker", Exchange.Name), Ticker.Update, 10, true, true);
            MainViewModel.TimerSeconds.Tick += TimerSecondsOnTick;
        }

        private void TimerSecondsOnTick(object sender, EventArgs eventArgs)
        {
            bool isActive = DashboardViewModel.IsActive;
            _updateController.Update(ref isActive);
        }

        public ExchangeViewModel ExchangeViewModel { get; set; }
        public TickerViewModel Ticker { get; set; }

        public override void UpdatePair(PairBase pair)
        {
            Ticker.UpdatePair(pair);
            base.UpdatePair(pair);
        }
    }
}
