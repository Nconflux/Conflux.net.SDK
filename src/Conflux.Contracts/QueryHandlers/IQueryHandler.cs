using System.Threading.Tasks;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.Contracts.QueryHandlers
{
    public interface IQueryHandler<TFunctionMessage, TOutput> 
        where TFunctionMessage : FunctionMessage, new()
    {
        Task<TOutput> QueryAsync(
             string contractAddress,
             TFunctionMessage functionMessage = null,
             BlockParameter block = null);
    }
}