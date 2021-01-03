using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth.Transactions
{
    public interface IEthGetTransactionByBlockNumberAndIndex
    {
        RpcRequest BuildRequest(HexBigInteger blockNumber, HexBigInteger transactionIndex, object id = null);
        Task<Transaction> SendRequestAsync(HexBigInteger blockNumber, HexBigInteger transactionIndex, object id = null);
    }
}