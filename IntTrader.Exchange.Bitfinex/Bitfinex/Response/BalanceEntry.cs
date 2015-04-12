using System;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Transform;
using Newtonsoft.Json;

namespace IntTrader.API.Exchange.Bitfinex.Response
{
    public class BalanceEntry : IBalanceEntry
    {
        //A list of wallet balances:
        //type (string): "trading", "deposit" or "exchange".
        //currency (string): Currency
        //amount (decimal): How much balance of this currency in this wallet
        //available (decimal): How much X there is in this wallet that is available to trade.

        private String _walletType;
        private String _currency;
        private decimal _amount;
        private decimal _available;

        [JsonProperty("type")]
        public string WalletType
        {
            get { return _walletType; }
            set { _walletType = value; }
        }

        [JsonProperty("currency")]
        public string Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

        [JsonProperty("amount")]
        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        [JsonProperty("available")]
        public decimal Available
        {
            get { return _available; }
            set { _available = value; }
        }

        public BalanceEntryModel Transform(ExchangeBase exchange)
        {
            return new BalanceEntryModel
                {
                    Amount = Amount,
                    CurrencyKey = Currency,
                    Available = Available,
                    WalletType = WalletType,
                    Currency = exchange.PairManager.GetCurrency(Currency)
                };
        }
    }
}
