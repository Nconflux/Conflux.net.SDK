using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Geth.RPC.Debug
{
    /// <Summary>
    ///     Turns on CPU profiling for the given duration and writes profile data to disk.
    /// </Summary>
    public class DebugCpuProfile : RpcRequestResponseHandler<object>, IDebugCpuProfile
    {
        public DebugCpuProfile(IClient client) : base(client, ApiMethods.debug_cpuProfile.ToString())
        {
        }

        public RpcRequest BuildRequest(string filePath, int seconds, object id = null)
        {
            return base.BuildRequest(id, filePath, seconds);
        }

        public Task<object> SendRequestAsync(string filePath, int seconds, object id = null)
        {
            return base.SendRequestAsync(id, filePath, seconds);
        }
    }
}