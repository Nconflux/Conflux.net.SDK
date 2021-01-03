using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Geth.RPC.Miner
{
    public interface IMinerStart
    {
        RpcRequest BuildRequest(int number, object id = null);
        Task<bool> SendRequestAsync(object id = null);
        Task<bool> SendRequestAsync(int number, object id = null);
    }
}