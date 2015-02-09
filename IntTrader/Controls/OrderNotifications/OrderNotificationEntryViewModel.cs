using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntTrader.API.Base.Exchange;
using IntTrader.API.Base.Model;
using IntTrader.ViewModel;
using Zicore.WPF.Base.Event;

namespace IntTrader.Controls.OrderNotifications
{
    public class OrderNotificationEntryViewModel : ExchangeManagerViewModel
    {
        public MainViewModel MainViewModel { get; set; }

        public OrderNotificationEntryViewModel(MainViewModel mainViewModel, ExchangeManager exchangeManager)
            : base(exchangeManager)
        {
            MainViewModel = mainViewModel;
        }

        private String _id;
        private decimal _price;
        private decimal _volume;
        private bool _successful;
        private String _message;
        private String _symbolText;
        private String _dateTime;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public decimal Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        public bool Successful
        {
            get { return _successful; }
            set { _successful = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public string SymbolText
        {
            get { return _symbolText; }
            set { _symbolText = value; }
        }

        public string DateTime
        {
            get { return _dateTime; }
            set { _dateTime = value; }
        }
    }
}
