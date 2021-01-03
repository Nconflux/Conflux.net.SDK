using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;
using Newtonsoft.Json.Linq;

namespace Conflux.Parity.RPC.Trace
{
    public interface ITraceCall
    {
        RpcRequest BuildRequest(CallInput callInput, TraceType[] typeOfTrace, BlockParameter block, object id = null);
        Task<JObject> SendRequestAsync(CallInput callInput, TraceType[] typeOfTrace, BlockParameter block, object id = null);
    }
}