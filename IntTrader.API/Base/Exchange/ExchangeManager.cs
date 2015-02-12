using System;
using System.Collections.Generic;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Settings;
using IntTrader.API.Event;
using NLog;
using Zicore.Settings.Json;
using Zicore.WPF.Base.Event;

namespace IntTrader.API.Base.Exchange
{
    public class ExchangeManager
    {
        public ExchangeManager()
        {

        }

        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public static Dictionary<String, APIFunction> Commands { get; private set; }
        public static Dictionary<APIFunction, String> Functions { get; private set; }

        static ExchangeManager()
        {
            Functions = new Dictionary<APIFunction, String>
            {
                {APIFunction.RequestTicker    ,"ticker"     },
                {APIFunction.RequestOrderBook ,"orderbook"   },
                {APIFunction.RequestOpenOrders,"orders"      },
                {APIFunction.RequestNewOrder  ,"neworder"    },
                {APIFunction.RequestBalances  ,"balance"     },
                {APIFunction.CancelOrder      ,"cancelorder" },
            };
            Commands = new Dictionary<String, APIFunction>
            {
                {"ticker"     ,APIFunction.RequestTicker    },
                {"orderbook"  ,APIFunction.RequestOrderBook  },
                {"orders"     ,APIFunction.RequestOpenOrders },
                {"neworder"   ,APIFunction.RequestNewOrder   },
                {"balance"    ,APIFunction.RequestBalances   },
                {"cancelorder",APIFunction.CancelOrder       },
            };
        }

        public void AddExchange(ExchangeBase exchangeBase)
        {
            Exchanges.Add(exchangeBase);
            LoadedExchanges.Add(exchangeBase.GetType().FullName);
        }



        public event EventHandler<CreateOrderEventArgs> CreateOrderEvent;

        List<ExchangeBase> _exchanges = new List<ExchangeBase>();
        public List<ExchangeBase> Exchanges
        {
            get { return _exchanges; }
            private set { _exchanges = value; }
        }

        HashSet<String> _loadedExchanges = new HashSet<string>();

        public HashSet<string> LoadedExchanges
        {
            get { return _loadedExchanges; }
            private set { _loadedExchanges = value; }
        }

        public bool SettingsAvailable()
        {
            String path = JsonSettings.GetFilePath(SettingsBase.ApplicationName, SettingsBase.FileName);
            return JsonSettings.Exists(path);
        }

        public bool SettingsUnlocked()
        {
            return Settings != null && Settings.IsLoaded;
        }

        public void LoadSettings(String password)
        {
            Settings = new SettingsBase();

            foreach (var exchangeBase in Exchanges)
            {
                Settings.Exchanges.Add(exchangeBase);
            }

            Settings.SetKey(password);
            Settings.Load(SettingsBase.ApplicationName, SettingsBase.FileName);

            Log.Info("Settings Loaded");

            foreach (var e in Exchanges)
            {
                if (e.VerifyAPI())
                {
                    Log.Info("{0} Authenticated API ready!", e.Name);
                }

                foreach (var function in Commands.Values)
                {
                    if (e.IsAvailable(function))
                    {
                        Log.Info("{0}/{1}", e.Name, function);
                    }
                    else
                    {
                        Log.Info("{0}/{1} **NOT available**", e.Name, function);
                    }
                }
            }
        }

        public void ChangePassword(String password)
        {
            if (!SettingsAvailable() && !SettingsUnlocked())
            {
                Settings = new SettingsBase();
            }

            Settings.SetKey(password);
            Settings.FilePath = JsonSettings.GetFilePath(SettingsBase.ApplicationName, SettingsBase.FileName);
            Settings.Save();
        }

        public void SaveSettings()
        {
            Settings.Save();
        }

        public SettingsBase GetSettings()
        {
            return Settings;
        }

        public SettingsBase Settings;

        public void UnloadSettings()
        {
            Settings = null;
        }

        public void OnCreateOrderEvent(CreateOrderEventArgs e)
        {
            var handler = CreateOrderEvent;
            if (handler != null) handler(this, e);
        }
    }
}
