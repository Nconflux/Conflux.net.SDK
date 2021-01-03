using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Newtonsoft.Json.Linq;

namespace Conflux.RPC.Eth.Compilation
{
    public interface IEthCompileSolidity
    {
        RpcRequest BuildRequest(string contractCode, object id = null);
        Task<JToken> SendRequestAsync(string contractCode, object id = null);
    }
}