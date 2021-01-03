using Conflux.Geth.RPC.GethEth;

namespace Conflux.Geth
{
    public interface IGethEthApiService
    {
        IEthPendingTransactions PendingTransactions { get; }
    }
}