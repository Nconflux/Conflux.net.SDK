using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;
using Conflux.RPC.Shh.DTOs;

namespace Conflux.RPC.Shh.MessageFilter
{
    public interface IShhGetFilterMessages : IGenericRpcRequestResponseHandlerParamString<ShhMessage[]>
    {

    }
}