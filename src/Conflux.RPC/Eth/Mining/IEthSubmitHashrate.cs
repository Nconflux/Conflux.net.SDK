using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.RPC.Eth.Mining
{
    public interface IEthSubmitHashrate
    {
        RpcRequest BuildRequest(string hashRate, string clientId, object id = null);
        Task<bool> SendRequestAsync(string hashRate, string clientId, object id = null);
    }
}