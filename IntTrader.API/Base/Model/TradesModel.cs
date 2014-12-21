using System.Collections.Generic;

namespace IntTrader.API.Base.Model
{
    public class TradesModel : ResponseModelBase
    {
        List<TradesEntryModel> _items = new List<TradesEntryModel>();

        public List<TradesEntryModel> Items
        {
            get { return _items; }
            set { _items = value; }
        }
    }
}
