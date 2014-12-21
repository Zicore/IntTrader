using System;
using System.Globalization;

namespace IntTrader.ViewModel
{
    public class PrefixSuffixEntry
    {
        public PrefixSuffixEntry()
        {

        }
        private string _prefix;
        private string _suffix;

        public string Prefix
        {
            get { return _prefix; }
            set { _prefix = value; }
        }

        public string Suffix
        {
            get { return _suffix; }
            set { _suffix = value; }
        }

        public static PrefixSuffixEntry CalculatePrice(decimal price)
        {
            var entry = new PrefixSuffixEntry();
            String strPrice = String.Format(CultureInfo.InvariantCulture, "{0:0.000000}", price);
            String strSuffix = "";
            while (strPrice.EndsWith("0"))
            {
                strPrice = strPrice.Remove(strPrice.Length - 1);
                strSuffix += "0";
            }
            entry.Suffix = strSuffix;
            entry.Prefix = strPrice;
            return entry;
        }

        public static PrefixSuffixEntry CalculateAmount(decimal amount)
        {
            var entry = new PrefixSuffixEntry();
            decimal fraction = amount - Math.Floor(amount);

            entry.Prefix = String.Format(CultureInfo.InvariantCulture, "{0:F0}", amount);
            int fractions = 5 - entry.Prefix.Length;
            if (fractions > 0)
            {
                fraction = Math.Round(fraction, fractions);
                var format = String.Format(CultureInfo.InvariantCulture, "{{0:F{0}}}", fractions);
                var result = String.Format(CultureInfo.InvariantCulture, format, fraction);
                entry.Suffix = result.Remove(0, 1);
            }
            return entry;
        }
    }
}
