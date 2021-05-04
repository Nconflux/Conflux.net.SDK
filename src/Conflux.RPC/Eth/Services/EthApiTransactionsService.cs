using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;
using Conflux.RPC.Eth.Transactions;

namespace Conflux.RPC.Eth.Services
{
    public class EthApiTransactionsService : RpcClientWrapper, IEthApiTransactionsService
    {
        public EthApiTransactionsService(IClient client) : base(client)
        {
            Call = new EthCall(client);
            EstimateGasAndCollateral = new EthEstimatedGasAndCollateral(client);
            GetTransactionByBlockHashAndIndex = new EthGetTransactionByBlockHashAndIndex(client);
            GetTransactionByBlockNumberAndIndex = new EthGetTransactionByBlockNumberAndIndex(client);
            GetTransactionByHash = new EthGetTransactionByHash(client);
            GetTransactionCount = new EthGetTransactionCount(client);
            GetTransactionReceipt = new EthGetTransactionReceipt(client);
            SendRawTransaction = new EthSendRawTransaction(client);
            SendTransaction = new EthSendTransaction(client);
           
        }

        public IEthGetTransactionByBlockHashAndIndex GetTransactionByBlockHashAndIndex { get; }
        public IEthGetTransactionByBlockNumberAndIndex GetTransactionByBlockNumberAndIndex { get; }
        public IEthGetTransactionByHash GetTransactionByHash { get; }
        public IEthGetTransactionCount GetTransactionCount { get; }
        public IEthGetTransactionReceipt GetTransactionReceipt { get; }
        public IEthSendRawTransaction SendRawTransaction { get; }
        public IEthSendTransaction SendTransaction { get; }
        public IEthCall Call { get; }
        public IEthEstimatedGasAndCollateral EstimateGasAndCollateral { get; }

        public void SetDefaultBlock(BlockParameter blockParameter)
        {
            Call.DefaultBlock = blockParameter;
            GetTransactionCount.DefaultBlock = blockParameter;
        }
    }
}