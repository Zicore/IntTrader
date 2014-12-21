using System;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Response;
using Newtonsoft.Json;

namespace IntTrader.API.Exchange.Bitfinex.Response
{
    public class BitfinexResponse : ResponseBase
    {
        public BitfinexResponse()
        {

        }

        public static T Deserialize<T>(ResponseData responseData) where T : BitfinexResponse
        {
            return JsonConvert.DeserializeObject<T>(responseData.Value);
        }

        private String _message;

        [JsonProperty("message")]
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public override void VerifyResponse()
        {
            if (!String.IsNullOrEmpty(Message))
            {
                ResponseData.ResponseState = ResponseState.Error;
            }
            base.VerifyResponse();
        }
    }
}
