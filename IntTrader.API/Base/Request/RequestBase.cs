using System;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Response;
using IntTrader.API.Event;
using Newtonsoft.Json;

namespace IntTrader.API.Base.Request
{
    public abstract class RequestBase
    {

        public virtual T Request<T>() where T : class
        {
            var rs = Request();
            rs.Request = this;
            return Deserialize<T>(rs);
        }

        public T Deserialize<T>(ResponseData responseData) where T : class
        {
            T result = null;

            if (!String.IsNullOrEmpty(responseData.Value))
            {
                result = JsonConvert.DeserializeObject<T>(responseData.Value);
            }

            var response = result as ResponseBase;
            if (response != null)
            {
                response.ResponseData = responseData;
                response.VerifyResponse();
                RequestMonitor.OnRequestEvent(this, response);
            }

            return result;
        }

        public T DeserializeList<T>(ResponseData responseData)
        {
            return JsonConvert.DeserializeObject<T>(responseData.Value);
        }

        public abstract ResponseData Request();
    }
}
