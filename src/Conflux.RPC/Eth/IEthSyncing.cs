using System.Threading.Tasks;
using Conflux.RPC.Eth.DTOs;
using Conflux.RPC.Infrastructure;

namespace Conflux.RPC.Eth
{
    public interface IEthSyncing: IGenericRpcRequestResponseHandlerNoParam<object>
    {
#if !DOTNET35
        Task<SyncingOutput> SendRequestAsync(object id = null);
#endif
    }
}