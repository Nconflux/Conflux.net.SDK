using System.Threading.Tasks;

namespace Conflux.Contracts.DeploymentHandlers
{
    public interface IDeploymentSigner<TContractDeploymentMessage> where TContractDeploymentMessage : ContractDeploymentMessage, new()
    {
        Task<string> SignTransactionAsync(TContractDeploymentMessage deploymentMessage);
    }
}