using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Geth.RPC.Debug
{
    public interface IDebugGoTrace
    {
        RpcRequest BuildRequest(string fileName, int seconds, object id = null);
        Task<object> SendRequestAsync(string fileName, int seconds, object id = null);
    }
}