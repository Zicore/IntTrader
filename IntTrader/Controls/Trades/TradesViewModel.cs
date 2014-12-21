using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;
using IntTrader.Controls.Exchange;
using IntTrader.Service;
using IntTrader.ViewModel;

namespace IntTrader.Controls.Trades
{
    public class TradesViewModel : ExchangeViewModelBase
    {
        public MainViewModel MainViewModel { get; set; }
        public ExchangeViewModelBase Parent { get; set; }

        public TradesViewModel(MainViewModel mainViewModel, ExchangeViewModelBase parent, ExchangeBase exchangeBase)
            : base(exchangeBase)
        {
            MainViewModel = mainViewModel;
            Parent = parent;
            MainViewModel.TimerSeconds.Tick += TimerSecondsOnTick;
            _updateController.Register("Trades", Update, 5, false, true);
            Update();
        }

        private void TimerSecondsOnTick(object sender, EventArgs eventArgs)
        {
            bool condition = Parent.IsActive;
            _updateController.Update(ref condition);
        }

        UpdateController _updateController = new UpdateController();

        ObservableCollection<TradesEntryViewModel> _items = new ObservableCollection<TradesEntryViewModel>();

        public ObservableCollection<TradesEntryViewModel> Items
        {
            get { return _items; }
            set { _items = value; }
        }
        public override void OnUpdate()
        {
            if (Exchange.IsAvailable(APIFunction.RequestTrades))
            {
                var model = Exchange.RequestTrades(Pair);
                Dispatch(() => Dispatch(model));
            }
        }

        private void Dispatch(TradesModel model)
        {
            Items.Clear();
            foreach (var trade in model.Items.Take(200))
            {
                var vm = TradesEntryViewModel.FromModel(Exchange, trade);
                Items.Add(vm);
            }
        }
    }
}
