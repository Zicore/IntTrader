using System;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Request;
using IntTrader.ViewModel;
using Zicore.WPF.Base.Commands;
using Zicore.WPF.Base.Event;

namespace IntTrader.Controls.UserOrders
{
    public class OrderViewModel : ExchangeViewModelBase
    {
        public OrderViewModel(ExchangeBase exchangeBase)
            : base(exchangeBase)
        {
        }

        public event EventHandler<EventArgs<CancelOrderModel>> OrderCanceled;

        protected virtual void OnOrderCanceled(EventArgs<CancelOrderModel> args)
        {
            EventHandler<EventArgs<CancelOrderModel>> handler = OrderCanceled;
            if (handler != null) handler(this, args);
        }


        private RelayCommand _cancelOrderCommand;
        public RelayCommand CancelOrderCommand
        {
            get
            {
                if (_cancelOrderCommand == null)
                {
                    _cancelOrderCommand = new RelayCommand(CancelOrder);
                }
                return _cancelOrderCommand;
            }
        }

        private void CancelOrder(object order)
        {
            var orderStatusModel = Exchange.CancelOrder(new CancelOrderRequestBase { OrderId = OrderId });
            OnOrderCanceled(new EventArgs<CancelOrderModel>(orderStatusModel));
        }

        public static OrderViewModel Create(ExchangeBase exchange, OpenOrderEntryModel model)
        {
            return new OrderViewModel(exchange)
                {
                    OrderId = model.OrderId,
                    SymbolText = model.Symbol,
                    ExchangeName = model.Exchange,
                    Price = model.Price,
                    AvgerageExecutionPrice = model.AverageExecutionPrice,
                    Type = model.Type,
                    DateTime = model.DateTime,
                    Status = model.OrderStatus,
                    ExecutedAmount = model.ExecutedAmount,
                    RemainingAmount = model.RemainingAmount,
                    OriginalAmount = model.OriginalAmount
                };
        }

        private String _orderId;
        private String _symbol;
        private String _exchangeName;
        private decimal _price;
        private decimal _avgerageExecutionPrice;
        private String _type;
        private DateTime _dateTime;
        private String _status;
        //private bool _wasForced;
        private decimal _executedAmount;
        private decimal _remainingAmount;
        private decimal _originalAmount;

        public String OrderId
        {
            get { return _orderId; }
            set
            {
                _orderId = value;
                OnPropertyChanged("OrderId");
            }
        }

        public string ExchangeName
        {
            get { return _exchangeName; }
            set
            {
                _exchangeName = value;
                OnPropertyChanged("Exchange");
            }
        }

        public string SymbolText
        {
            get { return _symbol; }
            set
            {
                _symbol = value;
                OnPropertyChanged("SymbolText");

            }
        }

        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }

        public decimal AvgerageExecutionPrice
        {
            get { return _avgerageExecutionPrice; }
            set
            {
                _avgerageExecutionPrice = value;
                OnPropertyChanged("AvgerageExecutionPrice");
            }
        }

        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("Type");
            }
        }

        public DateTime DateTime
        {
            get { return _dateTime; }
            set
            {
                _dateTime = value;
                OnPropertyChanged("DateTime");
            }
        }

        public String Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        public decimal ExecutedAmount
        {
            get { return _executedAmount; }
            set
            {
                _executedAmount = value;
                OnPropertyChanged("ExecutedAmount");
            }
        }

        public decimal RemainingAmount
        {
            get { return _remainingAmount; }
            set
            {
                _remainingAmount = value;
                OnPropertyChanged("RemainingAmount");
            }
        }

        public decimal OriginalAmount
        {
            get { return _originalAmount; }
            set
            {
                _originalAmount = value;
                OnPropertyChanged("OriginalAmount");
            }
        }

        public decimal Volume
        {
            get { return Price * OriginalAmount; }
        }
    }
}
