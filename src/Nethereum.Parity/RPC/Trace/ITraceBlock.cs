using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using Newtonsoft.Json.Linq;

namespace Conflux.Parity.RPC.Trace
{
    public interface ITraceBlock
    {
        RpcRequest BuildRequest(HexBigInteger blockNumber, object id = null);
        Task<JArray> SendRequestAsync(HexBigInteger blockNumber, object id = null);
    }
}