using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Geth.RPC.Debug
{
    public interface IDebugCpuProfile
    {
        RpcRequest BuildRequest(string filePath, int seconds, object id = null);
        Task<object> SendRequestAsync(string filePath, int seconds, object id = null);
    }
}