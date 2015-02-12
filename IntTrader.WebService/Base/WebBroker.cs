using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntTrader.API.Base.Attributes;
using IntTrader.API.Base.Exchange;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Request;
using IntTrader.API.Currency;
using IntTrader.API.ExchangeLoader;
using IntTrader.WebService.Base.Exchange;
using IntTrader.WebService.Base.Request;
using IntTrader.WebService.Exceptions;
using Newtonsoft.Json.Linq;
using NLog;

namespace IntTrader.WebService.Base
{
    public class WebBroker
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public WebBroker()
        {
            Exchanges = new ExchangeCollection();
            RequestCollection = new RequestService();
            ExchangeManager = new ExchangeManager();
            Initialize();
        }

        public ExchangeManager ExchangeManager { get; set; }

        //RequestTicker,
        //RequestOrderBook,
        //RequestTrades,
        //RequestOpenOrders,
        //RequestNewOrder,
        //RequestBalances,
        //CancelOrder,

        public void Initialize()
        {
            ExchangeLoader.LoadExchanges(ExchangeManager);

            foreach (var e in ExchangeManager.Exchanges)
            {
                Exchanges.Add(e);

                RequestCollection.Add(e, APIFunction.RequestTicker, ExchangeService.RequestTicker);
                RequestCollection.Add(e, APIFunction.RequestOrderBook, ExchangeService.RequestOrderBook);
                RequestCollection.Add(e, APIFunction.RequestOpenOrders, ExchangeService.RequestOpenOrders);
                RequestCollection.Add(e, APIFunction.RequestNewOrder, ExchangeService.RequestNewOrder);
                RequestCollection.Add(e, APIFunction.RequestBalances, ExchangeService.RequestBalances);
                RequestCollection.Add(e, APIFunction.CancelOrder, ExchangeService.CancelOrder);
            }
        }

        public ExchangeCollection Exchanges { get; set; }
        public RequestService RequestCollection { get; set; }

        public bool TryExecute(out ResponseModelBase result, String exchange, String command, params object[] args)
        {
            result = null;
            try
            {
                result = Execute(Exchanges.Items[exchange], command, args);
                return true;
            }
            catch (Exception ex)
            {
                Log.Warn(ex);
                return false;
            }
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
            if (!exchange.IsCommandAvailable(command))
                throw new CommandNotSupportedException(command);

            APIFunction func = exchange.GetFunction(command);

            return RequestCollection.Execute(exchange, func, args);
        }

    }
}
