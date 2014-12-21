using System;
using System.Collections.Generic;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Transform;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IntTrader.API.Exchange.Bitfinex.Response
{
    [JsonConverter(typeof(OrdersConverter))]
    public class OrdersResponse : BitfinexResponse, IOrders
    {
        public OrdersResponse()
        {

        }

        List<OrderResponse> _orders = new List<OrderResponse>();

        public List<OrderResponse> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }

        public OpenOrdersModel Transform()
        {
            var ordersModel = new OpenOrdersModel();

            Orders.ForEach(x => ordersModel.Orders.Add(x.Transform()));

            return ordersModel;
        }
    }

    public class OrdersConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(OrdersResponse));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var json = JToken.Load(reader);
            OrdersResponse response = new OrdersResponse();
            response.Orders = json.ToObject<List<OrderResponse>>();
            return response;
        }
    }

}
