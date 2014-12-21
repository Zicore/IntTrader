using IntTrader.API.Base.Settings;
using IntTrader.ViewModel;

namespace IntTrader.Controls.ExchangeSettings
{
    public class ExchangeSettingsEntryViewModel : ViewModelBase
    {
        private ExchangeAPI _exchangeAPI;
        public ExchangeAPI ExchangeAPI
        {
            get { return _exchangeAPI; }
            set { _exchangeAPI = value; }
        }

        public string Name
        {
            get { return _exchangeAPI.Name; }
            set
            {
                _exchangeAPI.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public string APIKey
        {
            get { return _exchangeAPI.APIKey; }
            set
            {
                _exchangeAPI.APIKey = value;
                OnPropertyChanged("APIKey");
            }
        }

        public string APISecret
        {
            get { return "*****"; }
            set
            {
                _exchangeAPI.APISecret = value;
                OnPropertyChanged("APISecret");
            }
        }


    }
}
