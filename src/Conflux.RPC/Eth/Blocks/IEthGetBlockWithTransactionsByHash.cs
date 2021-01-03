using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth.Blocks
{
    public interface IEthGetBlockWithTransactionsByHash
    {
        RpcRequest BuildRequest(string blockHash, object id = null);
        Task<BlockWithTransactions> SendRequestAsync(string blockHash, object id = null);
    }
}