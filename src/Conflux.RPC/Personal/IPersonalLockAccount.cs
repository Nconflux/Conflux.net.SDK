using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.RPC.Personal
{
    public interface IPersonalLockAccount
    {
        RpcRequest BuildRequest(string account, object id = null);
        Task<bool> SendRequestAsync(string account, object id = null);
    }
}