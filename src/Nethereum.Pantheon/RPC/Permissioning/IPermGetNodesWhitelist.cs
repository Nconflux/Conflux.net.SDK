using Conflux.RPC.Infrastructure;

namespace Conflux.Pantheon.RPC.Permissioning
{
    public interface IPermGetNodesWhitelist : IGenericRpcRequestResponseHandlerNoParam<string[]>
    {
    }
}