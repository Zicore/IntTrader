using System;
using System.Globalization;

namespace IntTrader.API.Converter
{
    public class DecimalConverter
    {
        public static String Convert(Decimal value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public static decimal ConvertToPrecision(decimal value, long precision)
        {
            return value / precision;
        }

        public static decimal Normalize(decimal value)
        {
            return value / 1.000000000000000000000000000000000m;
        }
    }
}
