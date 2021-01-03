using Conflux.RPC.Infrastructure;
using Newtonsoft.Json.Linq;

namespace Conflux.Pantheon.RPC.Admin
{
    public interface IAdminPeers : IGenericRpcRequestResponseHandlerNoParam<JArray>
    {
    }
}