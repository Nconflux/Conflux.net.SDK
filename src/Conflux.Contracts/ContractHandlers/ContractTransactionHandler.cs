using System;
using System.Threading;
using System.Threading.Tasks;
using Conflux.Contracts.Extensions;
using Conflux.Contracts.TransactionHandlers;
using Conflux.Hex.HexTypes;
using Conflux.RPC.Eth.DTOs;
using Conflux.RPC.TransactionManagers;

namespace Conflux.Contracts.ContractHandlers
{
#if !DOTNET35
    public class ContractTransactionHandler<TContractMessage> : ContractTransactionHandlerBase, IContractTransactionHandler<TContractMessage> where TContractMessage : FunctionMessage, new()
    {
        private ITransactionEstimatorHandler<TContractMessage> _estimatorHandler;
        private ITransactionReceiptPollHandler<TContractMessage> _receiptPollHandler;
        private ITransactionSenderHandler<TContractMessage> _transactionSenderHandler;
        private ITransactionSigner<TContractMessage> _transactionSigner;


        public ContractTransactionHandler(ITransactionManager transactionManager) : base(transactionManager)
        {
            _estimatorHandler = new TransactionEstimatorHandler<TContractMessage>(transactionManager);
            _receiptPollHandler = new TransactionReceiptPollHandler<TContractMessage>(transactionManager);
            _transactionSenderHandler = new TransactionSenderHandler<TContractMessage>(transactionManager);
            _transactionSigner = new TransactionSignerHandler<TContractMessage>(transactionManager);
        }

        public Task<string> SignTransactionAsync(
            string contractAddress, TContractMessage functionMessage = null)
        {
            return _transactionSigner.SignTransactionAsync(contractAddress, functionMessage);
        }

        public Task<TransactionReceipt> SendTransactionAndWaitForReceiptAsync(
            string contractAddress, TContractMessage functionMessage = null, CancellationTokenSource tokenSource = null)
        {
            return _receiptPollHandler.SendTransactionAsync(contractAddress, functionMessage, tokenSource);
        }

        [Obsolete("Use SendTransactionAndWaitForReceipt instead")]
        public Task<TransactionReceipt> SendRequestAndWaitForReceiptAsync(
            string contractAddress, TContractMessage functionMessage = null, CancellationTokenSource tokenSource = null)
        {
            return SendTransactionAndWaitForReceiptAsync(contractAddress, functionMessage, tokenSource);
        }

        public Task<string> SendTransactionAsync(string contractAddress, TContractMessage functionMessage = null)
        {
            return _transactionSenderHandler.SendTransactionAsync(contractAddress, functionMessage);
        }

        [Obsolete("Use SendTransactionAsync instead")]
        public Task<string> SendRequestAsync(string contractAddress, TContractMessage functionMessage = null)
        {
            return SendTransactionAsync(contractAddress, functionMessage);
        }

        public async Task<TransactionInput> CreateTransactionInputEstimatingGasAsync(
            string contractAddress, TContractMessage functionMessage = null)
        {
            var gasAndCollateralEstimate = await EstimateGasAndCollateralAsync(contractAddress, functionMessage).ConfigureAwait(false);
            functionMessage.Gas = gasAndCollateralEstimate.GasUsed;
            functionMessage.Storage = gasAndCollateralEstimate.StorageCollateralized;
            return functionMessage.CreateTransactionInput(contractAddress);
        }

        public Task<EstimatedGasAndCollateral> EstimateGasAndCollateralAsync(string contractAddress, TContractMessage functionMessage = null)
        {
            return _estimatorHandler.EstimateGasAndCollateralAsync(contractAddress, functionMessage);
        }
    }
#endif

}