using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Newtonsoft.Json.Linq;

namespace Conflux.Geth.RPC.Debug
{
    /// <Summary>
    ///     The traceBlock method will return a full stack trace of all invoked opcodes of all transaction that were included
    ///     included in this block. Note, the parent of this block must be present or it will fail.
    /// </Summary>
    public class DebugTraceBlock : RpcRequestResponseHandler<JObject>, IDebugTraceBlock
    {
        public DebugTraceBlock(IClient client) : base(client, ApiMethods.debug_traceBlock.ToString())
        {
        }

        public RpcRequest BuildRequest(string blockRlpHex, object id = null)
        {
            return base.BuildRequest(id, blockRlpHex);
        }

        public Task<JObject> SendRequestAsync(string blockRlpHex, object id = null)
        {
            return base.SendRequestAsync(id, blockRlpHex);
        }
    }
}