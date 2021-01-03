using System.Threading.Tasks;
using Conflux.Hex.HexConvertors.Extensions;
using Conflux.JsonRpc.Client;
using Conflux.RPC;
using Conflux.Rsk.RPC.RskEth.DTOs;

namespace Conflux.Rsk.RPC.RskEth
{
    public class RskEthGetBlockWithTransactionsByHash : RpcRequestResponseHandler<RskBlockWithTransactions>, IRskEthGetBlockWithTransactionsByHash
    {
        public RskEthGetBlockWithTransactionsByHash(IClient client)
            : base(client, ApiMethods.cfx_getBlockByHash.ToString())
        {
        }

        public Task<RskBlockWithTransactions> SendRequestAsync(string blockHash, object id = null)
        {
            return base.SendRequestAsync(id, blockHash.EnsureHexPrefix(), true);
        }

        public RpcRequest BuildRequest(string blockHash, object id = null)
        {
            return base.BuildRequest(id, blockHash.EnsureHexPrefix(), true);
        }
    }
}