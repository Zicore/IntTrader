using IntTrader.API.Base.Model;

namespace IntTrader.API.Base.Transform
{
    public interface ICreateOrder
    {
        CreateOrderModel Transform();
    }
}
