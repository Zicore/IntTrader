using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;
using IntTrader.API.Currency;
using IntTrader.Controls.Ticker;
using IntTrader.ViewModel;

namespace IntTrader.Controls.OrderBook
{
    public class OrderBookViewModel : ExchangeViewModelBase
    {
        public OrderBookViewModel(ExchangeBase exchangeBase)
            : base(exchangeBase)
        {
            Ticker = new TickerViewModel(exchangeBase);
            Update();
        }

        public event EventHandler SelectedAskChanged;
        protected virtual void OnSelectedAskChanged()
        {
            EventHandler handler = SelectedAskChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler SelectedBidChanged;
        protected virtual void OnSelectedBidChanged()
        {
            EventHandler handler = SelectedBidChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private OrderBookEntry _selectedAsk;
        private OrderBookEntry _selectedBid;

        private ObservableCollection<OrderBookEntry> _asks = new ObservableCollection<OrderBookEntry>();
        private ObservableCollection<OrderBookEntry> _bids = new ObservableCollection<OrderBookEntry>();

        public ObservableCollection<OrderBookEntry> Asks
        {
            get { return _asks; }
        }

        public ObservableCollection<OrderBookEntry> Bids
        {
            get { return _bids; }
        }

        public String Title
        {
            get { return String.Format("{0} {1}", Exchange.Name, Pair.SymbolPair); }
        }

        public TickerViewModel Ticker { get; private set; }

        public OrderBookEntry SelectedAsk
        {
            get { return _selectedAsk; }
            set
            {
                _selectedAsk = value;
                if (value != null)
                {
                    OnSelectedAskChanged();
                }
            }
        }

        public OrderBookEntry SelectedBid
        {
            get { return _selectedBid; }
            set
            {
                _selectedBid = value;
                if (value != null)
                {
                    OnSelectedBidChanged();
                }
            }
        }

        public override void OnUpdate()
        {
            if (Exchange.IsAvailable(APIFunction.RequestOrderBook))
            {
                var orderbookModel = Exchange.RequestOrderBook(Pair);
                orderbookModel.Asks.Reverse();
                GroupEntries(orderbookModel);
                Ticker.Update();
                Dispatch(() => Dispatch(orderbookModel));
            }
        }

        public override void UpdatePair(PairBase symbol)
        {
            base.UpdatePair(symbol);
            Ticker.UpdatePair(symbol);
            OnPropertyChanged("Title");
        }

        private void Dispatch(OrderBookModel orderBookModel)
        {
            Asks.Clear();
            Bids.Clear();
            int count = orderBookModel.Asks.Count;
            int takeCount = 100;
            orderBookModel.Asks.Skip(count - takeCount).Take(takeCount).ToList().ForEach(x => Asks.Add(new OrderBookEntry
            {
                Amount = x.Amount,
                DateTime = x.DateTime,
                Price = x.Price,
                GroupedPrice = x.GroupedPrice,
                GroupedVolume = x.GroupedVolume
            }));

            orderBookModel.Bids.Take(100).ToList().ForEach(x => Bids.Add(new OrderBookEntry
            {
                Amount = x.Amount,
                DateTime = x.DateTime,
                Price = x.Price,
                GroupedPrice = x.GroupedPrice,
                GroupedVolume = x.GroupedVolume
            }));
        }

        private void GroupEntries(OrderBookModel model)
        {
            GroupEntries(model.Asks, true);
            GroupEntries(model.Bids, false);
        }

        const double Epsilon = 0.0001;

        private void GroupEntries(IList<OrderBookEntryModel> entries, bool ask)
        {


            var list = new List<OrderBookEntryModel>();
            double countPrice = 0;
            int threshold = 2;

            float lastAmount = 0.0f;
            float lastPrice = 0.0f;

            float diffCount = 0;

            float sum = 0.0f;
            for (int i = 0; i < entries.Count; i++)
            {
                var e = entries[i];
                float price = (float)e.Price;
                float amount = (float)e.Amount;

                if (i == 0)
                {
                    if (ask)
                    {
                        countPrice = (double)entries[entries.Count - 1].Price;

                    }
                    else
                    {
                        countPrice = (double)entries[0].Price;
                    }
                }


                if (Math.Abs(lastPrice - 0) < Epsilon)
                {
                    lastPrice = price;
                }

                diffCount += lastPrice - price;
                sum += amount;

                if (diffCount >= threshold)
                {
                    diffCount = 0;
                    if (ask)
                    {
                        countPrice += threshold;
                    }
                    else
                    {
                        countPrice -= threshold;
                    }
                    list.Add(new OrderBookEntryModel { GroupedPrice = (int)countPrice, GroupedVolume = (int)sum });
                }

                lastAmount = amount;
                lastPrice = price;
            }

            for (int i = 0; i < entries.Count && i < list.Count; i++)
            {
                if (ask)
                {
                    entries[-1 + entries.Count - i].GroupedPrice = list[i].GroupedPrice;
                    entries[-1 + entries.Count - i].GroupedVolume = list[i].GroupedVolume;
                }
                else
                {
                    entries[i].GroupedPrice = list[i].GroupedPrice;
                    entries[i].GroupedVolume = list[i].GroupedVolume;
                }
            }
        }
    }
}
