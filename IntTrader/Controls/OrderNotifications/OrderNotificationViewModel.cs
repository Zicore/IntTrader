using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using IntTrader.API.Base.Exchange;
using IntTrader.API.Base.Model;
using IntTrader.API.Converter;
using IntTrader.API.Event;
using IntTrader.ViewModel;
using Zicore.WPF.Base.Event;

namespace IntTrader.Controls.OrderNotifications
{
    public class OrderNotificationViewModel : ExchangeManagerViewModel
    {
        public MainViewModel MainViewModel { get; set; }

        public OrderNotificationViewModel(MainViewModel mainViewModel, ExchangeManager exchangeManager)
            : base(exchangeManager)
        {
            MainViewModel = mainViewModel;
            ExchangeManager.CreateOrderEvent += ExchangeManagerOnCreateOrderEvent;
        }

        private void ExchangeManagerOnCreateOrderEvent(object sender, CreateOrderEventArgs e)
        {
            var item = new OrderNotificationEntryViewModel(MainViewModel, ExchangeManager)
            {
                Id = e.Model.OrderId,
                Price = e.Model.Price,
                Volume = e.Model.ExecutedAmount,
                Successful = e.Model.WasSuccessful,
                DateTime = e.Model.DateTime.ToShortDateString()
            };

            if (!String.IsNullOrEmpty(e.Model.Symbol) && e.Exchange.PairManager.SupportedPairs.ContainsKey(e.Model.Symbol))
            {
                var currency = e.Exchange.PairManager.GetPair(e.Model.Symbol);
                item.SymbolText = currency.SymbolPair;
            }

            Dispatch(() => Items.Add(item));
        }



        ObservableCollection<OrderNotificationEntryViewModel> _items = new ObservableCollection<OrderNotificationEntryViewModel>();

        public ObservableCollection<OrderNotificationEntryViewModel> Items
        {
            get { return _items; }
            set { _items = value; }
        }
    }
}
