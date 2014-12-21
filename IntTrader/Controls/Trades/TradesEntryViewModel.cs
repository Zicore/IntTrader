using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Exchange.Orders;
using IntTrader.API.Base.Model;
using IntTrader.Controls.Exchange;
using IntTrader.ViewModel;

namespace IntTrader.Controls.Trades
{
    public class TradesEntryViewModel : ExchangeViewModelBase
    {
        public TradesEntryViewModel(ExchangeBase exchangeBase)
            : base(exchangeBase)
        {

        }

        public static TradesEntryViewModel FromModel(ExchangeBase exchangeBase, TradesEntryModel model)
        {
            var vm = new TradesEntryViewModel(exchangeBase)
                {
                    Price = model.Price,
                    Amount = model.Amount,
                    Side = model.Side,
                    Timestamp = model.Timestamp,
                    Info = model.Info,
                    Type = model.Type
                };
            return vm;
        }

        private DateTime _timestamp;
        private decimal _price;
        private decimal _amount;
        private OrderSide _side = OrderSide.Buy;
        private String _type;
        private String _info;

        public bool IsBuyOrder
        {
            get { return Side == OrderSide.Buy; }
        }

        public bool IsSellOrder
        {
            get { return Side == OrderSide.Sell; }
        }

        public DateTime Timestamp
        {
            get { return _timestamp; }
            set
            {
                _timestamp = value;
                OnPropertyChanged("Timestamp");
                OnPropertyChanged("TimestampFormat");
            }
        }

        public String TimestampFormat
        {
            get { return String.Format("{0:HH:mm:ss.ff}", Timestamp); }
        }

        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
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

        public OrderSide Side
        {
            get { return _side; }
            set
            {
                _side = value;
                OnPropertyChanged("Side");
            }
        }

        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("Type");
            }
        }

        public string Info
        {
            get { return _info; }
            set
            {
                _info = value;
                OnPropertyChanged("Info");
            }
        }
    }
}
