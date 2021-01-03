using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Pantheon.RPC.EEA
{
    public interface IEeaSendRawTransaction
    {
        Task<string> SendRequestAsync(string signedTransaction, object id = null);
        RpcRequest BuildRequest(string signedTransaction, object id = null);
    }
}