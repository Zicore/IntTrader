using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntTrader.API.Blockchain.Model
{
    public class MultiAddressModel
    {
        List<MultiAddressEntryModel> _items = new List<MultiAddressEntryModel>();

        public List<MultiAddressEntryModel> Items
        {
            get { return _items; }
            set { _items = value; }
        }
    }
}
