using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.RPC.Eth
{
    public interface IEthGetCode
    {
        BlockParameter DefaultBlock { get; set; }

        RpcRequest BuildRequest(string address, BlockParameter block, object id = null);
        Task<string> SendRequestAsync(string address, object id = null);
        Task<string> SendRequestAsync(string address, BlockParameter block, object id = null);
    }
}