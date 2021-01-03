using Conflux.Pantheon.RPC.Debug;

namespace Conflux.Pantheon
{
    public interface IDebugApiService
    {
        IDebugStorageRangeAt DebugStorageRangeAt { get; }
        IDebugTraceTransaction DebugTraceTransaction { get; }
        IDebugMetrics DebugMetrics { get; }
    }
}