using System.Threading.Tasks;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.Pantheon.RPC.IBFT
{
    /// <Summary>
    ///     Lists the validators defined in the specified block.
    /// </Summary>
    public class IbftGetValidatorsByBlockNumber : RpcRequestResponseHandler<string[]>, IIbftGetValidatorsByBlockNumber
    {
        public IbftGetValidatorsByBlockNumber(IClient client) : base(client,
            ApiMethods.ibft_getValidatorsByBlockNumber.ToString())
        {
        }

        public async Task<string[]> SendRequestAsync(BlockParameter block, object id = null)
        {
            return await base.SendRequestAsync(id, block);
        }

        public RpcRequest BuildRequest(BlockParameter block, object id = null)
        {
            return base.BuildRequest(id, block);
        }
    }
}