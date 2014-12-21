namespace IntTrader.API.Base.Model
{
    public enum ResponseState
    {
        Success,
        Error,
        Exception,
        NotImplemented
    }

    public class ResponseModelBase
    {
        public virtual void Update()
        {

        }

        ResponseState _responseState = ResponseState.Success;

        public ResponseState ResponseState
        {
            get { return _responseState; }
            set { _responseState = value; }
        }
    }
}
