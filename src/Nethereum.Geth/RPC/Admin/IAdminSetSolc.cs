using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Geth.RPC.Admin
{
    public interface IAdminSetSolc
    {
        RpcRequest BuildRequest(string path, object id = null);
        Task<string> SendRequestAsync(string path, object id = null);
    }
}