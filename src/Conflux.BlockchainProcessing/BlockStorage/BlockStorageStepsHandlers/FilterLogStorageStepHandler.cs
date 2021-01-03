using System.Threading.Tasks;
using Conflux.BlockchainProcessing.BlockStorage.Repositories;
using Conflux.BlockchainProcessing.Processor;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.BlockchainProcessing.BlockStorage.BlockStorageStepsHandlers
{
    public class FilterLogStorageStepHandler : ProcessorBaseHandler<FilterLogVO>
    {
        private readonly ITransactionLogRepository _transactionLogRepository;

        public FilterLogStorageStepHandler(ITransactionLogRepository transactionLogRepository)
        {
            _transactionLogRepository = transactionLogRepository;
        }

        protected override Task ExecuteInternalAsync(FilterLogVO filterLog)
        {
            return _transactionLogRepository.UpsertAsync(filterLog);
        }
    }
}
