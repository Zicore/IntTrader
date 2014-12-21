using System;
using System.Globalization;

namespace IntTrader.API.Base.Model
{
    public class OrderBookEntryModel : ResponseModelBase
    {
        // "price": "0.0278",
        // "amount": "20.0",
        // "timestamp": "1395065891.0"

        private decimal _price;
        private decimal _amount;
        private int _groupedVolume;
        private int _groupedPrice;

        private DateTime _dateTime;

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public DateTime DateTime
        {
            get { return _dateTime; }
            set { _dateTime = value; }
        }

        public int GroupedVolume
        {
            get { return _groupedVolume; }
            set { _groupedVolume = value; }
        }

        public int GroupedPrice
        {
            get { return _groupedPrice; }
            set { _groupedPrice = value; }
        }

        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "{0:HH:mm:ss} {1:000.000000} {2:000000.000000}", DateTime, Price, Amount);
        }
    }
}
