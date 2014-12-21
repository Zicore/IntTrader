using System;
using System.Net;
using System.Text;
using IntTrader.API.Base.Request;
using IntTrader.API.Base.Response;
using IntTrader.API.Web;
using NLog;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IntTrader.API.Blockchain.Request
{
    public class BlockchainRequestBase : RequestBase
    {
        public static String ApiUri = "https://blockchain.info";
        private String _language = "de";

        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }

        private readonly static Logger Log = LogManager.GetCurrentClassLogger();
        private String _requestUri;

        [JsonProperty("request")]
        protected string RequestUri
        {
            get { return _requestUri; }
            set { _requestUri = value; }
        }

        [JsonIgnore]
        protected string Uri
        {
            get { return String.Format("{0}/{1}/{2}", ApiUri, Language, RequestUri); }
        }



        public String ToJsonBase64String()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(ToJsonString()));
        }

        public virtual String ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override ResponseData Request()
        {
            using (var client = WebClientExtended.GetDefault())
            {
                client.Headers["Host"] = "blockchain.info";

                try
                {
                    Log.Info(RequestUri);
                    AddGetParameters(this, client);
                    var postResult = client.DownloadString(Uri);
                    return new ResponseData(this, postResult);
                }
                catch (WebException ex)
                {
                    return new ResponseData(this, ex);
                }
            }
        }

        private void AddGetParameters(Object obj, WebClientExtended client)
        {
            String result = JsonConvert.SerializeObject(obj);
            JObject jObject = JObject.Parse(result);

            foreach (var token in jObject)
            {
                if (token.Key != "request")
                {
                    client.QueryString.Add(token.Key, token.Value.ToString());
                }
            }
        }
    }
}
