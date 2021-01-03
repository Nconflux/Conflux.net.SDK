using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;
using Conflux.Rsk.RPC.RskEth.DTOs;

namespace Conflux.Rsk.RPC.RskEth
{
    public interface IRskEthGetBlockWithTransactionsHashesByNumber
    {
        Task<RskBlockWithTransactionHashes> SendRequestAsync(HexBigInteger number, object id = null);
        Task<RskBlockWithTransactionHashes> SendRequestAsync(BlockParameter blockParameter, object id = null);
        RpcRequest BuildRequest(HexBigInteger number, object id = null);
        RpcRequest BuildRequest(BlockParameter blockParameter, object id = null);
    }
}