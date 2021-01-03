using Conflux.Pantheon.RPC.Admin;

namespace Conflux.Pantheon
{
    public interface IAdminApiService
    {
        IAdminAddPeer AddPeer { get; }
        IAdminNodeInfo NodeInfo { get; }
        IAdminPeers Peers { get; }
        IAdminRemovePeer RemovePeer { get; }
    }
}