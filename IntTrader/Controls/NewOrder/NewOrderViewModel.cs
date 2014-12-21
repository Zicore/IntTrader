using System;
using System.Collections.ObjectModel;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Exchange.Orders;
using IntTrader.API.Base.Request;
using IntTrader.API.Converter;
using IntTrader.ViewModel;
using Zicore.WPF.Base.Commands;

namespace IntTrader.Controls.NewOrder
{
    public class NewOrderViewModel : ExchangeViewModelBase
    {
        public NewOrderViewModel(ExchangeBase exchangeBase)
            : base(exchangeBase)
        {
            LoadOrderTypes();
        }

        private void LoadOrderTypes()
        {
            foreach (var orderType in Exchange.OrderTypes)
            {
                _orderTypes.Add(orderType);
            }
            OrderType = Exchange.DefaultOrderType;
        }

        private RelayCommand _createOrderCommand;

        public RelayCommand CreateOrderCommand
        {
            get
            {
                return _createOrderCommand ??
                       (_createOrderCommand = new RelayCommand(x => CreateOrder(), x => CanCreateOrder()));
            }
        }

        private RelayCommand _calculatePriceCommand;

        public RelayCommand CalculatePriceCommand
        {
            get { return _calculatePriceCommand ?? (_calculatePriceCommand = new RelayCommand(x => CalculatePrice())); }
        }

        private void CalculatePrice()
        {
            CalculateResultToPrice();
        }

        private RelayCommand _calculateAmountCommand;

        public RelayCommand CalculateAmountCommand
        {
            get { return _calculateAmountCommand ?? (_calculateAmountCommand = new RelayCommand(x => CalculateAmount())); }
        }

        private void CalculateAmount()
        {
            CalculateResultToAmount();
        }

        ObservableCollection<OrderType> _orderTypes = new ObservableCollection<OrderType>();

        private decimal _price = 0;
        private decimal _amount = 0;
        private decimal _result = 0;
        private OrderSide _side = OrderSide.Buy;

        public ObservableCollection<OrderType> OrderTypes
        {
            get { return _orderTypes; }
            set { _orderTypes = value; }
        }

        public OrderSide Side
        {
            get { return _side; }
            set { _side = value; }
        }

        public OrderType OrderType { get; set; }

        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                CalculateResultToAmount();
                OnPropertyChanged("Price");
            }
        }

        public decimal Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                CalculateResult();
                OnPropertyChanged("Amount");
            }
        }

        public decimal Result
        {
            get { return _result; }
            set
            {
                _result = value;
                CalculateResultToAmount();
                OnPropertyChanged("Result");
            }
        }

        private void CalculateResultToPrice()
        {
            if (Amount > 0)
            {
                _price = DecimalRounding.RoundDown(Result / Amount, Pair.LeftCurrency.Decimals);
                OnPropertyChanged("Price");
            }
        }

        private void CalculateResultToAmount()
        {
            if (Price > 0)
            {
                _amount = DecimalRounding.RoundDown(Result / Price, Pair.RightCurrency.Decimals + 4);
                OnPropertyChanged("Amount");
            }
        }

        private void CalculateResult()
        {
            _result = DecimalRounding.RoundDown(Price * Amount, Pair.RightCurrency.Decimals + 4);
            OnPropertyChanged("Result");
        }


        public void CreateOrder()
        {
            UpdateAsync(CreateOrderAsync);
        }

        private void CreateOrderAsync()
        {
            var result = Exchange.RequestNewOrder(new CreateOrderRequestBase()
                 {
                     Amount = Amount,
                     ExchangeType = Exchange.DefaultExchangeType,
                     OrderType = OrderType,
                     Side = Side,
                     Price = Price,
                     Pair = Pair.Name
                 });
        }

        public bool CanCreateOrder()
        {
            return Amount > 0 && Price > 0 && Result > 0 && Exchange.IsAvailable(APIFunction.RequestNewOrder);
        }
    }
}