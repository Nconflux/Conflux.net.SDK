using System.Threading.Tasks;
using Conflux.Contracts.Services;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.BlockchainProcessing.BlockProcessing.CrawlerSteps
{
    public class TransactionCrawlerStep : CrawlerStep<TransactionVO, TransactionVO>
    {
        public TransactionCrawlerStep(ICfxApiContractService ethApiContractService) : base(ethApiContractService)
        {
        }

        public override Task<TransactionVO> GetStepDataAsync(TransactionVO parentStep)
        {
            return Task.FromResult(parentStep);
        }
    }
}