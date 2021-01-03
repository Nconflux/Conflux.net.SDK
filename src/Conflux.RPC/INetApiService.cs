using Conflux.RPC.Net;

namespace Conflux.RPC
{
    public interface INetApiService
    {
        INetListening Listening { get; }
        INetPeerCount PeerCount { get; }
        INetVersion Version { get; }
    }
}