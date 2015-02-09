using System;
using IntTrader.API.Base.Exchange.Base;

namespace IntTrader.API.Base.Model
{
    public class OpenOrderEntryModel : ResponseModelBase
    {
        public OpenOrderEntryModel()
        {

        }

        private ExchangeBase _exchange;

        private bool _wasSuccessful;

        private String _orderId;
        private String _symbol;
        private String _exchangeString;
        private decimal _price;
        private decimal _avgerageExecutionPrice;
        private String _type;
        private DateTime _dateTime;
        private bool _isLive;
        private bool _isCancelled;
        private bool _wasForced;
        private String _orderStatus;
        private String _orderDescription;
        private String _closeDescription;


        private decimal _executedAmount;
        private decimal _remainingAmount;
        private decimal _originalAmount;

        private String _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public ExchangeBase Exchange
        {
            get { return _exchange; }
            set { _exchange = value; }
        }

        public bool WasSuccessful
        {
            get { return _wasSuccessful; }
            set { _wasSuccessful = value; }
        }

        public String OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        public string ExchangeString
        {
            get { return _exchangeString; }
            set { _exchangeString = value; }
        }

        public string Symbol
        {
            get { return _symbol; }
            set { _symbol = value; }
        }

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public decimal AverageExecutionPrice
        {
            get { return _avgerageExecutionPrice; }
            set { _avgerageExecutionPrice = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public DateTime DateTime
        {
            get { return _dateTime; }
            set { _dateTime = value; }
        }

        public string OrderStatus
        {
            get { return _orderStatus; }
            set { _orderStatus = value; }
        }

        public bool IsLive
        {
            get { return _isLive; }
            set { _isLive = value; }
        }

        public bool IsCancelled
        {
            get { return _isCancelled; }
            set { _isCancelled = value; }
        }

        public bool WasForced
        {
            get { return _wasForced; }
            set { _wasForced = value; }
        }

        public string OrderDescription
        {
            get { return _orderDescription; }
            set { _orderDescription = value; }
        }

        public string CloseDescription
        {
            get { return _closeDescription; }
            set { _closeDescription = value; }
        }

        public decimal ExecutedAmount
        {
            get { return _executedAmount; }
            set { _executedAmount = value; }
        }

        public decimal RemainingAmount
        {
            get { return _remainingAmount; }
            set { _remainingAmount = value; }
        }

        public decimal OriginalAmount
        {
            get { return _originalAmount; }
            set { _originalAmount = value; }
        }
    }
}
