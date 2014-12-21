using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Exchange.Orders;
using IntTrader.API.Currency;
using IntTrader.Controls.Balance;
using IntTrader.Controls.NewOrder;
using IntTrader.Controls.OrderBook;
using IntTrader.Controls.Ticker;
using IntTrader.Controls.Trades;
using IntTrader.Controls.UserOrders;
using IntTrader.ViewModel;

namespace IntTrader.Controls.Exchange
{
    public class ExchangeViewModel : ExchangeViewModelBase
    {
        public MainViewModel MainViewModel { get; set; }
        public OrderBookViewModel OrderBook { get; set; }
        public TickerViewModel Ticker { get; set; }
        public UserOrdersViewModel Orders { get; set; }
        public NewOrderViewModel NewBuyOrder { get; set; }
        public NewOrderViewModel NewSellOrder { get; set; }


        public TradesViewModel Trades { get; set; }

        public BalanceViewModel Balance { get; set; }

        public PairManageViewModel Pairs { get; set; }

        readonly DispatcherTimer _timerUpdate = new DispatcherTimer();
        readonly DispatcherTimer _timerSeconds = new DispatcherTimer();

        public override bool IsActive
        {
            get
            {
                return MainViewModel.SelectedTab == this;
            }
        }

        public ExchangeViewModel(MainViewModel mainViewModel, ExchangeBase exchangeBase)
            : base(exchangeBase)
        {
            Pairs = new PairManageViewModel(exchangeBase);
            this.MainViewModel = mainViewModel;
            this.Exchange = exchangeBase;
            this.Header = Exchange.Name;
            //this.Foreground = Brushes.DodgerBlue;

            MainViewModel.TimerSeconds.Tick += TimerSecondsOnTick;

            Orders = new UserOrdersViewModel(Exchange);
            Ticker = new TickerViewModel(Exchange);
            OrderBook = new OrderBookViewModel(Exchange);

            Trades = new TradesViewModel(MainViewModel, this, Exchange);

            Balance = new BalanceViewModel(Exchange);
            NewBuyOrder = new NewOrderViewModel(Exchange) { Side = OrderSide.Buy };
            NewSellOrder = new NewOrderViewModel(Exchange) { Side = OrderSide.Sell };

            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                UpdatePair();
                Pairs.PairChanged += PairsOnPairChanged;
                OrderBook.Ticker.LastTradeClickEvent += TickerOnLastTradeClickEvent;
                OrderBook.SelectedAskChanged += OrderBookOnSelectedAskChanged;
                OrderBook.SelectedBidChanged += OrderBookOnSelectedBidChanged;
                Balance.SelectedBalanceChanged += BalanceOnSelectedBalanceChanged;
                CreateTimer();
                UpdateViewModels();
                RegisterUpdates();
                Ticker.Dispatched += TickerOnDispatched;
            }
        }

        private void RegisterUpdates()
        {
            UpdateController.Register("ExchangeViewModelOrderBook", OrderBook.Update, 8, true, true);
            UpdateController.Register("ExchangeViewModelTicker", Ticker.Update, 5, true, true);
            UpdateController.Register("ExchangeViewModelOrders", Orders.Update, 10, true, true);
            UpdateController.Register("ExchangeViewModelBalance", Balance.Update, 15, true, true);
        }

        private void TimerSecondsOnTick(object sender, EventArgs eventArgs)
        {
            if (IsActive)
            {
                Ticker.LastUpdateSeconds++;
            }
        }

        private void BalanceOnSelectedBalanceChanged(object sender, EventArgs eventArgs)
        {
            if (!Balance.SelectedBalanceEntry.Currency.IsCryptoCurrency)
            {
                NewBuyOrder.Result = Balance.SelectedBalanceEntry.Amount;
                NewSellOrder.Result = Balance.SelectedBalanceEntry.Amount;
            }
            else
            {
                NewBuyOrder.Amount = Balance.SelectedBalanceEntry.Amount;
                NewSellOrder.Amount = Balance.SelectedBalanceEntry.Amount;
            }
        }

        private void OrderBookOnSelectedAskChanged(object sender, EventArgs eventArgs)
        {
            NewBuyOrder.Price = OrderBook.SelectedAsk.Price;
            NewSellOrder.Price = OrderBook.SelectedAsk.Price;
        }

        private void OrderBookOnSelectedBidChanged(object sender, EventArgs eventArgs)
        {
            NewBuyOrder.Price = OrderBook.SelectedBid.Price;
            NewSellOrder.Price = OrderBook.SelectedBid.Price;
        }

        private void TickerOnLastTradeClickEvent(object sender, EventArgs eventArgs)
        {
            NewBuyOrder.Price = Ticker.LastPrice;
            NewSellOrder.Price = Ticker.LastPrice;
        }

        private void TickerOnDispatched(object sender, EventArgs eventArgs)
        {

        }

        private void PairsOnPairChanged(object sender, EventArgs e)
        {
            UpdatePair(Pairs.SelectedPair.Pair);
            UpdateViewModels();
            MainViewModel.UpdateWindowText();
        }

        private void UpdatePair()
        {
            UpdatePair(Pairs.SelectedPair.Pair);
        }

        public override void UpdatePair(PairBase pair)
        {
            OrderBook.UpdatePair(pair);
            Ticker.UpdatePair(pair);
            Orders.UpdatePair(pair);
            NewBuyOrder.UpdatePair(pair);
            NewSellOrder.UpdatePair(pair);
            Trades.UpdatePair(pair);
        }

        private void CreateTimer()
        {
            _timerUpdate.Tick += TimerOnTick;
            _timerUpdate.Interval = new TimeSpan(0, 0, 0, 1);
            _timerUpdate.IsEnabled = true;
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            bool isActive = IsActive;
            UpdateController.Update(ref isActive);
        }

        private void UpdateViewModels()
        {
            if (IsActive)
            {
                OrderBook.Update();
                Ticker.Update();
                Orders.Update();
                Balance.Update();

                _timerUpdate.Start();
                Ticker.LastUpdateSeconds = 0;
            }
        }
    }
}
