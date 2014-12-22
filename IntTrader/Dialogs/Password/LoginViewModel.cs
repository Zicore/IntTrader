using System;
using System.Windows;
using IntTrader.ViewModel;
using Zicore.WPF.Base.Commands;

namespace IntTrader.Dialogs.Password
{
    public enum DialogResult
    {
        None,
        OK,
        Cancel
    }

    public class LoginViewModel : ViewModelBase
    {
        private DialogResult _dialogResult = DialogResult.None;
        public DialogResult DialogResult
        {
            get { return _dialogResult; }
            set { _dialogResult = value; }
        }

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

        private RelayCommand _applyCommand;
        private RelayCommand _cancelCommand;

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
                    _applyCommand = new RelayCommand(x => Apply(), x => CanApply());
                }
                return _applyCommand;
            }
        }

        public RelayCommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(x => Cancel());
                }
                return _cancelCommand;
            }
        }


        private Visibility _passwordWrongVisibility = Visibility.Collapsed;
        public Visibility PasswordWrongVisibility
        {
            get { return _passwordWrongVisibility; }
            set
            {
                _passwordWrongVisibility = value;
                OnPropertyChanged("PasswordWrongVisibility");
            }
        }

        public void Apply()
        {
            this.DialogResult = DialogResult.OK;
            OnClose();
            OnRequestClose();
        }

        public void Cancel()
        {
            this.DialogResult = DialogResult.Cancel;
            OnRequestClose();
        }

        public bool CanApply()
        {
            return !String.IsNullOrEmpty(Password);
        }
    }
}
