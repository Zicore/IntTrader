using IntTrader.API.Base.Exchange;

namespace IntTrader.ViewModel
{
    /// <summary>
    /// When it is not specific to one exchange, it should implement <see cref="ExchangeManagerViewModel"/>
    /// </summary>
    public class ExchangeManagerViewModel : ViewModelBase
    {
        public ExchangeManagerViewModel(ExchangeManager exchangeManager)
        {
            this.ExchangeManager = exchangeManager;
        }

        public ExchangeManager ExchangeManager { get; set; }
    }
}
