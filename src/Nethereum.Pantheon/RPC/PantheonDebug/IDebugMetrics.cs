using Conflux.RPC.Infrastructure;
using Newtonsoft.Json.Linq;

namespace Conflux.Pantheon.RPC.Debug
{
    public interface IDebugMetrics : IGenericRpcRequestResponseHandlerNoParam<JObject>
    {
    }
}