using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Newtonsoft.Json.Linq;

namespace Conflux.Geth.RPC.Debug
{
    public interface IDebugDumpBlock
    {
        RpcRequest BuildRequest(ulong blockNumber, object id = null);
        Task<JObject> SendRequestAsync(ulong blockNumber, object id = null);
    }
}