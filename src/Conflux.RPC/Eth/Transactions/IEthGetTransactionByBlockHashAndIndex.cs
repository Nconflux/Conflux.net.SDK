using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth.Transactions
{
    public interface IEthGetTransactionByBlockHashAndIndex
    {
        RpcRequest BuildRequest(string blockHash, HexBigInteger transactionIndex, object id = null);
        Task<Transaction> SendRequestAsync(string blockHash, HexBigInteger transactionIndex, object id = null);
    }
}