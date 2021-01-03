using Conflux.JsonRpc.Client;
using Conflux.Pantheon.RPC.Txpool;
using Conflux.RPC;

namespace Conflux.Pantheon
{
    public class TxPoolApiService : RpcClientWrapper, ITxPoolApiService
    {
        public TxPoolApiService(IClient client) : base(client)
        {
            PantheonStatistics = new TxpoolPantheonStatistics(client);
            PantheonTransactions = new TxpoolPantheonTransactions(client);
        }

        public ITxpoolPantheonStatistics PantheonStatistics { get; }
        public ITxpoolPantheonTransactions PantheonTransactions { get; }

    }
}