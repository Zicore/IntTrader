using IntTrader.API.Base.Exchange.Base;

namespace IntTrader.API.Base.Exchange.Orders
{
    public class OrderType : TypeBase
    {
        /// <summary>
        /// Creates an <see cref="OrderType"/>for the communication with the API's
        /// </summary>
        /// <param name="value">The value for the API</param>
        /// <param name="name">The name for the presentation layer</param>
        public OrderType(string value, string name)
            : base(value, name)
        {
        }

    }
}
