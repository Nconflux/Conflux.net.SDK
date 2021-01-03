using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Geth.RPC.Debug
{
    public interface IDebugBlockProfile
    {
        RpcRequest BuildRequest(string file, long seconds, object id = null);
        Task<object> SendRequestAsync(string file, long seconds, object id = null);
    }
}