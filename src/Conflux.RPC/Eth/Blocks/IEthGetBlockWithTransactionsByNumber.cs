using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth.Blocks
{
    public interface IEthGetBlockWithTransactionsByNumber
    {
        RpcRequest BuildRequest(BlockParameter blockParameter, object id = null);
        RpcRequest BuildRequest(HexBigInteger number, object id = null);
        Task<BlockWithTransactions> SendRequestAsync(BlockParameter blockParameter, object id = null);
        Task<BlockWithTransactions> SendRequestAsync(HexBigInteger number, object id = null);
    }
}