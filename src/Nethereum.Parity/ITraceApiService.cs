using Conflux.Parity.RPC.Trace;

namespace Conflux.Parity
{
    public interface ITraceApiService
    {
        ITraceBlock TraceBlock { get; }
        ITraceCall TraceCall { get; }
        ITraceFilter TraceFilter { get; }
        ITraceGet TraceGet { get; }
        ITraceRawTransaction TraceRawTransaction { get; }
        ITraceTransaction TraceTransaction { get; }
    }
}