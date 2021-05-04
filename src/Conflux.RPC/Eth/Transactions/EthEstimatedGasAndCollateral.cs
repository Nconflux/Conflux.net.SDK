using System;
using System.Threading.Tasks;

using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth.Transactions
{
    /// <Summary>
    ///     cfx_estimateGasAndCollateral
    ///     Makes a call or transaction, which won't be added to the blockchain and returns the used gas and storage collateral, which can be used for
    ///     estimating the used gas and collateral.
    ///     Parameters
    ///     See eth_call parameters, expect that all properties are optional.
    ///     Returns
    ///     QUANTITY - the amount of gas used.
    ///     Example
    ///     Request
    ///     curl -X POST --data '{"jsonrpc":"2.0","method":"eth_estimateGas","params":[{see above}],"id":1}'
    ///     Result
    ///     {
    ///     "id":1,
    ///     "jsonrpc": "2.0",
    ///     "result": "0x5208" // 21000
    ///     }
    /// </Summary>
    public class EthEstimatedGasAndCollateral : RpcRequestResponseHandler<EstimatedGasAndCollateral>, IEthEstimatedGasAndCollateral
    {
        public EthEstimatedGasAndCollateral(IClient client) : base(client, ApiMethods.cfx_estimateGasAndCollateral.ToString())
        {
        }

        public Task<EstimatedGasAndCollateral> SendRequestAsync(CallInput callInput, object id = null)
        {
            if (callInput == null) throw new ArgumentNullException(nameof(callInput));
            return SendRequestAsync(id, callInput);
        }

        public RpcRequest BuildRequest(CallInput callInput, object id = null)
        {
            if (callInput == null) throw new ArgumentNullException(nameof(callInput));
            return base.BuildRequest(id, callInput);
        }

    }
}