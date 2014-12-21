using IntTrader.API.Base.Model;

namespace IntTrader.API.Base.Response
{
    public class ResponseBase
    {
        public ResponseData ResponseData { get; set; }

        public virtual void VerifyResponse()
        {
            if (!ResponseData.HasResult)
            {
                ResponseData.ResponseState = ResponseState.Exception;
            }
        }
    }
}
