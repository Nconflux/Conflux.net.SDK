using System;
using System.Threading.Tasks;

namespace Conflux.BlockchainProcessing.Processor
{
    public static class ProcessorExtensions
    {
        public static void AddSynchronousProcessorHandler<T>(this IProcessor<T> processor, Action<T> action)
        {
            processor.AddProcessorHandler(t => { action(t); return Task.FromResult(0); });
        }
    }
}
