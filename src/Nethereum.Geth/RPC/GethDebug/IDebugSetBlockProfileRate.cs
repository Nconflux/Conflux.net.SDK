using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Geth.RPC.Debug
{
    public interface IDebugSetBlockProfileRate
    {
        RpcRequest BuildRequest(long rate, object id = null);
        Task<object> SendRequestAsync(long rate, object id = null);
    }
}