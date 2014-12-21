using IntTrader.API.Base.Model;

namespace IntTrader.API.Base.Transform
{
    public interface IOrderNew
    {
        CreateOrderModel Transform();
    }
}
