using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace IntTrader.API.Web
{
    public class WebClientExtended : WebClient
    {
        public WebClientExtended()
        {
            this.Proxy = null;
        }

        public static WebClientExtended GetDefault()
        {
            var client = new WebClientExtended
                {
                    //Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",
                    //AcceptLanguage = "de-de,de;q=0.8,en-us;q=0.5,en;q=0.3",
                    UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:27.0) Gecko/20100101 Firefox/27.0",
                };
            return client;
        }

        //private static readonly WebClientExtended _webClientExtended = new WebClientExtended
        //{
        //    Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",
        //    AcceptLanguage = "de-de,de;q=0.8,en-us;q=0.5,en;q=0.3",
        //    UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:27.0) Gecko/20100101 Firefox/27.0"
        //};

        //public static WebClientExtended GetStatic()
        //{
        //    return _webClientExtended;
        //}

        public static string Post(string uri, Cookie cookie, NameValueCollection postData)
        {
            using (var client = new WebClientExtended())
            {
                client.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                client.AcceptLanguage = "de-de,de;q=0.8,en-us;q=0.5,en;q=0.3";
                client.CacheControl = "no-cache";
                client.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:16.0) Gecko/20100101 Firefox/16.0";
                var postResult = client.UploadValues(uri, postData);
                return Encoding.UTF8.GetString(postResult);
            }
        }

        public static string Get(GetRequest get)
        {
            using (var client = new WebClientExtended())
            {
                client.AddCookie(get.Cookie);
                client.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                client.AcceptLanguage = "de-de,de;q=0.8,en-us;q=0.5,en;q=0.3";
                client.CacheControl = "no-cache";
                client.UserAgent = get.UserAgent;

                return client.DownloadString(get.Uri);
            }
        }

        private CookieContainer _cookieContainer = new CookieContainer();

        public CookieContainer CookieContainer
        {
            get { return _cookieContainer; }
            set { _cookieContainer = value; }
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);

            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).CookieContainer = _cookieContainer;
            }
            return request;
        }

        public void AddHeader(string key, string value)
        {
            if (!String.IsNullOrEmpty(key) && !String.IsNullOrEmpty(value))
            {
                Headers[key] = value;
            }
        }

        public void AddCookie(Cookie cookie)
        {
            if (cookie != null)
            {
                CookieContainer.Add(cookie);
            }
        }

        public String Accept
        {
            get { return Headers["Accept"]; }
            set { AddHeader("Accept", value); }
        }

        public String AcceptLanguage
        {
            get { return Headers["Accept-Language"]; }
            set { AddHeader("Accept-Language", value); }
        }

        public String CacheControl
        {
            get { return Headers["Cache-Control"]; }
            set { AddHeader("Cache-Control", value); }
        }

        public String UserAgent
        {
            get { return Headers["User-Agent"]; }
            set { AddHeader("User-Agent", value); }
        }

        public String Referer
        {
            get { return Headers["Referer"]; }
            set { AddHeader("Referer", value); }
        }

        public String Host
        {
            get { return Headers["Host"]; }
            set { AddHeader("Host", value); }
        }
    }
}
