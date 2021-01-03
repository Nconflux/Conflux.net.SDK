using Conflux.JsonRpc.Client;
using Conflux.Pantheon.RPC.Admin;
using Conflux.RPC;


namespace Conflux.Pantheon
{
    public class AdminApiService : RpcClientWrapper, IAdminApiService
    {
        public AdminApiService(IClient client) : base(client)
        {
            AddPeer = new AdminAddPeer(client);
            NodeInfo = new AdminNodeInfo(client);
            Peers = new AdminPeers(client);
            RemovePeer = new AdminRemovePeer(client);
        }

        public IAdminAddPeer AddPeer { get; }
        public IAdminNodeInfo NodeInfo { get; }
        public IAdminPeers Peers { get; }
        public IAdminRemovePeer RemovePeer { get; }
    }
}