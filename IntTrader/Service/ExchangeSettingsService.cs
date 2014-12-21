using System;
using System.ComponentModel;
using IntTrader.API.Base.Exchange;
using IntTrader.Dialogs.Password;
using IntTrader.View;
using IntTrader.ViewModel;

namespace IntTrader.Service
{
    public class ExchangeSettingsService : ExchangeManagerViewModel
    {
        public ExchangeSettingsService(ExchangeManager exchangeManager)
            : base(exchangeManager)
        {
        }

        public event EventHandler SettingsLoaded;
        public event EventHandler SettingsUnloaded;

        protected virtual void OnSettingsUnloaded()
        {
            EventHandler handler = SettingsUnloaded;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnSettingsLoaded()
        {
            EventHandler handler = SettingsLoaded;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void LoadSettings()
        {
            if (ExchangeManager.SettingsAvailable())
            {
                var login = new Login();
                login.Closing += LoginOnClosing;
                login.Show();
                login.Activate();
            }
            else
            {
                var createPassword = new CreatePassword();
                createPassword.Closing += CreatePasswordOnClosing;
                createPassword.Show();
                createPassword.Activate();
            }
        }

        public void UnloadSettings()
        {
            ExchangeManager.UnloadSettings();
            OnSettingsUnloaded();
        }

        public void ChangePassword()
        {
            if (ExchangeManager.SettingsUnlocked())
            {
                var createPassword = new CreatePassword();
                createPassword.Closing += CreatePasswordOnClosing;
                createPassword.Show();
                createPassword.Activate();
            }
        }

        private void LoginOnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            var dialog = sender as Login;
            if (dialog != null)
            {
                var vm = dialog.DataContext as LoginViewModel;
                if (vm != null)
                {
                    if (vm.DialogResult == DialogResult.OK)
                    {
                        try
                        {
                            ExchangeManager.LoadSettings(vm.Password);
                            OnSettingsLoaded();
                        }
                        catch
                        {

                        }
                    }
                    if (vm.DialogResult == DialogResult.Cancel)
                    {
                        dialog.Closing -= LoginOnClosing;
                    }
                }
            }
        }

        private void CreatePasswordOnClosing(object sender, CancelEventArgs e)
        {
            var dialog = sender as CreatePassword;
            if (dialog != null)
            {
                var vm = dialog.DataContext as CreatePasswordViewModel;
                if (vm != null)
                {
                    ExchangeManager.ChangePassword(vm.Password);
                    OnSettingsLoaded();
                }

                dialog.Closing -= CreatePasswordOnClosing;
            }
        }

    }
}
