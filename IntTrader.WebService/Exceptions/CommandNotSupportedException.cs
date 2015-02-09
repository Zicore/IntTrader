using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntTrader.WebService.Exceptions
{
    public class CommandNotSupportedException : Exception
    {
        private String _command;

        public string Command
        {
            get { return _command; }
            set { _command = value; }
        }

        public CommandNotSupportedException(string command)
        {
            _command = command;

        }

        public override string Message
        {
            get { return String.Format("The command {0} was not found", Command); }
        }
    }
}
