using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Text;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Response;
using IntTrader.API.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IntTrader.API.Exchange.Kraken.Request.Authenticated
{
    public abstract class AuthenticatedRequest : KrakenRequestBase
    {
        protected AuthenticatedRequest(ExchangeBase exchange)
        {
            this.Exchange = exchange;
        }

        ExchangeBase Exchange { get; set; }

        [JsonProperty("nonce")]
        protected String Nonce
        {
            get { return GetNonce(); }
        }

        [JsonIgnore]
        //[JsonProperty("otp")]
        public String TwoFactorPassword
        {
            get { return _twoFactorPassword; }
            set { _twoFactorPassword = value; }
        }

        private String _twoFactorPassword = "";

        protected static String GetNonce()
        {
            lock (LockObject)
            {
                long ticks = DateTime.Now.Ticks / 10000;
                return ticks.ToString(CultureInfo.InvariantCulture);
            }
        }

        private static readonly object LockObject = new object();

        //TODO: encrypt
        [JsonIgnore]
        protected virtual String ApiKey
        {
            get { return Exchange.ExchangeAPI.APIKey; }
        }

        //TODO: encrypt
        [JsonIgnore]
        protected virtual String ApiSecret
        {
            get { return Exchange.ExchangeAPI.APISecret; }
        }

        protected String GenerateSignature(String uri, String nonce, String postData, String apiSecert)
        {
            var auth = new Authentication();
            var signature = auth.CreateSignature(uri, nonce, postData, ApiSecret);
            return signature;
        }

        private String AddPostParameters(Object obj, String nonce, NameValueCollection nameValueCollection)
        {
            String result = JsonConvert.SerializeObject(obj);
            JObject jObject = JObject.Parse(result);

            var values = new List<string>();

            nameValueCollection.Add("nonce", nonce);
            values.Add(String.Format("nonce={0}", nonce));

            foreach (var token in jObject)
            {
                if (token.Key != "request" && token.Key != "nonce")
                {
                    nameValueCollection.Add(token.Key, token.Value.ToString());
                    values.Add(String.Format("{0}={1}", token.Key, token.Value));
                }
            }
            var sb = new StringBuilder();
            for (int i = 0; i < values.Count; i++)
            {
                sb.Append(values[i]);
                if (i < values.Count - 1)
                {
                    sb.Append("&");
                }
            }
            return sb.ToString();
        }

        public override ResponseData Request()
        {
            using (var client = WebClientExtended.GetDefault())
            {
                var nonce = Nonce;
                var postData = new NameValueCollection();
                var postDataUri = AddPostParameters(this, nonce, postData);

                client.Headers["Host"] = "api.kraken.com";
                client.Headers["API-Key"] = ApiKey;
                client.Headers["API-Sign"] = GenerateSignature(RequestUri, nonce, postDataUri, ApiSecret);

                try
                {

                    var postResult = client.UploadValues(Uri, "POST", postData);
                    return new ResponseData(this, Encoding.UTF8.GetString(postResult));
                }
                catch (WebException ex)
                {
                    return new ResponseData(this, ex);
                }
            }
        }
    }
}
