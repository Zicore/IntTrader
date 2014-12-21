using System;
using Newtonsoft.Json;

namespace IntTrader.API.Exchange.Bitfinex.Response
{
    public class OrderResponseBase : BitfinexResponse
    {
        private String _id;
        private String _symbol;
        private String _exchange;
        private decimal _price;
        private decimal _avgerageExecutionPrice;
        private String _type;
        private String _timestamp;
        private bool _isLive;
        private bool _isCancelled;
        private bool _wasForced;
        private decimal _executedAmount;
        private decimal _remainingAmount;
        private decimal _originalAmount;

        [JsonProperty("id")]
        public String Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [JsonProperty("exchange")]
        public string Exchange
        {
            get { return _exchange; }
            set { _exchange = value; }
        }

        [JsonProperty("symbol")]
        public string Symbol
        {
            get { return _symbol; }
            set { _symbol = value; }
        }

        [JsonProperty("price")]
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        [JsonProperty("avg_execution_price")]
        public decimal AvgerageExecutionPrice
        {
            get { return _avgerageExecutionPrice; }
            set { _avgerageExecutionPrice = value; }
        }

        [JsonProperty("type")]
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        [JsonProperty("timestamp")]
        public String Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        [JsonProperty("is_live")]
        public bool IsLive
        {
            get { return _isLive; }
            set { _isLive = value; }
        }

        [JsonProperty("is_cancelled")]
        public bool IsCancelled
        {
            get { return _isCancelled; }
            set { _isCancelled = value; }
        }

        [JsonProperty("was_forced")]
        public bool WasForced
        {
            get { return _wasForced; }
            set { _wasForced = value; }
        }

        [JsonProperty("executed_amount")]
        public decimal ExecutedAmount
        {
            get { return _executedAmount; }
            set { _executedAmount = value; }
        }

        [JsonProperty("remaining_amount")]
        public decimal RemainingAmount
        {
            get { return _remainingAmount; }
            set { _remainingAmount = value; }
        }

        [JsonProperty("original_amount")]
        public decimal OriginalAmount
        {
            get { return _originalAmount; }
            set { _originalAmount = value; }
        }
    }
}
