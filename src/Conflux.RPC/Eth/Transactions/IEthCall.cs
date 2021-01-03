using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth.Transactions
{
    public interface IEthCall
    {
        BlockParameter DefaultBlock { get; set; }

        RpcRequest BuildRequest(CallInput callInput, BlockParameter block, object id = null);
        Task<string> SendRequestAsync(CallInput callInput, object id = null);
        Task<string> SendRequestAsync(CallInput callInput, BlockParameter block, object id = null);
    }
}