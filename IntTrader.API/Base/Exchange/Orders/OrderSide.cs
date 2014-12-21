using IntTrader.API.Base.Exchange.Base;

namespace IntTrader.API.Base.Exchange.Orders
{
    public class OrderSide : TypeBase
    {
        public static readonly OrderSide Buy = new OrderSide("buy", "Buy");
        public static readonly OrderSide Sell = new OrderSide("sell", "Sell");

        public OrderSide(string value, string name)
            : base(value, name)
        {
        }
    }
}
