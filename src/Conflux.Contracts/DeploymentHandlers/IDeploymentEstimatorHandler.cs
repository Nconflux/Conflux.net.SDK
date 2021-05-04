using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.Contracts.DeploymentHandlers
{
    public interface IDeploymentEstimatorHandler<TContractDeploymentMessage> where TContractDeploymentMessage : ContractDeploymentMessage, new()
    {
        Task<EstimatedGasAndCollateral> EstimateGasAndCollateralAsync(TContractDeploymentMessage deploymentMessage);
    }
}