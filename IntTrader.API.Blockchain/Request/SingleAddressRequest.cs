using System;
using Newtonsoft.Json;

namespace IntTrader.API.Blockchain.Request
{
    public class SingleAddressRequest : BlockchainRequestBase
    {
        public SingleAddressRequest(String address)
        {
            this.Address = address;
            RequestUri = "address/" + Address + "?format=json";
        }

        private String _address = "";

        [JsonIgnore]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
    }
}
