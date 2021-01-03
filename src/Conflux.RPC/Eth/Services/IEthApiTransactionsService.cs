using Conflux.RPC.Eth.DTOs;
using Conflux.RPC.Eth.Transactions;

namespace Conflux.RPC.Eth.Services
{
    public interface IEthApiTransactionsService
    {
        IEthCall Call { get; }
        IEthEstimateGas EstimateGas { get; }
        IEthGetTransactionByBlockHashAndIndex GetTransactionByBlockHashAndIndex { get; }
        IEthGetTransactionByBlockNumberAndIndex GetTransactionByBlockNumberAndIndex { get; }
        IEthGetTransactionByHash GetTransactionByHash { get; }
        IEthGetTransactionCount GetTransactionCount { get; }
        IEthGetTransactionReceipt GetTransactionReceipt { get; }
        IEthSendRawTransaction SendRawTransaction { get; }
        IEthSendTransaction SendTransaction { get; }

        void SetDefaultBlock(BlockParameter blockParameter);
    }
}