using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntTrader.Web.Model
{
    public class DashboardModel
    {
        List<ExchangeModel> _items = new List<ExchangeModel>();

        public List<ExchangeModel> Items
        {
            get { return _items; }
            set { _items = value; }
        }
    }
}
