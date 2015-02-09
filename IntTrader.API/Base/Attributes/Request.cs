using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntTrader.API.Base.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class RequestCommand : Attribute
    {
        public RequestCommand(String command, bool isPublic)
        {
            Command = command;
            IsPublic = isPublic;
        }

        public string Command { get; set; }
        public bool IsPublic { get; set; }
    }
}
