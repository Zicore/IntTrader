using System;

namespace IntTrader.API.Base.Model
{
    public class TickerModel : ResponseModelBase
    {
        public TickerModel()
        {

        }

        private DateTime _dateTime;

        private decimal _ask;
        private decimal _askVolume;
        private decimal _bid;
        private decimal _bidVolume;

        private decimal _mid;
        private decimal _lastPrice;
        private decimal _lastPriceVolume;

        private decimal _lowToday;
        private decimal _low24Hour;

        private decimal _highToday;
        private decimal _high24Hour;

        private decimal _openingPriceToday;

        private decimal _volumeToday;
        private decimal _volume24Hour;

        private decimal _volumeAverageToday;
        private decimal _volumeAverage24Hour;

        private decimal _numberOfTradesToday;
        private decimal _numberOfTrades24Hour;

        public decimal AskVolume
        {
            get { return _askVolume; }
            set { _askVolume = value; }
        }

        public decimal LastPriceVolume
        {
            get { return _lastPriceVolume; }
            set { _lastPriceVolume = value; }
        }

        public decimal LowToday
        {
            get { return _lowToday; }
            set { _lowToday = value; }
        }

        public decimal BidVolume
        {
            get { return _bidVolume; }
            set { _bidVolume = value; }
        }

        public decimal Low24Hour
        {
            get { return _low24Hour; }
            set { _low24Hour = value; }
        }

        public decimal HighToday
        {
            get { return _highToday; }
            set { _highToday = value; }
        }

        public decimal High24Hour
        {
            get { return _high24Hour; }
            set { _high24Hour = value; }
        }

        public decimal OpeningPriceToday
        {
            get { return _openingPriceToday; }
            set { _openingPriceToday = value; }
        }

        public decimal VolumeToday
        {
            get { return _volumeToday; }
            set { _volumeToday = value; }
        }

        public decimal Volume24Hour
        {
            get { return _volume24Hour; }
            set { _volume24Hour = value; }
        }

        public decimal VolumeAverageToday
        {
            get { return _volumeAverageToday; }
            set { _volumeAverageToday = value; }
        }

        public decimal VolumeAverage24Hour
        {
            get { return _volumeAverage24Hour; }
            set { _volumeAverage24Hour = value; }
        }

        public decimal NumberOfTradesToday
        {
            get { return _numberOfTradesToday; }
            set { _numberOfTradesToday = value; }
        }

        public decimal NumberOfTrades24Hour
        {
            get { return _numberOfTrades24Hour; }
            set { _numberOfTrades24Hour = value; }
        }

        public decimal Ask
        {
            get { return _ask; }
            set { _ask = value; }
        }

        public decimal Bid
        {
            get { return _bid; }
            set { _bid = value; }
        }

        public decimal Mid
        {
            get { return _mid; }
            set { _mid = value; }
        }

        public decimal LastPrice
        {
            get { return _lastPrice; }
            set { _lastPrice = value; }
        }

        public DateTime DateTime
        {
            get { return _dateTime; }
            set { _dateTime = value; }
        }
    }
}
