using Conflux.ABI.FunctionEncoding.Attributes;
using Conflux.Contracts.ContractHandlers;
using Conflux.Contracts.CQS;
using Conflux.Contracts.Services;
using Conflux.JsonRpc.Client;
using Conflux.RPC;
using Conflux.RPC.TransactionManagers;

namespace Conflux.BlockchainProcessing.Services
{
    public class BlockchainProcessingService : IBlockchainProcessingService
    {
        private readonly ICfxApiContractService _ethApiContractService;
        public IBlockchainLogProcessingService Logs { get; }
        public IBlockchainBlockProcessingService Blocks { get; }
    
        public BlockchainProcessingService(ICfxApiContractService ethApiContractService)
        {
            _ethApiContractService = ethApiContractService;
            Logs = new BlockchainLogProcessingService(ethApiContractService);
            Blocks = new BlockchainBlockProcessingService(ethApiContractService);
        }

    }
}
