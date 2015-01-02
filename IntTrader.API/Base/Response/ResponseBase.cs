using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;
using IntTrader.API.Currency;

namespace IntTrader.API.Base.Response
{
    public class ResponseBase
    {
        public ResponseData ResponseData { get; set; }
        public ExchangeBase Exchange { get; set; }
        public PairManager PairManager { get; set; }

        public virtual void VerifyResponse()
        {
            if (!ResponseData.HasResult)
            {
                ResponseData.ResponseState = ResponseState.Exception;
            }
        }
    }
}
