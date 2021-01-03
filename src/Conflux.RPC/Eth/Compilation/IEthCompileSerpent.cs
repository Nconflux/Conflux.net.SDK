using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Newtonsoft.Json.Linq;

namespace Conflux.RPC.Eth.Compilation
{
    public interface IEthCompileSerpent
    {
        RpcRequest BuildRequest(string serpentCode, object id = null);
        Task<JObject> SendRequestAsync(string serpentCode, object id = null);
    }
}