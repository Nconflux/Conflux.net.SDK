using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Geth.RPC.Debug
{
    public interface IDebugStartGoTrace
    {
        RpcRequest BuildRequest(string filePath, object id = null);
        Task<object> SendRequestAsync(string filePath, object id = null);
    }
}