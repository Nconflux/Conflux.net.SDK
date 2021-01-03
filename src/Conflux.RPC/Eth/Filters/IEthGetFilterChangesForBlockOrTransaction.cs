using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;

namespace Conflux.RPC.Eth.Filters
{
    public interface IEthGetFilterChangesForBlockOrTransaction
    {
        RpcRequest BuildRequest(HexBigInteger filterId, object id = null);
        Task<string[]> SendRequestAsync(HexBigInteger filterId, object id = null);
    }
}