using System;
using System.Threading.Tasks;
 
using Conflux.Hex.HexConvertors.Extensions;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth
{
    /// <Summary>
    ///     eth_getCode
    ///     Returns code at a given address.
    ///     Parameters
    ///     DATA, 20 Bytes - address
    ///     QUANTITY|TAG - integer block number, or the string "latest", "earliest" or "pending", see the default block
    ///     parameter
    ///     params: [
    ///     '0xa94f5374fce5edbc8e2a8697c15331677e6ebf0b',
    ///     '0x2'  // 2
    ///     ]
    ///     Returns
    ///     DATA - the code from the given address.
    ///     Example
    ///     Request
    ///     curl -X POST --data
    ///     '{"jsonrpc":"2.0","method":"eth_getCode","params":["0xa94f5374fce5edbc8e2a8697c15331677e6ebf0b", "0x2"],"id":1}'
    ///     Result
    ///     {
    ///     "id":1,
    ///     "jsonrpc": "2.0",
    ///     "result": "0x600160008035811a818181146012578301005b601b6001356025565b8060005260206000f25b600060078202905091905056"
    ///     }
    /// </Summary>
    public class EthGetCode : RpcRequestResponseHandler<string>, IDefaultBlock, IEthGetCode
    {
        public EthGetCode(IClient client) : base(client, ApiMethods.cfx_getCode.ToString())
        {
            DefaultBlock = BlockParameter.CreateLatest();
        }

        public BlockParameter DefaultBlock { get; set; }

        public Task<string> SendRequestAsync(string address, BlockParameter block,
            object id = null)
        {
            if (address == null) throw new ArgumentNullException(nameof(address));
            if (block == null) throw new ArgumentNullException(nameof(block));
            return base.SendRequestAsync(id, address, block);
        }

        public Task<string> SendRequestAsync(string address, object id = null)
        {
            if (address == null) throw new ArgumentNullException(nameof(address));
            return base.SendRequestAsync(id, address, DefaultBlock);
        }

        public RpcRequest BuildRequest(string address, BlockParameter block, object id = null)
        {
            if (address == null) throw new ArgumentNullException(nameof(address));
            if (block == null) throw new ArgumentNullException(nameof(block));
            return base.BuildRequest(id, address, block);
        }
    }

}