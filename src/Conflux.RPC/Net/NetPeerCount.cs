using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;

namespace Conflux.RPC.Net
{
    /// <Summary>
    ///     net_peerCount
    ///     Returns number of peers currenly connected to the client.
    ///     Parameters
    ///     none
    ///     Returns
    ///     QUANTITY - integer of the number of connected peers.
    ///     Example
    ///     Request
    ///     curl -X POST --data '{"jsonrpc":"2.0","method":"net_peerCount","params":[],"id":74}'
    ///     Result
    ///     {
    ///     "id":74,
    ///     "jsonrpc": "2.0",
    ///     "result": "0x2" // 2
    ///     }
    /// </Summary>
    public class NetPeerCount : GenericRpcRequestResponseHandlerNoParam<HexBigInteger>, INetPeerCount
    {
        public NetPeerCount(IClient client) : base(client, ApiMethods.net_peerCount.ToString())
        {
        }
    }
}