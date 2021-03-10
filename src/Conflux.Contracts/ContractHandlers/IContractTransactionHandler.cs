using System.Threading;
using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.Contracts.ContractHandlers
{
    public interface IContractTransactionHandler<TContractMessage> where TContractMessage : FunctionMessage, new()
    {
        Task<TransactionInput> CreateTransactionInputEstimatingGasAsync(string contractAddress, TContractMessage functionMessage = null);
        Task<EstimatedGasAndCollateral> EstimateGasAndCollateralAsync(string contractAddress, TContractMessage functionMessage = null);
        Task<TransactionReceipt> SendRequestAndWaitForReceiptAsync(string contractAddress, TContractMessage functionMessage = null, CancellationTokenSource tokenSource = null);
        Task<string> SendRequestAsync(string contractAddress, TContractMessage functionMessage = null);
        Task<string> SignTransactionAsync(string contractAddress, TContractMessage functionMessage = null);
    }
}