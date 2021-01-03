using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Pantheon.RPC.Clique
{
    public interface ICliquePropose
    {
        Task<bool> SendRequestAsync(string address, bool addSigner, object id = null);
        RpcRequest BuildRequest(string address, bool addSigner, object id = null);
    }
}