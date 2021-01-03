using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.RPC.Eth.Transactions
{
    public interface IEthSendRawTransaction
    {
        RpcRequest BuildRequest(string signedTransactionData, object id = null);
        Task<string> SendRequestAsync(string signedTransactionData, object id = null);
    }
}