using IntTrader.API.Base.Exchange.Base;
using IntTrader.Service;

namespace IntTrader.ViewModel
{

    /// <summary>
    /// Every View model which is Exchange specific should inherit this
    /// </summary>
    public class ExchangeViewModelBase : ViewModelBase
    {
        public ExchangeViewModelBase(ExchangeBase exchangeBase)
        {
            this.Exchange = exchangeBase;
            UpdateController = new UpdateController();
        }

        public ExchangeBase Exchange { get; set; }

        public UpdateController UpdateController { get; private set; }
    }
}
