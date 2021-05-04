using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth
{
    public interface IEthGetEpochNumber
    {
        BlockParameter DefaultBlock { get; set; }

        RpcRequest BuildRequest(string address, BlockParameter block, object id = null);
        Task<HexBigInteger> SendRequestAsync(object id = null);
        Task<HexBigInteger> SendRequestAsync(string address, BlockParameter block, object id = null);
    }
}