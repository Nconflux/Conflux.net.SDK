using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Quorum.RPC
{
    public interface IQuorumIsBlockMaker
    {
        RpcRequest BuildRequest(string address, object id = null);
        Task<bool> SendRequestAsync(string address, object id = null);
    }
}