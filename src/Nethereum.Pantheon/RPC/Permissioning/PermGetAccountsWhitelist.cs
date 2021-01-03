using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;

namespace Conflux.Pantheon.RPC.Permissioning
{
    /// <Summary>
    ///     Lists accounts (participants) in the accounts whitelist.
    /// </Summary>
    public class PermGetAccountsWhitelist : GenericRpcRequestResponseHandlerNoParam<string[]>, IPermGetAccountsWhitelist
    {
        public PermGetAccountsWhitelist(IClient client) : base(client, ApiMethods.perm_getAccountsWhitelist.ToString())
        {
        }
    }
}