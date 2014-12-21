using System.Collections.Generic;

namespace IntTrader.API.Base.Model
{
    public class BalanceModel : ResponseModelBase
    {
        private List<BalanceEntryModel> _items = new List<BalanceEntryModel>();

        public List<BalanceEntryModel> Items
        {
            get { return _items; }
            set { _items = value; }
        }
    }
}
