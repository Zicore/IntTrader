using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IntTrader.Dialogs.OrderNotifications
{
    /// <summary>
    /// Interaktionslogik für OrderNotificationView.xaml
    /// </summary>
    public partial class OrderNotificationWindow : Window
    {
        public OrderNotificationWindow()
        {
            InitializeComponent();
        }

        private void OrderNotificationWindow_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Collapsed;
        }
    }
}
