using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntTrader.API.Base.Response;
using IntTrader.API.Event;

namespace IntTrader.API.Base.Request
{
    public class RequestMonitor
    {
        public static event EventHandler<RequestEventArgs> RequestEvent;

        public static void OnRequestEvent(object sender, ResponseBase response)
        {
            EventHandler<RequestEventArgs> handler = RequestEvent;
            if (handler != null) handler(sender, new RequestEventArgs(response));
        }
    }
}
