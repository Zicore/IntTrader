using System.Collections.Generic;
using Newtonsoft.Json;

namespace IntTrader.API.Base.Model
{
    public class OpenOrdersModel
    {
        private List<OpenOrderEntryModel> _orders = new List<OpenOrderEntryModel>();

        [JsonProperty("Orders")]
        public List<OpenOrderEntryModel> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }
    }
}
