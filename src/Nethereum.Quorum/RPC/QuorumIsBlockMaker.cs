using System;
using System.Threading.Tasks;
using Conflux.Hex.HexConvertors.Extensions;
using Conflux.JsonRpc.Client;

namespace Conflux.Quorum.RPC
{
    public class QuorumIsBlockMaker : RpcRequestResponseHandler<bool>, IQuorumIsBlockMaker
    {
        public QuorumIsBlockMaker(IClient client) : base(client, ApiMethods.quorum_isBlockMaker.ToString())
        {
        }

        public Task<bool> SendRequestAsync(string address, object id = null)
        {
            if (address == null) throw new ArgumentNullException(nameof(address));
            return base.SendRequestAsync(id, address.EnsureHexPrefix());
        }

        public RpcRequest BuildRequest(string address, object id = null)
        {
            if (address == null) throw new ArgumentNullException(nameof(address));
            return base.BuildRequest(id, address.EnsureHexPrefix());
        }
    }
}