using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntTrader.API.Base.Attributes;
using IntTrader.API.Base.Exchange;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;
using IntTrader.API.Currency;
using IntTrader.API.ExchangeLoader;
using IntTrader.WebService.Base.Exchange;
using IntTrader.WebService.Base.Request;
using IntTrader.WebService.Exceptions;
using Newtonsoft.Json.Linq;

namespace IntTrader.WebService.Base
{
    public class WebBroker
    {
        public WebBroker()
        {
            ExchangeManager = new ExchangeManager();
            Initialize();
        }

        public ExchangeManager ExchangeManager { get; set; }

        public void Initialize()
        {
            ExchangeLoader.LoadExchanges(ExchangeManager);

            foreach (var e in ExchangeManager.Exchanges)
            {
                Exchanges.Add(e);
            }
        }

        ExchangeCollection _requestCollection = new ExchangeCollection();
        public ExchangeCollection Exchanges
        {
            get { return _requestCollection; }
            set { _requestCollection = value; }
        }

        public ResponseModelBase Execute(String exchange, String command, params object[] args)
        {
            if (!Exchanges.Items.ContainsKey(exchange))
                throw new ExchangeNotSupportedException(exchange);

            return Execute(Exchanges.Items[exchange], command, args);
        }

        /// <summary>
        /// Executes requests directed to the underliying API
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="command"></param>
        /// <param name="args"></param>
        /// <exception cref="ArgumentRequiredException"></exception>
        /// <exception cref="CommandNotSupportedException"></exception>
        /// <returns></returns>
        public ResponseModelBase Execute(ExchangeBase exchange, String command, params object[] args)
        {
            if (!exchange.Commands.ContainsKey(command))
                throw new CommandNotSupportedException(command);

            APIFunction func = exchange.Commands[command];

            switch (func)
            {
                case APIFunction.RequestTicker:
                    {
                        String arg1 = (String)RequireArgument(exchange, command, args, 0);
                        return ExchangeService.RequestTicker(exchange, arg1);
                    }
                case APIFunction.RequestOrderBook:
                    {
                        String arg1 = (String)RequireArgument(exchange, command, args, 0);
                        return ExchangeService.RequestOrderBook(exchange, arg1);
                    }
                case APIFunction.RequestTrades:
                    {
                        String arg1 = (String)RequireArgument(exchange, command, args, 0);
                        return ExchangeService.RequestTrades(exchange, arg1);
                    }
                case APIFunction.RequestOpenOrders:
                    return ExchangeService.RequestOpenOrders(exchange);
                case APIFunction.RequestNewOrder:
                    return ExchangeService.RequestNewOrder(exchange);
                case APIFunction.RequestBalances:
                    return ExchangeService.RequestBalances(exchange);
                case APIFunction.CancelOrder:
                    return ExchangeService.CancelOrder(exchange);
                default:
                    throw new CommandNotSupportedException(command);
            }
        }

        private static object RequireArgument(ExchangeBase exchange, String command, object[] args, int index)
        {
            if (args.Length <= index)
            {
                throw new ArgumentRequiredException(exchange.Name, command);
            }
            return args[index];
        }
    }
}
