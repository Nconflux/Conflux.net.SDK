using System;
using System.Threading.Tasks;
using Conflux.Hex.HexConvertors.Extensions;
using Conflux.JsonRpc.Client;
using Conflux.RPC;
using Conflux.RPC.Eth.Blocks;
using Conflux.RPC.Eth.DTOs;
using Conflux.Rsk.RPC.RskEth.DTOs;

namespace Conflux.Rsk.RPC.RskEth
{
    public class RskEthGetBlockWithTransactionsHashesByHash : RpcRequestResponseHandler<RskBlockWithTransactionHashes>, IRskEthGetBlockWithTransactionsHashesByHash
    {
        public RskEthGetBlockWithTransactionsHashesByHash(IClient client)
            : base(client, ApiMethods.cfx_getBlockByHash.ToString())
        {
        }

        public Task<RskBlockWithTransactionHashes> SendRequestAsync(string blockHash, object id = null)
        {
            if (blockHash == null) throw new ArgumentNullException(nameof(blockHash));
            return base.SendRequestAsync(id, blockHash.EnsureHexPrefix(), false);
        }

        public RpcRequest BuildRequest(string blockHash, object id = null)
        {
            if (blockHash == null) throw new ArgumentNullException(nameof(blockHash));
            return base.BuildRequest(id, blockHash.EnsureHexPrefix(), false);
        }
    }
}