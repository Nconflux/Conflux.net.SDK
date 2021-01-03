using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Newtonsoft.Json.Linq;

namespace Conflux.Parity.RPC.Trace
{
    public interface ITraceRawTransaction
    {
        RpcRequest BuildRequest(string rawTransaction, TraceType[] traceTypes, object id = null);
        Task<JObject> SendRequestAsync(string rawTransaction, TraceType[] traceTypes, object id = null);
    }
}