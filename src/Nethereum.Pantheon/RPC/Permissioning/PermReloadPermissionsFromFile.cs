using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;

namespace Conflux.Pantheon.RPC.Permissioning
{
    /// <Summary>
    ///     Reloads the accounts and nodes whitelists from the permissions configuration file.
    ///     Returns
    ///     result - Success or error if the permissions configuration file is not valid.
    /// </Summary>
    public class PermReloadPermissionsFromFile : GenericRpcRequestResponseHandlerNoParam<string>,
        IPermReloadPermissionsFromFile
    {
        public PermReloadPermissionsFromFile(IClient client) : base(client,
            ApiMethods.perm_reloadPermissionsFromFile.ToString())
        {
        }
    }
}