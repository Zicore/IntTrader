using System;
using System.Collections.Generic;
using IntTrader.API.Base.Exchange.Orders;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Request;
using IntTrader.API.Base.Settings;
using IntTrader.API.Currency;
using Newtonsoft.Json;

namespace IntTrader.API.Base.Exchange.Base
{
    public enum APIFunction
    {
        RequestTicker,
        RequestOrderBook,
        RequestTrades,
        RequestOpenOrders,
        RequestNewOrder,
        RequestBalances,
        CancelOrder,
    }

    public class ExchangeBase
    {
        PairManager _currencyManager = new PairManager();

        public PairManager CurrencyManager
        {
            get { return _currencyManager; }
            set { _currencyManager = value; }
        }

        private OrderSide _defaultOrderSide;

        public virtual OrderSide DefaultOrderSide
        {
            get { return _defaultOrderSide; }
            protected set { _defaultOrderSide = value; }
        }

        private OrderType _defaultOrderType;

        public virtual OrderType DefaultOrderType
        {
            get { return _defaultOrderType; }
            set { _defaultOrderType = value; }
        }

        public virtual ExchangeType DefaultExchangeType
        {
            get { return null; }
        }

        private readonly List<OrderSide> _orderSides = new List<OrderSide>();

        [JsonIgnore]
        public List<OrderSide> OrderSides
        {
            get { return _orderSides; }
        }

        private readonly List<OrderType> _orderTypes = new List<OrderType>();

        [JsonIgnore]
        public List<OrderType> OrderTypes
        {
            get { return _orderTypes; }
        }

        private readonly HashSet<APIFunction> _publicFunctions = new HashSet<APIFunction>();

        [JsonIgnore]
        public HashSet<APIFunction> PublicFunctions
        {
            get { return _publicFunctions; }
        }

        private readonly HashSet<APIFunction> _availableFunctions = new HashSet<APIFunction>();

        [JsonIgnore]
        public HashSet<APIFunction> AvailableFunctions
        {
            get { return _availableFunctions; }
        }

        private String _name = "Empty";

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private ExchangeAPI _exchangeAPI = new ExchangeAPI();
        public ExchangeAPI ExchangeAPI
        {
            get { return _exchangeAPI; }
            set { _exchangeAPI = value; }
        }

        [JsonIgnore]
        public ExchangeManager ExchangeManager { get; set; }

        public ExchangeBase(ExchangeManager exchangeManager)
        {
            ExchangeManager = exchangeManager;
        }

        public virtual PairBase ConvertPair(PairBase pair)
        {
            return CurrencyManager.GetPair(pair.Key);
        }

        public virtual bool IsPublicFunction(APIFunction function)
        {
            return PublicFunctions.Contains(function);
        }

        public virtual bool IsAvailable(APIFunction function)
        {
            return (AvailableFunctions.Contains(function) && VerifyAPI()) || IsPublicFunction(function);
        }

        public virtual bool VerifyAPI()
        {
            return ExchangeAPI.IsValid;
        }

        public virtual OrderBookModel RequestOrderBook(PairBase pair)
        {
            return new OrderBookModel();
        }

        public virtual TickerModel RequestTicker(PairBase pair)
        {
            return new TickerModel();
        }

        public virtual OpenOrdersModel RequestOpenOrders()
        {
            return new OpenOrdersModel();
        }

        public virtual OpenOrderEntryModel RequestNewOrder(CreateOrderRequestBase orderNewRequest)
        {
            return new OpenOrderEntryModel();
        }

        public virtual BalanceModel RequestBalances()
        {
            return new BalanceModel();
        }

        public virtual CancelOrderModel CancelOrder(CancelOrderRequestBase orderCancelRequest)
        {
            return new CancelOrderModel();
        }

        public virtual TradesModel RequestTrades(PairBase pair)
        {
            return new TradesModel();
        }
    }
}
