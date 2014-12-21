using IntTrader.API.Base.Exchange;
using IntTrader.ViewModel;

namespace IntTrader.Service
{
    public class ApplicationSettingsService : ExchangeManagerViewModel
    {
        public ApplicationSettingsService(ExchangeManager exchangeManager)
            : base(exchangeManager)
        {
        }

    }
}
