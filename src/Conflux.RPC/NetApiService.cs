using Conflux.JsonRpc.Client;
using Conflux.RPC.Net;

namespace Conflux.RPC
{
    public class NetApiService : RpcClientWrapper, INetApiService
    {
        public NetApiService(IClient client) : base(client)
        {
            Listening = new NetListening(client);
            PeerCount = new NetPeerCount(client);
            Version = new NetVersion(client);
        }

        public INetListening Listening { get; private set; }
        public INetPeerCount PeerCount { get; private set; }
        public INetVersion Version { get; private set; }
    }
}