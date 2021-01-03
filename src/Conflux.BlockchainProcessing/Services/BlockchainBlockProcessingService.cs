using System;
using Common.Logging;
using Conflux.BlockchainProcessing.BlockProcessing;
using Conflux.BlockchainProcessing.BlockStorage;
using Conflux.BlockchainProcessing.BlockStorage.Repositories;
using Conflux.BlockchainProcessing.ProgressRepositories;
using Conflux.Contracts.Services;
using Conflux.RPC.Eth.Blocks;

namespace Conflux.BlockchainProcessing.Services
{
    public class BlockchainBlockProcessingService : IBlockchainBlockProcessingService
    {
        private readonly ICfxApiContractService _ethApiContractService;

        public BlockchainBlockProcessingService(ICfxApiContractService ethApiContractService)
        {
            _ethApiContractService = ethApiContractService;
        }

#if !DOTNET35

        public BlockchainProcessor CreateBlockProcessor(
            Action<BlockProcessingSteps> stepsConfiguration, 
            uint minimumBlockConfirmations, 
            ILog log = null) => CreateBlockProcessor(
                new InMemoryBlockchainProgressRepository(),
                stepsConfiguration, 
                minimumBlockConfirmations, 
                log);

        public BlockchainProcessor CreateBlockProcessor(
            IBlockProgressRepository blockProgressRepository,
            Action<BlockProcessingSteps> stepsConfiguration,
            uint minimumBlockConfirmations,
            ILog log = null)
        {
            var processingSteps = new BlockProcessingSteps();
            var orchestrator = new BlockCrawlOrchestrator(_ethApiContractService, processingSteps );
            var lastConfirmedBlockNumberService = new LastConfirmedBlockNumberService(_ethApiContractService.Blocks.GetBlockNumber, minimumBlockConfirmations);

            stepsConfiguration?.Invoke(processingSteps);

            return new BlockchainProcessor(orchestrator, blockProgressRepository, lastConfirmedBlockNumberService, log);
        }

        public BlockchainProcessor CreateBlockStorageProcessor(
            IBlockchainStoreRepositoryFactory blockchainStorageFactory, 
            uint minimumBlockConfirmations, 
            Action<BlockProcessingSteps> configureSteps = null, 
            ILog log = null) => CreateBlockStorageProcessor(
                blockchainStorageFactory, 
                null, 
                minimumBlockConfirmations, 
                configureSteps, 
                log);


        public BlockchainProcessor CreateBlockStorageProcessor(
            IBlockchainStoreRepositoryFactory blockchainStorageFactory,
            IBlockProgressRepository blockProgressRepository,
            uint minimumBlockConfirmations,
            Action<BlockProcessingSteps> configureSteps = null,
            ILog log = null)
        {
            var processingSteps = new BlockStorageProcessingSteps(blockchainStorageFactory);
            var orchestrator = new BlockCrawlOrchestrator(_ethApiContractService, processingSteps);

            if (blockProgressRepository == null && blockchainStorageFactory is IBlockProgressRepositoryFactory progressRepoFactory)
            {
                blockProgressRepository = progressRepoFactory.CreateBlockProgressRepository();
            }

            blockProgressRepository = blockProgressRepository ?? new InMemoryBlockchainProgressRepository();
            var lastConfirmedBlockNumberService = new LastConfirmedBlockNumberService(_ethApiContractService.Blocks.GetBlockNumber, minimumBlockConfirmations);

            configureSteps?.Invoke(processingSteps);

            return new BlockchainProcessor(orchestrator, blockProgressRepository, lastConfirmedBlockNumberService, log);

        }

#endif
    }
}