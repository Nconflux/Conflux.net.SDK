using System;
using System.Threading.Tasks;
using Conflux.ABI.FunctionEncoding;
using Conflux.Hex.HexTypes;
using Conflux.RPC.Eth.Transactions;
using Conflux.RPC.TransactionManagers;

namespace Conflux.Contracts.TransactionHandlers
{
#if !DOTNET35
    public class TransactionEstimatorHandler<TFunctionMessage> :
        TransactionHandlerBase<TFunctionMessage>, 
        ITransactionEstimatorHandler<TFunctionMessage> where TFunctionMessage : FunctionMessage, new()
    {

        public TransactionEstimatorHandler(ITransactionManager transactionManager) : base(transactionManager)
        {

        }

        public async Task<HexBigInteger> EstimateGasAsync(string contractAddress, TFunctionMessage functionMessage = null)
        {
            if (functionMessage == null) functionMessage = new TFunctionMessage();
            SetEncoderContractAddress(contractAddress);
            var callInput = FunctionMessageEncodingService.CreateCallInput(functionMessage);
            try
            {
                return await TransactionManager.EstimatedGasAndCollateralAsync(callInput).ConfigureAwait(false);
            }
            catch(Exception)
            {
                var ethCall = new EthCall(TransactionManager.Client);
                var result = await ethCall.SendRequestAsync(callInput).ConfigureAwait(false);
                new FunctionCallDecoder().ThrowIfErrorOnOutput(result);
                throw;
            }
        }
    }
#endif
}