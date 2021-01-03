using System.Threading.Tasks;
using Conflux.BlockchainProcessing.BlockStorage.Repositories;
using Conflux.BlockchainProcessing.Processor;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.BlockchainProcessing.BlockStorage.BlockStorageStepsHandlers
{
    public class ContractCreationStorageStepHandler : ProcessorBaseHandler<ContractCreationVO>
    {
        private readonly IContractRepository _contractRepository;
        public ContractCreationStorageStepHandler(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }
        protected override Task ExecuteInternalAsync(ContractCreationVO contractCreation)
        {
            return _contractRepository.UpsertAsync(
                contractCreation);
        }
    }
}
