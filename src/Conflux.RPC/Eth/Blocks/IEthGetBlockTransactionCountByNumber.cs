using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth.Blocks
{
    public interface IEthGetBlockTransactionCountByNumber
    {
        RpcRequest BuildRequest(BlockParameter block, object id = null);
        Task<HexBigInteger> SendRequestAsync(object id = null);
        Task<HexBigInteger> SendRequestAsync(BlockParameter block, object id = null);
    }
}