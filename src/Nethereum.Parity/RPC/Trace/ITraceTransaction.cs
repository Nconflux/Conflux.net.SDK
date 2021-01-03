using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Newtonsoft.Json.Linq;

namespace Conflux.Parity.RPC.Trace
{
    public interface ITraceTransaction
    {
        RpcRequest BuildRequest(string transactionHash, object id = null);
        Task<JArray> SendRequestAsync(string transactionHash, object id = null);
    }
}
