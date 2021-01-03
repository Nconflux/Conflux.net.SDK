using Conflux.RPC.Infrastructure;
using Newtonsoft.Json.Linq;

namespace Conflux.Pantheon.RPC.Txpool
{
    public interface ITxpoolPantheonStatistics : IGenericRpcRequestResponseHandlerNoParam<JObject>
    {
    }
}