using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Pantheon.RPC.Clique
{
    public interface ICliqueDiscard
    {
        Task<bool> SendRequestAsync(string addressSigner, object id = null);
        RpcRequest BuildRequest(string addressSigner, object id = null);
    }
}