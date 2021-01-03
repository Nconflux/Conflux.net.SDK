using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth.Blocks
{
    public interface IEthGetBlockWithTransactionsHashesByHash
    {
        RpcRequest BuildRequest(string blockHash, object id = null);
        Task<BlockWithTransactionHashes> SendRequestAsync(string blockHash, object id = null);
    }
}