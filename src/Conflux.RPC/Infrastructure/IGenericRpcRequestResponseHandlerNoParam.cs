using System.Threading.Tasks;

namespace Conflux.RPC.Infrastructure
{
    public interface IGenericRpcRequestResponseHandlerNoParam<TResponse>
    {
        Task<TResponse> SendRequestAsync(object id = null);
    }
}