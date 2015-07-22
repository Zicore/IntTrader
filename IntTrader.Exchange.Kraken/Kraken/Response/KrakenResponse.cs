using System;
using System.Collections.Generic;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Response;
using Newtonsoft.Json;

namespace IntTrader.API.Exchange.Kraken.Response
{
    public class KrakenResponse : ResponseBase
    {
        List<String> _error = new List<string>();

        [JsonProperty("error")]
        public List<string> Error
        {
            get { return _error; }
            set { _error = value; }
        }

        public override void VerifyResponse()
        {
            if (Error.Count > 0)
            {
                ResponseData.ResponseState = ResponseState.Error;
            }

            base.VerifyResponse();
        }
    }
}
