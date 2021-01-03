using Conflux.Pantheon.RPC.Permissioning;

namespace Conflux.Pantheon
{
    public interface IPermissioningApiService
    {
        IPermAddAccountsToWhitelist AddAccountsToWhitelist { get; }
        IPermAddNodesToWhitelist AddNodesToWhitelist { get; }
        IPermRemoveAccountsFromWhitelist RemoveAccountsFromWhitelist { get; }
        IPermRemoveNodesFromWhitelist RemoveNodesFromWhitelist { get; }
        IPermGetAccountsWhitelist GetAccountsWhitelist { get; }
        IPermGetNodesWhitelist GetNodesWhitelist { get; }
        IPermReloadPermissionsFromFile ReloadPermissionsFromFile { get; }
    }
}