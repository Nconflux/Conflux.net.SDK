using Conflux.JsonRpc.Client;
using Conflux.Parity.RPC.Admin;
using Conflux.RPC;

namespace Conflux.Parity
{
    public class AdminApiService : RpcClientWrapper, IAdminApiService
    {
        public AdminApiService(IClient client) : base(client)
        {
            ConsensusCapability = new ParityConsensusCapability(client);
            ListOpenedVaults = new ParityListOpenedVaults(client);
            ListVaults = new ParityListVaults(client);
            LocalTransactions = new ParityLocalTransactions(client);
            PendingTransactionsStats = new ParityPendingTransactionsStats(client);
            ReleasesInfo = new ParityReleasesInfo(client);
            VersionInfo = new ParityVersionInfo(client);
        }

        public IParityConsensusCapability ConsensusCapability { get; }
        public IParityListOpenedVaults ListOpenedVaults { get; }
        public IParityListVaults ListVaults { get; }
        public IParityLocalTransactions LocalTransactions { get; }
        public IParityPendingTransactionsStats PendingTransactionsStats { get; }
        public IParityReleasesInfo ReleasesInfo { get; }
        public IParityVersionInfo VersionInfo { get; }
    }
}