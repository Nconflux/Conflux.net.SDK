using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Geth.RPC.Debug
{
    public interface IDebugVmodule
    {
        RpcRequest BuildRequest(string pattern, object id = null);
        Task<object> SendRequestAsync(string pattern, object id = null);
    }
}