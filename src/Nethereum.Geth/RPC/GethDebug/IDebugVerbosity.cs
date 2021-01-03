using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Geth.RPC.Debug
{
    public interface IDebugVerbosity
    {
        RpcRequest BuildRequest(int level, object id = null);
        Task<object> SendRequestAsync(int level, object id = null);
    }
}