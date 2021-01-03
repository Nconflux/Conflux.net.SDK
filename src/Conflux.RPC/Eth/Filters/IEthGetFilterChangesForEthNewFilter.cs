using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth.Filters
{
    public interface IEthGetFilterChangesForEthNewFilter
    {
        RpcRequest BuildRequest(HexBigInteger filterId, object id = null);
        Task<FilterLog[]> SendRequestAsync(HexBigInteger filterId, object id = null);
    }
}