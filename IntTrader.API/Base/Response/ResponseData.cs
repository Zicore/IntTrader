using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Request;

namespace IntTrader.API.Base.Response
{
    public class ResponseData
    {
        ResponseState _responseState = ResponseState.Success;
        private RequestBase _request;
        private String _value;
        private bool _hasResult = false;

        public ResponseState ResponseState
        {
            get { return _responseState; }
            set { _responseState = value; }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public bool HasResult
        {
            get { return _hasResult; }
            set { _hasResult = value; }
        }

        public RequestBase Request
        {
            get { return _request; }
            set { _request = value; }
        }

        public ResponseData(RequestBase request, String message)
        {
            HasResult = true;
            this.Request = request;
            this.Value = message;
        }

        public ResponseData(RequestBase request, WebException exception)
        {
            this.Request = request;
            HasResult = false;
            ReadExceptionResponse(exception);
        }

        private void ReadExceptionResponse(WebException exception)
        {
            if (exception == null || exception.Response == null)
            {
                return;
            }

            var stream = exception.Response.GetResponseStream();
            if (stream != null)
            {
                using (var sr = new StreamReader(stream))
                {
                    Value = sr.ReadToEnd();
                }
            }
        }
    }
}
