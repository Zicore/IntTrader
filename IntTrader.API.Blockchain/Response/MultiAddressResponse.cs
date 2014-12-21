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
    [JsonConverter(typeof(MultiAddressConverter))]
    public class MultiAddressResponse : BlockchainResponse
    {
        MultiAddressResponseData _response = new MultiAddressResponseData();

        public MultiAddressResponseData Response
        {
            get { return _response; }
            set { _response = value; }
        }

        public MultiAddressModel Transform()
        {
            var model = new MultiAddressModel();
            foreach (var address in Response.Addresses)
            {
                model.Items.Add(address.Transform());
            }
            return model;
        }

        public override void VerifyResponse()
        {
            if (!String.IsNullOrEmpty(Message))
            {
                ResponseData.ResponseState = ResponseState.Error;
            }
            base.VerifyResponse();
        }
    }

    public class MultiAddressResponseData
    {
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

    public class MultiAddressConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(MultiAddressResponse));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            if (jObject.Type == JTokenType.String)
            {
                return new MultiAddressResponse { Message = jObject.ToObject<String>() };
            }

            if (jObject.Type == JTokenType.Object)
            {
                return new MultiAddressResponse { Response = jObject.ToObject<MultiAddressResponseData>() };
            }

            return null;
        }
    }
}
