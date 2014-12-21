using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntTrader.API.Base.Exchange;
using IntTrader.API.Blockchain.Model;
using IntTrader.Settings.Data;
using IntTrader.ViewModel;

namespace IntTrader.Controls.Blockchain.Address
{
    public class AddressItemViewModel : ExchangeManagerViewModel
    {
        public AddressItemViewModel(ExchangeManager exchangeManager)
            : base(exchangeManager)
        {

        }

        public AddressItemViewModel(ExchangeManager exchangeManager, AddressEntry addressEntry)
            : base(exchangeManager)
        {
            this.AddressSettingsEntry = addressEntry;
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        private AddressEntry _addressSettingsEntry = new AddressEntry();
        private MultiAddressEntryModel _addressModel = new MultiAddressEntryModel();

        public AddressEntry AddressSettingsEntry
        {
            get { return _addressSettingsEntry; }
            set
            {
                _addressSettingsEntry = value;
                OnPropertyChanged("AddressSettingsEntry");
            }
        }

        public MultiAddressEntryModel AddressModel
        {
            get { return _addressModel; }
            set
            {
                _addressModel = value;
                OnPropertyChanged("AddressModel");
                OnPropertyChanged("Name");
                OnPropertyChanged("Description");
                OnPropertyChanged("Hash160");
                OnPropertyChanged("Address");
                OnPropertyChanged("NumberTransactions");
                OnPropertyChanged("NumberUnredeemed");
                OnPropertyChanged("TotalReceived");
                OnPropertyChanged("TotalSent");
                OnPropertyChanged("FinalBalance");
            }
        }

        public int Sort
        {
            get { return AddressSettingsEntry.Sort; }
            set
            {
                AddressSettingsEntry.Sort = value;
                OnPropertyChanged("Sort");
            }
        }

        public string Name
        {
            get { return AddressSettingsEntry.Name; }
            set
            {
                AddressSettingsEntry.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Description
        {
            get { return AddressSettingsEntry.Description; }
            set
            {
                AddressSettingsEntry.Description = value;
                OnPropertyChanged("Description");
            }
        }

        public string Hash160
        {
            get { return AddressModel.Hash160; }
            set
            {
                AddressModel.Hash160 = value;
                OnPropertyChanged("Hash160");
            }
        }

        public string Address
        {
            get { return AddressSettingsEntry.Address; }
            set
            {
                AddressSettingsEntry.Address = value;
                OnPropertyChanged("Address");
            }
        }

        public long NumberTransactions
        {
            get { return AddressModel.NumberTransactions; }
            set
            {
                AddressModel.NumberTransactions = value;
                OnPropertyChanged("NumberTransactions");
            }
        }

        public long NumberUnredeemed
        {
            get { return AddressModel.NumberUnredeemed; }
            set
            {
                AddressModel.NumberUnredeemed = value;
                OnPropertyChanged("NumberUnredeemed");
            }
        }

        public decimal TotalReceived
        {
            get { return AddressModel.TotalReceived; }
            set
            {
                AddressModel.TotalReceived = value;
                OnPropertyChanged("TotalReceived");
            }
        }

        public decimal TotalSent
        {
            get { return AddressModel.TotalSent; }
            set
            {
                AddressModel.TotalSent = value;
                OnPropertyChanged("TotalSent");
            }
        }

        public decimal FinalBalance
        {
            get { return AddressModel.FinalBalance; }
            set
            {
                AddressModel.FinalBalance = value;
                OnPropertyChanged("FinalBalance");
            }
        }
    }
}
