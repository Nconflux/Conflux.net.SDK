using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth.Uncles
{
    public interface IEthGetUncleByBlockHashAndIndex
    {
        RpcRequest BuildRequest(string blockHash, HexBigInteger uncleIndex, object id = null);
        Task<BlockWithTransactionHashes> SendRequestAsync(string blockHash, HexBigInteger uncleIndex, object id = null);
    }
}