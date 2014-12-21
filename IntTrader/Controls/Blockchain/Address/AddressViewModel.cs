using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;
using IntTrader.API.Base.Exchange;
using IntTrader.API.Blockchain.Model;
using IntTrader.API.Blockchain.Request;
using IntTrader.API.Blockchain.Response;
using IntTrader.ViewModel;
using Zicore.WPF.Base.Commands;

namespace IntTrader.Controls.Blockchain.Address
{
    public class AddressViewModel : ExchangeManagerViewModel
    {
        public AddressViewModel(ExchangeManager exchangeManager)
            : base(exchangeManager)
        {
            //Foreground = Brushes.Gold;
            Header = "Addresses";
            UpdateAddressesFromSettings();
            Update();
        }

        ObservableCollection<AddressItemViewModel> _items = new ObservableCollection<AddressItemViewModel>();

        public ObservableCollection<AddressItemViewModel> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        private RelayCommand _addItemCommand;
        public ICommand AddItemCommand
        {
            get { return _addItemCommand ?? (_addItemCommand = new RelayCommand(x => AddItem())); }
        }

        private void AddItem()
        {
            var item = new AddressItemViewModel(ExchangeManager) { Name = "New Item" };
            App.Settings.Addresses.Add(item.AddressSettingsEntry);
            Items.Add(item);
        }

        private RelayCommand _removeItemCommand;
        public ICommand RemoveItemCommand
        {
            get { return _removeItemCommand ?? (_removeItemCommand = new RelayCommand(x => RemoveItem())); }
        }

        private RelayCommand _updateCommand;
        public ICommand UpdateCommand
        {
            get { return _updateCommand ?? (_updateCommand = new RelayCommand(x => Update())); }
        }

        private void RemoveItem()
        {
            var selected = Items.Where(x => x.IsSelected).ToList();
            for (int i = 0; i < selected.Count; i++)
            {
                Items.Remove(selected[i]);
                App.Settings.Addresses.Remove(selected[i].AddressSettingsEntry);
            }
            OnPropertyChanged("SumBalance");
        }

        private void UpdateAddressesFromSettings()
        {
            Items.Clear();
            foreach (var address in App.Settings.Addresses.OrderBy(x => x.Sort))
            {
                var item = new AddressItemViewModel(ExchangeManager, address);
                Items.Add(item);
            }
            OnPropertyChanged("SumBalance");
        }

        public override void OnUpdate()
        {
            var addresses = Items.Select(address => address.Address).ToList();

            var request = new MultiAddressRequest(addresses);
            var model = request.Request<MultiAddressResponse>().Transform();
            Dispatch(() => Dispatch(model));
        }

        private void Dispatch(MultiAddressModel model)
        {
            foreach (var item in model.Items)
            {
                foreach (var viewModel in Items)
                {
                    if (item.Address == viewModel.Address)
                    {
                        viewModel.AddressModel = item;
                    }
                }
            }
            OnPropertyChanged("SumBalance");
        }

        public decimal SumBalance
        {
            get { return Items.Sum(x => x.FinalBalance); }
        }
    }
}
