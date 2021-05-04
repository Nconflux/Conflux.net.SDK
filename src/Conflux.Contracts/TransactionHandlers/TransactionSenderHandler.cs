using System.Threading.Tasks;
using Conflux.Contracts.Extensions;
using Conflux.Hex.HexTypes;
using Conflux.RPC.Eth.DTOs;
using Conflux.RPC.TransactionManagers;

namespace Conflux.Contracts.TransactionHandlers
{
#if !DOTNET35
    public class TransactionSenderHandler<TFunctionMessage> :
        TransactionHandlerBase<TFunctionMessage>,
        ITransactionSenderHandler<TFunctionMessage> where TFunctionMessage : FunctionMessage, new()
    {
        private ITransactionEstimatorHandler<TFunctionMessage> _contractTransactionEstimatorHandler;

        public TransactionSenderHandler(ITransactionManager transactionManager) : this(transactionManager,
            new TransactionEstimatorHandler<TFunctionMessage>(transactionManager))
        {

        }

        public TransactionSenderHandler(ITransactionManager transactionManager,
            ITransactionEstimatorHandler<TFunctionMessage> contractTransactionEstimatorHandler) : base(transactionManager)
        {
            _contractTransactionEstimatorHandler = contractTransactionEstimatorHandler;
        }

        public async Task<string> SendTransactionAsync(string contractAddress, TFunctionMessage functionMessage = null)
        {
            if (functionMessage == null) functionMessage = new TFunctionMessage();
            SetEncoderContractAddress(contractAddress);
            if (functionMessage.Storage == null || functionMessage.Gas == null)
            {
                EstimatedGasAndCollateral estimatedGasAndCollateral = await _contractTransactionEstimatorHandler.EstimateGasAndCollateralAsync(contractAddress, functionMessage).ConfigureAwait(false);
                if (functionMessage.Gas == null)
                    functionMessage.Gas = estimatedGasAndCollateral.GasUsed;
                if (functionMessage.Storage == null)
                    functionMessage.Storage = estimatedGasAndCollateral.StorageCollateralized;
            }
            var transactionInput = FunctionMessageEncodingService.CreateTransactionInput(functionMessage);
            return await TransactionManager.SendTransactionAsync(transactionInput).ConfigureAwait(false);
        }
    }
#endif
}
