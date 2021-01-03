using System;
using System.Threading.Tasks;
using Conflux.Hex.HexConvertors.Extensions;
using Conflux.JsonRpc.Client;

namespace Conflux.Quorum.RPC
{
    public class QuorumVote : RpcRequestResponseHandler<string>, IQuorumVote
    {
        public QuorumVote(IClient client) : base(client, ApiMethods.quorum_vote.ToString())
        {
        }

        public Task<string> SendRequestAsync(string hash, object id = null)
        {
            if (hash == null) throw new ArgumentNullException(nameof(hash));
            return base.SendRequestAsync(id, hash.EnsureHexPrefix());
        }

        public RpcRequest BuildRequest(string hash, object id = null)
        {
            if (hash == null) throw new ArgumentNullException(nameof(hash));
            return base.BuildRequest(id, hash.EnsureHexPrefix());
        }
    }
}