using Conflux.RPC.Infrastructure;

namespace Conflux.Pantheon.RPC.Permissioning
{
    public interface IPermGetAccountsWhitelist : IGenericRpcRequestResponseHandlerNoParam<string[]>
    {
    }
}