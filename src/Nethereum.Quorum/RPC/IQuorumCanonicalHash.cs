using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Quorum.RPC
{
    public interface IQuorumCanonicalHash
    {
        RpcRequest BuildRequest(long blockNumber, object id = null);
        Task<string> SendRequestAsync(long blockNumber, object id = null);
    }
}