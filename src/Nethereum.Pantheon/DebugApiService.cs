using Conflux.JsonRpc.Client;
using Conflux.Pantheon.RPC.Debug;
using Conflux.RPC;

namespace Conflux.Pantheon
{
    public class DebugApiService : RpcClientWrapper, IDebugApiService
    {
        public DebugApiService(IClient client) : base(client)
        {
            DebugStorageRangeAt = new DebugStorageRangeAt(client);
            DebugTraceTransaction = new DebugTraceTransaction(client);
            DebugMetrics = new DebugMetrics(client);
        }

        public IDebugStorageRangeAt DebugStorageRangeAt { get; }
        public IDebugTraceTransaction DebugTraceTransaction { get; }
        public IDebugMetrics DebugMetrics { get; }
    }
}