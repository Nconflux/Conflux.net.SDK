using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth.Filters
{
    public interface IEthGetLogs
    {
        RpcRequest BuildRequest(NewFilterInput newFilter, object id = null);
        Task<FilterLog[]> SendRequestAsync(NewFilterInput newFilter, object id = null);
    }
}