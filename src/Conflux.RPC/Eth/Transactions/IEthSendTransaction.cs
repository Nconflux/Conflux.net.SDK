using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth.Transactions
{
    public interface IEthSendTransaction
    {
        RpcRequest BuildRequest(TransactionInput input, object id = null);
        Task<string> SendRequestAsync(TransactionInput input, object id = null);
    }
}