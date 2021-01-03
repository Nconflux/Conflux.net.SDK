using Conflux.RPC.Eth.Blocks;

namespace Conflux.RPC.Eth.Services
{
    public interface IEthApiBlockService
    {
        IEthBlockNumber GetBlockNumber { get; }
        IEthGetBlockTransactionCountByHash GetBlockTransactionCountByHash { get; }
        IEthGetBlockTransactionCountByNumber GetBlockTransactionCountByNumber { get; }
        IEthGetBlockWithTransactionsByHash GetBlockWithTransactionsByHash { get; }
        IEthGetBlockWithTransactionsByNumber GetBlockWithTransactionsByNumber { get; }
        IEthGetBlockWithTransactionsHashesByHash GetBlockWithTransactionsHashesByHash { get; }
        IEthGetBlockWithTransactionsHashesByNumber GetBlockWithTransactionsHashesByNumber { get; }
    }
}