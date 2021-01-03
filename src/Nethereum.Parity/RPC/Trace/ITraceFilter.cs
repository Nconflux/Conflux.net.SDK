using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Conflux.Parity.RPC.Trace.TraceDTOs;
using Newtonsoft.Json.Linq;

namespace Conflux.Parity.RPC.Trace
{
    public interface ITraceFilter
    {
        RpcRequest BuildRequest(TraceFilterDTO traceFilter, object id = null);
        Task<JArray> SendRequestAsync(TraceFilterDTO traceFilter, object id = null);
    }
}