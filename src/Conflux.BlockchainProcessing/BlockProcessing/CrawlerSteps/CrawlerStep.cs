using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conflux.Contracts.Services;

namespace Conflux.BlockchainProcessing.BlockProcessing.CrawlerSteps
{
    public abstract class CrawlerStep<TParentStep, TProcessStep>
    {
        //TODO: Disable step and / or handlers
        protected ICfxApiContractService EthApi { get; }
        public CrawlerStep(
            ICfxApiContractService ethApi
        )
        {
            EthApi = ethApi;
        }

        public abstract Task<TProcessStep> GetStepDataAsync(TParentStep parentStep);

        public virtual async Task<CrawlerStepCompleted<TProcessStep>> ExecuteStepAsync(TParentStep parentStep, IEnumerable<BlockProcessingSteps> executionStepsCollection)
        {
            var processStepValue = await GetStepDataAsync(parentStep);
            if (processStepValue == null) return null;
            var stepsToProcesss =
                await executionStepsCollection.FilterMatchingStepAsync(processStepValue).ConfigureAwait(false);

            if (stepsToProcesss.Any())
            {
                await stepsToProcesss.ExecuteCurrentStepAsync(processStepValue);
            }
            return new CrawlerStepCompleted<TProcessStep>(stepsToProcesss, processStepValue);

        }
    }
}