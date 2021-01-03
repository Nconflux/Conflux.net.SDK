using System;
using System.Threading.Tasks;
 
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth.Blocks
{
    /// <Summary>
    ///     eth_getBlockTransactionCountByNumber
    ///     Returns the number of transactions in a block from a block matching the given block number.
    ///     Parameters
    ///     QUANTITY|TAG - integer of a block number, or the string "earliest", "latest" or "pending", as in the default block
    ///     parameter.
    ///     params: [
    ///     '0xe8', // 232
    ///     ]
    ///     Returns
    ///     QUANTITY - integer of the number of transactions in this block.
    ///     Example
    ///     Request
    ///     curl -X POST --data '{"jsonrpc":"2.0","method":"eth_getBlockTransactionCountByNumber","params":["0xe8"],"id":1}'
    ///     Result
    ///     {
    ///     "id":1,
    ///     "jsonrpc": "2.0",
    ///     "result": "0xa" // 10
    ///     }
    /// </Summary>
    public class EthGetBlockTransactionCountByNumber : RpcRequestResponseHandler<HexBigInteger>, IEthGetBlockTransactionCountByNumber
    {
        public EthGetBlockTransactionCountByNumber(IClient client)
            : base(client, ApiMethods.cfx_getBlockTransactionCountByNumber.ToString())
        {
        }

        public Task<HexBigInteger> SendRequestAsync(BlockParameter block, object id = null)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            return base.SendRequestAsync(id, block);
        }

        public Task<HexBigInteger> SendRequestAsync(object id = null)
        {
            return SendRequestAsync(BlockParameter.CreateLatest(), id);
        }

        public RpcRequest BuildRequest(BlockParameter block, object id = null)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            return base.BuildRequest(id, block);
        }
    }
}