using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntTrader.Web.Model
{
    public class NewOrderModel
    {
        private decimal _amount;
        private decimal _price;

        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }
    }
}
