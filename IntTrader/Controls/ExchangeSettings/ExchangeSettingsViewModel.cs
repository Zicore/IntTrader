using System;
using System.Collections.ObjectModel;
using IntTrader.API.Base.Exchange;
using IntTrader.Service;
using IntTrader.ViewModel;
using Zicore.WPF.Base.Commands;

namespace IntTrader.Controls.ExchangeSettings
{
    public class ExchangeSettingsViewModel : ExchangeManagerViewModel
    {
        public ExchangeSettingsViewModel(ExchangeManager exchangeManager)
            : base(exchangeManager)
        {
            Header = "API Keys";
            SettingsService = new ExchangeSettingsService(exchangeManager);
            SettingsService.SettingsLoaded += SettingsServiceOnSettingsLoaded;
            SettingsService.SettingsUnloaded += SettingsServiceOnSettingsUnloaded;
        }

        public ExchangeSettingsService SettingsService { get; set; }

        private RelayCommand _changePasswordCommand;
        public RelayCommand ChangePasswordCommand
        {
            get
            {
                if (_changePasswordCommand == null)
                {
                    _changePasswordCommand = new RelayCommand(x => ChangePassword());
                }
                return _changePasswordCommand;
            }
        }


        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(x => Save());
                }
                return _saveCommand;
            }
        }

        private RelayCommand _loadCommand;
        public RelayCommand LoadCommand
        {
            get
            {
                if (_loadCommand == null)
                {
                    _loadCommand = new RelayCommand(x => Load());
                }
                return _loadCommand;
            }
        }

        readonly ObservableCollection<ExchangeSettingsEntryViewModel> _exchangeSettings = new ObservableCollection<ExchangeSettingsEntryViewModel>();
        public ObservableCollection<ExchangeSettingsEntryViewModel> ExchangeSettings
        {
            get { return _exchangeSettings; }
        }


        private void UpdateSettings()
        {
            _exchangeSettings.Clear();
            if (ExchangeManager.SettingsUnlocked())
                foreach (var setting in ExchangeManager.Settings.Exchanges)
                {
                    var exchange = new ExchangeSettingsEntryViewModel()
                    {
                        ExchangeAPI = setting.ExchangeAPI
                    };
                    _exchangeSettings.Add(exchange);
                }
            OnPropertyChanged("SettingsUnlocked");
        }

        private void SettingsServiceOnSettingsUnloaded(object sender, EventArgs eventArgs)
        {
            UpdateSettings();
            OnPropertyChanged("SettingsUnlocked");
        }

        public void Load()
        {
            if (!ExchangeManager.SettingsUnlocked())
            {

                SettingsService.LoadSettings();
            }
            else
            {
                SettingsService.UnloadSettings();
            }
        }

        private void ChangePassword()
        {
            if (ExchangeManager.SettingsUnlocked())
            {
                SettingsService.ChangePassword();
            }
            else
            {
                SettingsService.LoadSettings();
            }
        }

        public bool SettingsUnlocked
        {
            get { return ExchangeManager.SettingsUnlocked(); }
        }

        private void SettingsServiceOnSettingsLoaded(object sender, EventArgs eventArgs)
        {
            UpdateSettings();
        }

        public bool CanLoad()
        {
            return ExchangeManager.SettingsUnlocked();
        }

        public void Save()
        {
            if (ExchangeManager.SettingsUnlocked())
            {
                ExchangeManager.SaveSettings();
            }
        }
    }
}
