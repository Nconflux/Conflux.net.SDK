using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Geth.RPC.Debug
{
    /// <Summary>
    ///     Fetches and retrieves the seed hash of the block by number
    /// </Summary>
    public class DebugSeedHash : RpcRequestResponseHandler<string>, IDebugSeedHash
    {
        public DebugSeedHash(IClient client) : base(client, ApiMethods.debug_seedHash.ToString())
        {
        }

        public RpcRequest BuildRequest(ulong blockNumber, object id = null)
        {
            return base.BuildRequest(id, blockNumber);
        }

        public Task<string> SendRequestAsync(ulong blockNumber, object id = null)
        {
            return base.SendRequestAsync(id, blockNumber);
        }
    }
}