using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using IntTrader.API.Base.Exchange.Orders;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Request;
using IntTrader.API.Base.Response;
using IntTrader.API.Base.Settings;
using IntTrader.API.Base.Transform;
using IntTrader.API.Currency;
using IntTrader.API.Event;
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
        private readonly Dictionary<String, APIFunction> _commands = new Dictionary<String, APIFunction>
        {
            {"ticker"     ,APIFunction.RequestTicker    },
            {"orderbook"  ,APIFunction.RequestOrderBook  },
            {"orders"     ,APIFunction.RequestOpenOrders },
            {"neworder"   ,APIFunction.RequestNewOrder   },
            {"balance"    ,APIFunction.RequestBalances   },
            {"cancelorder",APIFunction.CancelOrder       },
        };

        public Dictionary<String, APIFunction> Commands
        {
            get { return _commands; }
        }

        PairManager _pairManager = new PairManager();

        public PairManager PairManager
        {
            get { return _pairManager; }
            set { _pairManager = value; }
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
            return PairManager.GetPair(pair.Key);
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

        [Attributes.RequestCommand("orderbook", true)]
        public virtual OrderBookModel RequestOrderBook(PairBase pair)
        {
            return new OrderBookModel();
        }

        [Attributes.RequestCommand("ticker", true)]
        public virtual TickerModel RequestTicker(PairBase pair)
        {
            return new TickerModel();
        }

        [Attributes.RequestCommand("openorders", false)]
        public virtual OpenOrdersModel RequestOpenOrders()
        {
            return new OpenOrdersModel();
        }

        [Attributes.RequestCommand("createorder", false)]
        public virtual OpenOrderEntryModel RequestNewOrder(CreateOrderRequestBase orderNewRequest)
        {
            return new OpenOrderEntryModel();
        }

        public virtual OpenOrderEntryModel OnCreateOrder(ICreateOrder transform, ResponseBase response)
        {
            CreateOrderModel model;
            try
            {
                model = transform.Transform();
                model.WasSuccessful = true;
            }
            catch (Exception)
            {
                model = new CreateOrderModel { WasSuccessful = false, Message = response.ResponseData.Value };
            }

            if (response.ResponseData.ResponseState == ResponseState.Error)
            {
                model.WasSuccessful = false;
            }

            ExchangeManager.OnCreateOrderEvent(new CreateOrderEventArgs(this, model));

            return model;
        }

        [Attributes.RequestCommand("balance", false)]
        public virtual BalanceModel RequestBalances()
        {
            return new BalanceModel();
        }

        [Attributes.RequestCommand("cancelorder", false)]
        public virtual CancelOrderModel CancelOrder(CancelOrderRequestBase orderCancelRequest)
        {
            return new CancelOrderModel();
        }

        [Attributes.RequestCommand("trades", false)]
        public virtual TradesModel RequestTrades(PairBase pair)
        {
            return new TradesModel();
        }
    }
}
