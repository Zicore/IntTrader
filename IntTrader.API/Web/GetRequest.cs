using System.Net;

namespace IntTrader.API.Web
{
    public class GetRequest
    {
        private Cookie _cookie;
        private string _host;
        private string _referer;
        private string _uri;
        private string _userAgent;

        public Cookie Cookie
        {
            get { return _cookie; }
            set { _cookie = value; }
        }

        public string Host
        {
            get { return _host; }
            set { _host = value; }
        }

        public string Referer
        {
            get { return _referer; }
            set { _referer = value; }
        }

        public string Uri
        {
            get { return _uri; }
            set { _uri = value; }
        }

        public string UserAgent
        {
            get { return _userAgent; }
            set { _userAgent = value; }
        }

        public string Request()
        {
            return WebClientExtended.Get(this);
        }
    }
}