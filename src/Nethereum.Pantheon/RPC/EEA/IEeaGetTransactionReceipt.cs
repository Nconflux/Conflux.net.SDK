using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Conflux.Pantheon.RPC.EEA.DTOs;

namespace Conflux.Pantheon.RPC.EEA
{
    public interface IEeaGetTransactionReceipt
    {
        Task<EeaTransactionReceipt> SendRequestAsync(string transactionHash, object id = null);
        RpcRequest BuildRequest(string transactionHash, object id = null);
    }
}