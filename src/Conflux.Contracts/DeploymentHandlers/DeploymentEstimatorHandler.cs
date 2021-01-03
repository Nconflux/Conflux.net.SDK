using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.RPC.TransactionManagers;

namespace Conflux.Contracts.DeploymentHandlers
{
#if !DOTNET35
    public class DeploymentEstimatorHandler<TContractDeploymentMessage> : DeploymentHandlerBase<TContractDeploymentMessage>, 
        IDeploymentEstimatorHandler<TContractDeploymentMessage> where TContractDeploymentMessage : ContractDeploymentMessage, new()
    {
    
        public DeploymentEstimatorHandler(ITransactionManager transactionManager):base(transactionManager)
        { 
        }


        public Task<HexBigInteger> EstimateGasAsync(TContractDeploymentMessage deploymentMessage = null)
        {
            if(deploymentMessage == null) deploymentMessage = new TContractDeploymentMessage();
            var callInput = DeploymentMessageEncodingService.CreateCallInput(deploymentMessage);
            return TransactionManager.EstimateGasAsync(callInput);
        }
    }
#endif
}