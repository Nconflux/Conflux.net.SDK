using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.Contracts.TransactionHandlers
{
    public interface ITransactionEstimatorHandler<TFunctionMessage> where TFunctionMessage : FunctionMessage, new()
    {
        Task<EstimatedGasAndCollateral> EstimateGasAndCollateralAsync(string contractAddress, TFunctionMessage functionMessage = null);
    }
}