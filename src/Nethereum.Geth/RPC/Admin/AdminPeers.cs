using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;
using Newtonsoft.Json.Linq;

namespace Conflux.Geth.RPC.Admin
{
    /// <Summary>
    ///     The peers administrative property can be queried for all the information known about the connected remote nodes at
    ///     the networking granularity. These include general information about the nodes themselves as participants of the
    ///     ÐΞVp2p P2P overlay protocol, as well as specialized information added by each of the running application protocols
    ///     (e.g. eth, les, shh, bzz).
    /// </Summary>
    public class AdminPeers : GenericRpcRequestResponseHandlerNoParam<JArray>, IAdminPeers
    {
        public AdminPeers(IClient client) : base(client, ApiMethods.admin_peers.ToString())
        {
        }
    }
}