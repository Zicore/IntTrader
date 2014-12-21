using System;
using System.Globalization;

namespace IntTrader.API.Converter
{
    public class DateTimeConverter
    {

        /// <summary>
        /// Converts a Unix timestamp into a System.DateTime
        /// </summary>
        /// <param name="timestamp">The Unix timestamp in milliseconds to convert, as a double</param>
        /// <returns>DateTime obtained through conversion</returns>
        public static DateTime ConvertTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp); // convert from milliseconds to seconds
        }

        public static DateTime ConvertTimestamp(String timestamp)
        {
            if (String.IsNullOrEmpty(timestamp))
            {
                return DateTime.MinValue;
            }
            //double result = 0;
            //double.TryParse(timestamp, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result);
            return ConvertTimestamp(double.Parse(timestamp, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture));
        }

        public static double ConvertDateTime(DateTime dateTime)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return (dateTime - origin).TotalSeconds;
        }
    }
}
