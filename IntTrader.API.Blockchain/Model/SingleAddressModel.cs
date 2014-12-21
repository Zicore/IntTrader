using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntTrader.API.Blockchain.Model
{
    public class SingleAddressModel
    {
        private String _hash160;
        private String _address;
        private long _numberTransactions;
        private long _numberUnredeemed;
        private decimal _totalReceived;
        private decimal _totalSent;
        private decimal _finalBalance;

        public string Hash160
        {
            get { return _hash160; }
            set { _hash160 = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public long NumberTransactions
        {
            get { return _numberTransactions; }
            set { _numberTransactions = value; }
        }

        public long NumberUnredeemed
        {
            get { return _numberUnredeemed; }
            set { _numberUnredeemed = value; }
        }

        public decimal TotalReceived
        {
            get { return _totalReceived; }
            set { _totalReceived = value; }
        }

        public decimal TotalSent
        {
            get { return _totalSent; }
            set { _totalSent = value; }
        }

        public decimal FinalBalance
        {
            get { return _finalBalance; }
            set { _finalBalance = value; }
        }

        public override string ToString()
        {
            return string.Format(
                "Hash160: {0}, Address: {1}, NumberTransactions: {2}, NumberUnredeemed: {3}, TotalReceived: {4}, TotalSent: {5}, FinalBalance: {6}",
                Hash160, Address, NumberTransactions, NumberUnredeemed, TotalReceived, TotalSent, FinalBalance);
        }
    }
}
