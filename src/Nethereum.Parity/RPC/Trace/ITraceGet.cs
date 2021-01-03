using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using Newtonsoft.Json.Linq;

namespace Conflux.Parity.RPC.Trace
{
    public interface ITraceGet
    {
        RpcRequest BuildRequest(string transactionHash, HexBigInteger[] index, object id = null);
        Task<JObject> SendRequestAsync(string transactionHash, HexBigInteger[] index, object id = null);
    }
}