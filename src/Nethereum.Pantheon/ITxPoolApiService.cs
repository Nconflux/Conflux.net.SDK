using Conflux.Pantheon.RPC.Txpool;

namespace Conflux.Pantheon
{
    public interface ITxPoolApiService
    {
        ITxpoolPantheonStatistics PantheonStatistics { get; }
        ITxpoolPantheonTransactions PantheonTransactions { get; }
    }
}