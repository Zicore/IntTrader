using IntTrader.API.Base.Model;
using IntTrader.API.Base.Transform;
using IntTrader.API.Converter;

namespace IntTrader.API.Exchange.Bitfinex.Response
{
    public class OrderResponse : OrderResponseBase, IOrder
    {
        //id (int)
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

        public OpenOrderEntryModel Transform()
        {
            return new OpenOrderEntryModel
            {
                AverageExecutionPrice = AvgerageExecutionPrice,
                Exchange = Exchange,
                ExecutedAmount = ExecutedAmount,
                IsCancelled = IsCancelled,
                IsLive = IsLive,
                OrderId = Id,
                OriginalAmount = OriginalAmount,
                Price = Price,
                RemainingAmount = RemainingAmount,
                Symbol = Symbol,
                DateTime = DateTimeConverter.ConvertTimestamp(Timestamp),
                Type = Type,
                WasForced = WasForced
            };
        }
    }
}
