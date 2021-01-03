using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;

namespace Conflux.Geth.RPC.Admin
{
    /// <Summary>
    ///     The datadir administrative property can be queried for the absolute path the running Geth node currently uses to
    ///     store all its databases.
    /// </Summary>
    public class AdminDatadir : GenericRpcRequestResponseHandlerNoParam<string>, IAdminDatadir
    {
        public AdminDatadir(IClient client) : base(client, ApiMethods.admin_datadir.ToString())
        {
        }
    }
}