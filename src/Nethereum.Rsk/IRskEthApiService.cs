using Conflux.Rsk.RPC.RskEth;

namespace Conflux.Rsk
{
    public interface IRskEthApiService
    {
        IRskEthGetBlockWithTransactionsByHash GetBlockWithTransactionsByHash { get; }
        IRskEthGetBlockWithTransactionsByNumber GetBlockWithTransactionsByNumber { get; }
        IRskEthGetBlockWithTransactionsHashesByHash GetBlockWithTransactionsHashesByHash { get; }
        IRskEthGetBlockWithTransactionsHashesByNumber GetBlockWithTransactionsHashesByNumber { get; }
    }
}