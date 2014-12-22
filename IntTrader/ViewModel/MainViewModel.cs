﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using IntTrader.API.Base.Exchange;
using IntTrader.API.ExchangeLoader;
using IntTrader.Controls.Blockchain.Address;
using IntTrader.Controls.CommandToolBar;
using IntTrader.Controls.Dashboard;
using IntTrader.Controls.Exchange;
using IntTrader.Controls.ExchangeSettings;
using IntTrader.Controls.Request;
using IntTrader.Controls.Trades;
using IntTrader.Service;

namespace IntTrader.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public static readonly DispatcherTimer TimerSeconds = new DispatcherTimer();
        ExchangeManager _exchangeManager = new ExchangeManager();

        public ExchangeManager ExchangeManager
        {
            get { return _exchangeManager; }
            private set { _exchangeManager = value; }
        }

        public CommandToolBarViewModel CommandToolBarViewModel { get; set; }
        public ObservableCollection<ViewModelBase> Tabs { get; set; }
        public ExchangeSettingsService SettingsService { get; set; }
        public ExchangeSettingsViewModel SettingsViewModel { get; set; }
        public ObservableCollection<ExchangeViewModel> Exchanges { get; set; }
        public DashboardViewModel DashboardViewModel { get; set; }
        public AddressViewModel AddressViewModel { get; set; }
        public RequestViewModel RequestViewModel { get; set; }


        public String WindowText
        {
            get { return String.Format("IntTrader - {0} - {1}", PrimaryTitle, SecondaryTitle); }
        }

        public ExchangeViewModel CurrentExchange { get; set; }

        public String PrimaryTitle
        {
            get
            {
                return SelectedTab.Header;
            }
        }

        public String SecondaryTitle
        {
            get
            {
                return !(SelectedTab is ExchangeViewModel) ? ExchangesTitle : CurrentExchange.Pairs.SelectedPair.Description;
            }
        }

        public String ExchangesTitle
        {
            get
            {
                var sb = new StringBuilder();
                for (int i = 0; i < Exchanges.Count; i++)
                {
                    sb.Append(Exchanges[i].Exchange.Name);
                    if (i < Exchanges.Count - 1)
                    {
                        sb.Append(" | ");
                    }
                }
                return sb.ToString();
            }
        }

        private ViewModelBase _selectedTab = null;
        public ViewModelBase SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                if (_selectedTab != value)
                {
                    _selectedTab = value;
                    if (_selectedTab is ExchangeViewModel)
                    {
                        CurrentExchange = _selectedTab as ExchangeViewModel;
                    }
                    OnPropertyChanged("SelectedTab");
                    OnPropertyChanged("WindowText");
                }
            }
        }

        public void UpdateWindowText()
        {
            OnPropertyChanged("WindowText");
        }

        public MainViewModel()
        {
            Tabs = new ObservableCollection<ViewModelBase>();
            SettingsViewModel = new ExchangeSettingsViewModel(_exchangeManager);
            SettingsService = new ExchangeSettingsService(_exchangeManager);

            CommandToolBarViewModel = new CommandToolBarViewModel(_exchangeManager, SettingsViewModel);
            Exchanges = new ObservableCollection<ExchangeViewModel>();



            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                RequestViewModel = new RequestViewModel(this, ExchangeManager);
                AddressViewModel = new AddressViewModel(_exchangeManager);

                var loader = new ExchangeLoader();
                loader.LoadExchanges(ExchangeManager);

                // Exchanges
                foreach (var exchangeBase in ExchangeManager.Exchanges)
                {
                    var exchangeViewModel = new ExchangeViewModel(this, exchangeBase);
                    Exchanges.Add(exchangeViewModel);
                }

                // Tabs
                DashboardViewModel = new DashboardViewModel(ExchangeManager, this);
                Tabs.Add(DashboardViewModel);

                foreach (var exchange in Exchanges)
                {
                    Tabs.Add(exchange);
                }

                Tabs.Add(AddressViewModel);
                Tabs.Add(RequestViewModel);
                Tabs.Add(SettingsViewModel);
                CreateTimer();
            }
        }

        public void RequestClose()
        {
            RequestHandler.IsEnabled = false;
        }

        private void CreateTimer()
        {
            TimerSeconds.Interval = new TimeSpan(0, 0, 0, 1);
            TimerSeconds.IsEnabled = true;
        }

        public void OnLoaded()
        {
            //throw new NotImplementedException();
        }
    }
}