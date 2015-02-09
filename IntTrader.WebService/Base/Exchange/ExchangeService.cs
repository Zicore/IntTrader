using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Request;
using IntTrader.API.Currency;
using IntTrader.API.Exceptions;

namespace IntTrader.WebService.Base.Exchange
{
    public class ExchangeService
    {
        //case APIFunction.RequestTicker:
        //    break;
        //case APIFunction.RequestOrderBook:
        //    break;
        //case APIFunction.RequestTrades:
        //    break;
        //case APIFunction.RequestOpenOrders:
        //    break;
        //case APIFunction.RequestNewOrder:
        //    break;
        //case APIFunction.RequestBalances:
        //    break;
        //case APIFunction.CancelOrder:
        //    break;

        public static ResponseModelBase RequestTicker(ExchangeBase exchange, String pairKey)
        {
            PairBase pair = exchange.PairManager.GetPair(pairKey);
            return exchange.RequestTicker(pair);
        }

        public static ResponseModelBase RequestOrderBook(ExchangeBase exchange, String pairKey)
        {
            PairBase pair = exchange.PairManager.GetPair(pairKey);
            return exchange.RequestOrderBook(pair);
        }

        public static ResponseModelBase RequestTrades(ExchangeBase exchange, String pairKey)
        {
            PairBase pair = exchange.PairManager.GetPair(pairKey);
            return exchange.RequestTrades(pair);
        }

        public static ResponseModelBase RequestOpenOrders(ExchangeBase exchange)
        {
            return exchange.RequestOpenOrders();
        }

        public static ResponseModelBase RequestNewOrder(ExchangeBase exchange)
        {
            return exchange.RequestNewOrder(new CreateOrderRequestBase());
        }

        public static ResponseModelBase RequestBalances(ExchangeBase exchange)
        {
            return exchange.RequestBalances();
        }

        public static ResponseModelBase CancelOrder(ExchangeBase exchange)
        {
            return exchange.CancelOrder(new CancelOrderRequestBase());
        }
    }
}
