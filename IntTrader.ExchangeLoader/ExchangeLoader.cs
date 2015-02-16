using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using IntTrader.API.Base.Exchange;
using IntTrader.API.Base.Exchange.Base;
using NLog;

namespace IntTrader.API.ExchangeLoader
{
    public static class ExchangeLoader
    {
        private static bool _pluginsLoaded = false;
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public static void LoadExchanges(String basePath, ExchangeManager exchangeManager)
        {
            var lookupPaths = new string[]
                {
                    basePath,
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Exchanges"),
                };
            foreach (var lookupPath in lookupPaths)
            {
                if (Directory.Exists(lookupPath))
                {
                    var exchanges = Directory.GetFiles(lookupPath, "*.Exchange.dll", SearchOption.TopDirectoryOnly);
                    foreach (var exchange in exchanges)
                    {
                        LoadAssemblyByName(exchangeManager, exchange);
                    }
                }
            }
        }

        private static void LoadAssemblyByName(ExchangeManager exchangeManager, String assembly)
        {
            var asm = Assembly.LoadFile(assembly);
            foreach (Type type in asm.GetTypes())
            {
                try
                {
                    if (type.BaseType == typeof(ExchangeBase))
                    {
                        CreateExchange(exchangeManager, asm, type);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
        }

        private static void CreateExchange(ExchangeManager exchangeManager, Assembly asm, Type type)
        {
            if (!exchangeManager.LoadedExchanges.Contains(type.FullName))
            {
                var instance =
                    asm.CreateInstance(type.FullName, false, BindingFlags.CreateInstance,
                                       null,
                                       new object[] { exchangeManager },
                                       CultureInfo.CurrentCulture,
                                       null)
                    as ExchangeBase;
                if (instance != null)
                {
                    Log.Info("Exchange {0} loaded", instance.Name);
                    exchangeManager.AddExchange(instance);
                }
            }
        }
    }
}
