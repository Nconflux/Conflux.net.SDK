using System;
using System.Numerics;

namespace Conflux.BlockchainProcessing.Orchestrator
{
    public class OrchestrationProgress
    {
        public BigInteger? BlockNumberProcessTo { get; set; }
        public Exception Exception { get; set; }
        public bool HasErrored => Exception != null;
    }
}