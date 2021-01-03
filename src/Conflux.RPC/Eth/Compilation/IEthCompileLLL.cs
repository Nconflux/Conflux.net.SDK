using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Newtonsoft.Json.Linq;

namespace Conflux.RPC.Eth.Compilation
{
    public interface IEthCompileLLL
    {
        RpcRequest BuildRequest(string lllcode, object id = null);
        Task<JObject> SendRequestAsync(string lllcode, object id = null);
    }
}