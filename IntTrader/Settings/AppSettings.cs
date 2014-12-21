using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntTrader.Settings.Data;

namespace IntTrader.Settings
{
    public class AppSettings : AppSettingsBase
    {

        List<AddressEntry> _addresses = new List<AddressEntry>();
        public List<AddressEntry> Addresses
        {
            get { return _addresses; }
            set { _addresses = value; }
        }
    }
}
