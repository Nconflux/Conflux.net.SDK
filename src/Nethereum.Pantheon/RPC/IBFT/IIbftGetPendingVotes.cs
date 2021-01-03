using Conflux.RPC.Infrastructure;
using Newtonsoft.Json.Linq;

namespace Conflux.Pantheon.RPC.IBFT
{
    public interface IIbftGetPendingVotes : IGenericRpcRequestResponseHandlerNoParam<JObject>
    {
    }
}