using Conflux.RPC.Infrastructure;
using Newtonsoft.Json.Linq;

namespace Conflux.Parity.RPC.Accounts
{
    public interface IParityAccountsInfo : IGenericRpcRequestResponseHandlerNoParam<JObject>
    {

    }
}