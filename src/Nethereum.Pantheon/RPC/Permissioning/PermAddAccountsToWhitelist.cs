using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Pantheon.RPC.Permissioning
{
    /// <Summary>
    ///     Adds accounts (participants) to the accounts whitelist.
    ///     Returns
    ///     result - Success or error. Errors include attempting to add accounts already on the whitelist or including invalid
    ///     account addresses.
    /// </Summary>
    public class PermAddAccountsToWhitelist : RpcRequestResponseHandler<string>, IPermAddAccountsToWhitelist
    {
        public PermAddAccountsToWhitelist(IClient client) : base(client,
            ApiMethods.perm_addAccountsToWhitelist.ToString())
        {
        }

        public async Task<string> SendRequestAsync(string[] addresses, object id = null)
        {
            return await base.SendRequestAsync(id, new object[]{addresses});
        }

        public RpcRequest BuildRequest(string[] addresses, object id = null)
        {
            return base.BuildRequest(id, new object[]{addresses});
        }
    }
}