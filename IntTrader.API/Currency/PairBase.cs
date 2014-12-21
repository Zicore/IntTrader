using System;

namespace IntTrader.API.Currency
{
    public class PairBase
    {
        public static readonly String LTCEUR = "LTCEUR";
        public static readonly String LTCUSD = "LTCUSD";

        public static readonly String BTCEUR = "BTCEUR";
        public static readonly String BTCUSD = "BTCUSD";

        public static readonly String DRKUSD = "DRKUSD";
        public static readonly String DRKEUR = "DRKEUR";

        public static readonly String DRKBTC = "DRKBTC";
        public static readonly String LTCBTC = "LTCBTC";

        public static readonly String BTCLTC = "BTCLTC";


        public PairBase()
        {

        }

        private String _name;
        private String _description;
        private String _key;

        private CurrencyBase _leftCurrency;
        private CurrencyBase _rightCurrency;


        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public CurrencyBase LeftCurrency
        {
            get { return _leftCurrency; }
            set { _leftCurrency = value; }
        }

        public CurrencyBase RightCurrency
        {
            get { return _rightCurrency; }
            set { _rightCurrency = value; }
        }

        public String NamePair
        {
            get { return String.Format("{0}/{1}", LeftCurrency.Name, RightCurrency.Name); }
        }

        public String SymbolPair
        {
            get { return String.Format("{0}/{1}", LeftCurrency.Symbol, RightCurrency.Symbol); }
        }
    }
}
