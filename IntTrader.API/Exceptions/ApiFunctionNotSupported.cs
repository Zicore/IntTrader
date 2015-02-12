using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntTrader.API.Base.Exchange.Base;

namespace IntTrader.API.Exceptions
{
    public class ApiFunctionNotSupported : Exception
    {
        public ApiFunctionNotSupported(String message, APIFunction apiFunction)
            : base(message)
        {
            APIFunction = apiFunction;
        }

        public APIFunction APIFunction { get; set; }
    }
}
