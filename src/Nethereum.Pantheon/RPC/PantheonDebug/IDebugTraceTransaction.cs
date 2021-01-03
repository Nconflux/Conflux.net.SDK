using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Newtonsoft.Json.Linq;

namespace Conflux.Pantheon.RPC.Debug
{

    public interface IDebugTraceTransaction
    {
        Task<JObject> SendRequestAsync(string transactionHash, object id = null);
        RpcRequest BuildRequest(string transactionHash, object id = null);
    }

}