using System;
using System.Windows;
using System.Windows.Media;
using IntTrader.API.Currency;
using IntTrader.API.Threading;
using Zicore.WPF.Base.Common;

namespace IntTrader.ViewModel
{
    public class ViewModelBase : BindableBase
    {
        public ViewModelBase()
        {

        }

        public event EventHandler<EventArgs> UpdateEvent;

        protected virtual void OnUpdateEvent()
        {
            EventHandler<EventArgs> handler = UpdateEvent;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private Brush _foreground = Brushes.DarkGray;
        private Brush _background = Brushes.Black;

        public Brush Foreground
        {
            get { return _foreground; }
            set { _foreground = value; }
        }

        public Brush Background
        {
            get { return _background; }
            set { _background = value; }
        }

        public virtual bool IsActive
        {
            get { return false; }
        }

        private String _header = "...";
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }

        public event EventHandler RequestCloseEvent;
        protected virtual void OnRequestClose()
        {
            EventHandler handler = RequestCloseEvent;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler Dispatched;

        protected virtual void OnDispatched(object sender, EventArgs e)
        {
            EventHandler handler = Dispatched;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        // This ensures that all requests are made in the proper order.
        protected readonly static RequestHandler RequestHandler = new RequestHandler();
        private PairBase _pair = null;

        public PairBase Pair
        {
            get { return _pair; }
            set
            {
                _pair = value;
                OnPropertyChanged("Pair");
            }
        }

        public virtual void UpdatePair(PairBase pair)
        {
            Pair = pair;
        }

        public virtual void OnUpdate()
        {
            OnUpdateEvent();
        }

        public void Update()
        {
            UpdateAsync(OnUpdate);
        }

        public void UpdateAsync(Action action)
        {
            RequestHandler.Queue.Enqueue(action);
        }

        public void Dispatch(Action action)
        {
            if (Application.Current != null)
            {
                var dis = Application.Current.Dispatcher.BeginInvoke(action);
                dis.Completed += OnDispatched;
            }
        }
    }
}
