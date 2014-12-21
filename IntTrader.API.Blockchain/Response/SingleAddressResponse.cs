using System;
using IntTrader.API.Base.Response;
using IntTrader.API.Blockchain.Model;
using IntTrader.API.Converter;
using Newtonsoft.Json;

namespace IntTrader.API.Blockchain.Response
{
    public class SingleAddressResponse : ResponseBase
    {
        // "hash160":"660d4ef3a743e3e696ad990364e555c271ad504b",
        // "address":"1AJbsFZ64EpEfS5UAjAfcUG8pH8Jn3rn1F",
        // "n_tx":17,
        // "n_unredeemed":2,
        // "total_received":1031350000,
        // "total_sent":931250000,
        // "final_balance":100100000,
        // "txs":[--Array of Transactions--]

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

        public SingleAddressModel Transform()
        {
            return new SingleAddressModel
                {
                    Address = Address,
                    Hash160 = Hash160,
                    FinalBalance = DecimalConverter.ConvertToPrecision(FinalBalance, 100000000),
                    TotalReceived = DecimalConverter.ConvertToPrecision(TotalReceived, 100000000),
                    TotalSent = DecimalConverter.ConvertToPrecision(TotalSent, 100000000),
                    NumberTransactions = NTx,
                    NumberUnredeemed = NUnredeemed
                };
        }
    }
}
