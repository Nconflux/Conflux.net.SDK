using System.Threading;
using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.Contracts.CQS
{
    public interface IContractDeploymentTransactionHandler<TContractDeploymentMessage> where TContractDeploymentMessage : ContractDeploymentMessage, new()
    {
        Task<TransactionInput> CreateTransactionInputEstimatingGasAsync(TContractDeploymentMessage deploymentMessage = null);
        Task<HexBigInteger> EstimateGasAsync(TContractDeploymentMessage contractDeploymentMessage);
        Task<TransactionReceipt> SendRequestAndWaitForReceiptAsync(TContractDeploymentMessage contractDeploymentMessage = null, CancellationTokenSource tokenSource = null);
        Task<string> SendRequestAsync(TContractDeploymentMessage contractDeploymentMessage = null);
        Task<string> SignTransactionAsync(TContractDeploymentMessage contractDeploymentMessage);
    }
}