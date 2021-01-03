using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Pantheon.RPC.Admin
{
    /// <Summary>
    ///     Removes a static node.
    /// </Summary>
    public class AdminRemovePeer : RpcRequestResponseHandler<bool>, IAdminRemovePeer
    {
        public AdminRemovePeer(IClient client) : base(client, ApiMethods.admin_removePeer.ToString())
        {
        }

        public async Task<bool> SendRequestAsync(string enodeUrl, object id = null)
        {
            return await base.SendRequestAsync(id, enodeUrl);
        }

        public RpcRequest BuildRequest(string enodeUrl, object id = null)
        {
            return base.BuildRequest(id, enodeUrl);
        }
    }
}