using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntTrader.Web.Model
{
    public class ExchangeModel
    {
        public ExchangeModel()
        {
            Commands = new List<string>();

        }

        public String Name { get; set; }
        public String Pair { get; set; }

        public List<String> Commands { get; set; }


    }
}
