using System.Threading.Tasks;
using Conflux.Contracts.Extensions;
using Conflux.Hex.HexTypes;
using Conflux.RPC.Eth.DTOs;
using Conflux.RPC.TransactionManagers;

namespace Conflux.Contracts.TransactionHandlers
{
#if !DOTNET35
    /// <summary>
    /// Signs a transaction estimating the gas if not set and retrieving the next nonce if not set
    /// </summary>
    public class TransactionSignerHandler<TFunctionMessage> :
        TransactionHandlerBase<TFunctionMessage>,
        ITransactionSigner<TFunctionMessage>
        where TFunctionMessage : FunctionMessage, new()
    {
        private ITransactionEstimatorHandler<TFunctionMessage> _contractTransactionEstimatorHandler;


        public TransactionSignerHandler(ITransactionManager transactionManager) : this(transactionManager,
            new TransactionEstimatorHandler<TFunctionMessage>(transactionManager))
        {

        }

        public TransactionSignerHandler(ITransactionManager transactionManager,
            ITransactionEstimatorHandler<TFunctionMessage> contractTransactionEstimatorHandler) : base(transactionManager)
        {
            _contractTransactionEstimatorHandler = contractTransactionEstimatorHandler;
        }



        public async Task<string> SignTransactionAsync(string contractAddress, TFunctionMessage functionMessage = null)
        {
            if(functionMessage == null) functionMessage = new TFunctionMessage();
            SetEncoderContractAddress(contractAddress);
            if (functionMessage.Storage == null || functionMessage.Gas == null)
            {
                EstimatedGasAndCollateral estimatedGasAndCollateral= await _contractTransactionEstimatorHandler.EstimateGasAndCollateralAsync(contractAddress, functionMessage).ConfigureAwait(false);
                if (functionMessage.Gas == null)
                    functionMessage.Gas = estimatedGasAndCollateral.GasUsed;
                if (functionMessage.Storage == null)
                    functionMessage.Storage = estimatedGasAndCollateral.StorageCollateralized; 
            }
            var transactionInput = FunctionMessageEncodingService.CreateTransactionInput(functionMessage);
            return await TransactionManager.SignTransactionAsync(transactionInput).ConfigureAwait(false);
        }
         
    }
#endif
}