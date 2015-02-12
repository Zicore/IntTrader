using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;
using IntTrader.API.Exceptions;
using IntTrader.WebService.Exceptions;
using NLog;

namespace IntTrader.WebService.Base.Request
{
    public class RequestService
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        Dictionary<ExchangeBase, Dictionary<APIFunction, RequestCommand>> _items = new Dictionary<ExchangeBase, Dictionary<APIFunction, RequestCommand>>();

        public Dictionary<ExchangeBase, Dictionary<APIFunction, RequestCommand>> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public void Add(ExchangeBase exchange, APIFunction command, RequestCommand.RequestDelegate requestDelegate)
        {
            if (!Items.ContainsKey(exchange))
            {
                Items[exchange] = new Dictionary<APIFunction, RequestCommand>();
            }

            if (!Items[exchange].ContainsKey(command))
            {
                Items[exchange][command] = new RequestCommand(exchange, command, requestDelegate);
            }
        }

        public ResponseModelBase Execute(ExchangeBase exchange, APIFunction func, object[] args)
        {
            if (!Items.ContainsKey(exchange))
            {
                throw new ExchangeNotSupportedException(exchange.Name);
            }

            if (!Items[exchange].ContainsKey(func))
            {
                throw new ApiFunctionNotSupported("The API Function is not supported", func);
            }

            return Items[exchange][func].RequestAction(exchange, func, args);
        }

        public bool TryExecute(out ResponseModelBase response, ExchangeBase exchange, APIFunction func, object[] args)
        {
            response = null;
            try
            {
                response = Execute(exchange, func, args);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
