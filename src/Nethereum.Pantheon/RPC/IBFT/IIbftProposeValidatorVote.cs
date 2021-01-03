using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Pantheon.RPC.IBFT
{
    public interface IIbftProposeValidatorVote
    {
        Task<bool> SendRequestAsync(string accountAddress, bool addValidator, object id = null);
        RpcRequest BuildRequest(string accountAddress, bool addValidator, object id = null);
    }
}