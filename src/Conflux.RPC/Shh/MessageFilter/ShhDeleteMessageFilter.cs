using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;

namespace Conflux.RPC.Shh.MessageFilter
{
    public class ShhDeleteMessageFilter : GenericRpcRequestResponseHandlerParamString<bool>, IShhDeleteMessageFilter
    {
        public ShhDeleteMessageFilter(IClient client) : base(client, ApiMethods.shh_deleteMessageFilter.ToString())
        {
        }
    }
}
