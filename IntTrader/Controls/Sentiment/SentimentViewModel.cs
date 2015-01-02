using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Exchange.Orders;
using IntTrader.Controls.Exchange;
using IntTrader.Controls.Trades;
using IntTrader.ViewModel;

namespace IntTrader.Controls.Sentiment
{
    public class SentimentViewModel : ExchangeViewModelBase
    {
        public TradesViewModel TradesViewModel { get; set; }

        public SentimentViewModel(ExchangeBase exchangeBase, TradesViewModel tradesViewModel)
            : base(exchangeBase)
        {
            TradesViewModel = tradesViewModel;

            TradesViewModel.UpdateEvent += TradesViewModelOnUpdateEvent;
        }

        private void TradesViewModelOnUpdateEvent(object sender, EventArgs eventArgs)
        {
            Update();
        }

        private decimal _volumeBuy;
        private decimal _volumeSell;
        private decimal _sentiment;

        public decimal VolumeBuy
        {
            get { return _volumeBuy; }
            set
            {
                _volumeBuy = value;
                OnPropertyChanged("VolumeBuy");
                OnPropertyChanged("VolumeBuyFormat");
                OnPropertyChanged("IsBuySentiment");
            }
        }

        public String VolumeBuyFormat
        {
            get { return String.Format(CultureInfo.InvariantCulture, "{0:0.00} {1}", VolumeBuy, Pair.LeftCurrency.Symbol); }
        }

        public decimal VolumeSell
        {
            get { return _volumeSell; }
            set
            {
                _volumeSell = value;
                OnPropertyChanged("VolumeSell");
                OnPropertyChanged("VolumeSellFormat");
                OnPropertyChanged("IsBuySentiment");
            }
        }

        public String VolumeSellFormat
        {
            get { return String.Format(CultureInfo.InvariantCulture, "{0:0.00} {1}", VolumeSell, Pair.LeftCurrency.Symbol); }
        }

        public decimal Sentiment
        {
            get { return _sentiment; }
            set
            {
                _sentiment = value;
                OnPropertyChanged("Sentiment");
                OnPropertyChanged("SentimentFormat");
                OnPropertyChanged("IsBuySentiment");
            }
        }

        public String SentimentFormat
        {
            get { return String.Format(CultureInfo.InvariantCulture, "{0:0.00} %", Sentiment); }
        }

        public bool IsBuySentiment
        {
            get { return VolumeBuy > VolumeSell; }
        }

        public override void OnUpdate()
        {
            if (TradesViewModel.TradesModel.Items.Count > 0)
            {
                VolumeBuy = TradesViewModel.TradesModel.Items.Where(x => x.Side == OrderSide.Buy).Sum(x => x.Amount);
                VolumeSell = TradesViewModel.TradesModel.Items.Where(x => x.Side == OrderSide.Sell).Sum(x => x.Amount);


                var diff = VolumeBuy - VolumeSell;
                var max = Math.Max(VolumeBuy, VolumeSell);
                var min = Math.Min(VolumeBuy, VolumeSell);

                if (max == 0)
                {
                    return;
                }

                decimal sentiment = (min / max * 100) - 100;
                if (diff > 0)
                {
                    sentiment = sentiment * -1;
                }
                Sentiment = sentiment;
            }

            base.OnUpdate();
        }

    }
}
