using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.RPC.Personal
{
    public interface IPersonalNewAccount
    {
        RpcRequest BuildRequest(string passPhrase, object id = null);
        Task<string> SendRequestAsync(string passPhrase, object id = null);
    }
}