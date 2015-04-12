using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;

namespace IntTrader.API.Base.Transform
{
    public interface IBalance
    {
        BalanceModel Transform(ExchangeBase exchange);
    }
}
