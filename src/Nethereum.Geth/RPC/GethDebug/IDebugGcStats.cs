using Conflux.RPC.Infrastructure;
using Newtonsoft.Json.Linq;

namespace Conflux.Geth.RPC.Debug
{
    public interface IDebugGcStats : IGenericRpcRequestResponseHandlerNoParam<JObject>
    {

    }
}