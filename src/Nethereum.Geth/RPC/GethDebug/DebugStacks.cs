using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;

namespace Conflux.Geth.RPC.Debug
{
    /// <Summary>
    ///     Returns a printed representation of the stacks of all goroutines.
    /// </Summary>
    public class DebugStacks : GenericRpcRequestResponseHandlerNoParam<string>, IDebugStacks
    {
        public DebugStacks(IClient client) : base(client, ApiMethods.debug_stacks.ToString())
        {
        }
    }
}