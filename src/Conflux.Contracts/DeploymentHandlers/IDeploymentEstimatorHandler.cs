using System.Threading.Tasks;
using Conflux.Hex.HexTypes;

namespace Conflux.Contracts.DeploymentHandlers
{
    public interface IDeploymentEstimatorHandler<TContractDeploymentMessage> where TContractDeploymentMessage : ContractDeploymentMessage, new()
    {
        Task<HexBigInteger> EstimateGasAsync(TContractDeploymentMessage deploymentMessage);
    }
}