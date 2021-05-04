using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth.Transactions
{
    public interface IEthEstimatedGasAndCollateral
    {
        RpcRequest BuildRequest(CallInput callInput, object id = null);
        Task<EstimatedGasAndCollateral> SendRequestAsync(CallInput callInput, object id = null);
    }
}