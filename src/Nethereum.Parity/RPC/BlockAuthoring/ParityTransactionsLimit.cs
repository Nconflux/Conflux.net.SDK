using Conflux.JsonRpc.Client;
using Conflux.RPC.Infrastructure;

namespace Conflux.Parity.RPC.BlockAuthoring
{
    /// <Summary>
    ///     parity_transactionsLimit
    ///     Changes limit for transactions in queue.
    ///     Parameters
    ///     None
    ///     Returns
    ///     Quantity - Current max number of transactions in queue.
    ///     Example
    ///     Request
    ///     curl --data '{"method":"parity_transactionsLimit","params":[],"id":1,"jsonrpc":"2.0"}' -H "Content-Type:
    ///     application/json" -X POST localhost:8545
    ///     Response
    ///     {
    ///     "id": 1,
    ///     "jsonrpc": "2.0",
    ///     "result": 1024
    ///     }
    /// </Summary>
    public class ParityTransactionsLimit : GenericRpcRequestResponseHandlerNoParam<int>, IParityTransactionsLimit
    {
        public ParityTransactionsLimit(IClient client) : base(client, ApiMethods.parity_transactionsLimit.ToString())
        {
        }
    }

    public interface IParityTransactionsLimit : IGenericRpcRequestResponseHandlerNoParam<int>
    {


    }
}