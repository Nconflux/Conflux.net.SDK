using System.Threading.Tasks;
using Conflux.Contracts.Extensions;
using Conflux.Hex.HexTypes;
using Conflux.RPC.TransactionManagers;

namespace Conflux.Contracts.DeploymentHandlers
{
#if !DOTNET35
    public class DeploymentTransactionSenderHandler<TContractDeploymentMessage> : DeploymentHandlerBase<TContractDeploymentMessage>, 
        IDeploymentTransactionSenderHandler<TContractDeploymentMessage> where TContractDeploymentMessage : ContractDeploymentMessage, new()
    {
        private IDeploymentEstimatorHandler<TContractDeploymentMessage> _deploymentEstimatorHandler;

        public DeploymentTransactionSenderHandler(ITransactionManager transactionManager):base(transactionManager)
        {
            _deploymentEstimatorHandler = new DeploymentEstimatorHandler<TContractDeploymentMessage>(transactionManager);
        }

        public async Task<string> SendTransactionAsync(TContractDeploymentMessage deploymentMessage = null)
        {
            if(deploymentMessage == null) deploymentMessage = new TContractDeploymentMessage();
             
            //deploymentMessage.Gas = new System.Numerics.BigInteger(1159914);// await GetOrEstimateMaximumGasAsync(deploymentMessage).ConfigureAwait(false);
            deploymentMessage.GasPrice= new System.Numerics.BigInteger(10);
            var transactionInput = DeploymentMessageEncodingService.CreateTransactionInput(deploymentMessage);
            transactionInput.Gas =new HexBigInteger(15000000);
            //deploymentMessage.Gas = await GetOrEstimateMaximumGasAsync(deploymentMessage);
            return await TransactionManager.SendTransactionAsync(transactionInput).ConfigureAwait(false);
        }

        protected virtual async Task<HexBigInteger> GetOrEstimateMaximumGasAsync(
            TContractDeploymentMessage deploymentMessage)
        {
            return deploymentMessage.GetHexMaximumGas()
                   ?? await _deploymentEstimatorHandler.EstimateGasAsync(deploymentMessage).ConfigureAwait(false);
        }
    }
#endif
}