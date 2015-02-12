using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Exchange.Orders;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Request;
using IntTrader.API.Currency;
using IntTrader.API.Exceptions;
using IntTrader.WebService.Exceptions;

namespace IntTrader.WebService.Base.Exchange
{
    public class ExchangeService
    {
        public static ResponseModelBase RequestTicker(ExchangeBase exchange, APIFunction command, object[] args)
        {
            String pairKey = RequireArgument<String>(exchange, command, args, 0);
            PairBase pair = exchange.PairManager.GetPair(pairKey);
            return exchange.RequestTicker(pair);
        }

        public static ResponseModelBase RequestOrderBook(ExchangeBase exchange, APIFunction command, object[] args)
        {
            String pairKey = RequireArgument<String>(exchange, command, args, 0);
            PairBase pair = exchange.PairManager.GetPair(pairKey);
            return exchange.RequestOrderBook(pair);
        }

        public static ResponseModelBase RequestTrades(ExchangeBase exchange, APIFunction command, object[] args)
        {
            String pairKey = RequireArgument<String>(exchange, command, args, 0);
            PairBase pair = exchange.PairManager.GetPair(pairKey);
            return exchange.RequestTrades(pair);
        }

        public static ResponseModelBase RequestOpenOrders(ExchangeBase exchange, APIFunction command, object[] args)
        {
            return exchange.RequestOpenOrders();
        }

        public static ResponseModelBase RequestNewOrder(ExchangeBase exchange, APIFunction command, object[] args)
        {
            String pairKey = RequireArgument<String>(exchange, command, args, 0);
            String sideKey = RequireArgument<String>(exchange, command, args, 1);
            String typeKey = RequireArgument<String>(exchange, command, args, 2);
            decimal amount = RequireArgument<decimal>(exchange, command, args, 3);
            decimal price = RequireArgument<decimal>(exchange, command, args, 4);

            PairBase pair = exchange.PairManager.GetPair(pairKey);
            OrderSide side = exchange.GetOrderSide(sideKey);
            OrderType type = exchange.GetOrderType(typeKey);

            var request = new CreateOrderRequestBase
            {
                Pair = pair.Key,
                Amount = amount,
                Price = price,
                ExchangeType = exchange.DefaultExchangeType,
                OrderType = type,
                Side = side
            };

            return exchange.RequestNewOrder(request);
        }

        public static ResponseModelBase RequestBalances(ExchangeBase exchange, APIFunction command, object[] args)
        {
            return exchange.RequestBalances();
        }

        public static ResponseModelBase CancelOrder(ExchangeBase exchange, APIFunction command, object[] args)
        {
            return exchange.CancelOrder(new CancelOrderRequestBase { });
        }


        private static T RequireArgument<T>(ExchangeBase exchange, APIFunction command, object[] args, int index)
        {
            if (args.Length <= index)
            {
                throw new ArgumentRequiredException(exchange.Name, command.ToString());
            }

            if (!(args[index] is T))
            {
                throw new InvalidCastException(String.Format("Could not cast argument {0} to required type {1}.", index, typeof(T)));
            }
            var value = (T)args[index];

            return value;
        }

        private static bool OptionalArgument<T>(out T value, ExchangeBase exchange, String command, object[] args, int index)
        {
            value = default(T);

            if (args.Length <= index)
            {
                return false;
            }

            if (!(args[index] is T))
            {
                throw new InvalidCastException(String.Format("Could not cast argument {0} to required type {1}.", index, typeof(T)));
            }

            value = (T)args[index];

            return true;
        }
    }
}
