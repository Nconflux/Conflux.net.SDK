using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.Pantheon.RPC.IBFT
{
    public interface IIbftGetValidatorsByBlockNumber
    {
        Task<string[]> SendRequestAsync(BlockParameter block, object id = null);
        RpcRequest BuildRequest(BlockParameter block, object id = null);
    }

}