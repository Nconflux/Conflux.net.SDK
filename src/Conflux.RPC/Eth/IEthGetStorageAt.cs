using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth
{
    public interface IEthGetStorageAt
    {
        BlockParameter DefaultBlock { get; set; }

        RpcRequest BuildRequest(string address, HexBigInteger position, BlockParameter block, object id = null);
        Task<string> SendRequestAsync(string address, HexBigInteger position, object id = null);
        Task<string> SendRequestAsync(string address, HexBigInteger position, BlockParameter block, object id = null);
    }
}