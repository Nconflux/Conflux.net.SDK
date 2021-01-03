using System.Threading.Tasks;
using Conflux.Geth.RPC.Debug.DTOs;
using Conflux.JsonRpc.Client;
using Newtonsoft.Json.Linq;

namespace Conflux.Geth.RPC.Debug
{
    public interface IDebugTraceTransaction
    {
        RpcRequest BuildRequest(string txnHash, TraceTransactionOptions options, object id = null);
        Task<JObject> SendRequestAsync(string txnHash, TraceTransactionOptions options, object id = null);
    }
}