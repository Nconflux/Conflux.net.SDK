using Conflux.RPC.Infrastructure;
using Newtonsoft.Json.Linq;

namespace Conflux.Pantheon.RPC.Clique
{
    public interface ICliqueProposals : IGenericRpcRequestResponseHandlerNoParam<JObject>
    {
    }
}