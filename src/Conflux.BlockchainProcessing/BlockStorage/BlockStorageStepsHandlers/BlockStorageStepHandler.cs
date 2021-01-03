using System.Threading.Tasks;
using Conflux.BlockchainProcessing.BlockStorage.Repositories;
using Conflux.BlockchainProcessing.Processor;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.BlockchainProcessing.BlockStorage.BlockStorageStepsHandlers
{
    public class BlockStorageStepHandler : ProcessorBaseHandler<BlockWithTransactions>
    {
        private readonly IBlockRepository _blockRepository;

        public BlockStorageStepHandler(IBlockRepository blockRepository)
        {
            _blockRepository = blockRepository;
        }
        protected override Task ExecuteInternalAsync(BlockWithTransactions block)
        {
            return _blockRepository.UpsertBlockAsync(block);
        }
    }
}
