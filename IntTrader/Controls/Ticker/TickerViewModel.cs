using System;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;
using IntTrader.ViewModel;
using NLog;
using Zicore.WPF.Base.Commands;

namespace IntTrader.Controls.Ticker
{
    public class TickerViewModel : ExchangeViewModelBase
    {
        public TickerViewModel(ExchangeBase exchangeBase)
            : base(exchangeBase)
        {
            Update();
        }

        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public event EventHandler LastTradeClickEvent;

        private RelayCommand _lastTradeClickCommand;
        public RelayCommand LastTradeClickCommand
        {
            get
            {
                if (_lastTradeClickCommand == null)
                {
                    _lastTradeClickCommand = new RelayCommand(x => LastTradeClick());
                }
                return _lastTradeClickCommand;
            }
        }

        private void LastTradeClick()
        {
            if (LastTradeClickEvent != null)
            {
                LastTradeClickEvent(this, EventArgs.Empty);
            }
        }

        private long _lastUpdateSeconds = 0;

        private decimal _mid;
        private decimal _ask;
        private decimal _bid;
        private decimal _lastPrice;
        private DateTime _dateTime;

        public decimal Mid
        {
            get { return _mid; }
            set
            {
                _mid = value;
                OnPropertyChanged("Mid");
            }
        }

        public decimal Ask
        {
            get { return _ask; }
            set
            {
                _ask = value;
                OnPropertyChanged("Ask");
            }
        }

        public decimal Bid
        {
            get { return _bid; }
            set
            {
                _bid = value;
                OnPropertyChanged("Bid");
            }
        }

        public decimal LastPrice
        {
            get { return _lastPrice; }
            set
            {
                _lastPrice = value;
                OnPropertyChanged("LastPrice");
                OnPropertyChanged("LastPriceString");
            }
        }

        public String LastPriceString
        {
            get
            {
                return String.Format("{0} {1}", Pair.RightCurrency.FormatDecimals(LastPrice), Pair.RightCurrency.Symbol);
            }
        }

        public bool IsAsk
        {
            get { return LastPrice >= Ask; }
        }

        public DateTime DateTime
        {
            get { return _dateTime; }
            set
            {
                _dateTime = value;
                OnPropertyChanged("DateTime");
            }
        }

        public long LastUpdateSeconds
        {
            get { return _lastUpdateSeconds; }
            set
            {
                _lastUpdateSeconds = value;
                OnPropertyChanged("LastUpdateSeconds");
            }
        }

        public override void OnUpdate()
        {
            if (Exchange.IsAvailable(APIFunction.RequestTicker))
            {
                var tickerModel = Exchange.RequestTicker(Pair);
                Dispatch(() => Dispatch(tickerModel));
            }
        }

        private void Dispatch(TickerModel tickerModel)
        {
            this.Ask = tickerModel.Ask;
            this.Bid = tickerModel.Bid;
            this.LastPrice = tickerModel.LastPrice;
            this.DateTime = tickerModel.DateTime;
            this.Mid = tickerModel.Mid;
            OnPropertyChanged("IsAsk");
        }
    }
}
