using System.Collections.Generic;

namespace IntTrader.API.Base.Model
{
    public class OrderBookModel : ResponseModelBase
    {
        public OrderBookModel()
        {

        }

        public override void Update()
        {
            base.Update();
        }

        private List<OrderBookEntryModel> _bids = new List<OrderBookEntryModel>();
        private List<OrderBookEntryModel> _asks = new List<OrderBookEntryModel>();

        public List<OrderBookEntryModel> Bids
        {
            get { return _bids; }
            set { _bids = value; }
        }

        public List<OrderBookEntryModel> Asks
        {
            get { return _asks; }
            set { _asks = value; }
        }
    }
}
