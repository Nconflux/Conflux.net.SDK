using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Geth.RPC.Admin
{
    public interface IAdminStartRPC
    {
        RpcRequest BuildRequest(string host, int port, string cors, string api, object id = null);
        Task<bool> SendRequestAsync(object id = null);
        Task<bool> SendRequestAsync(string host, object id = null);
        Task<bool> SendRequestAsync(string host, int port, object id = null);
        Task<bool> SendRequestAsync(string host, int port, string cors, object id = null);
        Task<bool> SendRequestAsync(string host, int port, string cors, string api, object id = null);
    }
}