using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Quorum.RPC
{
    public interface IQuorumVote
    {
        RpcRequest BuildRequest(string hash, object id = null);
        Task<string> SendRequestAsync(string hash, object id = null);
    }
}