using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntTrader.API.Base.Exchange;
using IntTrader.Controls.OrderNotifications;
using IntTrader.ViewModel;

namespace IntTrader.Dialogs.OrderNotifications
{
    public class OrderNotificationWindowViewModel : ExchangeManagerViewModel
    {
        public MainViewModel MainViewModel { get; set; }

        public OrderNotificationWindowViewModel(MainViewModel mainViewModel, ExchangeManager exchangeManager)
            : base(exchangeManager)
        {
            MainViewModel = mainViewModel;
            OrderNotificationViewModel = new OrderNotificationViewModel(MainViewModel, ExchangeManager);
        }

        public OrderNotificationViewModel OrderNotificationViewModel { get; set; }
    }
}
