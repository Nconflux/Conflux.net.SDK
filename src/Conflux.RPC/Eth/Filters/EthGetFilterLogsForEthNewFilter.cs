using System;
using System.Threading.Tasks;
 
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth.Filters
{
    /// <Summary>
    ///     eth_getFilterLogs
    ///     Returns an array of all logs matching filter with given id.
    ///     Parameters
    ///     QUANTITY - The filter id.
    ///     params: [
    ///     "0x16" // 22
    ///     ]
    ///     Returns
    ///     See eth_getFilterChanges
    ///     Example
    ///     Request
    ///     curl -X POST --data '{"jsonrpc":"2.0","method":"eth_getFilterLogs","params":["0x16"],"id":74}'
    /// </Summary>
    public class EthGetFilterLogsForEthNewFilter : RpcRequestResponseHandler<FilterLog[]>, IEthGetFilterLogsForEthNewFilter
    {
        public EthGetFilterLogsForEthNewFilter(IClient client) : base(client, ApiMethods.cfx_getFilterLogs.ToString())
        {
        }

        public Task<FilterLog[]> SendRequestAsync(HexBigInteger filterId, object id = null)
        {
            if (filterId == null) throw new ArgumentNullException(nameof(filterId));
            return base.SendRequestAsync(id, filterId);
        }

        public RpcRequest BuildRequest(HexBigInteger filterId, object id = null)
        {
            if (filterId == null) throw new ArgumentNullException(nameof(filterId));
            return base.BuildRequest(id, filterId);
        }
    }
}