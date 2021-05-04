using System.Threading;
using System.Threading.Tasks;
using Conflux.RPC.Eth.DTOs;
using Conflux.RPC.TransactionManagers;

namespace Conflux.Contracts.DeploymentHandlers
{
#if !DOTNET35
    public class DeploymentTransactionReceiptPollHandler<TContractDeploymentMessage> : 
        DeploymentHandlerBase<TContractDeploymentMessage>, IDeploymentTransactionReceiptPollHandler<TContractDeploymentMessage>
        where TContractDeploymentMessage : ContractDeploymentMessage, new()
    {
        private IDeploymentTransactionSenderHandler<TContractDeploymentMessage>
            _deploymentTransactionHandler;


        public DeploymentTransactionReceiptPollHandler(ITransactionManager transactionManager,
            IDeploymentTransactionSenderHandler<TContractDeploymentMessage> deploymentTransactionHandler) : base(transactionManager)
        {
            _deploymentTransactionHandler = deploymentTransactionHandler;
        }

        public DeploymentTransactionReceiptPollHandler(ITransactionManager transactionManager) : this(transactionManager,
            new DeploymentTransactionSenderHandler<TContractDeploymentMessage>(transactionManager))
        {

        }

        public async Task<TransactionReceipt> SendTransactionAsync(TContractDeploymentMessage deploymentMessage = null,
            CancellationTokenSource cancellationTokenSource = null)
        {
            if (deploymentMessage == null) deploymentMessage = new TContractDeploymentMessage();
            var transactionHash = await _deploymentTransactionHandler.SendTransactionAsync(deploymentMessage)
                .ConfigureAwait(false);
            return await TransactionManager.TransactionReceiptService
                .PollForReceiptAsync(transactionHash, cancellationTokenSource).ConfigureAwait(false);
        }
    }
#endif
}


