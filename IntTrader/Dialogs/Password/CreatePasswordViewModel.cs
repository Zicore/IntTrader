using System;
using IntTrader.ViewModel;
using Zicore.WPF.Base.Commands;

namespace IntTrader.Dialogs.Password
{
    public class CreatePasswordViewModel : ViewModelBase
    {
        private String _password;

        public String Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        private String _passwordRepeat;

        public String PasswordRepeat
        {
            get { return _passwordRepeat; }
            set
            {
                _passwordRepeat = value;
                OnPropertyChanged("PasswordRepeat");
            }
        }

        private RelayCommand _applyCommand;

        public event EventHandler Close;

        protected virtual void OnClose()
        {
            EventHandler handler = Close;
            if (handler != null) handler(this, EventArgs.Empty);

        }

        public RelayCommand ApplyCommand
        {
            get
            {
                if (_applyCommand == null)
                {
                    _applyCommand = new RelayCommand(x => Apply(), x => CanExecute());
                }
                return _applyCommand;
            }
        }

        public void Apply()
        {
            OnClose();
            OnRequestClose();
        }

        public bool CanExecute()
        {
            return !string.IsNullOrEmpty(Password) && Password == PasswordRepeat;
        }
    }
}
