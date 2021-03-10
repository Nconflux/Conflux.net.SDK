using Conflux.Hex.HexConvertors.Extensions;
using Conflux.Hex.HexTypes;
using Conflux.Util;
using Newtonsoft.Json;
using System;

namespace Conflux.RPC.Eth.DTOs
{
    /// <summary>
    ///     Object - The transaction call object
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class CallInput
    {
        private string _from;
        private string _to;
        private string _data;

        public CallInput()
        {
        }

        public CallInput(string data, string addressTo)
        {
            Data = data;
            To = addressTo;
        }

        public CallInput(string data, string addressTo, HexBigInteger value) : this(data, addressTo)
        {
            Value = value;
        }

        public CallInput(string data, string addressTo, string addressFrom, HexBigInteger gas, HexBigInteger value, HexBigInteger epochNumber = null, HexBigInteger nonce = null)
            : this(data, addressTo, value)
        {
            From = addressFrom;
            Gas = gas;
            EpochNumber = epochNumber;
            Nonce = nonce;
        }

        public CallInput(string data, string addressTo, string addressFrom, HexBigInteger gas, HexBigInteger gasPrice, HexBigInteger value)
            : this(data, addressTo, addressFrom, gas, value)
        {
            GasPrice = gasPrice;
        }

        public CallInput(string data, string addressFrom, HexBigInteger gas, HexBigInteger value)
            : this(data, null, value)
        {
            From = addressFrom;
            Gas = gas;
        }

        public CallInput(string data, HexBigInteger gas, string addressFrom)
        {
            Data = data;
            Gas = gas;
            From = addressFrom;
        }

        /// <summary>
        ///     DATA, 20 Bytes - The address the transaction is send from.
        /// </summary>
        [JsonProperty(PropertyName = "from")]
        public string From
        {
            get { return _from; }
            set { _from = EnsureCIP37Address(value); }
        }

        /// <summary>
        ///     DATA, 20 Bytes - (optional when creating new contract) The address the transaction is directed to.
        /// </summary>
        [JsonProperty(PropertyName = "to")]
        public string To
        {
            get { return _to; }
            set { _to = EnsureCIP37Address(value); }
        }
        /// <summary>
        ///  storageLimit: QUANTITY - (optional, default: 0) Integer of the storage collateral
        /// </summary>
        [JsonProperty(PropertyName = "storageLimit")]
        public HexBigInteger StorageLimit
        {
            get; set;
        }
        /// <summary>
        ///     QUANTITY - (optional, default: 90000) Integer of the gas provided for the transaction execution.It will return
        ///     unused gas.
        /// </summary>
        [JsonProperty(PropertyName = "gas")]
        public HexBigInteger Gas { get; set; }

        /// <summary>
        ///     gasPrice: QUANTITY - (optional, default: To-Be-Determined) Integer of the gasPrice used for each paid gas
        /// </summary>
        [JsonProperty(PropertyName = "gasPrice")]
        public HexBigInteger GasPrice { get; set; }

        /// <summary>
        ///     value: QUANTITY - (optional) Integer of the value send with this transaction
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public HexBigInteger Value { get; set; }

        /// <summary>
        ///     data: DATA - (optional) The compiled code of a contract
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public string Data
        {
            get { return _data.EnsureHexPrefix(); }
            set { _data = value; }
        }
 
        [JsonProperty(PropertyName = "epochNumber")]
        public HexBigInteger EpochNumber
        {
            get; set;
        }
        [JsonProperty(PropertyName = "nonce")]
        public HexBigInteger Nonce
        {
            get; set;
        }


        private static string EnsureCIP37Address(string address)
        {
            if (!string.IsNullOrWhiteSpace(address))
            {
                if (CIP37.IsHex40Address(address))
                    throw new Exception("Hex40 address is obslete, using CIP37 standard address.");
            }
            return address;
        }
    }
}