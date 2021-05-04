using System.Threading.Tasks;
using Conflux.Contracts.Extensions;
using Conflux.Hex.HexTypes;
using Conflux.RPC.Eth.DTOs;
using Conflux.RPC.TransactionManagers;

namespace Conflux.Contracts.DeploymentHandlers
{
#if !DOTNET35
    public class DeploymentTransactionSenderHandler<TContractDeploymentMessage> : DeploymentHandlerBase<TContractDeploymentMessage>,
        IDeploymentTransactionSenderHandler<TContractDeploymentMessage> where TContractDeploymentMessage : ContractDeploymentMessage, new()
    {
        private IDeploymentEstimatorHandler<TContractDeploymentMessage> _deploymentEstimatorHandler;

        public DeploymentTransactionSenderHandler(ITransactionManager transactionManager) : base(transactionManager)
        {
            _deploymentEstimatorHandler = new DeploymentEstimatorHandler<TContractDeploymentMessage>(transactionManager);
        }

        public async Task<string> SendTransactionAsync(TContractDeploymentMessage deploymentMessage = null)
        {
            if (deploymentMessage == null)
                deploymentMessage = new TContractDeploymentMessage();
            if (deploymentMessage.Storage == null || deploymentMessage.Gas == null)
            {
                EstimatedGasAndCollateral estimatedGasAndCollateral = await _deploymentEstimatorHandler.EstimateGasAndCollateralAsync(deploymentMessage).ConfigureAwait(false);
                if (deploymentMessage.Gas == null)
                    deploymentMessage.Gas = estimatedGasAndCollateral.GasUsed;
                if (deploymentMessage.Storage == null)
                    deploymentMessage.Storage = estimatedGasAndCollateral.StorageCollateralized;
            }
            var transactionInput = DeploymentMessageEncodingService.CreateTransactionInput(deploymentMessage);
            return await TransactionManager.SendTransactionAsync(transactionInput).ConfigureAwait(false);
        }
    }
#endif
}