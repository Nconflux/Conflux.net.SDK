using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;
using Conflux.RPC.Shh.DTOs;

namespace Conflux.RPC.Shh.MessageFilter
{
    public class ShhGetFilterMessages : GenericRpcRequestResponseHandlerParamString<ShhMessage[]>, IShhGetFilterMessages
    {
        public ShhGetFilterMessages(IClient client) : base(client, ApiMethods.shh_getFilterMessages.ToString())
        {
        }
    }
}