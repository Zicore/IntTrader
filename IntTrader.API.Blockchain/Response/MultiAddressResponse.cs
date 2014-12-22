using System;
using System.Collections.Generic;
using IntTrader.API.Base.Model;
using IntTrader.API.Base.Response;
using IntTrader.API.Blockchain.Model;
using IntTrader.API.Converter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IntTrader.API.Blockchain.Response
{
    public class MultiAddressResponse : BlockchainResponse
    {
        public MultiAddressModel Transform()
        {
            var model = new MultiAddressModel();
            foreach (var address in Addresses)
            {
                model.Items.Add(address.Transform());
            }
            return model;
        }

        public override void VerifyResponse()
        {
            base.VerifyResponse();
        }

        List<MultiAddressResponseEntry> _addresses = new List<MultiAddressResponseEntry>();

        public List<MultiAddressResponseEntry> Addresses
        {
            get { return _addresses; }
            set { _addresses = value; }
        }
    }

    public class MultiAddressResponseEntry
    {
        private String _hash160;
        private String _address;
        private long _nTx;
        private long _nUnredeemed;
        private decimal _totalReceived;
        private decimal _totalSent;
        private decimal _finalBalance;

        [JsonProperty("hash160")]
        public string Hash160
        {
            get { return _hash160; }
            set { _hash160 = value; }
        }

        [JsonProperty("address")]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        [JsonProperty("n_tx")]
        public long NTx
        {
            get { return _nTx; }
            set { _nTx = value; }
        }

        [JsonProperty("n_unredeemed")]
        public long NUnredeemed
        {
            get { return _nUnredeemed; }
            set { _nUnredeemed = value; }
        }

        [JsonProperty("total_received")]
        public decimal TotalReceived
        {
            get { return _totalReceived; }
            set { _totalReceived = value; }
        }

        [JsonProperty("total_sent")]
        public decimal TotalSent
        {
            get { return _totalSent; }
            set { _totalSent = value; }
        }

        [JsonProperty("final_balance")]
        public decimal FinalBalance
        {
            get { return _finalBalance; }
            set { _finalBalance = value; }
        }

        public MultiAddressEntryModel Transform()
        {
            var entry = new MultiAddressEntryModel
            {
                Address = Address,
                Hash160 = Hash160,
                FinalBalance = DecimalConverter.ConvertToPrecision(FinalBalance, 100000000),
                TotalReceived = DecimalConverter.ConvertToPrecision(TotalReceived, 100000000),
                TotalSent = DecimalConverter.ConvertToPrecision(TotalSent, 100000000),
                NumberTransactions = NTx,
                NumberUnredeemed = NUnredeemed
            };
            return entry;
        }
    }
}
