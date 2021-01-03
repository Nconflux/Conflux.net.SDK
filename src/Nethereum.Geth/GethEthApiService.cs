using Conflux.Geth.RPC.GethEth;
using Conflux.JsonRpc.Client;
using Conflux.RPC;

namespace Conflux.Geth
{
    public class GethEthApiService : RpcClientWrapper, IGethEthApiService
    {
        public GethEthApiService(IClient client) : base(client)
        {
            PendingTransactions = new EthPendingTransactions(client);
        }

        public IEthPendingTransactions PendingTransactions { get; }
    }
}