using System.Collections.Generic;
using IntTrader.API.Base.Exchange.Base;
using Zicore.Settings.Json;

namespace IntTrader.API.Base.Settings
{
    public class SettingsBase : JsonSettingsEncrypted
    {
        List<ExchangeBase> _exchanges = new List<ExchangeBase> { };

        public List<ExchangeBase> Exchanges
        {
            get { return _exchanges; }
            set
            {
                foreach (var exchangeBase in value)
                {
                    foreach (var exchange in Exchanges)
                    {
                        if (exchange.Name == exchangeBase.Name)
                        {
                            exchange.ExchangeAPI = exchangeBase.ExchangeAPI;
                        }
                    }
                }
            }
        }

        public static string ApplicationName = "IntTrader";
        public static string FileName = "ExchangeSettings.aes.json";
    }
}
