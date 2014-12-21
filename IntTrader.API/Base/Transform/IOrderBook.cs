using IntTrader.API.Base.Model;

namespace IntTrader.API.Base.Transform
{
    public interface IOrderBook
    {
        OrderBookModel Transform();
    }
}
