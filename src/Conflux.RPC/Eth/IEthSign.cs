using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.RPC.Eth
{
    public interface IEthSign
    {
        RpcRequest BuildRequest(string address, string data, object id = null);
        Task<string> SendRequestAsync(string address, string data, object id = null);
    }
}