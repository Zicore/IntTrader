using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using Zicore.Security.Cryptography;

namespace IntTrader.Dialogs.Password
{
    /// <summary>
    /// Interaktionslogik für CreatePassword.xaml
    /// </summary>
    public partial class CreatePassword : Window
    {
        public CreatePassword()
        {
            InitializeComponent();
            var vm = DataContext as CreatePasswordViewModel;
            if (vm != null)
            {
                vm.RequestCloseEvent += (sender, args) => Close();
            }
        }

        readonly SHA256Managed _sha256 = new SHA256Managed();

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Password_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var pBox = sender as PasswordBox;
            if (pBox != null)
            {
                var vm = DataContext as CreatePasswordViewModel;
                if (vm != null)
                {
                    vm.Password = Hash.TextToHexStringHash(pBox.Password, _sha256, 256);
                }
            }
        }

        private void PasswordRepeat_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var pBox = sender as PasswordBox;
            if (pBox != null)
            {
                var vm = DataContext as CreatePasswordViewModel;
                if (vm != null)
                {
                    vm.PasswordRepeat = Hash.TextToHexStringHash(pBox.Password, _sha256, 256);
                }
            }
        }
    }
}
