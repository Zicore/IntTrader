using System.ComponentModel;
using System.Windows;
using IntTrader.ViewModel;

namespace IntTrader.View
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            var m = this.DataContext as MainViewModel;
            if (m != null)
            {
                m.RequestClose();
            }
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var m = this.DataContext as MainViewModel;
            if (m != null)
            {
                m.OnLoaded();
            }
        }
    }
}
