using Conflux.Parity.RPC.Network;

namespace Conflux.Parity
{
    public interface INetworkApiService
    {
        IParityChainStatus ChainStatus { get; }
        IParityGasPriceHistogram GasPriceHistogram { get; }
        IParityNetPeers NetPeers { get; }
        IParityNetPort NetPort { get; }
        IParityPendingTransactions PendingTransactions { get; }
    }
}