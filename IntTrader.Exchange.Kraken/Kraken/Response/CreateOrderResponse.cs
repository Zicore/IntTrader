using System;
using System.Collections.Generic;
using System.Linq;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Transform;
using Newtonsoft.Json;

namespace IntTrader.API.Exchange.Kraken.Response
{
    public class CreateOrderResponse : KrakenResponse, ICreateOrder
    {
        public CreateOrderModel Transform()
        {
            var m = new CreateOrderModel();

            if (Result != null && Result.Txid.Count > 0)
            {
                var firstId = this.Result.Txid.FirstOrDefault();
                m.OrderId = firstId;
                m.OrderDescription = Result.OrderDescriptionInfo.Order;
                m.CloseDescription = Result.OrderDescriptionInfo.Close;
            }

            return m;
        }

        OrderInfo _result = new OrderInfo();

        [JsonProperty("result")]
        public OrderInfo Result
        {
            get { return _result; }
            set { _result = value; }
        }
    }

    public class OrderInfo
    {
        private List<string> _txid;

        [JsonProperty("txid")]
        public List<string> Txid
        {
            get { return _txid; }
            set { _txid = value; }
        }

        OrderDescriptionInfo _orderDescriptionInfo = new OrderDescriptionInfo();

        [JsonProperty("descr")]
        public OrderDescriptionInfo OrderDescriptionInfo
        {
            get { return _orderDescriptionInfo; }
            set { _orderDescriptionInfo = value; }
        }
    }

    public class OrderDescriptionInfo
    {
        private String _order;
        private String _close;


        public string Order
        {
            get { return _order; }
            set { _order = value; }
        }

        public string Close
        {
            get { return _close; }
            set { _close = value; }
        }
    }
}
