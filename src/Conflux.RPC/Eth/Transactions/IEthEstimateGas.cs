using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth.Transactions
{
    public interface IEthEstimateGas
    {
        RpcRequest BuildRequest(CallInput callInput, object id = null);
        Task<HexBigInteger> SendRequestAsync(CallInput callInput, object id = null);
    }
}