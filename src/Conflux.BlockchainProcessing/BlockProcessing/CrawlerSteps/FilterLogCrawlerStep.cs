using System.Threading.Tasks;
using Conflux.Contracts.Services;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.BlockchainProcessing.BlockProcessing.CrawlerSteps
{
    public class FilterLogCrawlerStep : CrawlerStep<FilterLogVO, FilterLogVO>
    {
        public FilterLogCrawlerStep(ICfxApiContractService ethApiContractService) : base(ethApiContractService)
        {
        }

        public override Task<FilterLogVO> GetStepDataAsync(FilterLogVO filterLogVO)
        {
            return Task.FromResult(filterLogVO);
        }
    }
}