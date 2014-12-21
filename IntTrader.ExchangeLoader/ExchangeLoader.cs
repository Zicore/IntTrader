using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using IntTrader.API.Base.Exchange;
using IntTrader.API.Base.Exchange.Base;

namespace IntTrader.API.ExchangeLoader
{
    public class ExchangeLoader
    {
        private static bool _pluginsLoaded = false;

        public ExchangeLoader()
        {

        }

        public void LoadExchanges(ExchangeManager exchangeManager)
        {
            if (!_pluginsLoaded)
            {
                var lookupPaths = new string[]
                    {
                        AppDomain.CurrentDomain.BaseDirectory,
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Exchanges"),
                    };
                foreach (var lookupPath in lookupPaths)
                {
                    if (Directory.Exists(lookupPath))
                    {
                        var plugins = Directory.GetFiles(lookupPath, "*.Exchange.dll", SearchOption.TopDirectoryOnly);
                        foreach (var plugin in plugins)
                        {
                            try
                            {

                                var asm = Assembly.LoadFile(plugin);
                                foreach (Type type in asm.GetTypes())
                                {
                                    try
                                    {
                                        if (type.BaseType == typeof(ExchangeBase))
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
                                                exchangeManager.AddExchange(instance);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Debug.Write(ex);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.Write(ex);
                            }
                        }
                    }
                }
                _pluginsLoaded = true;
            }
        }
    }
}
