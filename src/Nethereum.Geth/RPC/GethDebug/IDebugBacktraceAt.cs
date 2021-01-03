using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Geth.RPC.Debug
{
    public interface IDebugBacktraceAt
    {
        RpcRequest BuildRequest(string fileAndLine, object id = null);
        Task<string> SendRequestAsync(string fileAndLine, object id = null);
    }
}