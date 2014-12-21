using System;
using IntTrader.API.Base.Exchange;
using IntTrader.Controls.ExchangeSettings;
using IntTrader.ViewModel;

namespace IntTrader.Controls.CommandToolBar
{
    public class CommandToolBarViewModel : ExchangeManagerViewModel
    {
        public CommandToolBarViewModel(ExchangeManager exchangeManager, ExchangeSettingsViewModel settings)
            : base(exchangeManager)
        {
            this.Settings = settings;
            Settings.SettingsService.SettingsLoaded += SettingsServiceOnSettingsLoaded;
            Settings.SettingsService.SettingsUnloaded += SettingsServiceOnSettingsUnloaded;
        }

        private void SettingsServiceOnSettingsUnloaded(object sender, EventArgs eventArgs)
        {
            OnPropertyChanged("SettingsUnlocked");
        }

        private void SettingsServiceOnSettingsLoaded(object sender, EventArgs eventArgs)
        {
            OnPropertyChanged("SettingsUnlocked");
        }

        public ExchangeSettingsViewModel Settings { get; set; }

        public bool SettingsUnlocked
        {
            get { return ExchangeManager.SettingsUnlocked(); }
        }
    }
}
