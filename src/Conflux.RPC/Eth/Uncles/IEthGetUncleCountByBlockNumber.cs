using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;

namespace Conflux.RPC.Eth.Uncles
{
    public interface IEthGetUncleCountByBlockNumber
    {
        RpcRequest BuildRequest(HexBigInteger blockNumber, object id = null);
        Task<HexBigInteger> SendRequestAsync(HexBigInteger blockNumber, object id = null);
    }
}