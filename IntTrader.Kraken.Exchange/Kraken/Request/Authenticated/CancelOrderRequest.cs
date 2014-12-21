using System;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Response;
using IntTrader.API.Exchange.Kraken.Response;
using Newtonsoft.Json;

namespace IntTrader.API.Exchange.Kraken.Request.Authenticated
{
    public class CancelOrderRequest : AuthenticatedRequest
    {
        public CancelOrderRequest(ExchangeBase exchange)
            : base(exchange)
        {
            RequestUri = "/0/private/CancelOrder";
        }

        private String _txid;

        [JsonProperty("txid")]
        public String Txid
        {
            get { return _txid; }
            set { _txid = value; }
        }

        public CancelOrderResponse Deserialize(ResponseData responseData)
        {
            return Deserialize<CancelOrderResponse>(responseData);
        }
    }
}
