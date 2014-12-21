using System;
using IntTrader.ViewModel;

namespace IntTrader.Controls.OrderBook
{
    public class OrderBookEntry : ViewModelBase
    {
        public OrderBookEntry()
        {

        }

        private PrefixSuffixEntry _priceEntry;
        private PrefixSuffixEntry _amountEntry;

        private decimal _price;
        private decimal _amount;
        private int _groupedPrice;

        public int GroupedPrice
        {
            get { return _groupedPrice; }
            set
            {
                _groupedPrice = value;
                OnPropertyChanged("GroupedPrice");
            }
        }

        public int GroupedVolume
        {
            get { return _groupedVolume; }
            set
            {
                _groupedVolume = value;
                OnPropertyChanged("GroupedVolume");
            }
        }

        private int _groupedVolume;

        private DateTime _dateTime;

        public PrefixSuffixEntry PriceEntry
        {
            get { return _priceEntry; }
            set
            {
                _priceEntry = value;
                OnPropertyChanged("PriceEntry");
            }
        }

        public PrefixSuffixEntry AmountEntry
        {
            get { return _amountEntry; }
            set
            {
                _amountEntry = value;
                OnPropertyChanged("AmountEntry");
            }
        }

        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                _priceEntry = PrefixSuffixEntry.CalculatePrice(Price);
                OnPropertyChanged("Price");
            }
        }

        public decimal Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                _amountEntry = PrefixSuffixEntry.CalculateAmount(Amount);
                OnPropertyChanged("Amount");
            }
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


    }
}
