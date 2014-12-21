using System;
using System.Collections.ObjectModel;
using IntTrader.API.Base.Exchange.Base;
using IntTrader.API.Base.Model;
using IntTrader.ViewModel;
using Zicore.WPF.Base.Event;

namespace IntTrader.Controls.UserOrders
{
    public class UserOrdersViewModel : ExchangeViewModelBase
    {
        public UserOrdersViewModel(ExchangeBase exchangeBase)
            : base(exchangeBase)
        {
        }

        public event EventHandler<EventArgs<CancelOrderModel>> OrderCanceled;
        protected virtual void OnOrderCanceled(object sender, EventArgs<CancelOrderModel> args)
        {
            EventHandler<EventArgs<CancelOrderModel>> handler = OrderCanceled;
            if (handler != null) handler(sender, args);
        }

        public override void OnUpdate()
        {
            if (Exchange.IsAvailable(APIFunction.RequestOpenOrders))
            {
                var model = Exchange.RequestOpenOrders();
                Dispatch(() => Dispatch(model));
            }
        }

        private void Dispatch(OpenOrdersModel model)
        {
            Orders.Clear();
            foreach (var order in model.Orders)
            {
                var orderViewModel = OrderViewModel.Create(Exchange, order);
                orderViewModel.OrderCanceled += OrderViewModelOnOrderCanceled;
                Orders.Add(orderViewModel);
            }
        }

        private void OrderViewModelOnOrderCanceled(object sender, EventArgs<CancelOrderModel> eventArgs)
        {
            OnOrderCanceled(sender, eventArgs);
            OnUpdate();
        }

        readonly ObservableCollection<OrderViewModel> _orders = new ObservableCollection<OrderViewModel>();

        public ObservableCollection<OrderViewModel> Orders
        {
            get { return _orders; }
        }
    }
}
