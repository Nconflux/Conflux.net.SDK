using System.Threading.Tasks;
using Conflux.JsonRpc.Client;

namespace Conflux.Pantheon.RPC.IBFT
{
    public interface IIbftDiscardValidatorVote
    {
        Task<bool> SendRequestAsync(string validatorAddress, object id = null);
        RpcRequest BuildRequest(string validatorAddress, object id = null);
    }
}