using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth.Filters
{
    public interface IEthNewFilter
    {
        RpcRequest BuildRequest(NewFilterInput newFilterInput, object id = null);
        Task<HexBigInteger> SendRequestAsync(NewFilterInput newFilterInput, object id = null);
    }
}