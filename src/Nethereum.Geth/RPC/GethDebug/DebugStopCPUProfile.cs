using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;

namespace Conflux.Geth.RPC.Debug
{
    /// <Summary>
    ///     Stops an ongoing CPU profile.
    /// </Summary>
    public class DebugStopCPUProfile : GenericRpcRequestResponseHandlerNoParam<object>, IDebugStopCPUProfile
    {
        public DebugStopCPUProfile(IClient client) : base(client, ApiMethods.debug_stopCPUProfile.ToString())
        {
        }
    }
}