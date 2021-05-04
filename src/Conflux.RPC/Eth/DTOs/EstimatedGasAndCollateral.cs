using Conflux.Hex.HexTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conflux.RPC.Eth.DTOs
{
    public class EstimatedGasAndCollateral
    {
        [JsonProperty(PropertyName = "gasUsed")]
        public HexBigInteger GasUsed { get; set; }
        [JsonProperty(PropertyName = "storageCollateralized")]
        public HexBigInteger StorageCollateralized { get; set; }
    }
}
