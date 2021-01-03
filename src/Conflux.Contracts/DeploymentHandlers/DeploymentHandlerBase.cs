using Conflux.Contracts.ContractHandlers;
using Conflux.Contracts.MessageEncodingServices;
using Conflux.RPC.TransactionManagers;

namespace Conflux.Contracts.DeploymentHandlers
{
#if !DOTNET35
    public abstract class DeploymentHandlerBase<TContractDeploymentMessage> : ContractTransactionHandlerBase
        where TContractDeploymentMessage : ContractDeploymentMessage, new()
    {
        protected DeploymentMessageEncodingService<TContractDeploymentMessage> DeploymentMessageEncodingService { get; set;}

        private void InitialiseEncodingService()
        {
            DeploymentMessageEncodingService = new DeploymentMessageEncodingService<TContractDeploymentMessage>();
            DeploymentMessageEncodingService.DefaultAddressFrom = GetAccountAddressFrom();
        }

        protected DeploymentHandlerBase(ITransactionManager transactionManager) : base(transactionManager)
        {
            InitialiseEncodingService();
        }
    }
#endif
}