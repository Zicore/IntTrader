using System;
using Newtonsoft.Json;

namespace IntTrader.API.Base.Settings
{
    public class ExchangeAPI
    {
        private String _name;
        private String _apiKey;
        private String _apiSecret;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string APIKey
        {
            get { return _apiKey; }
            set { _apiKey = value; }
        }

        public string APISecret
        {
            get { return _apiSecret; }
            set { _apiSecret = value; }
        }

        [JsonIgnore]
        public bool IsValid
        {
            get { return !String.IsNullOrEmpty(APIKey) && !String.IsNullOrEmpty(APISecret); }
        }
    }
}
