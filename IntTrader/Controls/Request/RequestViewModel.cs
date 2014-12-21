using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using IntTrader.API.Base.Exchange;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Request;
using IntTrader.API.Event;
using IntTrader.ViewModel;
using Zicore.WPF.Base.Commands;

namespace IntTrader.Controls.Request
{
    public class RequestViewModel : ExchangeManagerViewModel
    {
        public MainViewModel MainViewModel { get; set; }

        public RequestViewModel(MainViewModel mainViewModel, ExchangeManager exchangeManager)
            : base(exchangeManager)
        {
            MainViewModel = mainViewModel;
            this.Header = "Requests";
            RequestMonitor.RequestEvent += RequestMonitorOnRequestEvent;
        }

        private ObservableCollection<RequestEntryViewModel> _items = new ObservableCollection<RequestEntryViewModel>();

        public ObservableCollection<RequestEntryViewModel> Items
        {
            get { return _items; }
            protected set { _items = value; }
        }

        private bool _isErrorsOnly = true;

        public bool IsErrorsOnly
        {
            get { return _isErrorsOnly; }
            set
            {
                _isErrorsOnly = value;
                OnPropertyChanged("IsErrorsOnly");
            }
        }

        private RelayCommand _clearCommand;

        public ICommand ClearCommand
        {
            get { return _clearCommand ?? (_clearCommand = new RelayCommand(x => Clear())); }
        }

        private void Clear()
        {
            Items.Clear();
        }

        private void RequestMonitorOnRequestEvent(object sender, RequestEventArgs e)
        {
            Dispatch(() => Dispatch(e));
        }

        private void Dispatch(RequestEventArgs e)
        {
            if (e.Response != null)
            {
                bool addEntry = true;

                if (IsErrorsOnly)
                {
                    addEntry = (e.Response.ResponseData.ResponseState == ResponseState.Error ||
                                e.Response.ResponseData.ResponseState == ResponseState.Exception);
                }

                if (addEntry)
                {
                    var vm = new RequestEntryViewModel(e.Response.ResponseData);
                    Items.Insert(0, vm);
                }
            }
        }
    }
}
