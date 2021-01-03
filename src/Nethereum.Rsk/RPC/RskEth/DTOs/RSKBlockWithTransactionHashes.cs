using System.Numerics;
using Conflux.ABI.Util;
using Conflux.Hex.HexTypes;
using Conflux.RPC.Eth.DTOs;
using Newtonsoft.Json;

namespace Conflux.Rsk.RPC.RskEth.DTOs
{
    public class RskBlockWithTransactionHashes : BlockWithTransactionHashes, IRskBlockExtended
    {
        /// <summary>
        ///     QUANTITY - the minimum gas price in Wei
        /// </summary>
        [JsonProperty(PropertyName = "minimumGasPrice")]
        public string MinimumGasPriceString { get; set; }

        
        

    }

}