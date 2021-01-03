using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;

namespace Conflux.Geth.RPC.Debug
{
    /// <Summary>
    ///     Stops writing the Go runtime trace.
    /// </Summary>
    public class DebugStopGoTrace : GenericRpcRequestResponseHandlerNoParam<object>, IDebugStopGoTrace
    {
        public DebugStopGoTrace(IClient client) : base(client, ApiMethods.debug_stopGoTrace.ToString())
        {
        }
    }
}