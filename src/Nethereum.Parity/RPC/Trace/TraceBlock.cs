using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using Newtonsoft.Json.Linq;

namespace Conflux.Parity.RPC.Trace
{
    /// <Summary>
    ///     Returns traces created at given block
    /// </Summary>
    public class TraceBlock : RpcRequestResponseHandler<JArray>, ITraceBlock
    {
        public TraceBlock(IClient client) : base(client, ApiMethods.trace_block.ToString())
        {
        }

        public async Task<JArray> SendRequestAsync(HexBigInteger blockNumber, object id = null)
        {
            return await base.SendRequestAsync(id, blockNumber);
        }

        public RpcRequest BuildRequest(HexBigInteger blockNumber, object id = null)
        {
            return base.BuildRequest(id, blockNumber);
        }
    }
}