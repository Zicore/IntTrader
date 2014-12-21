using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntTrader.API.Base.Response;
using IntTrader.ViewModel;

namespace IntTrader.Controls.Request
{
    public class RequestEntryViewModel : ViewModelBase
    {
        public RequestEntryViewModel(ResponseData responseBase)
        {
            this.ResponseData = responseBase;
            this.Name = ResponseData.Request.GetType().Name;
            this.State = ResponseData.ResponseState.ToString();
        }

        private DateTime _timestamp = DateTime.Now;
        private String _name;
        private String _state;
        private ResponseData _responseData;

        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        public String TimestampFormat
        {
            get { return String.Format("{0:yyyy-MM-dd HH:mm:ss}", Timestamp); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        public ResponseData ResponseData
        {
            get { return _responseData; }
            set { _responseData = value; }
        }
    }
}
