using System;
using System.Collections.Generic;
using System.Linq;
using IntTrader.API.Exceptions;

namespace IntTrader.API.Currency
{

    /// <summary>
    /// Every Exchange has an instance of this <see cref="PairManager"/> it gives access to Pairs which are Available in this Application and should provide pairs which the exchange supports itself.
    /// </summary>
    public class PairManager
    {
        /// <summary>
        /// These should be loaded from JSON File in Future
        /// </summary>
        private List<CurrencyBase> _supportedCurrencies = new List<CurrencyBase>
            {
                CurrencyBase.EUR,CurrencyBase.USD,
                CurrencyBase.BTC,CurrencyBase.LTC,CurrencyBase.DRK,CurrencyBase.DGE,CurrencyBase.NMC, CurrencyBase.XRP
            };

        private Dictionary<String, PairBase> _supportedPairs = new Dictionary<string, PairBase>();
        private Dictionary<String, String> _supportedPairsReverse = new Dictionary<string, string>();

        /// <summary>
        /// These should be loaded from JSON File in Future
        /// </summary>
        private Dictionary<String, PairBase> _availablePairs = new Dictionary<string, PairBase>
            {
                {PairBase.BTCEUR,new PairBase{Key = PairBase.BTCEUR,Description = "BTC/EUR", LeftCurrency = CurrencyBase.BTC, RightCurrency = CurrencyBase.EUR}},
                {PairBase.BTCUSD,new PairBase{Key = PairBase.BTCUSD,Description = "BTC/USD", LeftCurrency = CurrencyBase.BTC, RightCurrency = CurrencyBase.USD}},

                {PairBase.LTCEUR,new PairBase{Key = PairBase.LTCEUR,Description = "LTC/EUR", LeftCurrency = CurrencyBase.LTC, RightCurrency = CurrencyBase.EUR}},
                {PairBase.LTCUSD,new PairBase{Key = PairBase.LTCUSD,Description = "LTC/USD", LeftCurrency = CurrencyBase.LTC, RightCurrency = CurrencyBase.USD}},
                                                                   
                {PairBase.LTCBTC,new PairBase{Key = PairBase.LTCBTC,Description = "LTC/BTC", LeftCurrency = CurrencyBase.LTC, RightCurrency = CurrencyBase.BTC}},
                {PairBase.BTCLTC,new PairBase{Key = PairBase.BTCLTC,Description = "BTC/LTC", LeftCurrency = CurrencyBase.BTC, RightCurrency = CurrencyBase.LTC}},
                                                                   
                {PairBase.DRKUSD,new PairBase{Key = PairBase.DRKUSD,Description = "DRK/USD", LeftCurrency = CurrencyBase.DRK, RightCurrency = CurrencyBase.USD}},
                {PairBase.DRKBTC,new PairBase{Key = PairBase.DRKBTC,Description = "DRK/BTC", LeftCurrency = CurrencyBase.DRK, RightCurrency = CurrencyBase.BTC}},

                {PairBase.BTCXRP,new PairBase{Key = PairBase.BTCXRP,Description = "BTC/XRP", LeftCurrency = CurrencyBase.BTC, RightCurrency = CurrencyBase.XRP}},
            };

        public List<CurrencyBase> SupportedCurrencies
        {
            get { return _supportedCurrencies; }
            protected set { _supportedCurrencies = value; }
        }

        public Dictionary<string, PairBase> SupportedPairs
        {
            get { return _supportedPairs; }
            protected set { _supportedPairs = value; }
        }

        public Dictionary<string, PairBase> AvailablePairs
        {
            get { return _availablePairs; }
            protected set { _availablePairs = value; }
        }

        /// <summary>
        /// This should only be used with the predefined keys.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pairKey"></param>
        public void AddSupportedPair(String key, String pairKey)
        {
            _supportedPairs[key] = _availablePairs[key];
            _supportedPairs[key].Name = pairKey;
            _supportedPairsReverse[pairKey] = key;
        }

        public void RemoveSupportedPair(String key)
        {
            _supportedPairs.Remove(key);
        }

        /// <summary>
        /// Finds the right pair by a naming chosen by the target API.
        /// This is currently only used when a new <see cref="PairBase"/> is created with the key only
        /// </summary>
        /// <param name="key">The more or less official naming for the pair e.g. BTCUSD, BTCEUR</param>
        /// <returns>The pair if it's supported</returns>
        /// <exception cref="PairNotSupportedException"></exception>
        public PairBase GetPair(String key)
        {
            key = key.ToUpperInvariant();

            if (_supportedPairsReverse.ContainsKey(key))
            {
                key = _supportedPairsReverse[key];
            }

            if (_supportedPairs.ContainsKey(key))
            {
                return _supportedPairs[key];
            }
            throw new PairNotSupportedException() { PairKey = key };
        }

        /// <summary>
        /// Finds the right Currency by the official naming. The most API's should match ISO 4217. If not it is possible to add another alternative key here
        /// </summary>
        /// <param name="key">The currency naming in three letters most likely ISO 4217 e.g. USD, EUR, CNY</param>
        /// <returns>The currency which matches the three letter naming</returns>
        /// <exception cref="CurrencyNotSupportedException"></exception>
        public CurrencyBase GetCurrency(String key)
        {
            key = key.ToUpperInvariant();
            // I have implemented two keys, because crypto currencies are not official yet and there are some inconsistencies e.g. Kraken (XBT) differs from Bitfinex (BTC)
            var currency = SupportedCurrencies.FirstOrDefault(x => x.Key == key || x.AlternativeKey == key);
            if (default(CurrencyBase) == currency)
            {
                throw new CurrencyNotSupportedException { CurrencyKey = key };
            }
            return currency;
        }
    }
}
