using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Response;
using IntTrader.API.Web;
using Newtonsoft.Json;

namespace IntTrader.API.Exchange.Bitfinex.Request.Authenticated
{
    public abstract class AuthenticatedRequest : BitfinexRequestBase
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

        public String CreateSignature(String base64Str, String apiSecret)
        {
            byte[] key = Encoding.UTF8.GetBytes(apiSecret);
            using (var hmac = new HMACSHA384(key))
            {
                byte[] input = Encoding.UTF8.GetBytes(base64Str);
                byte[] output = hmac.ComputeHash(input);

                string hex = BitConverter.ToString(output).Replace("-", string.Empty);
                return hex;
            }
        }

        protected String GenerateSignature(String base64Str)
        {
            var signature = CreateSignature(base64Str, ApiSecret);
            return signature.ToLower();
        }

        public override ResponseData Request()
        {
            using (var client = WebClientExtended.GetDefault())
            {
                var payload = ToJsonBase64String();

                client.Headers["Host"] = "api.bitfinex.com";
                client.Headers["X-BFX-APIKEY"] = ApiKey;
                client.Headers["X-BFX-PAYLOAD"] = payload;
                client.Headers["X-BFX-SIGNATURE"] = GenerateSignature(payload);

                try
                {
                    var postResult = client.UploadValues(Uri, "POST", new NameValueCollection());
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
