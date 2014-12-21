using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntTrader.Settings.Data
{
    public class AddressEntry
    {
        private int _sort = 0;

        private String _address;
        private String _name;
        private String _description;

        public int Sort
        {
            get { return _sort; }
            set { _sort = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
    }
}
