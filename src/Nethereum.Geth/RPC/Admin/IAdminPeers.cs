using Conflux.RPC.Infrastructure;
using Newtonsoft.Json.Linq;

namespace Conflux.Geth.RPC.Admin
{
    public interface IAdminPeers : IGenericRpcRequestResponseHandlerNoParam<JArray>
    {

    }
}