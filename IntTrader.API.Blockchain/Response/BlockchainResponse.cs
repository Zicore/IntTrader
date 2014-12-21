using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntTrader.API.Base.Response;

namespace IntTrader.API.Blockchain.Response
{
    public class BlockchainResponse : ResponseBase
    {
        private String _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}
