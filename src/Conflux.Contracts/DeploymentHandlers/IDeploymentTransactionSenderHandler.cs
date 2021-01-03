using System.Threading.Tasks;

namespace Conflux.Contracts.DeploymentHandlers
{
    public interface IDeploymentTransactionSenderHandler<TContractDeploymentMessage> where TContractDeploymentMessage : ContractDeploymentMessage, new()
    {
        Task<string> SendTransactionAsync(TContractDeploymentMessage deploymentMessage = null);
    }
}