using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using Zicore.Security.Cryptography;

namespace IntTrader.Dialogs.Password
{
    /// <summary>
    /// Interaktionslogik für Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            var vm = DataContext as LoginViewModel;
            if (vm != null)
            {
                vm.RequestCloseEvent += (sender, args) => Close();
            }
        }

        readonly SHA256Managed _sha256 = new SHA256Managed();

        private void Password_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var pBox = sender as PasswordBox;
            if (pBox != null)
            {
                var vm = DataContext as LoginViewModel;
                if (vm != null)
                {
                    vm.Password = Hash.TextToHexStringHash(pBox.Password, _sha256, 256);
                }
            }
        }
    }
}
