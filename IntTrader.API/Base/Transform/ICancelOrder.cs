using IntTrader.API.Base.Model;

namespace IntTrader.API.Base.Transform
{
    public interface ICancelOrder
    {
        CancelOrderModel Transform();
    }
}
