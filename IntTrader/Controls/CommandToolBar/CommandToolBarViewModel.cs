using System;
using IntTrader.API.Base.Exchange;
using IntTrader.Controls.ExchangeSettings;
using IntTrader.ViewModel;
using Zicore.WPF.Base.Commands;

namespace IntTrader.Controls.CommandToolBar
{
    public class CommandToolBarViewModel : ExchangeManagerViewModel
    {
        public CommandToolBarViewModel(MainViewModel mainViewModel, ExchangeManager exchangeManager, ExchangeSettingsViewModel settings)
            : base(exchangeManager)
        {
            MainViewModel = mainViewModel;
            this.Settings = settings;
            Settings.SettingsService.SettingsLoaded += SettingsServiceOnSettingsLoaded;
            Settings.SettingsService.SettingsUnloaded += SettingsServiceOnSettingsUnloaded;
        }

        private RelayCommand _showNotificationsCommand;

        public RelayCommand ShowNotificationsCommand
        {
            get
            {
                if (_showNotificationsCommand == null)
                {
                    _showNotificationsCommand = new RelayCommand(x => MainViewModel.ShowNotifications());
                }
                return _showNotificationsCommand;
            }
        }

        private void SettingsServiceOnSettingsUnloaded(object sender, EventArgs eventArgs)
        {
            OnPropertyChanged("SettingsUnlocked");
        }

        private void SettingsServiceOnSettingsLoaded(object sender, EventArgs eventArgs)
        {
            OnPropertyChanged("SettingsUnlocked");
        }

        public MainViewModel MainViewModel { get; set; }
        public ExchangeSettingsViewModel Settings { get; set; }

        public bool SettingsUnlocked
        {
            get { return ExchangeManager.SettingsUnlocked(); }
        }
    }
}
