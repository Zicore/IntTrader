using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntTrader.API.Base.Response;

namespace IntTrader.API.Event
{
    public class RequestEventArgs : EventArgs
    {
        public RequestEventArgs(ResponseBase response)
        {
            this.Response = response;
        }

        private ResponseBase _response;

        public ResponseBase Response
        {
            get { return _response; }
            set { _response = value; }
        }
    }
}
