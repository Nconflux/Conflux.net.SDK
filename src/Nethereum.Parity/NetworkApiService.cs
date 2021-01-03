using Conflux.JsonRpc.Client;
using Conflux.Parity.RPC.Network;
using Conflux.RPC;

namespace Conflux.Parity
{
    public class NetworkApiService : RpcClientWrapper, INetworkApiService
    {
        public NetworkApiService(IClient client) : base(client)
        {
            ChainStatus = new ParityChainStatus(client);
            GasPriceHistogram = new ParityGasPriceHistogram(client);
            NetPeers = new ParityNetPeers(client);
            NetPort = new ParityNetPort(client);
            PendingTransactions = new ParityPendingTransactions(client);
        }

        public IParityChainStatus ChainStatus { get; }
        public IParityGasPriceHistogram GasPriceHistogram { get; }
        public IParityNetPeers NetPeers { get; }
        public IParityNetPort NetPort { get; }
        public IParityPendingTransactions PendingTransactions { get; }
    }
}