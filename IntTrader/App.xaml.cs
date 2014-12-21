using System.Windows;
using IntTrader.Settings;

namespace IntTrader
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {

        public static AppSettings Settings = new AppSettings();
        public App()
        {
            Settings.Load();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Settings.Save();
            base.OnExit(e);
        }
    }
}
