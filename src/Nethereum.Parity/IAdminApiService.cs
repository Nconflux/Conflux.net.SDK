using Conflux.Parity.RPC.Admin;

namespace Conflux.Parity
{
    public interface IAdminApiService
    {
        IParityConsensusCapability ConsensusCapability { get; }
        IParityListOpenedVaults ListOpenedVaults { get; }
        IParityListVaults ListVaults { get; }
        IParityLocalTransactions LocalTransactions { get; }
        IParityPendingTransactionsStats PendingTransactionsStats { get; }
        IParityReleasesInfo ReleasesInfo { get; }
        IParityVersionInfo VersionInfo { get; }
    }
}