using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.Geth.RPC.GethEth
{
    public interface IEthPendingTransactions
    {
        RpcRequest BuildRequest(object id = null);
        Task<Transaction[]> SendRequestAsync(object id = null);
    }
}