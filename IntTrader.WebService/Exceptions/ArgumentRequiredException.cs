using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntTrader.WebService.Exceptions
{
    public class ArgumentRequiredException : Exception
    {
        private String _exchange;
        private String _command;

        public ArgumentRequiredException(string exchange, string command)
        {
            _exchange = exchange;
            _command = command;
        }

        public string Exchange
        {
            get { return _exchange; }
            set { _exchange = value; }
        }

        public string Command
        {
            get { return _command; }
            set { _command = value; }
        }

        public override string Message
        {
            get { return String.Format("One or more arguments are required to run {0}/{1} command.", Exchange, Command); }
        }
    }
}
