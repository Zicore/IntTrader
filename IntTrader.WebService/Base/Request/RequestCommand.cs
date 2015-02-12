using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;

namespace IntTrader.WebService.Base.Request
{
    public class RequestCommand
    {
        public RequestCommand(ExchangeBase exchange, APIFunction command, RequestDelegate requestDelegate)
        {
            Exchange = exchange;
            Command = command;
            RequestAction = requestDelegate;
        }

        public APIFunction Command { get; set; }
        public ExchangeBase Exchange { get; set; }

        public delegate ResponseModelBase RequestDelegate(ExchangeBase exchange, APIFunction command, object[] args);

        public RequestDelegate RequestAction { get; set; }
    }
}
