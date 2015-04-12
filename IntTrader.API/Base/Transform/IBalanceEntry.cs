using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;

namespace IntTrader.API.Base.Transform
{
    public interface IBalanceEntry
    {
        BalanceEntryModel Transform(ExchangeBase exchange);
    }
}
