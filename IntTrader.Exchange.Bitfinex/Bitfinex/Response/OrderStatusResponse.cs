using System;
using Newtonsoft.Json;

namespace IntTrader.API.Exchange.Bitfinex.Response
{
    public class OrderStatusResponse : BitfinexResponse
    {
        //symbol (string): The symbol name the order belongs to.
        //exchange (string): "bitfinex", "bitstamp".
        //price (decimal): The price the order was issued at (can be null for market orders).
        //avg_execution_price (decimal): The average price at which this order as been executed so far. 0 if the order has not been executed at all. side (string): Either "buy" or "sell".
        //type (string): Either "market" / "limit" / "stop" / "trailing-stop".
        //timestamp (time): The timestamp the order was submitted.
        //is_live (bool): Could the order still be filled?
        //is_cancelled (bool): Has the order been cancelled?
        //was_forced (bool): For margin only: true if it was forced by the system.
        //executed_amount (decimal): How much of the order has been executed so far in its history?
        //remaining_amount (decimal): How much is still remaining to be submitted?
        //original_amount (decimal): What was the order originally submitted for?
        //Active Orders

        private String _symbol;
        private String _exchange;
        private decimal _price;
        private decimal _averageExecutionPrice;
        private String _type;
        private String _timestamp;
        private bool _isLive;
        private bool _isCancelled;
        private bool _wasForced;
        private decimal _executedAmount;
        private decimal _remainingAmount;
        private decimal _originalAmount;

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
        public decimal AverageExecutionPrice
        {
            get { return _averageExecutionPrice; }
            set { _averageExecutionPrice = value; }
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
