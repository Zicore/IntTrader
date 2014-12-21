using System;

namespace IntTrader.API.Currency
{
    public class CurrencyBase
    {
        public static readonly CurrencyBase EUR = new CurrencyBase { Key = "EUR", AlternativeKey = "ZEUR", Name = "Euro", Symbol = "€", Decimals = 2 };
        public static readonly CurrencyBase USD = new CurrencyBase { Key = "USD", AlternativeKey = "ZUSD", Name = "US-Dollar", Symbol = "$", Decimals = 2 };
        public static readonly CurrencyBase BTC = new CurrencyBase { Key = "BTC", AlternativeKey = "XXBT", Name = "Bitcoin", Symbol = "Ƀ", Decimals = 8, IsCryptoCurrency = true };
        public static readonly CurrencyBase LTC = new CurrencyBase { Key = "LTC", AlternativeKey = "XLTC", Name = "Litecoin", Symbol = "Ł", Decimals = 8, IsCryptoCurrency = true };
        public static readonly CurrencyBase DRK = new CurrencyBase { Key = "DRK", AlternativeKey = "XDRK", Name = "Darkcoin", Symbol = "D", Decimals = 8, IsCryptoCurrency = true };
        public static readonly CurrencyBase DGE = new CurrencyBase { Key = "DGE", AlternativeKey = "XXDG", Name = "Dogecoin", Symbol = "Dog", Decimals = 8, IsCryptoCurrency = true };
        public static readonly CurrencyBase NMC = new CurrencyBase { Key = "NMC", AlternativeKey = "XNMC", Name = "Namecoin", Symbol = "N", Decimals = 8, IsCryptoCurrency = true };

        private bool _isCryptoCurrency = false;
        private String _name;
        private String _key;
        private String _alternativeKey;

        private String _symbol;
        private int _decimals = 2;

        public CurrencyBase()
        {
            UpdateFormatString();
        }

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public string Symbol
        {
            get { return _symbol; }
            set { _symbol = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string AlternativeKey
        {
            get { return _alternativeKey; }
            set { _alternativeKey = value; }
        }

        public bool IsCryptoCurrency
        {
            get { return _isCryptoCurrency; }
            set { _isCryptoCurrency = value; }
        }

        public int Decimals
        {
            get { return _decimals; }
            set
            {
                if (_decimals != value)
                {
                    _decimals = value;
                    UpdateFormatString();
                }
            }
        }

        private String _formatString = "";

        public void UpdateFormatString()
        {
            String decimalsFormat = "";
            for (int i = 2; i < Decimals; i++)
            {
                decimalsFormat += "#";
            }
            _formatString = String.Format("{{0:0.00{0}}}", decimalsFormat);
        }

        public String FormatDecimals(decimal value)
        {
            return String.Format(_formatString, value);
        }

        public override string ToString()
        {
            return Symbol;
        }
    }
}
