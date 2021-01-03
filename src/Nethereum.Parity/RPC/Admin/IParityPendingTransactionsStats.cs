using Conflux.RPC.Infrastructure;
using Newtonsoft.Json.Linq;

namespace Conflux.Parity.RPC.Admin
{
    public interface IParityPendingTransactionsStats : IGenericRpcRequestResponseHandlerNoParam<JObject>
    {

    }
}