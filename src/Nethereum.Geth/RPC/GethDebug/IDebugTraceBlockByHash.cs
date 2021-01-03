using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Newtonsoft.Json.Linq;

namespace Conflux.Geth.RPC.Debug
{
    public interface IDebugTraceBlockByHash
    {
        RpcRequest BuildRequest(string hash, object id = null);
        Task<JObject> SendRequestAsync(string hash, object id = null);
    }
}