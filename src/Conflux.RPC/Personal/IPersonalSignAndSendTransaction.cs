using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Personal
{
    public interface IPersonalSignAndSendTransaction
    {
        RpcRequest BuildRequest(TransactionInput txn, string password, object id = null);
        Task<string> SendRequestAsync(TransactionInput txn, string password, object id = null);
    }
}